using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day08
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day08";
            Console.WriteLine("Hello World!");

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1()}\n");
            Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2()}\n");
            //Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2()}");

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
            UInt64 counter = 0;
            String[] entries = input.Split(new char[] { '\n' });
            foreach (String entry in entries)
            {
                String[] value = entry
                    .Trim()
                    .Split(new char[] { '|' });

                String[] codes = value[1].Trim().Split(new char[] { ' ' });
                foreach(String code in codes)
                {
                    if (code.Length == 2 || code.Length == 4 || code.Length == 3 || code.Length == 7)
                        counter++;
                }
            }

            return counter;
        }

        static UInt64 Part_2_Logic(String input)
        {
            Dictionary<Byte, Int32> bitMapper = new Dictionary<byte, int>();
            bitMapper.Add((byte)0b01110111, 0);
            bitMapper.Add((byte)0b00010010, 1);
            bitMapper.Add((byte)0b01011101, 2);
            bitMapper.Add((byte)0b01011011, 3);
            bitMapper.Add((byte)0b00111010, 4);
            bitMapper.Add((byte)0b01101011, 5);
            bitMapper.Add((byte)0b01101111, 6);
            bitMapper.Add((byte)0b01010010, 7);
            bitMapper.Add((byte)0b01111111, 8);
            bitMapper.Add((byte)0b01111011, 9);

            List<UInt32> entryNumbers = new List<UInt32>();
            String[] entries = input.Split(new char[] { '\n' });
            foreach (String entry in entries)
            {
                String[] value = entry
                    .Trim()
                    .Split(new char[] { '|' });


                entryNumbers.Add(
                    CalculateEntry(in bitMapper, value[0].Trim(), value[1].Trim()));
            }

            return 0;

        }

        static UInt32 CalculateEntry(in Dictionary<Byte, Int32> bitMapper, in String cipher, in String code)
        {
            String[] cipherCodes = cipher.Split(new char[] { ' ' });
            String[] displayCodes = code.Split(new char[] { ' ' });

            Dictionary<Int32, String> uniqueCiphers = new Dictionary<Int32, String>();
            //Dictionary<String, Int32> cipherPointers = new Dictionary<string, int>();

            Dictionary<Byte, Char> codeMap = new Dictionary<Byte, Char>();

            BuildUniqueCiphers(cipherCodes, uniqueCiphers);


            String temp = uniqueCiphers[7];
            for(Int32 i = 0; i < uniqueCiphers[1].Length; ++i)
                temp = temp.Replace(uniqueCiphers[1][i], ' ');
            temp = temp.Trim();
            codeMap.Add((byte)0b01000000, temp[0]);



            
            return 0;
        }

        static void BuildUniqueCiphers(in String[] cipherCodes, Dictionary<Int32, String> uniqueCiphers)
        {
            for (Int32 i = 0; i < cipherCodes.Length; ++i)
            {
                if (cipherCodes[i].Length == 2)
                    uniqueCiphers.Add(1, cipherCodes[i]);

                if (cipherCodes[i].Length == 3)
                    uniqueCiphers.Add(7, cipherCodes[i]);

                if (cipherCodes[i].Length == 4)
                    uniqueCiphers.Add(4, cipherCodes[i]);

                if (cipherCodes[i].Length == 7)
                    uniqueCiphers.Add(8, cipherCodes[i]);
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
