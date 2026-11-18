using System.Numerics;

namespace Module.WarhammerTools.Models.Dice;

public enum DieType { D4, D6, D8, D10, D10Tens, D12, D20 }

public record DieFace(int[] VertexIndices, string Label, Vector3 Normal);

public class DieMesh
{
    public Vector3[] Vertices { get; }
    public DieFace[] Faces { get; }

    public DieMesh(Vector3[] vertices, (int[] indices, string label)[] rawFaces)
    {
        // Normalize vertices so all dice share a consistent bounding radius (~1.0)
        float maxLen = vertices.Max(v => v.Length());
        Vertices = vertices.Select(v => v / maxLen).ToArray();

        Faces = rawFaces.Select(f =>
        {
            var v0 = Vertices[f.indices[0]];
            var v1 = Vertices[f.indices[1]];
            var v2 = Vertices[f.indices[2]];
            var normal = Vector3.Normalize(Vector3.Cross(v1 - v0, v2 - v0));

            // Ensure outward-facing normal
            var center = f.indices.Aggregate(Vector3.Zero, (acc, i) => acc + Vertices[i]) / f.indices.Length;
            if (Vector3.Dot(normal, center) < 0)
            {
                Array.Reverse(f.indices);
                normal = -normal;
            }
            return new DieFace(f.indices, f.label, normal);
        }).ToArray();
    }

    public static DieMesh Create(DieType type) => type switch
    {
        DieType.D4 => CreateD4(),
        DieType.D6 => CreateD6(),
        DieType.D8 => CreateD8(),
        DieType.D10 => CreateD10(tens: false),
        DieType.D10Tens => CreateD10(tens: true),
        DieType.D12 => CreateD12(),
        DieType.D20 => CreateD20(),
        _ => CreateD6()
    };

    private static DieMesh CreateD4()
    {
        var v = new[]
        {
            new Vector3( 1,  1,  1),
            new Vector3( 1, -1, -1),
            new Vector3(-1,  1, -1),
            new Vector3(-1, -1,  1)
        };
        var f = new[]
        {
            (new[] { 0, 1, 2 }, "1"),
            (new[] { 0, 3, 1 }, "2"),
            (new[] { 0, 2, 3 }, "3"),
            (new[] { 1, 3, 2 }, "4")
        };
        return new DieMesh(v, f);
    }

    private static DieMesh CreateD6()
    {
        var v = new[]
        {
            new Vector3(-1,-1,-1), new Vector3( 1,-1,-1), new Vector3( 1, 1,-1), new Vector3(-1, 1,-1),
            new Vector3(-1,-1, 1), new Vector3( 1,-1, 1), new Vector3( 1, 1, 1), new Vector3(-1, 1, 1)
        };
        var f = new[]
        {
            (new[] { 4, 5, 6, 7 }, "1"), (new[] { 1, 0, 3, 2 }, "6"),
            (new[] { 5, 1, 2, 6 }, "2"), (new[] { 0, 4, 7, 3 }, "5"),
            (new[] { 7, 6, 2, 3 }, "3"), (new[] { 0, 1, 5, 4 }, "4")
        };
        return new DieMesh(v, f);
    }
    
    private static DieMesh CreateD8()
    {
	    var v = new[]
	    {
		    new Vector3( 0,  0,  1), // 0: Top apex
		    new Vector3( 0,  0, -1), // 1: Bottom apex
		    new Vector3( 1,  0,  0), // 2: +X
		    new Vector3( 0,  1,  0), // 3: +Y
		    new Vector3(-1,  0,  0), // 4: -X
		    new Vector3( 0, -1,  0)  // 5: -Y
	    };

	    var f = new[]
	    {
		    // Top 4 faces (apex 0 is first vertex so numbers point upright toward the apex)
		    (new[] { 0, 2, 3 }, "1"),
		    (new[] { 0, 3, 4 }, "2"),
		    (new[] { 0, 4, 5 }, "3"),
		    (new[] { 0, 5, 2 }, "4"),

		    // Bottom 4 faces (apex 1 is first vertex)
		    (new[] { 1, 3, 2 }, "8"),
		    (new[] { 1, 4, 3 }, "7"),
		    (new[] { 1, 5, 4 }, "6"),
		    (new[] { 1, 2, 5 }, "5")
	    };

	    return new DieMesh(v, f);
    }

