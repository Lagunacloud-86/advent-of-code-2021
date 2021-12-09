using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    public class Heightmap
    {
        private short[,] _data = null;

        public Int32 Height { get; }

        public Int32 Width { get; }

        public Heightmap(in String input)
        {
            String[] lines = input.Split(new char[] { '\n' });
            Height = lines.Length;
            Width = lines[0].Trim().Length;
            _data = new short[Width, Height];
            for(Int32 i = 0; i < Height; ++i)
            {
                for(Int32 c = 0; c < Width; ++c)
                {
                    
                    _data[c, i] = Convert.ToInt16(Char.GetNumericValue(lines[i][c]));
                }
            }



        }
        
        public short GetHeight(Int32 x, Int32 y)
        {
            return _data[x, y];
        }

        public bool TryGetHeight(Int32 x, Int32 y, out short height)
        {
            height = -1;
            if (x < 0 || x >= Width)
                return false;

            if (y < 0 || y >= Height)
                return false;

            height = _data[x, y];

            return true;
        }
    }
}
