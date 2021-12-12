using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day12
{
    public class Path
    {
        public List<Cave> Caves { get;}

        public Path(Cave startCave)
        {
            Caves = new List<Cave>();
            Caves.Add(startCave);
        }
        public Path(List<Cave> caves)
        {
            Caves = new List<Cave>(caves);
        }

        public bool AddCave(Cave cave)
        {
            if (Caves.Any(x => x.Name == cave.Name && x.IsSmall))
                return false;


            Caves.Add(cave);
            return true;
        }

        public bool IsFinished => Caves[Caves.Count - 1].Name == "end";

        public List<Cave> CurrentConnectedCaves()
        {
            return Caves[Caves.Count - 1].ConnectedCaves;
        }

    }
}
