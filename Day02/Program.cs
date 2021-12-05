using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine($"Test Case 1 Result: {Test_Case_1()}");
            Console.WriteLine($"Puzzle 1 Result: {Puzzle_Case_1()}");
            Console.WriteLine($"Test Case 2 Result: {Test_Case_2()}");
            Console.WriteLine($"Puzzle 2 Result: {Puzzle_Case_2()}");

        }


        static Int32 Test_Case_1()
        {
            String input = ReadEmbeddedResource("Day02.test-case-1.txt");

            return Part_1_Logic(input);
        }
        static Int32 Puzzle_Case_1()
        {
            String input = ReadEmbeddedResource("Day02.puzzle-input-1.txt");

            return Part_1_Logic(input);
        }

        static Int32 Test_Case_2()
        {
            String input = ReadEmbeddedResource("Day02.test-case-2.txt");

            return Part_2_Logic(input);
        }
        static Int32 Puzzle_Case_2()
        {
            String input = ReadEmbeddedResource("Day02.puzzle-input-2.txt");

            return Part_2_Logic(input);
        }

        static Int32 Part_1_Logic(String input)
        {
            Position positionInfo = new Position
            {
                Horizontal = 0,
                Depth = 0
            };
            Dictionary<String, Action<Position, Int32>> positionHandlers = new Dictionary<string, Action<Position, Int32>>
            {
                { "forward", (p, v) => p.Horizontal += v },
                { "down", (p, v) => p.Depth += v },
                { "up", (p, v) => p.Depth -= v }
            };


            String[] instructions = input.Split('\n');
            for (Int32 i = 0; i < instructions.Length; ++i)
            {
                String[] parts = instructions[i].Split(' ');
                positionHandlers[parts[0]]
                    .Invoke(positionInfo, Convert.ToInt32(parts[1]));
            }
            Console.WriteLine($"\n\n\tHorizontal: {positionInfo.Horizontal}, Depth: {positionInfo.Depth}");
            return positionInfo.Horizontal * positionInfo.Depth;
        }
        static Int32 Part_2_Logic(String input)
        {
            Position positionInfo = new Position
            {
                Horizontal = 0,
                Depth = 0,
                Aim = 0
            };
            Dictionary<String, Action<Position, Int32>> positionHandlers = new Dictionary<string, Action<Position, Int32>>
            {
                { 
                    "forward", 
                    (p, v) =>
                    {
                        p.Horizontal += v;
                        p.Depth += p.Aim * v;
                    }
                },
                { "down", (p, v) => p.Aim += v },
                { "up", (p, v) => p.Aim -= v }
            };


            String[] instructions = input.Split('\n');
            for (Int32 i = 0; i < instructions.Length; ++i)
            {
                String[] parts = instructions[i].Split(' ');
                positionHandlers[parts[0]]
                    .Invoke(positionInfo, Convert.ToInt32(parts[1]));
            }
            Console.WriteLine($"\n\n\tHorizontal: {positionInfo.Horizontal}, Depth: {positionInfo.Depth}, Aim: {positionInfo.Aim}");
            return positionInfo.Horizontal * positionInfo.Depth;
        }



        static String ReadEmbeddedResource(String resource)
        {

            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
