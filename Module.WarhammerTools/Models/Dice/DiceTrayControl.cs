using System.Globalization;
using System.Numerics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Threading;

namespace Module.WarhammerTools.Models.Dice;

public record DiceRollRequest(DieType[] Dice, bool IsD100 = false);

public class RpgDiceTrayControl : Control
{
    private readonly List<DiePhysics> _dice = new();
    private readonly DispatcherTimer _timer;
    private bool _isD100Roll;

    // Bindable Result Text (TwoWay or OneWayToSource)
    public static readonly StyledProperty<string> RollResultTextProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, string>(
            nameof(RollResultText),
            defaultValue: "Ready to roll",
            defaultBindingMode: BindingMode.TwoWay);

    public string RollResultText
    {
        get => GetValue(RollResultTextProperty);
        set => SetValue(RollResultTextProperty, value);
    }

    // Bindable Total Value
    public static readonly StyledProperty<int> LastRollTotalProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, int>(
            nameof(LastRollTotal),
            defaultBindingMode: BindingMode.TwoWay);

    public int LastRollTotal
    {
        get => GetValue(LastRollTotalProperty);
        set => SetValue(LastRollTotalProperty, value);
    }

    // Bindable Roll Trigger from ViewModel
    public static readonly StyledProperty<DiceRollRequest?> RollRequestProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, DiceRollRequest?>(nameof(RollRequest));

    public DiceRollRequest? RollRequest
    {
        get => GetValue(RollRequestProperty);
        set => SetValue(RollRequestProperty, value);
    }

    // Optional ICommand invoked when dice finish settling
    public static readonly StyledProperty<ICommand?> RollCompletedCommandProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, ICommand?>(nameof(RollCompletedCommand));

    public ICommand? RollCompletedCommand
    {
        get => GetValue(RollCompletedCommandProperty);
        set => SetValue(RollCompletedCommandProperty, value);
    }

    static RpgDiceTrayControl()
    {
        RollRequestProperty.Changed.AddClassHandler<RpgDiceTrayControl>((control, e) =>
        {
            if (e.NewValue is DiceRollRequest req)
            {
                if (req.IsD100)
                    control.RollD100();
                else
                    control.RollDice(req.Dice);
            }
        });
    }

    public RpgDiceTrayControl()
    {
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
        _timer.Tick += OnTick;
    }

    public void RollDice(params DieType[] types)
    {
        _isD100Roll = false;
        RollResultText = "Rolling...";
        SpawnAndThrow(types);
    }

    public void RollD100()
    {
        _isD100Roll = true;
        RollResultText = "Rolling d100...";
        SpawnAndThrow(DieType.D10Tens, DieType.D10);
    }

    private void SpawnAndThrow(params DieType[] types)
    {
	    _dice.Clear();
	    var rng = Random.Shared;
	    float halfW = (float)(Bounds.Width > 0 ? Bounds.Width : 500) / 2f;
	    float halfH = (float)(Bounds.Height > 0 ? Bounds.Height : 400) / 2f;

	    // Pick a random corner/edge side to throw from for variety
	    bool throwFromLeft = rng.Next(2) == 0;
	    float startX = throwFromLeft ? -halfW + 45f : halfW - 45f;
	    float baseDirX = throwFromLeft ? 1f : -1f;

	    for (int i = 0; i < types.Length; i++)
	    {
		    var color = types[i] switch
		    {
			    DieType.D4 => Color.FromRgb(46, 139, 87),
			    DieType.D6 => Color.FromRgb(220, 60, 60),
			    DieType.D10 => Color.FromRgb(65, 105, 225),
			    DieType.D10Tens => Color.FromRgb(30, 60, 160),
			    DieType.D12 => Color.FromRgb(148, 70, 210),
			    DieType.D20 => Color.FromRgb(218, 125, 32),
			    _ => Colors.SteelBlue
		    };

		    var die = new DiePhysics(types[i], color);

		    float spreadY = Math.Clamp((i - (types.Length - 1) / 2f) * 42f, -halfH * 0.6f, halfH * 0.6f);
		    var start = new Vector2(startX, spreadY);

		    // Wide diagonal spread (-0.75 to +0.75 Y) guarantees lively wall rebounds
		    var dir = Vector2.Normalize(new Vector2(
			    baseDirX,
			    (float)(rng.NextDouble() * 1.5 - 0.75)));

		    die.Throw(start, dir);
		    _dice.Add(die);
	    }

	    _timer.Start();
    }


