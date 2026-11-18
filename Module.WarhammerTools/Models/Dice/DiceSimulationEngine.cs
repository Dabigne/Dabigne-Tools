using System.Numerics;
using Avalonia.Media;

namespace Module.WarhammerTools.Models.Dice;

public record RollOutcome(int Total, string Summary, Vector2? CritPosition, bool? IsCritSuccess);

public class DiceSimulationEngine
{
    private readonly List<DiePhysics> _dice = new();
    private bool _isD100Roll;

    public IReadOnlyList<DiePhysics> Dice => _dice;
    public bool IsRolling => _dice.Count > 0 && !_dice.All(d => d.IsSettled);

    public void ThrowDice(DieType[] types, bool isD100, float trayWidth, float trayHeight)
    {
        _isD100Roll = isD100;
        _dice.Clear();

        var rng = Random.Shared;
        float halfW = (trayWidth > 0 ? trayWidth : 500f) / 2f;
        float halfH = (trayHeight > 0 ? trayHeight : 400f) / 2f;

        bool throwFromLeft = rng.Next(2) == 0;
        float startX = throwFromLeft ? -halfW + 45f : halfW - 45f;
        float baseDirX = throwFromLeft ? 1f : -1f;

        var palette = new[]
        {
            Color.FromRgb(195, 48, 58),   // Speckled Ruby
            Color.FromRgb(38, 138, 92),   // Speckled Emerald
            Color.FromRgb(48, 98, 198),   // Speckled Sapphire
            Color.FromRgb(138, 62, 188),  // Speckled Amethyst
            Color.FromRgb(205, 118, 32),  // Speckled Amber
            Color.FromRgb(36, 136, 152)   // Speckled Teal
        };

        for (int i = 0; i < types.Length; i++)
        {
            var color = palette[(int)types[i] % palette.Length];
            var die = new DiePhysics(types[i], color);

            float spreadY = Math.Clamp((i - (types.Length - 1) / 2f) * 42f, -halfH * 0.6f, halfH * 0.6f);
            var start = new Vector2(startX, spreadY);
            var dir = Vector2.Normalize(new Vector2(baseDirX, (float)(rng.NextDouble() * 1.5 - 0.75)));

            die.Throw(start, dir);
            _dice.Add(die);
        }
    }

    public bool Step(float dt, float trayWidth, float trayHeight)
    {
        if (!IsRolling) return false;

        float halfW = trayWidth / 2f;
        float halfH = trayHeight / 2f;
        const float dieRadius = 38f;

        foreach (var die in _dice)
            die.Step(dt, halfW, halfH, dieRadius);

        ResolveDieCollisions(dieRadius);

        // Returns true on the exact frame all dice finish settling
        return !IsRolling;
    }

    private void ResolveDieCollisions(float dieRadius)
    {
        float minDist = dieRadius * 1.85f;
        for (int i = 0; i < _dice.Count; i++)
        {
            for (int j = i + 1; j < _dice.Count; j++)
            {
                var a = _dice[i];
                var b = _dice[j];
                var delta = b.Position - a.Position;
                float dist = delta.Length();

                if (dist > 0.01f && dist < minDist)
                {
                    var normal = delta / dist;
                    float overlap = (minDist - dist) * 0.5f;
                    a.Position -= normal * overlap;
                    b.Position += normal * overlap;

                    var relVel = b.Velocity - a.Velocity;
                    float sepVel = Vector3.Dot(relVel, normal);
                    if (sepVel < 0)
                    {
                        var impulse = normal * (-sepVel * 0.8f);
                        a.Velocity -= impulse;
                        b.Velocity += impulse;
                    }
                }
            }
        }
    }

    public RollOutcome EvaluateOutcome()
    {
        int total;
        string summary;

        if (_isD100Roll && _dice.Count == 2)
        {
            int tens = int.Parse(_dice[0].ResultLabel);
            int ones = int.Parse(_dice[1].ResultLabel);
            total = (tens == 0 && ones == 0) ? 100 : tens + ones;
            summary = $"d100: {total} ({_dice[0].ResultLabel} + {_dice[1].ResultLabel})";
        }
        else
        {
            total = 0;
            foreach (var d in _dice)
            {
                int val = int.Parse(d.ResultLabel);
                if (d.Type == DieType.D10 && val == 0) val = 10;
                total += val;
            }
            var breakdown = string.Join(" + ", _dice.Select(d => $"{d.Type}:{d.ResultLabel}"));
            summary = $"Total: {total} ({breakdown})";
        }

        Vector2? critPos = null;
        bool? isCritSuccess = null;

        var d20 = _dice.FirstOrDefault(d => d.Type == DieType.D20);
        if (d20 != null && d20.ResultLabel is "20" or "1")
        {
            critPos = new Vector2(d20.Position.X, d20.Position.Y);
            isCritSuccess = d20.ResultLabel == "20";
            summary = isCritSuccess.Value
                ? $"★ CRITICAL HIT! ★  {summary}"
                : $"☠ CRITICAL FUMBLE! ☠  {summary}";
        }

        return new RollOutcome(total, summary, critPos, isCritSuccess);
    }
}
