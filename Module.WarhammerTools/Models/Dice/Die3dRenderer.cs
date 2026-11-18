using System.Numerics;
using Avalonia;
using Avalonia.Media;
using Vector = Avalonia.Vector;

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

        // 1. Ombre portée au sol
        float shadowScale = Math.Clamp(1f - die.Position.Z / 260f, 0.35f, 1f);
        var shadowBrush = new SolidColorBrush(Color.FromArgb((byte)(110 * shadowScale), 0, 0, 0));
        context.DrawEllipse(
            shadowBrush,
            null,
            new Point(center.X + die.Position.X + die.Position.Z * 0.2f, center.Y + die.Position.Y + 12),
            scale * 0.95f * shadowScale,
            scale * 0.65f * shadowScale);

        // 2. Transformation des sommets dans l'espace 3D
        var worldVerts = die.Mesh.Vertices
            .Select(v => Vector3.Transform(v, die.Orientation))
            .ToArray();

        Point Project(Vector3 v)
        {
            float fov = 4.2f / (4.2f - v.Z);
            return new Point(screenPos.X + v.X * scale * fov, screenPos.Y - v.Y * scale * fov);
        }

        // 3. Back-face culling
        var visible = new List<(Point[] Pts, DieFace Face, float AvgZ, float Light)>();
        foreach (var face in die.Mesh.Faces)
        {
            var n = Vector3.Transform(face.Normal, die.Orientation);
            if (n.Z <= 0.02f) continue;

            float avgZ = face.VertexIndices.Average(i => worldVerts.ElementAt(i).Z);
            float light = Math.Clamp(Vector3.Dot(n, LightDir), 0.38f, 1.0f);
            var pts = face.VertexIndices.Select(i => Project(worldVerts.ElementAt(i))).ToArray();
            visible.Add((pts, face, avgZ, light));
        }

        var noisyBitmap = DiceTextureCache.GetNoisyBitmap(die.BaseColor);
        double texW = noisyBitmap.PixelSize.Width;
        double texH = noisyBitmap.PixelSize.Height;

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

        // 4. Rendu des faces de l'arrière vers l'avant
        foreach (var f in visible.OrderBy(x => x.AvgZ))
        {
            var geo = new StreamGeometry();
            using (var ctx = geo.Open())
            {
                ctx.BeginFigure(f.Pts.First(), true);
                foreach (var pt in f.Pts.Skip(1))
                    ctx.LineTo(pt);
                ctx.EndFigure(true);
            }

            // Calcul du repère UV local 3D de la face
            var faceCenter = f.Face.VertexIndices
                .Aggregate(Vector3.Zero, (acc, idx) => acc + die.Mesh.Vertices.ElementAt(idx)) / f.Face.VertexIndices.Length;

            var firstVert = die.Mesh.Vertices.ElementAt(f.Face.VertexIndices.First());
            var upDir = Vector3.Normalize(firstVert - faceCenter);
            var rightDir = Vector3.Normalize(Vector3.Cross(upDir, f.Face.Normal));
            upDir = Vector3.Cross(f.Face.Normal, rightDir);

            // Projection des 3 points de repère de la face vers l'écran
            var pOrigin = Project(Vector3.Transform(faceCenter, die.Orientation));
            var pRight  = Project(Vector3.Transform(faceCenter + rightDir, die.Orientation));
            var pUp     = Project(Vector3.Transform(faceCenter + upDir, die.Orientation));

            // Vecteurs d'axes 2D projetés (cisaillement, rotation et écrasement perspectif inclus)
            var axisX = new Vector(pRight.X - pOrigin.X, pRight.Y - pOrigin.Y);
            var axisY = new Vector(pUp.X - pOrigin.X, pUp.Y - pOrigin.Y);

            // Matrice affine qui mappe le centre de la texture (texW/2, texH/2) sur pOrigin
            // et étire les axes UV sur la géométrie réelle de la face
            double uvScale = 0.85;
            var m11 = (axisX.X * uvScale) / (texW * 0.5);
            var m12 = (axisX.Y * uvScale) / (texW * 0.5);
            var m21 = (axisY.X * uvScale) / (texH * 0.5);
            var m22 = (axisY.Y * uvScale) / (texH * 0.5);

            var tx = pOrigin.X - (texW * 0.5 * m11 + texH * 0.5 * m21);
            var ty = pOrigin.Y - (texW * 0.5 * m12 + texH * 0.5 * m22);

            var uvMatrix = new Matrix(m11, m12, m21, m22, tx, ty);

            // A. Découpe sur le polygone de la face + dessin de la texture déformée en 3D
            using (context.PushGeometryClip(geo))
            using (context.PushTransform(uvMatrix))
            {
                context.DrawImage(noisyBitmap, new Rect(0, 0, texW, texH));
            }

            // B. Ombrage directionnel 3D + arête lumineuse
            IBrush shadeOverlay = f.Light >= 0.78f
                ? new SolidColorBrush(Color.FromArgb((byte)((f.Light - 0.78f) * 180f), 255, 255, 255))
                : new SolidColorBrush(Color.FromArgb((byte)((0.78f - f.Light) * 260f), 0, 0, 0));

            context.DrawGeometry(shadeOverlay, EdgePen, geo);

            // C. Chiffre gravé projeté sur le plan de la face
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
