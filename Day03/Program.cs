using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Day03
{
    class Program
    {
        static String Namespace { get; set; }
        static void Main(string[] args)
        {
            Namespace = "Day03";

            Console.WriteLine("Hello World!");

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1()}\n");
            Console.WriteLine($"Puzzle 1 Result: {Puzzle_Case_1()}");
            Console.WriteLine($"Test Case 2 Result: {Test_Case_2()}");
            Console.WriteLine($"Puzzle 2 Result: {Puzzle_Case_2()}");

        }


        static Int32 Test_Case_1()
        {
            String input = ReadEmbeddedResource("Day03.test-case-1.txt");

            return Part_1_Logic(input);
        }
        static Int32 Puzzle_Case_1()
        {
            String input = ReadEmbeddedResource("Day03.puzzle-input-1.txt");

            return Part_1_Logic(input);
        }

        static Int32 Test_Case_2()
        {
            String input = ReadEmbeddedResource("Day03.test-case-2.txt");

            return Part_2_Logic(input);
        }
        static Int32 Puzzle_Case_2()
        {
            String input = ReadEmbeddedResource("Day03.puzzle-input-2.txt");

            return Part_2_Logic(input);
        }

        static Int32 Part_1_Logic(String input)
        {
            Int32[] resultData = null;
            String[] inputData = input.Split('\n');
            resultData = new Int32[inputData.Length];

            for (Int32 i = 0; i < inputData.Length; ++i)
                resultData[i] = Convert.ToInt32(inputData[i].Trim(), 2);

            Int32 bitLength = inputData[0].Trim().Length;
            Int32 epsilonMask = 0;
            for (Int32 i = 0; i < bitLength; ++i)
                epsilonMask |= 1 << i;

            Int32 gamma = 0;
            for(Int32 bp = 0; bp < bitLength; bp++)
            {
                Int32 counter = 0;
                for (Int32 i = 0; i < resultData.Length; ++i)
                {
                    if ((resultData[i] & (1 << bp)) != 0)
                        counter++;
                }
                if (counter > resultData.Length / 2)
                    gamma |= (1 << bp);
            }

            Int32 epsilon = ~gamma & epsilonMask;

            Console.WriteLine($"Gamma: {gamma}, Epsilon: {epsilon}");


            return gamma * epsilon;
        }
        static Int32 Part_2_Logic(String input)
        {

            Int32[] resultData = null;
            String[] inputData = input.Split('\n');
            resultData = new Int32[inputData.Length];

            for (Int32 i = 0; i < inputData.Length; ++i)
                resultData[i] = Convert.ToInt32(inputData[i].Trim(), 2);

            Int32 bitLength = inputData[0].Trim().Length;
            Int32 mask = 0;
            for (Int32 i = 0; i < bitLength; ++i)
                mask |= 1 << i;

            List<Int32> oxygenEntries = new List<int>(resultData);
            List<Int32> co2Entries = new List<int>(resultData);
            for (Int32 bp = bitLength - 1; bp >= 0; bp--)
            {      
                if(oxygenEntries.Count > 1)
                {
                    Int32 bit = FindMostCommonBitResult(oxygenEntries.ToArray(), bp);
                    TakeCommonBitResult(oxygenEntries, bp, (entry) => entry >> bp == bit);
                }

                if (co2Entries.Count > 1)
                {
                    Int32 bit = FindMostCommonBitResult(co2Entries.ToArray(), bp);
                    TakeCommonBitResult(co2Entries, bp, (entry) => entry >> bp == (~bit & 1));
                }
            }


            

            Console.WriteLine($"Oxygen: {oxygenEntries[0] & mask}, Co2: {co2Entries[0] & mask}");


            return (oxygenEntries[0] & mask) * (co2Entries[0] & mask);
        }

        static Int32 FindMostCommonBitResult(Int32[] data, Int32 bit)
        {
            Int32 counter = 0;
            for (Int32 i = 0; i < data.Length; ++i)
            {
                if ((data[i] & (1 << bit)) != 0)
                    counter++;
            }
            Int32 difference = data.Length - counter;

            if (counter == difference)
                return 1;

            return counter > difference ? 1 : 0;
        }
        static void TakeCommonBitResult(List<Int32> data, Int32 bit, Func<Int32, Boolean> bitPredicate)
        {
            for (Int32 i = 0; i < data.Count; ++i)
            {
                if(!bitPredicate(data[i] & (1 << bit)))
                {
                    data.RemoveAt(i);
                    i--;
                }
            }
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