    // Pentagonal Trapezohedron (10 kite faces)
    private static DieMesh CreateD10(bool tens)
    {
        var verts = new List<Vector3>
        {
            new(0, 0,  1.25f), // 0: Top apex
            new(0, 0, -1.25f)  // 1: Bottom apex
        };

        float ringZ = 0.18f;
        for (int i = 0; i < 10; i++)
        {
            float angle = i * MathF.PI / 5f;
            float z = (i % 2 == 0) ? ringZ : -ringZ;
            verts.Add(new Vector3(MathF.Cos(angle), MathF.Sin(angle), z));
        }

        string[] labels = tens
            ? new[] { "00", "10", "20", "30", "40", "50", "60", "70", "80", "90" }
            : new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        var faces = new List<(int[], string)>();
        for (int i = 0; i < 5; i++)
        {
            int topCurr = 2 + (i * 2);
            int midNext = 2 + ((i * 2 + 1) % 10);
            int topNext = 2 + ((i * 2 + 2) % 10);
            int midPrev = 2 + ((i * 2 + 9) % 10);

            // Top kite (Apex 0 -> topCurr -> midNext -> topNext)
            faces.Add((new[] { 0, topCurr, midNext, topNext }, labels[i * 2]));
            // Bottom kite (Apex 1 -> midNext -> topNext -> midNext2)
            int botNext = 2 + ((i * 2 + 3) % 10);
            faces.Add((new[] { 1, botNext, topNext, midNext }, labels[i * 2 + 1]));
        }
        return new DieMesh(verts.ToArray(), faces.ToArray());
    }

    private static DieMesh CreateD12()
    {
        float phi = (1f + MathF.Sqrt(5f)) / 2f;
        float inv = 1f / phi;
        var v = new[]
        {
            new Vector3(-1,-1,-1), new Vector3(-1,-1, 1), new Vector3(-1, 1,-1), new Vector3(-1, 1, 1),
            new Vector3( 1,-1,-1), new Vector3( 1,-1, 1), new Vector3( 1, 1,-1), new Vector3( 1, 1, 1),
            new Vector3(0,-inv,-phi), new Vector3(0,-inv, phi), new Vector3(0, inv,-phi), new Vector3(0, inv, phi),
            new Vector3(-inv,-phi,0), new Vector3(-inv, phi,0), new Vector3( inv,-phi,0), new Vector3( inv, phi,0),
            new Vector3(-phi,0,-inv), new Vector3( phi,0,-inv), new Vector3(-phi,0, inv), new Vector3( phi,0, inv)
        };
        var f = new[]
        {
            (new[]{3,11,7,15,13}, "1"),  (new[]{7,19,5,9,11}, "2"),
            (new[]{11,9,1,18,3}, "3"),   (new[]{3,18,16,2,13}, "4"),
            (new[]{13,2,10,6,15}, "5"),  (new[]{15,6,17,19,7}, "6"),
            (new[]{19,17,4,14,5}, "7"),  (new[]{5,14,12,1,9}, "8"),
            (new[]{1,12,0,16,18}, "9"),  (new[]{2,16,0,8,10}, "10"),
            (new[]{6,10,8,4,17}, "11"),  (new[]{0,12,14,4,8}, "12")
        };
        return new DieMesh(v, f);
    }

    private static DieMesh CreateD20()
    {
        float phi = (1f + MathF.Sqrt(5f)) / 2f;
        var v = new[]
        {
            new Vector3(-1,  phi, 0), new Vector3( 1,  phi, 0), new Vector3(-1, -phi, 0), new Vector3( 1, -phi, 0),
            new Vector3( 0, -1,  phi), new Vector3( 0,  1,  phi), new Vector3( 0, -1, -phi), new Vector3( 0,  1, -phi),
            new Vector3( phi, 0, -1), new Vector3( phi, 0,  1), new Vector3(-phi, 0, -1), new Vector3(-phi, 0,  1)
        };
        var f = new[]
        {
            (new[]{0,11,5}, "1"),  (new[]{0,5,1}, "2"),   (new[]{0,1,7}, "3"),   (new[]{0,7,10}, "4"),  (new[]{0,10,11}, "5"),
            (new[]{1,5,9}, "6"),   (new[]{5,11,4}, "7"),  (new[]{11,10,2}, "8"), (new[]{10,7,6}, "9"),  (new[]{7,1,8}, "10"),
            (new[]{3,9,4}, "11"),  (new[]{3,4,2}, "12"),  (new[]{3,2,6}, "13"),  (new[]{3,6,8}, "14"),  (new[]{3,8,9}, "15"),
            (new[]{4,9,5}, "16"),  (new[]{2,4,11}, "17"), (new[]{6,2,10}, "18"), (new[]{8,6,7}, "19"),  (new[]{9,8,1}, "20")
        };
        return new DieMesh(v, f);
    }
}
