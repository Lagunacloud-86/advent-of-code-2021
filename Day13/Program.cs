using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day13
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day13";

            //Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1(true)}\n");
            //Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2(true)}\n");
            Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2(false)}");

        }


        static UInt64 Test_Case_1(bool verbose = false)
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-1.txt");
            return Part_1_Logic(input, verbose);
        }
        static UInt64 Puzzle_Case_1(bool verbose = false)
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-1.txt");

            return Part_1_Logic(input, verbose);
        }

        static UInt64 Test_Case_2(bool verbose = false)
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-2.txt");

            return Part_2_Logic(input, verbose);
        }
        static UInt64 Puzzle_Case_2(bool verbose = false)
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-2.txt");

            return Part_2_Logic(input, verbose);
        }

        static UInt64 Part_1_Logic(String input, bool verbose = false)
        {
            String[] lines = input.Split(new char[] { '\n' });
            char[,] dotMap = null;
            Int32 i;

            List<PointInfo> points = new List<PointInfo>();
            List<FoldInfo> folds = new List<FoldInfo>();

            for (i = 0; i < lines.Length; ++i)
            {
                if (String.IsNullOrWhiteSpace(lines[i]))
                    break;

                String[] vectorInfo = lines[i]
                    .Trim()
                    .Split(new char[] { ',' });

                PointInfo pointInfo =
                    new PointInfo(Convert.ToInt32(vectorInfo[0]), Convert.ToInt32(vectorInfo[1]));
                points.Add(pointInfo);
            }
            for(i = i + 1; i < lines.Length; ++i)
            {
                String[] instruction = lines[i]
                    .Split(new char[] { '=' });
                String[] descriptions = instruction[0]
                    .Split(new char[] { ' ' });
                Int32 value = Convert
                    .ToInt32(instruction[1].Trim());

                folds.Add(new FoldInfo(in descriptions[2], in value));
            }

            Int32
                x = points.Max(x => x.X + 1),
                y = points.Max(x => x.Y + 1),
                prevX, prevY;
            dotMap = new char[x, y];

            InitializeDots(dotMap);
            MapDots(dotMap, points);

            if (verbose)
                DisplayDotMap(dotMap, x, y);

            Console.WriteLine();

            prevX = x;
            prevY = y;
            Fold(dotMap, folds[0], in prevX, in prevY, out x, out y);

            UInt64 total = CountPoints(dotMap, in x, in y);
            //for (i = 0; i < folds.Count; ++i)
            //{
            //    prevX = x;
            //    prevY = y;
            //    Fold(dotMap, folds[i], in prevX, in prevY, out x, out y);

            //    if (verbose)
            //        DisplayDotMap(dotMap, x, y);
            //}

            //for (i = 0; i < folds.Count; ++i)
            //{
            //    prevX = x;
            //    prevY = y;
             //    Fold(dotMap, folds[i], in prevX, in prevY, out x, out y);

            //    if (verbose)
            //        DisplayDotMap(dotMap, x, y);
            //}





            return total;
        }

        static UInt64 Part_2_Logic(String input, bool verbose = false)
        {
            String[] lines = input.Split(new char[] { '\n' });
            char[,] dotMap = null;
            Int32 i;

            List<PointInfo> points = new List<PointInfo>();
            List<FoldInfo> folds = new List<FoldInfo>();

            for (i = 0; i < lines.Length; ++i)
            {
                if (String.IsNullOrWhiteSpace(lines[i]))
                    break;

                String[] vectorInfo = lines[i]
                    .Trim()
                    .Split(new char[] { ',' });

                PointInfo pointInfo =
                    new PointInfo(Convert.ToInt32(vectorInfo[0]), Convert.ToInt32(vectorInfo[1]));
                points.Add(pointInfo);
            }
            for (i = i + 1; i < lines.Length; ++i)
            {
                String[] instruction = lines[i]
                    .Split(new char[] { '=' });
                String[] descriptions = instruction[0]
                    .Split(new char[] { ' ' });
                Int32 value = Convert
                    .ToInt32(instruction[1].Trim());

                folds.Add(new FoldInfo(in descriptions[2], in value));
            }

            Int32
                x = points.Max(x => x.X + 1),
                y = points.Max(x => x.Y + 1),
                prevX, prevY;
            dotMap = new char[x, y];

            InitializeDots(dotMap);
            MapDots(dotMap, points);

            if (verbose)
                DisplayDotMap(dotMap, x, y);

            Console.WriteLine();

            for (i = 0; i < folds.Count; ++i)
            {
                prevX = x;
                prevY = y;
                Fold(dotMap, folds[i], in prevX, in prevY, out x, out y);

                if (verbose)
                    DisplayDotMap(dotMap, x, y);
            }

            Console.WriteLine();
            DisplayDotMap(dotMap, x, y);


            return 0UL;
        }

        static void InitializeDots(char[,] dotMap)
        {
            for (Int32 y = 0; y <= dotMap.GetUpperBound(1); ++y)
                for (Int32 x = 0; x <= dotMap.GetUpperBound(0); ++x)
                    dotMap[x, y] = '.';
        }
        static void MapDots(char[,] dotMap, List<PointInfo> points)
        {
            foreach(var point in points)
            {
                dotMap[point.X, point.Y] = '#';
            }
        }
        static void Fold(char[,] dotMap, in FoldInfo fold, in Int32 prevX, in Int32 prevY, out Int32 newX, out Int32 newY)
        {
            newX = prevX;
            newY = prevY;

            if (fold.Axis == "x")
                FoldAlongX(dotMap, in fold, in prevX, in prevY, out newX);

            if (fold.Axis == "y")
                FoldAlongY(dotMap, in fold, in prevX, in prevY, out newY);
        }
        static void FoldAlongX(char[,] dotMap, in FoldInfo fold, in Int32 prevX, in Int32 prevY, out Int32 newX)
        {
            newX = prevX - (prevX - fold.Value);
            for (Int32 y = 0; y < prevY; ++y)
            {
                for (Int32 x = fold.Value + 1; x < prevX; ++x)
                {
                    if (dotMap[x, y] == '#')
                    {
                        Int32 inverseX =  fold.Value + (fold.Value - x);
                        dotMap[inverseX, y] = '#';
                    }
                }
            }
        }
        static void FoldAlongY(char[,] dotMap, in FoldInfo fold, in Int32 prevX, in Int32 prevY, out Int32 newY)
        {

            newY = prevY - (prevY - fold.Value);
            for (Int32 x = 0; x < prevX; ++x)
            {
                for (Int32 y = fold.Value + 1; y < prevY; ++y)
                {
                    if (dotMap[x, y] == '#')
                    {
                        Int32 inverseY = fold.Value + (fold.Value - y);
                        dotMap[x, inverseY] = '#';
                    }
                }
              
            }

        }

        static UInt64 CountPoints(char[,] dotMap, in Int32 sizeX, in Int32 sizeY)
        {
            UInt64 total = 0;
            for (Int32 y = 0; y < sizeY; ++y)
            {
                for (Int32 x = 0; x < sizeX; ++x)
                {
                    if (dotMap[x, y] == '#')
                        total++;
                }
            }
            return total;
        }

        static void DisplayDotMap(char[,] dotMap, Int32 sizeX, Int32 sizeY)
        {
            for (Int32 y = 0; y < sizeY; ++y)
            {
                for (Int32 x = 0; x < sizeX; ++x)
                {
                    Console.Write(dotMap[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }



        static String ReadEmbeddedResource(String resource)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(resource);
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
