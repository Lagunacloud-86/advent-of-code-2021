using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_Day12
{
    public class Vector
    {

        private Int32[] _vector;

        public Int32 X
        {
            get => _vector[0];
            set => _vector[0] = value;
        }

        public Int32 Y
        {
            get => _vector[1];
            set => _vector[1] = value;
        }

        public Int32 Z
        {
            get => _vector[2];
            set => _vector[2] = value;
        }

        public Int32 this[Int32 index]
        {
            get => _vector[index];
            set => _vector[index] = value;
        }

        public Vector()
        {
            _vector = new int[3];
        }

        public Vector(in Int32 x, in Int32 y, in Int32 z)
        {
            _vector = new int[3];
            _vector[0] = x;
            _vector[1] = y;
            _vector[2] = z;
        }

    }
}
