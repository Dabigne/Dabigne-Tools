using System.Globalization;
using System.Numerics;
using Avalonia;
using Avalonia.Media;

namespace Module.WarhammerTools.Models.Dice;

public class CritEffectSystem
{
    private enum CritState { None, CritSuccess, CritFumble }

    private record struct EffectParticle(
        Vector2 Position,
        Vector2 Velocity,
        float Life,
        float MaxLife,
        float Size,
        Color Color);

    private CritState _state = CritState.None;
    private Vector2 _origin;
    private float _elapsed;
    private readonly List<EffectParticle> _particles = new();

    public bool IsActive => _state != CritState.None && (_elapsed < 1.6f || _particles.Count > 0);

    public void Clear()
    {
        _state = CritState.None;
        _particles.Clear();
    }

    public void Trigger(Vector2 origin, bool isSuccess)
    {
        _state = isSuccess ? CritState.CritSuccess : CritState.CritFumble;
        _origin = origin;
        _elapsed = 0f;
        _particles.Clear();

        var rng = Random.Shared;
        int count = isSuccess ? 36 : 26;

        for (int i = 0; i < count; i++)
        {
            float angle = (float)(rng.NextDouble() * MathF.Tau);
            float speed = isSuccess ? rng.Next(70, 220) : rng.Next(40, 140);
            var vel = new Vector2(MathF.Cos(angle) * speed, MathF.Sin(angle) * speed);

            Color color = isSuccess
                ? (rng.Next(2) == 0 ? Color.FromRgb(255, 215, 64) : Color.FromRgb(255, 250, 190))
                : (rng.Next(2) == 0 ? Color.FromRgb(220, 38, 38) : Color.FromRgb(120, 20, 20));

            _particles.Add(new EffectParticle(
                Position: origin,
                Velocity: vel,
                Life: 0f,
                MaxLife: (float)(0.7 + rng.NextDouble() * 0.7),
                Size: (float)(2.5 + rng.NextDouble() * 3.5),
                Color: color));
        }
    }

    public void Step(float dt)
    {
        if (!IsActive) return;

        _elapsed += dt;
        for (int i = _particles.Count - 1; i >= 0; i--)
        {
            var p = _particles[i];
            p.Life += dt;
            if (p.Life >= p.MaxLife)
            {
                _particles.RemoveAt(i);
                continue;
            }

            float gravityY = _state == CritState.CritFumble ? 120f : -35f;
            p.Velocity = new Vector2(p.Velocity.X * 0.96f, p.Velocity.Y * 0.96f + gravityY * dt);
            p.Position += p.Velocity * dt;
            _particles[i] = p;
        }
    }

    public void RenderBackground(DrawingContext context, Vector2 center)
    {
        if (!IsActive || _elapsed >= 1.6f) return;

        var originScreen = new Point(center.X + _origin.X, center.Y + _origin.Y);
        float fade = 1f - Math.Clamp(_elapsed / 1.6f, 0f, 1f);

        Color primary = _state == CritState.CritSuccess
            ? Color.FromRgb(255, 205, 50)
            : Color.FromRgb(220, 35, 35);

        double auraRadius = 56 + Math.Sin(_elapsed * 10f) * 6;
        var auraBrush = new RadialGradientBrush
        {
            GradientStops =
            {
                new GradientStop(Color.FromArgb((byte)(150 * fade), primary.R, primary.G, primary.B), 0.0),
                new GradientStop(Color.FromArgb(0, primary.R, primary.G, primary.B), 1.0)
            }
        };
        context.DrawEllipse(auraBrush, null, originScreen, auraRadius, auraRadius);

        for (int ring = 0; ring < 2; ring++)
        {
            float ringT = Math.Clamp((_elapsed - ring * 0.14f) / 0.85f, 0f, 1f);
            if (ringT > 0f && ringT < 1f)
            {
                double r = 28 + ringT * 115;
                byte alpha = (byte)((1f - ringT) * 210);
                var ringPen = new Pen(
                    new SolidColorBrush(Color.FromArgb(alpha, primary.R, primary.G, primary.B)),
                    3.0 * (1f - ringT) + 0.8);
                context.DrawEllipse(null, ringPen, originScreen, r, r);
            }
        }
    }

    public void RenderForeground(DrawingContext context, Vector2 center)
    {
        if (!IsActive || _elapsed >= 1.6f) return;

        foreach (var p in _particles)
        {
            float lifeRatio = 1f - (p.Life / p.MaxLife);
            byte alpha = (byte)(255 * lifeRatio);
            var brush = new SolidColorBrush(Color.FromArgb(alpha, p.Color.R, p.Color.G, p.Color.B));
            var pt = new Point(center.X + p.Position.X, center.Y + p.Position.Y);
            context.DrawEllipse(brush, null, pt, p.Size * lifeRatio, p.Size * lifeRatio);
        }

        float textProgress = Math.Clamp(_elapsed / 1.4f, 0f, 1f);
        byte textAlpha = (byte)(255 * Math.Sin(textProgress * MathF.PI));
        string bannerText = _state == CritState.CritSuccess ? "NATURAL 20!" : "CRITICAL 1!";
        Color bannerColor = _state == CritState.CritSuccess
            ? Color.FromArgb(textAlpha, 255, 225, 90)
            : Color.FromArgb(textAlpha, 255, 85, 85);

        var typeface = new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Black);
        var formatted = new FormattedText(bannerText, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 20, new SolidColorBrush(bannerColor));
        var shadow = new FormattedText(bannerText, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 20, new SolidColorBrush(Color.FromArgb((byte)(textAlpha * 0.75f), 0, 0, 0)));

        double riseY = center.Y + _origin.Y - 48 - textProgress * 22;
        double textX = center.X + _origin.X - formatted.Width / 2;

        context.DrawText(shadow, new Point(textX + 1.5, riseY + 1.5));
        context.DrawText(formatted, new Point(textX, riseY));
    }
}
