using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;

namespace Day06
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day06";
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
            String[] initialFish = input.Split(new char[] { ',' });
            Dictionary<Int32, UInt64> FishCounter = new()
            {
                { 0, 0UL },
                { 1, 0UL },
                { 2, 0UL },
                { 3, 0UL },
                { 4, 0UL },
                { 5, 0UL },
                { 6, 0UL },
                { 7, 0UL },
                { 8, 0UL },
                { -1, 0UL }
            };
            for (Int32 i = 0; i < initialFish.Length; ++i)
            {
                Int32 value = Convert.ToInt32(initialFish[i]);
                FishCounter[value]++;
            }

            for (Int32 day = 0; day < 80; ++day)
            {
                for (Int32 fc = 0; fc < 8; fc++)
                {
                    FishCounter[fc - 1] = FishCounter[fc];
                    FishCounter[fc] = FishCounter[fc + 1];
                }
                FishCounter[8] = 0;
                FishCounter[6] += FishCounter[-1];
                FishCounter[8] += FishCounter[-1];
                FishCounter[-1] = 0;
            }

            UInt64 result = 0;
            foreach (var kvp in FishCounter)
                result += kvp.Value;

            return result;
        }

        static UInt64 Part_2_Logic(String input)
        {
            String[] initialFish = input.Split(new char[] { ',' });
            Dictionary<Int32, UInt64> FishCounter = new()
            {
                { 0, 0UL },
                { 1, 0UL },
                { 2, 0UL },
                { 3, 0UL },
                { 4, 0UL },
                { 5, 0UL },
                { 6, 0UL },
                { 7, 0UL },
                { 8, 0UL },
                { -1, 0UL }
            };
            for (Int32 i = 0; i < initialFish.Length; ++i)
            {
                Int32 value = Convert.ToInt32(initialFish[i]);
                FishCounter[value]++;
            }

            for (Int32 day = 0; day < 256; ++day)
            {
                for (Int32 fc = 0; fc < 8; fc++)
                {
                    FishCounter[fc - 1] = FishCounter[fc];
                    FishCounter[fc] = FishCounter[fc + 1];
                }
                FishCounter[8] = 0;
                FishCounter[6] += FishCounter[-1];
                FishCounter[8] += FishCounter[-1];
                FishCounter[-1] = 0;
            }

            UInt64 result = 0;
            foreach (var kvp in FishCounter)
                result += kvp.Value;

            return result;
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
