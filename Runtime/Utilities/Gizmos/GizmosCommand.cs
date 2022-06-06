using UnityEngine;

namespace Lab5Games
{
    public abstract class GizmosCommand
    {
        public readonly float StartTime;
        public float Duration { get; set; } = 1;

        public Color GizmosColor { get; set; }

        public GizmosCommand(Color color)
        {
            this.StartTime = Time.time;
            this.GizmosColor = color;
        }

        public abstract void Draw();
    }

    public class DrawPoint : GizmosCommand
    {
        public Vector3 position;
        public float scale;

        public DrawPoint(Vector3 position, float scale, Color color) : base(color)
        {
            this.position = position;
            this.scale = scale;
        }

        public override void Draw()
        {
            GizmosEx.DrawPoint(position, GizmosColor, scale);
        }
    }

    public class DrawRay : GizmosCommand
    {
        public Vector3 position;
        public Vector3 direction;

        public DrawRay(Vector3 position, Vector3 direction, Color color) : base(color)
        {
            this.position = position;
            this.direction = direction;
        }

        public override void Draw()
        {
            GizmosEx.DrawRay(position, direction, GizmosColor);
        }
    }

    public class DrawLine : GizmosCommand
    {
        public Vector3 from;
        public Vector3 to;

        public DrawLine(Vector3 from, Vector3 to, Color color) : base(color)
        {
            this.from = from;
            this.to = to;
        }

        public override void Draw()
        {
            GizmosEx.DrawLine(from, to, GizmosColor);
        }
    }

    public class DrawBounds : GizmosCommand
    {
        public Bounds bounds;

        public DrawBounds(Bounds bounds, Color color) : base(color)
        {
            this.bounds = bounds;
        }

        public override void Draw()
        {
            GizmosEx.DrawBounds(bounds, GizmosColor);
        }
    }

    public class DrawCircle : GizmosCommand
    {
        public Vector3 position;
        public Vector3 up;
        public float radius;

        public DrawCircle(Vector3 position, Vector3 up, float radius, Color color) : base(color)
        {
            this.position = position;
            this.up = up;
            this.radius = radius;
        }

        public override void Draw()
        {
            GizmosEx.DrawCircle(position, up, GizmosColor, radius);
        }
    }

    public class DrawCylinder : GizmosCommand
    {
        public Vector3 start;
        public Vector3 end;
        public float radius;

        public DrawCylinder(Vector3 start, Vector3 end, float radius, Color color) : base(color)
        {
            this.start = start;
            this.end = end;
            this.radius = radius;
        }

        public override void Draw()
        {
            GizmosEx.DrawCylinder(start, end, GizmosColor, radius);
        }
    }

    public class DrawCone : GizmosCommand
    {
        public Vector3 position;
        public Vector3 direction;
        public float angle;

        public DrawCone(Vector3 position, Vector3 direction, float angle, Color color) : base(color)
        {
            this.position = position;
            this.direction = direction;
            this.angle = angle;
        }

        public override void Draw()
        {
            GizmosEx.DrawCone(position, direction, GizmosColor, angle);
        }
    }

    public class DrawArrow : GizmosCommand
    {
        public Vector3 position;
        public Vector3 direction;
        public float angle;
        public float headLength;

        public DrawArrow(Vector3 position, Vector3 direction, float angle, float headLength,Color color) : base(color)
        {
            this.position = position;
            this.direction = direction;
            this.angle = angle;
            this.headLength = headLength;
        }

        public override void Draw()
        {
            GizmosEx.DrawArrow(position, direction, GizmosColor, angle, headLength);
        }
    }

    public class DrawCapsule : GizmosCommand
    {
        public Vector3 pointA;
        public Vector3 pointB;
        public float radius;

        public DrawCapsule(Vector3 pointA, Vector3 pointB, float radius, Color color) : base(color)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.radius = radius;
        }

        public override void Draw()
        {
            GizmosEx.DrawCapsule(pointA, pointB, radius, GizmosColor);
        }
    }

    public class DrawFrustum : GizmosCommand
    {
        Camera camera;

        public DrawFrustum(Camera camera, Color color) : base(color)
        {
            this.camera = camera;
        }

        public override void Draw()
        {
            GizmosEx.DrawFrustum(camera, GizmosColor);
        }
    }

    public class DrawPlane : GizmosCommand
    {
        public Vector3 start;
        public Vector3 end;
        public Vector3 upward;
        public float height;

        public DrawPlane(Vector3 start, Vector3 end, Vector3 upward, float height, Color color) : base(color)
        {
            this.start = start;
            this.end = end;
            this.upward = upward;
            this.height = height;
        }

        public override void Draw()
        {
            GizmosEx.DrawPlane(start, end, upward, height, GizmosColor);
        }
    }

    public class DrawSphere : GizmosCommand
    {
        public Vector3 position;
        public float radius;

        public DrawSphere(Vector3 position, float radius, Color color) : base(color)
        {
            this.position = position;
            this.radius = radius;
        }

        public override void Draw()
        {
            GizmosEx.DrawSphere(position, radius, GizmosColor);
        }
    }

    public class DrawDirection : GizmosCommand
    {
        public Vector3 position;
        public Vector3 direction;
        public float distance;

        public DrawDirection(Vector3 position, Vector3 direction, float distance, Color color) : base(color)
        {
            this.position = position;
            this.direction = direction;
            this.distance = distance;
        }

        public override void Draw()
        {
            GizmosEx.DrawDirection(position, direction, distance, GizmosColor);
        }
    }

    public class DrawBox : GizmosCommand
    {
        public Vector3 origin;
        public Vector3 halfExtents;
        public Quaternion orientation;

        public DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color) : base( color)
        {
            this.origin = origin;
            this.halfExtents = halfExtents;
            this.orientation = orientation;
        }

        public override void Draw()
        {
            GizmosEx.DrawBox(origin, halfExtents, orientation, GizmosColor);
        }
    }

    public class DrawLable : GizmosCommand
    {
        public Vector3 position;
        public string text;
        public float offsetX, offsetY;

        public DrawLable(Vector3 position, string text, float offsetX, float offsetY, Color color) : base(color)
        {
            this.position = position;
            this.text = text;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public override void Draw()
        {
            GizmosEx.DrawLabel(position, text, default(GUIStyle), GizmosColor, offsetX, offsetY);
        }
    }
}
