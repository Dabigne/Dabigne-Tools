using System.Numerics;
using Avalonia;
using Avalonia.Media;

namespace Module.WarhammerTools.Models.Dice;

public static class DiceFaceLabelRenderer
{
    // Clean geometric vector strokes for digits 0-9 in normalized [-0.5, 0.5] glyph space
    private static readonly Dictionary<char, Vector2[][]> DigitStrokes = new()
    {
        ['0'] =
        [
	        [new Vector2(-0.32f, 0.45f), new Vector2(0.32f, 0.45f), new Vector2(0.32f, -0.45f), new Vector2(-0.32f, -0.45f), new Vector2(-0.32f, 0.45f)]
        ],
        ['1'] = new[]
        {	
            new[] { new Vector2(-0.15f, 0.30f), new Vector2(0.05f, 0.45f), new Vector2(0.05f, -0.45f) },
            new[] { new Vector2(-0.22f, -0.45f), new Vector2(0.28f, -0.45f) }
        },
        ['2'] = new[]
        {
            new[] { new Vector2(-0.32f, 0.28f), new Vector2(-0.15f, 0.45f), new Vector2(0.18f, 0.45f), new Vector2(0.32f, 0.28f), new Vector2(0.32f, 0.10f), new Vector2(-0.32f, -0.45f), new Vector2(0.34f, -0.45f) }
        },
        ['3'] = new[]
        {
            new[] { new Vector2(-0.30f, 0.45f), new Vector2(0.28f, 0.45f), new Vector2(0.02f, 0.05f), new Vector2(0.30f, -0.08f), new Vector2(0.30f, -0.32f), new Vector2(0.12f, -0.45f), new Vector2(-0.30f, -0.45f) }
        },
        ['4'] = new[]
        {
            new[] { new Vector2(0.18f, -0.45f), new Vector2(0.18f, 0.45f), new Vector2(-0.34f, -0.12f), new Vector2(0.34f, -0.12f) }
        },
        ['5'] = new[]
        {
            new[] { new Vector2(0.30f, 0.45f), new Vector2(-0.28f, 0.45f), new Vector2(-0.30f, 0.05f), new Vector2(0.15f, 0.05f), new Vector2(0.32f, -0.10f), new Vector2(0.32f, -0.32f), new Vector2(0.12f, -0.45f), new Vector2(-0.30f, -0.45f) }
        },
        ['6'] = new[]
        {
            new[] { new Vector2(0.25f, 0.45f), new Vector2(-0.15f, 0.45f), new Vector2(-0.32f, 0.20f), new Vector2(-0.32f, -0.30f), new Vector2(-0.14f, -0.45f), new Vector2(0.18f, -0.45f), new Vector2(0.32f, -0.28f), new Vector2(0.32f, -0.06f), new Vector2(0.15f, 0.06f), new Vector2(-0.32f, 0.06f) }
        },
        ['7'] = new[]
        {
            new[] { new Vector2(-0.32f, 0.45f), new Vector2(0.32f, 0.45f), new Vector2(-0.08f, -0.45f) }
        },
        ['8'] = new[]
        {
            new[] { new Vector2(-0.16f, 0.04f), new Vector2(-0.28f, 0.18f), new Vector2(-0.28f, 0.34f), new Vector2(-0.12f, 0.45f), new Vector2(0.12f, 0.45f), new Vector2(0.28f, 0.34f), new Vector2(0.28f, 0.18f), new Vector2(0.16f, 0.04f), new Vector2(-0.16f, 0.04f), new Vector2(-0.32f, -0.10f), new Vector2(-0.32f, -0.32f), new Vector2(-0.14f, -0.45f), new Vector2(0.14f, -0.45f), new Vector2(0.32f, -0.32f), new Vector2(0.32f, -0.10f), new Vector2(0.16f, 0.04f) }
        },
        ['9'] =
        [
	        [new Vector2(0.32f, -0.02f), new Vector2(-0.15f, -0.02f), new Vector2(-0.32f, 0.12f), new Vector2(-0.32f, 0.32f), new Vector2(-0.15f, 0.45f), new Vector2(0.15f, 0.45f), new Vector2(0.32f, 0.30f), new Vector2(0.32f, -0.25f), new Vector2(0.14f, -0.45f), new Vector2(-0.25f, -0.45f)]
        ]
    };

    /// <summary>
    /// Draws a 3D perspective-projected label directly onto the plane of a die face.
    /// </summary>
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
        // 1. Compute face center in local 3D die space
        var faceCenter = face.VertexIndices
            .Aggregate(Vector3.Zero, (acc, idx) => acc + localVertices[idx]) / face.VertexIndices.Length;

        // 2. Build orthonormal 2D basis (Right, Up) lying flat on the face plane
        var upDir = Vector3.Normalize(localVertices[face.VertexIndices[0]] - faceCenter);
        var rightDir = Vector3.Normalize(Vector3.Cross(upDir, face.Normal));
        upDir = Vector3.Cross(face.Normal, rightDir);

        var surfaceOrigin = faceCenter + face.Normal * 0.004f;

        // 3. Layout single or double digits (e.g. "12", "00") centered at (0, 0)
        string label = face.Label;
        float charSpacing = 0.82f;
        float totalWidth = label.Length * charSpacing;
        float startX = -totalWidth / 2f + charSpacing / 2f;

        // Multi-digit labels scale down slightly to fit inside triangles/kites
        float effectiveScale = label.Length > 1 ? labelScale * 0.72f : labelScale;

        var pen = new Pen(new SolidColorBrush(textColor), strokeThickness, lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);
        var geo = new StreamGeometry();

        using (var ctx = geo.Open())
        {
            for (int cIdx = 0; cIdx < label.Length; cIdx++)
            {
                char ch = label[cIdx];
                if (!DigitStrokes.TryGetValue(ch, out var strokes))
                    continue;

                float xOffset = startX + cIdx * charSpacing;

                foreach (var stroke in strokes)
                {
                    for (int i = 0; i < stroke.Length; i++)
                    {
                        var uv = new Vector2((stroke[i].X + xOffset) * effectiveScale, stroke[i].Y * effectiveScale);
                        var localPoint3D = surfaceOrigin + (rightDir * uv.X) + (upDir * uv.Y);
                        var worldPoint3D = Vector3.Transform(localPoint3D, orientation);
                        var screenPt = projectFunc(worldPoint3D);

                        if (i == 0)
                            ctx.BeginFigure(screenPt, isFilled: false);
                        else
                            ctx.LineTo(screenPt);
                    }
                    ctx.EndFigure(isClosed: false);
                }
            }

            // Optional orientation bar beneath "6" and "9" so players never confuse them
            if (label is "6" or "9")
            {
                var leftBar = new Vector2(-0.26f * effectiveScale, -0.62f * effectiveScale);
                var rightBar = new Vector2(0.26f * effectiveScale, -0.62f * effectiveScale);

                var p1 = projectFunc(Vector3.Transform(surfaceOrigin + rightDir * leftBar.X + upDir * leftBar.Y, orientation));
                var p2 = projectFunc(Vector3.Transform(surfaceOrigin + rightDir * rightBar.X + upDir * rightBar.Y, orientation));

                ctx.BeginFigure(p1, isFilled: false);
                ctx.LineTo(p2);
                ctx.EndFigure(isClosed: false);
            }
        }

        context.DrawGeometry(null, pen, geo);
    }
}
