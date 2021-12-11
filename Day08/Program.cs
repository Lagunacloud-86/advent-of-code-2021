using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
            Dictionary<Byte, char> bitMapper = new Dictionary<byte, char>();
            bitMapper.Add((byte)0b01110111, '0');
            bitMapper.Add((byte)0b00010010, '1');
            bitMapper.Add((byte)0b01011101, '2');
            bitMapper.Add((byte)0b01011011, '3');
            bitMapper.Add((byte)0b00111010, '4');
            bitMapper.Add((byte)0b01101011, '5');
            bitMapper.Add((byte)0b01101111, '6');
            bitMapper.Add((byte)0b01010010, '7');
            bitMapper.Add((byte)0b01111111, '8');
            bitMapper.Add((byte)0b01111011, '9');



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

        static UInt32 CalculateEntry(in Dictionary<Byte, Char> bitMapper, in String cipher, in String code)
        {
            String[] cipherCodes = cipher.Split(new char[] { ' ' });
            String[] displayCodes = code.Split(new char[] { ' ' });


            Dictionary<Char, Byte?> codeMap = new Dictionary<Char, Byte?>
            {
                { 'a', null },
                { 'b', null },
                { 'c', null },
                { 'd', null },
                { 'e', null },
                { 'f', null },
                { 'g', null },
            };

            Dictionary<Char, List<String>> cipherPossibilities = new Dictionary<Char, List<String>>();
            Dictionary<Byte, List<Char>> bitPossibilities = new Dictionary<Byte, List<Char>>();
            //Dictionary<Byte, List<Char>> deductor = new Dictionary<Byte, List<Char>>();

            foreach (var bm in bitMapper)
            {
                Int32 count = CountBits(bm.Key);
                List<String> ciphers = cipherCodes
                    .Where(x => x.Length == count)
                    .ToList();

                cipherPossibilities.Add(bm.Value, ciphers);


                //NOTE: THIS IS GROSS
                List<char> possibilities = new List<char>();
                foreach (var cc in ciphers)
                    for (Int32 i = 0; i < cc.Length; ++i)
                        if (!possibilities.Contains(cc[i]))
                            possibilities.Add(cc[i]);

                bitPossibilities.Add(bm.Key, possibilities);

                //for(Int32 i = 0; i < 8; ++i)
                //{
                //    Byte mask = (Byte)(1 << i);
                //    if ((bm.Key & mask) != 0)
                //    {
                //        bitPossibilities.Add((Byte)(bm.Key & mask), possibilities);
                //    }
                //}
            }




            StringBuilder stringBuilder = new StringBuilder();
            for (Int32 i = 0; i < displayCodes.Length; ++i)
            {
                byte displayCode = 0;
                for(int c = 0; c < displayCodes[i].Length; ++c)
                    displayCode |= codeMap[displayCodes[i][c]].Value;

                stringBuilder.Append(bitMapper[displayCode]);
            }

            UInt32 result = Convert
                .ToUInt32(stringBuilder.ToString());

            return result;
        }

        static Int32 CountBits(byte value)
        {
            Int32 count = 0;

            for (Int32 i = 0; i < 8; ++i)
            {
                byte shifted = (byte)(value >> i);
                count += (shifted & 0x1) == 1 ? 1 : 0;
            }

            return count;
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
