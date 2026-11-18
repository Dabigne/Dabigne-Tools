using System.Numerics;
using Avalonia;
using Avalonia.Media;

namespace Module.WarhammerTools.Models.Dice;

public static class Die3DRenderer
{
    private static readonly Vector3 LightDir = Vector3.Normalize(new Vector3(-0.35f, -0.55f, 1f));
    private static readonly Pen EdgePen = new(new SolidColorBrush(Color.FromArgb(155, 255, 255, 255)), 1.2);

    public static void RenderDie(DrawingContext context, DiePhysics die, Vector2 center)
    {
        float scale = 34f * (1f + die.Position.Z / 350f);
        var screenPos = new Vector2(
            center.X + die.Position.X,
            center.Y + die.Position.Y - die.Position.Z * 0.35f);

        // 1. Ground drop shadow
        float shadowScale = Math.Clamp(1f - die.Position.Z / 260f, 0.35f, 1f);
        var shadowBrush = new SolidColorBrush(Color.FromArgb((byte)(110 * shadowScale), 0, 0, 0));
        context.DrawEllipse(
            shadowBrush,
            null,
            new Point(center.X + die.Position.X + die.Position.Z * 0.2f, center.Y + die.Position.Y + 12),
            scale * 0.95f * shadowScale,
            scale * 0.65f * shadowScale);

        // 2. Transform vertices
        var worldVerts = die.Mesh.Vertices
            .Select(v => Vector3.Transform(v, die.Orientation))
            .ToArray();

        Point Project(Vector3 v)
        {
            float fov = 4.2f / (4.2f - v.Z);
            return new Point(screenPos.X + v.X * scale * fov, screenPos.Y - v.Y * scale * fov);
        }

        // 3. Back-face cull
        var visible = new List<(Point[] Pts, DieFace Face, float AvgZ, float Light)>();
        foreach (var face in die.Mesh.Faces)
        {
            var n = Vector3.Transform(face.Normal, die.Orientation);
            if (n.Z <= 0.02f) continue;

            float avgZ = face.VertexIndices.Average(i => worldVerts[i].Z);
            float light = Math.Clamp(Vector3.Dot(n, LightDir), 0.38f, 1.0f);
            var pts = face.VertexIndices.Select(i => Project(worldVerts[i])).ToArray();
            visible.Add((pts, face, avgZ, light));
        }

        var noisyBitmap = DiceTextureCache.GetNoisyBitmap(die.BaseColor);
        var destRect = new Rect(screenPos.X - scale * 1.4, screenPos.Y - scale * 1.4, scale * 2.8, scale * 2.8);

        float labelScale = die.Type switch
        {
            DieType.D4 => 0.42f,
            DieType.D6 => 0.65f,
            DieType.D8 => 0.44f,
            DieType.D10 or DieType.D10Tens => 0.34f,
            DieType.D12 => 0.44f,
            DieType.D20 => 0.32f,
            _ => 0.45f
        };

        double strokeThickness = Math.Max(1.5, scale * 0.058);

        // 4. Draw faces back-to-front
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

            using (context.PushGeometryClip(geo))
            {
                context.DrawImage(noisyBitmap, destRect);
            }

            IBrush shadeOverlay = f.Light >= 0.78f
                ? new SolidColorBrush(Color.FromArgb((byte)((f.Light - 0.78f) * 180f), 255, 255, 255))
                : new SolidColorBrush(Color.FromArgb((byte)((0.78f - f.Light) * 260f), 0, 0, 0));

            context.DrawGeometry(shadeOverlay, EdgePen, geo);

            DiceFaceLabelRenderer.DrawLabelOnFace(
                context,
                f.Face,
                die.Mesh.Vertices,
                die.Orientation,
                Project,
                labelScale,
                Color.FromArgb(245, 255, 255, 255),
                strokeThickness);
        }
    }
}
