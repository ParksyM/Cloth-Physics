using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothPhysics
{
    class Vector2
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public static Vector2 Zero = new Vector2(0, 0);

        public Vector2() : this(0f, 0f)
        { }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        //dot product
        public static float operator *(Vector2 lhs, Vector2 rhs)
        {
            //UxVx + UyVy;
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        //cross product
        public static float operator %(Vector2 lhs, Vector2 rhs)
        {
            //UxVy - UyVx
            return lhs.X * rhs.Y - lhs.Y * rhs.X;
        }

        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.X * rhs, lhs.Y * rhs);
        }

        public static Vector2 operator *(float lhs, Vector2 rhs)
        {
            return rhs * lhs;
        }

        public static Vector2 operator /(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.X / rhs, lhs.Y / rhs);
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public float LengthSqr()
        {
            return X * X + Y * Y;
        }

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return (v1 - v2).Length();
        }

        public Vector2 Normalise()
        {
            return new Vector2(X / Length(), Y / Length());
        }

        public override string ToString()
        {
            //return base.ToString();
            return "X: " + X + " Y: " + Y;
        }
    }
}