    private void OnTick(object? sender, EventArgs e)
    {
        const float dt = 0.016f;
        float halfW = (float)Bounds.Width / 2f;
        float halfH = (float)Bounds.Height / 2f;
        const float dieRadius = 38f;

        foreach (var die in _dice)
            die.Step(dt, halfW, halfH, dieRadius);

        for (int i = 0; i < _dice.Count; i++)
        {
            for (int j = i + 1; j < _dice.Count; j++)
            {
                var a = _dice[i];
                var b = _dice[j];
                var delta = b.Position - a.Position;
                float dist = delta.Length();
                float minDist = dieRadius * 1.85f;

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

        InvalidateVisual();

        if (_dice.Count > 0 && _dice.All(d => d.IsSettled))
        {
            _timer.Stop();
            ReportResult();
        }
    }

    private void ReportResult()
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

        LastRollTotal = total;
        RollResultText = summary;

        if (RollCompletedCommand?.CanExecute(total) == true)
            RollCompletedCommand.Execute(total);
    }

	public override void Render(DrawingContext context)
    {
        base.Render(context);

        var center = new Vector2((float)Bounds.Width / 2f, (float)Bounds.Height / 2f);
        var lightDir = Vector3.Normalize(new Vector3(-0.35f, -0.55f, 1f));
        var edgePen = new Pen(new SolidColorBrush(Color.FromArgb(155, 255, 255, 255)), 1.2);

        // Draw dice sorted by Z height so airborne dice render above grounded ones
        foreach (var die in _dice.OrderBy(d => d.Position.Z))
        {
            // Scale grows slightly as the die bounces closer to the camera (+Z)
            float scale = 34f * (1f + die.Position.Z / 350f);
            var screenPos = new Vector2(
                center.X + die.Position.X,
                center.Y + die.Position.Y - die.Position.Z * 0.35f);

            // 1. Ground Drop Shadow (shrinks and fades as the die bounces higher)
            float shadowScale = Math.Clamp(1f - die.Position.Z / 260f, 0.35f, 1f);
            byte shadowAlpha = (byte)(110 * shadowScale);
            var shadowBrush = new SolidColorBrush(Color.FromArgb(shadowAlpha, 0, 0, 0));

            context.DrawEllipse(
                shadowBrush,
                null,
                new Point(center.X + die.Position.X + die.Position.Z * 0.2f, center.Y + die.Position.Y + 12),
                scale * 0.95f * shadowScale,
                scale * 0.65f * shadowScale);

            // 2. Transform local vertices into rotated 3D space
            var worldVerts = die.Mesh.Vertices
                .Select(v => Vector3.Transform(v, die.Orientation))
                .ToArray();

            Point Project(Vector3 v)
            {
                float fov = 4.2f / (4.2f - v.Z);
                return new Point(screenPos.X + v.X * scale * fov, screenPos.Y - v.Y * scale * fov);
            }

            // 3. Back-face cull & collect visible faces
            var visible = new List<(Point[] Pts, DieFace Face, float AvgZ, float Light)>();
            foreach (var face in die.Mesh.Faces)
            {
                var n = Vector3.Transform(face.Normal, die.Orientation);
                if (n.Z <= 0.02f) continue;

                float avgZ = face.VertexIndices.Average(i => worldVerts[i].Z);
                float light = Math.Clamp(Vector3.Dot(n, lightDir), 0.38f, 1.0f);
                var pts = face.VertexIndices.Select(i => Project(worldVerts[i])).ToArray();
                visible.Add((pts, face, avgZ, light));
            }

            // 4. Prepare speckled/noisy texture brush anchored to the die's position
            var noisyBitmap = DiceTextureCache.GetNoisyBitmap(die.BaseColor);
            // Center the 128x128 noise bitmap on the die so the grain moves with the die
            var destRect = new Rect(
	            screenPos.X - scale * 1.4,
	            screenPos.Y - scale * 1.4,
	            scale * 2.8,
	            scale * 2.8);

            float labelScale = die.Type switch
            {
                DieType.D4 => 0.42f,
                DieType.D6 => 0.65f,
                DieType.D10 or DieType.D10Tens => 0.34f,
                DieType.D12 => 0.44f,
                DieType.D20 => 0.32f,
                _ => 0.45f
            };

            double strokeThickness = Math.Max(1.5, scale * 0.058);

            // 5. Render each visible face back-to-front (painter's algorithm)
            foreach (var f in visible.OrderBy(x => x.AvgZ))
            {
	            var geo = new StreamGeometry();
	            using (var ctx = geo.Open())
	            {
		            ctx.BeginFigure(f.Pts[0], true);
		            for (int k = 1; k < f.Pts.Length; k++)
			            ctx.LineTo(f.Pts[k]);
		            ctx.EndFigure(true);
	            }

	            // A. Clip to the face polygon and draw the noisy resin bitmap
	            using (context.PushGeometryClip(geo))
	            {
		            context.DrawImage(noisyBitmap, destRect);
	            }

	            // B. 3D directional lighting overlay + crisp bevel edge
	            IBrush shadeOverlay;
	            if (f.Light >= 0.78f)
	            {
		            byte highlightAlpha = (byte)((f.Light - 0.78f) * 180f);
		            shadeOverlay = new SolidColorBrush(Color.FromArgb(highlightAlpha, 255, 255, 255));
	            }
	            else
	            {
		            byte alpha = (byte)((0.78f - f.Light) * 260f);
		            shadeOverlay = new SolidColorBrush(Color.FromArgb(alpha, 0, 0, 0));
	            }

	            context.DrawGeometry(shadeOverlay, edgePen, geo);

	            // C. 3D perspective-projected number painted onto the face plane
	            DiceFaceLabelRenderer.DrawLabelOnFace(
		            context,
		            f.Face,
		            die.Mesh.Vertices,
		            die.Orientation,
		            Project,
		            labelScale,
		            Color.FromArgb(245, 255, 255, 255),
		            strokeThickness);            }
        }
    }

}
