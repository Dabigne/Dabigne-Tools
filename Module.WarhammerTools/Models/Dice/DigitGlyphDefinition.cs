using System.Numerics;

namespace Module.WarhammerTools.Models.Dice;

public record StrokeDef(Vector2[] Points, bool Smooth = true, bool Closed = false);

public static class DigitGlyphDefinitions
{
    public static readonly Dictionary<char, StrokeDef[]> Strokes = new()
    {
        ['0'] = new[]
        {
            new StrokeDef(new[]
            {
                new Vector2( 0.00f,  0.46f), new Vector2( 0.24f,  0.42f), new Vector2( 0.33f,  0.22f),
                new Vector2( 0.33f, -0.22f), new Vector2( 0.24f, -0.42f), new Vector2( 0.00f, -0.46f),
                new Vector2(-0.24f, -0.42f), new Vector2(-0.33f, -0.22f), new Vector2(-0.33f,  0.22f),
                new Vector2(-0.24f,  0.42f)
            }, Smooth: true, Closed: true)
        },
        ['1'] = new[]
        {
            new StrokeDef(new[] { new Vector2(-0.16f, 0.28f), new Vector2(0.04f, 0.45f), new Vector2(0.04f, -0.45f) }, Smooth: false),
            new StrokeDef(new[] { new Vector2(-0.22f, -0.45f), new Vector2(0.28f, -0.45f) }, Smooth: false)
        },
        ['2'] = new[]
        {
            new StrokeDef(new[]
            {
                new Vector2(-0.28f,  0.26f), new Vector2(-0.18f,  0.42f), new Vector2( 0.02f,  0.46f),
                new Vector2( 0.22f,  0.40f), new Vector2( 0.31f,  0.24f), new Vector2( 0.26f,  0.06f),
                new Vector2( 0.05f, -0.14f), new Vector2(-0.22f, -0.36f)
            }, Smooth: true),
            new StrokeDef(new[] { new Vector2(-0.30f, -0.45f), new Vector2(0.32f, -0.45f) }, Smooth: false)
        },
        ['3'] = new[]
        {
            new StrokeDef(new[]
            {
                new Vector2(-0.25f,  0.34f), new Vector2(-0.08f,  0.45f), new Vector2( 0.14f,  0.44f),
                new Vector2( 0.28f,  0.32f), new Vector2( 0.26f,  0.14f), new Vector2( 0.08f,  0.04f),
                new Vector2(-0.06f,  0.03f)
            }, Smooth: true),
            new StrokeDef(new[]
            {
                new Vector2(-0.06f,  0.03f), new Vector2( 0.12f,  0.01f), new Vector2( 0.30f, -0.12f),
                new Vector2( 0.30f, -0.32f), new Vector2( 0.14f, -0.45f), new Vector2(-0.10f, -0.45f),
                new Vector2(-0.28f, -0.34f)
            }, Smooth: true)
        },
        ['4'] = new[]
        {
            new StrokeDef(new[] { new Vector2(0.16f, -0.45f), new Vector2(0.16f, 0.45f), new Vector2(-0.32f, -0.14f), new Vector2(0.32f, -0.14f) }, Smooth: false)
        },
        ['5'] = new[]
        {
            new StrokeDef(new[] { new Vector2(0.26f, 0.45f), new Vector2(-0.24f, 0.45f), new Vector2(-0.28f, 0.06f) }, Smooth: false),
            new StrokeDef(new[]
            {
                new Vector2(-0.28f,  0.06f), new Vector2(-0.08f,  0.12f), new Vector2( 0.16f,  0.09f),
                new Vector2( 0.30f, -0.06f), new Vector2( 0.30f, -0.28f), new Vector2( 0.14f, -0.44f),
                new Vector2(-0.10f, -0.46f), new Vector2(-0.28f, -0.35f)
            }, Smooth: true)
        },
        ['6'] = new[]
        {
            new StrokeDef(new[]
            {
                new Vector2( 0.22f,  0.42f), new Vector2( 0.02f,  0.46f), new Vector2(-0.20f,  0.36f),
                new Vector2(-0.31f,  0.12f), new Vector2(-0.32f, -0.18f), new Vector2(-0.22f, -0.40f),
                new Vector2( 0.00f, -0.46f), new Vector2( 0.22f, -0.40f), new Vector2( 0.31f, -0.20f),
                new Vector2( 0.24f,  0.00f), new Vector2( 0.02f,  0.06f), new Vector2(-0.22f,  0.00f),
                new Vector2(-0.31f, -0.14f)
            }, Smooth: true)
        },
        ['7'] = new[]
        {
            new StrokeDef(new[] { new Vector2(-0.30f, 0.45f), new Vector2(0.30f, 0.45f) }, Smooth: false),
            new StrokeDef(new[] { new Vector2(0.30f, 0.45f), new Vector2(0.06f, 0.02f), new Vector2(-0.08f, -0.45f) }, Smooth: true)
        },
        ['8'] = new[]
        {
            new StrokeDef(new[]
            {
                new Vector2( 0.00f,  0.04f), new Vector2( 0.20f,  0.10f), new Vector2( 0.27f,  0.26f),
                new Vector2( 0.18f,  0.42f), new Vector2( 0.00f,  0.46f), new Vector2(-0.18f,  0.42f),
                new Vector2(-0.27f,  0.26f), new Vector2(-0.20f,  0.10f)
            }, Smooth: true, Closed: true),
            new StrokeDef(new[]
            {
                new Vector2( 0.00f,  0.04f), new Vector2( 0.24f, -0.04f), new Vector2( 0.32f, -0.22f),
                new Vector2( 0.22f, -0.41f), new Vector2( 0.00f, -0.46f), new Vector2(-0.22f, -0.41f),
                new Vector2(-0.32f, -0.22f), new Vector2(-0.24f, -0.04f)
            }, Smooth: true, Closed: true)
        },
        ['9'] = new[]
        {
            new StrokeDef(new[]
            {
                new Vector2( 0.31f,  0.14f), new Vector2( 0.22f,  0.00f), new Vector2( 0.00f, -0.06f),
                new Vector2(-0.24f,  0.00f), new Vector2(-0.31f,  0.20f), new Vector2(-0.22f,  0.40f),
                new Vector2( 0.00f,  0.46f), new Vector2( 0.22f,  0.40f), new Vector2( 0.32f,  0.18f),
                new Vector2( 0.31f, -0.12f), new Vector2( 0.20f, -0.36f), new Vector2(-0.02f, -0.46f),
                new Vector2(-0.22f, -0.42f)
            }, Smooth: true)
        }
    };

    public static Vector2[] GetSmoothedPoints(StrokeDef stroke, int iterations = 2)
    {
        if (!stroke.Smooth || stroke.Points.Length < 3)
            return stroke.Points;

        var current = stroke.Points;
        for (int iter = 0; iter < iterations; iter++)
        {
            var next = new List<Vector2>(current.Length * 2);
            if (!stroke.Closed)
                next.Add(current.First());

            int count = stroke.Closed ? current.Length : current.Length - 1;
            for (int i = 0; i < count; i++)
            {
                var p0 = current.ElementAt(i);
                var p1 = current.ElementAt((i + 1) % current.Length);
                next.Add(p0 * 0.75f + p1 * 0.25f);
                next.Add(p0 * 0.25f + p1 * 0.75f);
            }

            if (!stroke.Closed)
                next.Add(current.Last());

            current = next.ToArray();
        }
        return current;
    }
}
