using System;
using System.IO;
using System.Reflection;

namespace Day15
{
    public class RiskMap
    {
        public UInt64[,] _data = null;

        public Int32 Width { get; }

        public Int32 Height { get; }

        public UInt64 this[Int32 x, Int32 y]
        {
            get => _data[x, y];
        }

        public RiskMap(in String input)
        {
            String[] lines = input.Split(new char[] { '\n' });
            Width = lines[0].Trim().Length;
            Height = lines.Length;

            _data = new UInt64[Width, Height];

            for(Int32 y = 0; y < Height; ++y)
                for(Int32 x = 0; x < Width; ++x)
                    _data[x, y] = (UInt64)Char.GetNumericValue(lines[y][x]);


        }


        public Boolean TryGetRisk(in Int32 x, in Int32 y, out UInt64 risk)
        {
            risk = default;
            if (x < 0 || x > _data.GetUpperBound(0))
                return false;

            if (y < 0 || y > _data.GetUpperBound(1))
                return false;

            risk = _data[x, y];
            return true;
        }

    }
}
