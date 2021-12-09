using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day07
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day07";
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
            String[] horizontals = input.Split(new char[] { ',' });
            Int32[] numbers = new int[horizontals.Length];
            Dictionary<Int32, UInt64> fuelCount = new Dictionary<Int32, UInt64>();

            Int32 min = Int32.MaxValue, max = Int32.MinValue;
            for (Int32 i = 0; i < horizontals.Length; ++i)
            {
                numbers[i] = Convert.ToInt32(horizontals[i]);
                min = Math.Min(numbers[i], min);
                max = Math.Max(numbers[i], max);
            }

            for (Int32 i = min; i <= max; ++i)
                Calculate_Fuel_Usage(fuelCount, i, numbers);
            


            return fuelCount.Min(x => x.Value);
        }

        static UInt64 Part_2_Logic(String input)
        {
            String[] horizontals = input.Split(new char[] { ',' });
            Int32[] numbers = new int[horizontals.Length];
            Dictionary<Int32, UInt64> fuelCount = new Dictionary<Int32, UInt64>();

            Int32 min = Int32.MaxValue, max = Int32.MinValue;
            for (Int32 i = 0; i < horizontals.Length; ++i)
            {
                numbers[i] = Convert.ToInt32(horizontals[i]);
                min = Math.Min(numbers[i], min);
                max = Math.Max(numbers[i], max);
            }

            for (Int32 i = min; i <= max; ++i)
                Calculate_Fuel_Usage_2(fuelCount, i, numbers);



            return fuelCount.Min(x => x.Value);

        }



        static void Calculate_Fuel_Usage(Dictionary<Int32, UInt64> fuelCount, Int32 index, Int32[] numbers)
        {
            if (!fuelCount.ContainsKey(index))
                fuelCount.Add(index, 0);


            for(Int32 i = 0; i < numbers.Length; ++i)
                fuelCount[index] += (UInt64)Math.Abs(index - numbers[i]);

        }

        static void Calculate_Fuel_Usage_2(Dictionary<Int32, UInt64> fuelCount, Int32 index, Int32[] numbers)
        {
            if (!fuelCount.ContainsKey(index))
                fuelCount.Add(index, 0);

            for (Int32 i = 0; i < numbers.Length; ++i)
            {
                UInt64 fuelIncrmenter = 1;
                Int32 indexIncrementer = Math.Sign(index - numbers[i]);
                for (Int32 j = numbers[i]; j != index; j += indexIncrementer)
                {
                    fuelCount[index] += fuelIncrmenter;
                    fuelIncrmenter++;
                }
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
