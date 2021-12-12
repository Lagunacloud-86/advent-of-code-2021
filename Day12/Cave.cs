using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Day12
{
    public class Cave
    {
        public String Name { get; }

        public Boolean IsSmall => Char.IsLower(this.Name[0]);

        public List<Cave> ConnectedCaves { get; }


        public Cave(String name)
        {
            this.Name = name.Trim();
            this.ConnectedCaves = new List<Cave>();
        }

    }
}
