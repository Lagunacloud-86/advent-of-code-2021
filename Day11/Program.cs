using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Day11
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day11";

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1(false)}\n");
            Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2()}\n");
            Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2()}");

        }


        static UInt64 Test_Case_1(bool verbose = false)
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-1.txt");
            return Part_1_Logic(input, verbose);
        }
        static UInt64 Puzzle_Case_1()
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-1.txt");

            return Part_1_Logic(input);
        }

        static UInt64 Test_Case_2()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-2.txt");

            return Part_2_Logic(input);
        }
        static UInt64 Puzzle_Case_2()
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-2.txt");

            return Part_2_Logic(input);
        }

        static UInt64 Part_1_Logic(String input, Boolean verbose = false)
        {
            Int32[,] octopuses = null;
            String[] entries = input.Split(new char[] { '\n' });
            Int32 height = entries.Length;
            Int32 width = entries[0].Trim().Length;
            octopuses = new int[width, height];
            for(Int32 i = 0; i < entries.Length; ++i)
            {
                for(Int32 c = 0; c < width; ++c)
                {
                    octopuses[c, i] = 
                        Convert.ToInt32(Char.GetNumericValue(entries[i][c]));
                }
            }

            UInt64 total = 0;
            for(Int32 i = 0; i < 100; ++i)
            {
                total += TakeStep(octopuses, width, height);
                if (verbose)
                {
                    Console.WriteLine($"\nStep {i + 1}:");
                    PrintOctopuses(octopuses, width, height);
                    Console.WriteLine();
                }
            }

            return total;
        }

        static UInt64 Part_2_Logic(String input)
        {
            Int32[,] octopuses = null;
            String[] entries = input.Split(new char[] { '\n' });
            UInt64 height = (UInt64)entries.Length;
            UInt64 width = (UInt64)entries[0].Trim().Length;
            octopuses = new int[width, height];
            for (UInt64 i = 0; i < (UInt64)entries.Length; ++i)
            {
                for (UInt64 c = 0; c < width; ++c)
                {
                    octopuses[c, i] =
                        Convert.ToInt32(Char.GetNumericValue(entries[i][(Int32)c]));
                }
            }

            UInt64 step = 1;
            //for (Int32 i = 0; i < 100; ++i)
            while(TakeStep(octopuses, (Int32)width, (Int32)height) != width * height)
            {
                step++;
            }

            return step;
        }

        static UInt64 TakeStep(Int32[,] octopuses, Int32 width, Int32 height)
        {
            UInt64 flashCount = 0;

            Stack<PointInfo> points = new Stack<PointInfo>();
            Stack<PointInfo> flashers = new Stack<PointInfo>();

            //Initial increment
            bool[,] hasFlashed = new bool[width, height];
            for (Int32 y = 0; y < height; ++y)
                for (Int32 x = 0; x < width; ++x)
                {
                    octopuses[x, y]++;
                    hasFlashed[x, y] = false;

                    if (octopuses[x, y] > 9)
                        flashers.Push(new PointInfo(in x, in y));
                }



            //flash
            PointInfo[] adderDirections = new PointInfo[]
            {
                new PointInfo(0, -1),
                new PointInfo(1, -1),
                new PointInfo(1, 0),
                new PointInfo(1, 1),
                new PointInfo(0, 1),
                new PointInfo(-1, 1),
                new PointInfo(-1, 0),
                new PointInfo(-1, -1)
            };

            while (flashers.Count > 0)
            {
                PointInfo pointInfo = flashers.Pop();
                if (hasFlashed[pointInfo.X, pointInfo.Y])
                    continue;
                hasFlashed[pointInfo.X, pointInfo.Y] = true;
                octopuses[pointInfo.X, pointInfo.Y] = 0;
                flashCount++;

                for (Int32 i = 0; i < adderDirections.Length; ++i)
                {
                    Int32 x = pointInfo.X + adderDirections[i].X;
                    Int32 y = pointInfo.Y + adderDirections[i].Y;

                    if (x < 0 || x >= width)
                        continue;

                    if (y < 0 || y >= height)
                        continue;

                    points.Push( new PointInfo(x, y) );
                }


                while (points.Count > 0)
                {
                    PointInfo adjacentPoint = points.Pop();
                    if (hasFlashed[adjacentPoint.X, adjacentPoint.Y])
                        continue;

                    octopuses[adjacentPoint.X, adjacentPoint.Y]++;
                    if (octopuses[adjacentPoint.X, adjacentPoint.Y] > 9)
                    {
                        flashers.Push(new PointInfo(adjacentPoint.X, adjacentPoint.Y));
                    }

                }
            }

            return flashCount;
        }


        static void PrintOctopuses(Int32[,] octopuses, Int32 width, Int32 height)
        {
            for (Int32 y = 0; y < height; ++y)
            {
                for (Int32 x = 0; x < width; ++x)
                {
                    Console.Write(octopuses[x, y]);
                }
                Console.WriteLine();
            }
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
