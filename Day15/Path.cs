using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day15
{
    public class Path
    {
        public static PointInfo Up = new PointInfo(0, -1);
        public static PointInfo Down = new PointInfo(0, 1);
        public static PointInfo Left = new PointInfo(-1, 0);
        public static PointInfo Right = new PointInfo(1, 0);

        public class PathNode
        {
            public PointInfo Point { get; set; }

            public UInt64 Risk { get; set; }

            //public Int32 Distance { get; set; }

            //public Int32 Depth { get; set; }


            public PathNode Parent { get; set; } = null;

            //public List<PathNode> Children { get; set; } = new List<PathNode>();
        }


        public PointInfo[] PathPoints { get; private set; }


        private Path(PointInfo[] points)
        {
            PathPoints = new PointInfo[points.Length];
            for(Int32 i = 0; i < points.Length; ++i)
                PathPoints[i] = points[i];
        }

        public static Path MapPath(
            RiskMap riskMap, 
            in PointInfo startPoint,
            in PointInfo endPoint)
        {
            List<PointInfo> path = new List<PointInfo>();
            Internal_InitializePath(path, in startPoint, in endPoint);

            bool changeHappened;
            Int32 segmentLength = 5;
            Int32 iterationCount = 0;
            Int32 index, endIndex;
            do
            {
                changeHappened = false;
                for (index = 0; index < path.Count - segmentLength; ++index)
                {
                    endIndex = index + segmentLength - 1;
                    changeHappened |= Internal_IterateSection(
                        riskMap, path,
                        in segmentLength, in index, in endIndex);
                }
                iterationCount++;
            } while (changeHappened);


            return new Path(path.ToArray());
        }




        private static void Internal_InitializePath(
            List<PointInfo> path, 
            in PointInfo start, in PointInfo end)
        {
            if (path == null)
                path = new List<PointInfo>();
            path.Clear();

            Double dist = Math.Sqrt(
                Math.Pow(end.X - start.X, 2D) + 
                Math.Pow(end.Y - start.Y, 2D));

            Double slopeX, slopeY, currentX, currentY, prevX = -0D, prevY = -0D;
            slopeX = (Double)(end.X - start.X) / dist;
            slopeY = (Double)(end.Y - start.Y) / dist;
            currentX = start.X;
            currentY = start.Y;
            path.Add(new PointInfo(start.X, start.Y));
            while ((Int32)currentX != end.X && (Int32)currentY != end.Y)
            {
                currentX += slopeX;
                if (prevX != Math.Round(currentX))
                {
                    prevX = Math.Round(currentX);
                    path.Add(new PointInfo((Int32)Math.Round(currentX, 0), (Int32)Math.Round(currentY, 0)));
                }
                
                currentY += slopeY;
                if (prevY != Math.Round(currentY))
                {
                    prevY = Math.Round(currentY);
                    path.Add(new PointInfo((Int32)Math.Round(currentX, 0), (Int32)Math.Round(currentY, 0)));
                }
            }
        }

        private static bool Internal_IterateSection(
            RiskMap riskMap,
            List<PointInfo> path,
            in Int32 segmentLength,
            in Int32 index,
            in Int32 endIndex)
        {

            List<PathNode> leafNodes = new List<PathNode>();
            bool changeHappened = false;

            Stack<PathNode> nodeStack = new Stack<PathNode>();
            nodeStack.Push(new PathNode
            {
                Parent = null,
                Point = path[index],
                Risk = 0
            });
            while(nodeStack.Count > 0)
            {
                PathNode current = nodeStack.Pop();

                if(current.Point.Equals(path[endIndex]))
                {
                    leafNodes.Add(current);
                    continue;
                }

                if (current.Point.X >= path[index].X + segmentLength)
                    continue;
                if (current.Point.X < path[index].X)
                    continue;

                if (current.Point.Y >= path[index].Y + segmentLength)
                    continue;
                if (current.Point.Y < path[index].Y)
                    continue;

                Internal_TryAddNode(riskMap, nodeStack, current, in Up);
                Internal_TryAddNode(riskMap, nodeStack, current, in Left);
                Internal_TryAddNode(riskMap, nodeStack, current, in Down);
                Internal_TryAddNode(riskMap, nodeStack, current, in Right);
            }


            PathNode node = leafNodes
                .OrderBy(x => x.Risk)
                .First();

            List<PointInfo> newPath = new List<PointInfo>();
            while (node != null)
            {
                newPath.Insert(0, node.Point);
                node = node.Parent;
            }

            changeHappened = newPath.Count != segmentLength;

            if (!changeHappened)
            {
                for(Int32 i = 0; i < segmentLength; ++i)
                    if(!path[index + i].Equals( newPath[i]))
                    {
                        changeHappened = true;
                        break;
                    }
            }

            if (changeHappened)
            {
                path.RemoveRange(index, segmentLength);
                path.InsertRange(index, newPath);
            }
            



            return changeHappened;
        }


        private static void Internal_TryAddNode(
            RiskMap riskMap,
            Stack<PathNode> nodeStack,
            PathNode current,
            in PointInfo direction)
        {
            PointInfo newPoint = 
                new PointInfo(current.Point.X + direction.X, current.Point.Y + direction.Y);

            bool valid = true;
            PathNode node = current;
            while(node != null)
            {
                if (node.Point.Equals(newPoint))
                {
                    valid = false;
                    break;
                }
                node = node.Parent;
            }

            if (valid && riskMap.TryGetRisk(newPoint.X, newPoint.Y, out UInt64 risk))
            {
                nodeStack.Push(new PathNode
                {
                    Point = newPoint,
                    Risk = current.Risk + risk,
                    Parent = current
                });
            }
        }

    }
}
