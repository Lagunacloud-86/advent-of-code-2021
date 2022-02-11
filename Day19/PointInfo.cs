using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public enum EAxis
    {
        X,
        Y,
        Z
    }

    public class Point : IEquatable<Point>
    {
        public Int32 X { get; set; }

        public Int32 Y { get; set; }

        public Int32 Z { get; set; }

        public Point(in Int32 x, in Int32 y, in Int32 z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public Point(in Point other)
        {
            this.X = other.X;
            this.Y = other.Y;
            this.Z = other.Z;
        }

        public bool Equals(Point other)
        {
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        public bool Equals(in Int32 x, in Int32 y, in Int32 z)
        {
            return this.X == x && this.Y == y && this.Z == z;
        }

        public override int GetHashCode()
        {
            const uint hash = 0x9e3779b9;
            var seed = X + hash;
            seed ^= Y + hash + (seed << 6) + (seed >> 2) + Z - hash - (seed << 2) + (seed >> 1);
            return (int)seed;
        }
        public override bool Equals(object obj) => obj is Point other && (X == other.X && Y == other.Y);

        public Point Rotate90L(EAxis axis)
        {
            switch (axis)
            {
                case EAxis.X: return new Point(X, -Z, Y);
                case EAxis.Y: return new Point(-Z, Y, X);
                case EAxis.Z: return new Point(-Y, X, Z);
            }
            return default;
        }
        public Point Rotate90R(EAxis axis)
        {
            switch (axis)
            {
                case EAxis.X: return new Point(X, Z, -Y);
                case EAxis.Y: return new Point(Z, Y, -X);
                case EAxis.Z: return new Point(Y, -X, Z);
            }
            return default;
        }
        public Point Rotate180(EAxis axis)
        {
            switch (axis)
            {
                case EAxis.X: return new Point(X, -Y, -Z);
                case EAxis.Y: return new Point(-X, Y, -Z);
                case EAxis.Z: return new Point(-X, -Y, Z);
            }
            return default;
        }
        //public static Point operator =(in Point p)
        //{
        //    return new Point(p.X, p.Y, p.Z);
        //}

        public static Point operator+(in Point a, in Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Point operator -(in Point a, in Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Boolean operator ==(in Point a, in Point b)
        {
            return a.Equals(b);
        }
        public static Boolean operator !=(in Point a, in Point b)
        {
            return !a.Equals(b);
        }
        public static Point operator -(in Point point)
        {
            return new Point(-point.X, -point.Y, -point.Z);
        }

        public static Int32 ManhattanDistance(in Point a, in Point b)
        {
            return (a.X - b.X) + (a.Y - b.Y) + (a.Z + b.Z);
        }
    }
}
