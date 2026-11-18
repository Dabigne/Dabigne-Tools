using System.Numerics;
using Avalonia;
using Avalonia.Media;

namespace Module.WarhammerTools.Models.Dice;

public static class DiceFaceLabelRenderer
{
    public static void DrawLabelOnFace(
        DrawingContext context,
        DieFace face,
        Vector3[] localVertices,
        Quaternion orientation,
        Func<Vector3, Point> projectFunc,
        float labelScale,
        Color textColor,
        double strokeThickness)
    {
        var faceCenter = face.VertexIndices
            .Aggregate(Vector3.Zero, (acc, idx) => acc + localVertices.ElementAt(idx)) / face.VertexIndices.Length;

        var firstVertex = localVertices.ElementAt(face.VertexIndices.First());
        var upDir = Vector3.Normalize(firstVertex - faceCenter);
        var rightDir = Vector3.Normalize(Vector3.Cross(upDir, face.Normal));
        upDir = Vector3.Cross(face.Normal, rightDir);

        var surfaceOrigin = faceCenter + face.Normal * 0.004f;

        var worldLightDir = Vector3.Normalize(new Vector3(-0.35f, -0.55f, 1f));
        var localLightDir = Vector3.Transform(worldLightDir, Quaternion.Conjugate(orientation));
        var lightOnFace2D = new Vector2(
            Vector3.Dot(localLightDir, rightDir),
            Vector3.Dot(localLightDir, upDir));

        lightOnFace2D = lightOnFace2D.LengthSquared() > 0.001f
            ? Vector2.Normalize(lightOnFace2D)
            : Vector2.Normalize(new Vector2(-0.5f, 0.8f));

        string label = face.Label;
        float charSpacing = 0.82f;
        float startX = -(label.Length * charSpacing) / 2f + charSpacing / 2f;
        float effectiveScale = label.Length > 1 ? labelScale * 0.72f : labelScale;

        StreamGeometry BuildGeometry(Vector2 uvOffset)
        {
            var geo = new StreamGeometry();
            using var ctx = geo.Open();

            foreach (var (ch, cIdx) in label.Select((c, idx) => (c, idx)))
            {
                if (!DigitGlyphDefinitions.Strokes.TryGetValue(ch, out var strokes))
                    continue;

                float xOffset = startX + cIdx * charSpacing;

                foreach (var strokeDef in strokes)
                {
                    var pts = DigitGlyphDefinitions.GetSmoothedPoints(strokeDef);
                    foreach (var (pt, i) in pts.Select((p, idx) => (p, idx)))
                    {
                        var uv = new Vector2(
                            (pt.X + xOffset) * effectiveScale + uvOffset.X,
                            pt.Y * effectiveScale + uvOffset.Y);

                        var worldPt = Vector3.Transform(surfaceOrigin + rightDir * uv.X + upDir * uv.Y, orientation);
                        var screenPt = projectFunc(worldPt);

                        if (i == 0) ctx.BeginFigure(screenPt, false);
                        else ctx.LineTo(screenPt);
                    }
                    ctx.EndFigure(strokeDef.Closed);
                }
            }

            if (label is "6" or "9")
            {
                var p1 = projectFunc(Vector3.Transform(surfaceOrigin + rightDir * (-0.24f * effectiveScale + uvOffset.X) + upDir * (-0.62f * effectiveScale + uvOffset.Y), orientation));
                var p2 = projectFunc(Vector3.Transform(surfaceOrigin + rightDir * ( 0.24f * effectiveScale + uvOffset.X) + upDir * (-0.62f * effectiveScale + uvOffset.Y), orientation));
                ctx.BeginFigure(p1, false);
                ctx.LineTo(p2);
                ctx.EndFigure(false);
            }

            return geo;
        }

        float bevel = effectiveScale * 0.055f;

        // 1. Reflet inférieur de la gravure
        context.DrawGeometry(null, new Pen(new SolidColorBrush(Color.FromArgb(135, 255, 255, 255)), strokeThickness * 1.15, lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round), BuildGeometry(-lightOnFace2D * bevel));

        // 2. Ombre interne du creux
        context.DrawGeometry(null, new Pen(new SolidColorBrush(Color.FromArgb(200, 12, 14, 18)), strokeThickness * 1.35, lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round), BuildGeometry(lightOnFace2D * bevel));

        // 3. Peinture principale
        context.DrawGeometry(null, new Pen(new SolidColorBrush(textColor), strokeThickness * 0.92, lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round), BuildGeometry(Vector2.Zero));
    }
}
