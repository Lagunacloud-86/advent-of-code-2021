using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public readonly struct Node
    {
        public Int32 Depth { get; }

        public Char Left { get; }

        public Char Right { get; }

        public Node(in Int32 depth, in Char left, in Char right)
        {
            this.Depth = depth;
            this.Left = left;
            this.Right = right;
        }
    }

}
