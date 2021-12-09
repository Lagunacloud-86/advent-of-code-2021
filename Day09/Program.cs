using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day09
{
    class Program
    {

        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day09";
            Console.WriteLine("Hello World!");

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1()}\n");
            Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2()}\n");
            Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2()}");

        }


        static UInt64 Test_Case_1()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-1.txt");
            return Part_1_Logic(input);
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

        static UInt64 Part_1_Logic(String input)
        {
            Heightmap heightmap = new Heightmap(input);

            List<PointInfo> lowestPoints = new List<PointInfo>();
            for(Int32 y = 0; y < heightmap.Height; ++y)
            {
                Stack<short> adjacent = new Stack<short>();
                for(Int32 x = 0; x < heightmap.Width; ++x)
                {
                    short current = heightmap.GetHeight(x, y);
                    short temp;
                    if (heightmap.TryGetHeight(x - 1, y, out temp))
                        adjacent.Push(temp);
                    if (heightmap.TryGetHeight(x + 1, y, out temp))
                        adjacent.Push(temp);
                    if (heightmap.TryGetHeight(x, y - 1, out temp))
                        adjacent.Push(temp);
                    if (heightmap.TryGetHeight(x, y + 1, out temp))
                        adjacent.Push(temp);


                    bool isLowest = true;
                    while(adjacent.Count > 0)
                    {
                        temp = adjacent.Pop();
                        if(current >= temp)
                        {
                            isLowest = false;
                            break;
                        }
                    }
                    adjacent.Clear();
                    if (isLowest)
                        lowestPoints.Add(new PointInfo(in x, in y));

                }
            }

            Int32 total = 0;
            foreach (PointInfo pi in lowestPoints)
                total += heightmap.GetHeight(pi.X, pi.Y) + 1;

            return (UInt64)total;
        }

        static UInt64 Part_2_Logic(String input)
        {
            Heightmap heightmap = new Heightmap(input);

            List<PointInfo> lowestPoints = new List<PointInfo>();
            for (Int32 y = 0; y < heightmap.Height; ++y)
            {
                Stack<short> adjacent = new Stack<short>();
                for (Int32 x = 0; x < heightmap.Width; ++x)
                {
                    short current = heightmap.GetHeight(x, y);
                    short temp;

                    if (heightmap.TryGetHeight(x - 1, y, out temp))
                        adjacent.Push(temp);
                    if (heightmap.TryGetHeight(x + 1, y, out temp))
                        adjacent.Push(temp);
                    if (heightmap.TryGetHeight(x, y - 1, out temp))
                        adjacent.Push(temp);
                    if (heightmap.TryGetHeight(x, y + 1, out temp))
                        adjacent.Push(temp);


                    bool isLowest = true;
                    while (adjacent.Count > 0)
                    {
                        temp = adjacent.Pop();
                        if (current >= temp)
                        {
                            isLowest = false;
                            break;
                        }
                    }
                    adjacent.Clear();
                    if (isLowest)
                        lowestPoints.Add(new PointInfo(in x, in y));

                }
            }


            List<Int32> basinValues = new List<int>();
            foreach (PointInfo pi in lowestPoints)
            {
                short initialHeight = heightmap.GetHeight(pi.X, pi.Y);
                Stack<PointInfo> basinStack = new Stack<PointInfo>();
                basinStack.Push(pi);

                List<PointInfo> selectedPoints = new List<PointInfo>();
                while(basinStack.Count > 0)
                {
                    PointInfo point = basinStack.Pop();

                    if (!selectedPoints.Contains(point))
                        selectedPoints.Add(point);



                    short current = heightmap.GetHeight(point.X, point.Y);
                    short temp;

                    

                    if (heightmap.TryGetHeight(point.X - 1, point.Y, out temp) && temp > current && temp != 9)
                        basinStack.Push(new PointInfo(point.X - 1, point.Y));

                    if (heightmap.TryGetHeight(point.X + 1, point.Y, out temp) && temp > current && temp != 9)
                        basinStack.Push(new PointInfo(point.X + 1, point.Y));

                    if (heightmap.TryGetHeight(point.X, point.Y - 1, out temp) && temp > current && temp != 9)
                        basinStack.Push(new PointInfo(point.X, point.Y - 1));

                    if (heightmap.TryGetHeight(point.X, point.Y + 1, out temp) && temp > current && temp != 9)
                        basinStack.Push(new PointInfo(point.X, point.Y + 1));

                }
                basinValues.Add(selectedPoints.Count);
            }

            var largest = basinValues
                .OrderByDescending(x => x)
                .Take(3);
            Int64 total = 1;
            foreach(var l in largest)
                total *= l;


            return (UInt64)total;
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
