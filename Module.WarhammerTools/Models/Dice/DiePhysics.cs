using System.Numerics;
using Avalonia.Media;

namespace Module.WarhammerTools.Models.Dice;

public class DiePhysics
{
    public DieType Type { get; }
    public DieMesh Mesh { get; }
    public Color BaseColor { get; }

    public Vector3 Position;
    public Vector3 Velocity;

    public Quaternion Orientation = Quaternion.Identity;
    public Vector3 AngularVelocity;

    public bool IsSettled { get; private set; }
    public string ResultLabel { get; private set; } = "";

    private Quaternion _targetOrientation;
    private bool _isSnapping;

    public DiePhysics(DieType type, Color color)
    {
        Type = type;
        Mesh = DieMesh.Create(type);
        BaseColor = color;
    }

    public void Throw(Vector2 startPos, Vector2 throwDirection)
    {
        var rng = Random.Shared;

        // Launch from edge with energetic upward arc
        Position = new Vector3(startPos.X, startPos.Y, rng.Next(70, 120));

        float speed = rng.Next(720, 1080); // Much higher horizontal speed to reach and bounce off borders
        Velocity = new Vector3(
            throwDirection.X * speed,
            throwDirection.Y * speed,
            rng.Next(180, 340)); // Punchy upward pop

        // Fast, aggressive initial tumble
        AngularVelocity = new Vector3(
            (float)(rng.NextDouble() * 38 - 19),
            (float)(rng.NextDouble() * 38 - 19),
            (float)(rng.NextDouble() * 38 - 19));

        Orientation = Quaternion.CreateFromYawPitchRoll(
            (float)(rng.NextDouble() * MathF.Tau),
            (float)(rng.NextDouble() * MathF.Tau),
            (float)(rng.NextDouble() * MathF.Tau));

        IsSettled = false;
        _isSnapping = false;
    }

    public void Step(float dt, float halfWidth, float halfHeight, float dieRadius)
    {
        if (IsSettled) return;

        // Sub-step physics (4x) so fast throws never tunnel through borders
        const int subSteps = 4;
        float subDt = dt / subSteps;

        const float gravity = -2450f;         // Heavy, snappy gravity (no floaty slow-mo)
        const float floorRestitution = 0.60f; // Crisp table bounce
        const float wallRestitution = 0.84f;  // Energetic border ricochet
        const float floorFriction = 0.982f;   // Allows sliding across the tray before biting
        const float airDrag = 0.997f;

        for (int s = 0; s < subSteps; s++)
        {
            if (!_isSnapping)
            {
                Velocity.Z += gravity * subDt;
                Position += Velocity * subDt;

                // Integrate quaternion rotation
                float angSpeed = AngularVelocity.Length();
                if (angSpeed > 0.0001f)
                {
                    var axis = AngularVelocity / angSpeed;
                    var deltaRot = Quaternion.CreateFromAxisAngle(axis, angSpeed * subDt);
                    Orientation = Quaternion.Normalize(deltaRot * Orientation);
                }

                // 1. Table Floor Collision (Z = 0)
                if (Position.Z <= 0f)
                {
                    Position.Z = 0f;
                    if (MathF.Abs(Velocity.Z) > 65f)
                    {
                        Velocity.Z = -Velocity.Z * floorRestitution;

                        // Convert some linear momentum into fresh tumble on impact
                        var rng = Random.Shared;
                        AngularVelocity = AngularVelocity * 0.85f + new Vector3(
                            (float)(rng.NextDouble() * 14 - 7),
                            (float)(rng.NextDouble() * 14 - 7),
                            (float)(rng.NextDouble() * 10 - 5));
                    }
                    else
                    {
                        Velocity.Z = 0f;
                    }

                    Velocity.X *= floorFriction;
                    Velocity.Y *= floorFriction;
                    AngularVelocity *= 0.965f;
                }
                else
                {
                    Velocity.X *= airDrag;
                    Velocity.Y *= airDrag;
                    AngularVelocity *= 0.995f;
                }

                // 2. Control Border Collisions (Left, Right, Top, Bottom walls)
                float maxX = MathF.Max(20f, halfWidth - dieRadius);
                float maxY = MathF.Max(20f, halfHeight - dieRadius);
                bool hitWall = false;

                if (Position.X < -maxX)
                {
                    Position.X = -maxX;
                    Velocity.X = MathF.Abs(Velocity.X) * wallRestitution;
                    hitWall = true;
                }
                else if (Position.X > maxX)
                {
                    Position.X = maxX;
                    Velocity.X = -MathF.Abs(Velocity.X) * wallRestitution;
                    hitWall = true;
                }

                if (Position.Y < -maxY)
                {
                    Position.Y = -maxY;
                    Velocity.Y = MathF.Abs(Velocity.Y) * wallRestitution;
                    hitWall = true;
                }
                else if (Position.Y > maxY)
                {
                    Position.Y = maxY;
                    Velocity.Y = -MathF.Abs(Velocity.Y) * wallRestitution;
                    hitWall = true;
                }

                if (hitWall)
                {
                    // Wall impact pops the die slightly upward and kicks its angular spin
                    var rng = Random.Shared;
                    Velocity.Z += rng.Next(45, 110);
                    AngularVelocity += new Vector3(
                        (float)(rng.NextDouble() * 18 - 9),
                        (float)(rng.NextDouble() * 18 - 9),
                        (float)(rng.NextDouble() * 18 - 9));
                }

                // Snap quickly once speed drops so the roll finishes decisively
                if (Position.Z <= 0.5f && Velocity.Length() < 45f && AngularVelocity.Length() < 3.5f)
                {
                    BeginFaceSnap();
                }
            }
            else
            {
                // Fast, crisp magnetic settle onto the winning face
                Velocity *= 0.88f;
                Position += Velocity * subDt;
                Orientation = Quaternion.Slerp(Orientation, _targetOrientation, 22f * subDt);

                if (Quaternion.Dot(Orientation, _targetOrientation) > 0.9998f)
                {
                    Orientation = _targetOrientation;
                    IsSettled = true;
                    break;
                }
            }
        }
    }

    private void BeginFaceSnap()
    {
        _isSnapping = true;

        var bestFace = Mesh.Faces
            .OrderByDescending(f => Vector3.Transform(f.Normal, Orientation).Z)
            .First();

        ResultLabel = bestFace.Label;

        var currentNormal = Vector3.Normalize(Vector3.Transform(bestFace.Normal, Orientation));
        var targetNormal = Vector3.UnitZ;

        var rotAxis = Vector3.Cross(currentNormal, targetNormal);
        if (rotAxis.LengthSquared() > 1e-6f)
        {
            float angle = MathF.Acos(Math.Clamp(Vector3.Dot(currentNormal, targetNormal), -1f, 1f));
            var alignRot = Quaternion.CreateFromAxisAngle(Vector3.Normalize(rotAxis), angle);
            _targetOrientation = Quaternion.Normalize(alignRot * Orientation);
        }
        else
        {
            _targetOrientation = Orientation;
        }
    }
}
