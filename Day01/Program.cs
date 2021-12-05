using System;
using System.IO;
using System.Reflection;

namespace Day01
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
            String input = ReadEmbeddedResource("Day01.test-case-1.txt");

            return Part_1_Logic(input);
        }
        static Int32 Puzzle_Case_1()
        {
            String input = ReadEmbeddedResource("Day01.puzzle-input-1.txt");

            return Part_1_Logic(input);
        }

        static Int32 Test_Case_2()
        {
            String input = ReadEmbeddedResource("Day01.test-case-2.txt");

            return Part_2_Logic(input);
        }
        static Int32 Puzzle_Case_2()
        {
            String input = ReadEmbeddedResource("Day01.puzzle-input-2.txt");

            return Part_2_Logic(input);
        }

        static Int32 Part_1_Logic(String input)
        {
            Int32 count = 0, previous;
            String[] depths = input.Split("\n");
            previous = Convert.ToInt32(depths[0]);
            for (Int32 i = 1; i < depths.Length; ++i)
            {
                Int32 current = Convert.ToInt32(depths[i]);
                if (current > previous)
                    count++;
                previous = current;
            }
            return count;
        }
        static Int32 Part_2_Logic(String input)
        {
            Int32 count = -1, previous = Int32.MinValue;
            String[] depths = input.Split("\n");
            for (Int32 i = 0; i <= depths.Length  - 3; ++i)
            {
                Int32 current = 0;
                for(Int32 j = i; j < i + 3; ++j)
                {
                    current += Convert.ToInt32(depths[j]);
                }
                if (current > previous)
                    count++;
                previous = current;
            }
            return count;
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
