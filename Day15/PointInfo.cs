using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public readonly struct PointInfo : IEquatable<PointInfo>
    {
        public Int32 X { get; }

        public Int32 Y { get; }

        public PointInfo(in Int32 x, in Int32 y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(PointInfo other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public bool Equals(in Int32 x, in Int32 y)
        {
            return this.X == x && this.Y == y;
        }

        public override int GetHashCode()
        {
            const uint hash = 0x9e3779b9;
            var seed = X + hash;
            seed ^= Y + hash + (seed << 6) + (seed >> 2);
            return (int)seed;
        }
        public override bool Equals(object obj) => obj is PointInfo other && (X == other.X && Y == other.Y);
    }
}
