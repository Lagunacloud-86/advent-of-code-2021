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


        //static Boolean ReadNextHyrdoVentLine(in ReadOnlySpan<char> input, in Int32 startIndex, out Int32 endIndex)
        //{
        //    endIndex = -1;

        //    if (startIndex >= input.Length)
        //        return false;

        //    Int32 i = 0;
        //    for (i = startIndex; i < input.Length; ++i)
        //    {
        //        if (input[i] == '\n')
        //        {
        //            endIndex = i + 1;
        //            return true;
        //        }

        //    }

        //    endIndex = i;
        //    return true;
        //}

        //static Line ParseLine(in ReadOnlySpan<char> lineInput)
        //{
        //    Int32 x1 = 0, x2 = 0, y1 = 0, y2 = 0;

        //    ReadOnlySpan<char> lineSeparator = "->";

        //    for (Int32 i = 0; i < lineInput.Length - 1; ++i)
        //    {
        //        ReadOnlySpan<char> section = lineInput.Slice(i, 2);
        //        if (section.SequenceEqual(lineSeparator))
        //        {
        //            ReadOnlySpan<char> left = lineInput.Slice(0, i - 1);
        //            ReadOnlySpan<char> right = lineInput.Slice(i + 3);
        //            ParseVector(in left, out x1, out y1);
        //            ParseVector(in right, out x2, out y2);
        //            break;
        //        }
        //    }

        //    return new Line(in x1, in y1, in x2, in y2);
        //}
        //static void ParseVector(in ReadOnlySpan<char> vectorInput, out Int32 x, out Int32 y)
        //{
        //    for (Int32 i = 0; i < vectorInput.Length; ++i)
        //    {
        //        if (vectorInput[i] == ',')
        //        {
        //            ReadOnlySpan<char> xInput = vectorInput.Slice(0, i);
        //            ReadOnlySpan<char> yInput = vectorInput.Slice(i + 1);

        //            x = Int32.Parse(xInput.ToString().Trim());
        //            y = Int32.Parse(yInput.ToString().Trim());

        //            return;
        //        }
        //    }

        //    x = -1;
        //    y = -1;


        //}


        static String ReadEmbeddedResource(String resource)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using Stream stream = assembly.GetManifestResourceStream(resource);
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
