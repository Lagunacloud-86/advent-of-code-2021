using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Day12
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day12";

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1(false)}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2()}\n");
            Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            Console.WriteLine($"\tTest Case 3 Result: {Test_Case_3()}\n");
            Console.WriteLine($"\tTest Case 4 Result: {Test_Case_4()}\n");
            Console.WriteLine($"\tTest Case 5 Result: {Test_Case_5()}\n");
            Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2()}");

        }


        static UInt64 Test_Case_1(bool verbose = false)
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-1.txt");
            return Part_1_Logic(input, verbose);
        }
        static UInt64 Test_Case_2()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-2.txt");

            return Part_1_Logic(input);
        }

        static UInt64 Puzzle_Case_1()
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-1.txt");

            return Part_1_Logic(input);
        }

        static UInt64 Test_Case_3()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-3.txt");

            return Part_2_Logic(input);
        }

        static UInt64 Test_Case_4()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-4.txt");

            return Part_2_Logic(input);
        }
        static UInt64 Test_Case_5()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-5.txt");

            return Part_2_Logic(input);
        }

        static UInt64 Puzzle_Case_2()
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-2.txt");

            return Part_2_Logic(input);
        }

        static UInt64 Part_1_Logic(String input, Boolean verbose = false)
        {
            String[] rules = input.Split(new char[] { '\n' });
            Dictionary<String, Cave> caves = new Dictionary<string, Cave>();
            foreach(String rule in rules)
            {
                String[] caveInfo = rule.Split(new char[] { '-' });
                Cave cave1, cave2;
                if (!caves.TryGetValue(caveInfo[0].Trim(), out cave1))
                {
                    cave1 = new Cave(caveInfo[0].Trim());
                    caves.Add(cave1.Name, cave1);
                }
                if (!caves.TryGetValue(caveInfo[1].Trim(), out cave2))
                {
                    cave2 = new Cave(caveInfo[1].Trim());
                    caves.Add(cave2.Name, cave2);
                }

                cave1.ConnectedCaves.Add(cave2);
                cave2.ConnectedCaves.Add(cave1);
            }

            UInt64 validPaths = 0;
            List<Path> pathing = new List<Path>();

            Stack<Path> currentPath = new Stack<Path>();
            currentPath.Push(new Path(caves["start"]));
            while(currentPath.Count > 0)
            {
                Path path = currentPath.Pop();
                foreach(var cave in path.CurrentConnectedCaves())
                {
                    Path newPath = new Path(path.Caves);
                    if(newPath.AddCave(cave))
                    {
                        if (newPath.IsFinished)
                        {
                            pathing.Add(newPath);
                            validPaths++;
                        }
                        else
                        {
                            currentPath.Push(newPath);
                        }
                    }
                }
            }
        
            return validPaths;
        }

        static UInt64 Part_2_Logic(String input)
        {
            String[] rules = input.Split(new char[] { '\n' });
            Dictionary<String, Cave> caves = new Dictionary<string, Cave>();
            foreach (String rule in rules)
            {
                String[] caveInfo = rule.Split(new char[] { '-' });
                Cave cave1, cave2;
                if (!caves.TryGetValue(caveInfo[0].Trim(), out cave1))
                {
                    cave1 = new Cave(caveInfo[0].Trim());
                    caves.Add(cave1.Name, cave1);
                }
                if (!caves.TryGetValue(caveInfo[1].Trim(), out cave2))
                {
                    cave2 = new Cave(caveInfo[1].Trim());
                    caves.Add(cave2.Name, cave2);
                }

                cave1.ConnectedCaves.Add(cave2);
                cave2.ConnectedCaves.Add(cave1);
            }

            UInt64 validPaths = 0;
            List<Path2> pathing = new List<Path2>();

            Stack<Path2> currentPath = new Stack<Path2>();
            currentPath.Push(new Path2(caves["start"]));
            while (currentPath.Count > 0)
            {
                Path2 path = currentPath.Pop();
                foreach (var cave in path.CurrentConnectedCaves())
                {
                    Path2 newPath = new Path2(path);
                    if (newPath.AddCave(cave))
                    {
                        if (newPath.IsFinished)
                        {
                            pathing.Add(newPath);
                            validPaths++;
                        }
                        else
                        {
                            currentPath.Push(newPath);
                        }
                    }
                }
            }

            return validPaths;
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
