using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public readonly struct FoldInfo
    {
        public String Axis { get; }

        public Int32 Value { get; }

        public FoldInfo(
            in String axis,
            in Int32 value)
        {
            this.Axis = axis;
            this.Value = value;
        }
    }
}
