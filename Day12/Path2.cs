using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day12
{
    public class Path2
    {
        private Boolean _smallVisitedTwice;

        public List<Cave> Caves { get; }

        public Path2(Cave startCave)
        {
            _smallVisitedTwice = false;
            Caves = new List<Cave>();
            Caves.Add(startCave);
        }
        public Path2(Path2 previousPath)
        {
            _smallVisitedTwice = previousPath._smallVisitedTwice;
            Caves = new List<Cave>(previousPath.Caves);
        }

        public bool AddCave(Cave cave)
        {
            if (cave.Name == "start")
                return false;

            Boolean previousSmallCave = Caves.Any(x => x.Name == cave.Name && x.IsSmall);


            if (previousSmallCave && _smallVisitedTwice)
                return false;

            if (previousSmallCave && !_smallVisitedTwice)
                _smallVisitedTwice = true;

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
