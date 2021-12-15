using System;
using System.IO;
using System.Reflection;

namespace Day15
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day14";

            //Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1(true)}\n");
            //Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2(true)}\n");
            //Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2(true)}");

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
            return 0UL;
        }

        static UInt64 Part_2_Logic(String input, bool verbose = false)
        {
            return 0UL;
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
