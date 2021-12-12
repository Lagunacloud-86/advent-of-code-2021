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

            Dictionary<Char, Byte> displayMapper = new Dictionary<Char, Byte>();
            displayMapper.Add('0', (byte)0b01110111);
            displayMapper.Add('1', (byte)0b00010010);
            displayMapper.Add('2', (byte)0b01011101);
            displayMapper.Add('3', (byte)0b01011011);
            displayMapper.Add('4', (byte)0b00111010);
            displayMapper.Add('5', (byte)0b01101011);
            displayMapper.Add('6', (byte)0b01101111);
            displayMapper.Add('7', (byte)0b01010010);
            displayMapper.Add('8', (byte)0b01111111);
            displayMapper.Add('9', (byte)0b01111011);



            //List<UInt64> entryNumbers = new List<UInt64>();
            UInt64 total = 0;
            String[] entries = input.Split(new char[] { '\n' });
            foreach (String entry in entries)
            {
                String[] value = entry
                    .Trim()
                    .Split(new char[] { '|' });


                total += CalculateEntry(in bitMapper, in displayMapper, value[0].Trim(), value[1].Trim());
                //entryNumbers.Add(
                //    CalculateEntry(
                //        in bitMapper,
                //        in displayMapper,
                //        value[0].Trim(),
                //        value[1].Trim()));
            }

            return total;

        }

        static UInt32 CalculateEntry(
            in Dictionary<Byte, Char> bitMapper,
            in Dictionary<Char, Byte> displayMapper, 
            in String cipher, in String code)
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


            foreach (var bm in bitMapper)
            {
                Int32 count = CountBits(bm.Key);

                List<String> ciphers = cipherCodes
                    .Where(x => x.Length == count)
                    .ToList();

                cipherPossibilities.Add(bm.Value, ciphers);

            }

            String one = cipherPossibilities['1'][0];
            String four = cipherPossibilities['4'][0];
            String seven = cipherPossibilities['7'][0];
            String eight = cipherPossibilities['8'][0];
            //List<char> remaining;

            String top = MaskEntry(seven, one);
            //GetRemainingCharacters(seven, one, out remaining);
            codeMap[top[0]] = 0b01000000;



            MaskDisplays('3', cipherPossibilities, one);
            ClearOutUsedEntry(cipherPossibilities, cipherPossibilities['3'][0], '3');

            String topLeft = MaskEntry(four, cipherPossibilities['3'][0]);
            //GetRemainingCharacters(four, cipherPossibilities['3'][0], out remaining);
            codeMap[topLeft[0]] = 0b00100000;

            String center = MaskEntry(four, one, topLeft);
            codeMap[center[0]] = 0b00001000;


            String zeroCipher = cipherPossibilities['0']
                .First(x => !x.Contains(center[0]));
            cipherPossibilities['0'].RemoveAll(x => x != zeroCipher);
            ClearOutUsedEntry(cipherPossibilities, zeroCipher, '0');


            String nineCipher = cipherPossibilities['9']
                .First(x => HasAllCharacters(x, one));
            cipherPossibilities['9'].RemoveAll(x => x != nineCipher);
            ClearOutUsedEntry(cipherPossibilities, nineCipher, '9');

            String bottomLeft = MaskEntry(eight, nineCipher);
            codeMap[bottomLeft[0]] = 0b00000100;


            String topRight = MaskEntry(eight, cipherPossibilities['6'][0]);
            codeMap[topRight[0]] = 0b00010000;

            String bottomRight = MaskEntry(one, topRight);
            codeMap[bottomRight[0]] = 0b00000010;

            String bottom = MaskEntry(eight, top, topLeft, topRight, center, bottomLeft, bottomRight);
            codeMap[bottom[0]] = 0b00000001;



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

        static void MaskDisplays(
            Char pointer,
            Dictionary<Char, List<String>> cipherPossibilities,
            String mask)
        {
            for (Int32 i = 0; i < cipherPossibilities[pointer].Count; ++i)
            {
                String entry = cipherPossibilities[pointer][i];
                bool missing = false;
                for (Int32 c = 0; c < mask.Length; ++c)
                {
                    if (!entry.Contains(mask[c]))
                    {
                        missing = true;
                        break;
                    }
                }
                if (missing)
                {

                    cipherPossibilities[pointer].RemoveAt(i);
                    i--;
                }
            }
        }

        static String MaskEntry(String input, params String[] masks)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for(Int32 i = 0; i < input.Length; ++i)
            {
                Boolean masked = false;
                foreach(String mask in masks)
                {
                    for (Int32 m = 0; m < mask.Length; ++m)
                    {
                        if (input[i] == mask[m])
                        {
                            masked = true;
                            break;
                        }
                    }
                }
                if (!masked)
                    stringBuilder.Append(input[i]);
            }

            return stringBuilder.ToString();
        }

        static void ClearOutUsedEntry(
            Dictionary<Char, List<String>> cipherPossibilities,
            String cipher,
            Char ignore)
        {
            foreach(var entry in cipherPossibilities)
            {
                if (entry.Key == ignore) continue;

                entry.Value.Remove(cipher);
            }
        }

        static void GetRemainingCharacters(String cipher1, String cipher2, out List<Char> remaining)
        {
            remaining = new List<char>();

            for (Int32 c1 = 0; c1 < cipher1.Length; c1++)
            {
                bool found = false;
                for (Int32 c2 = 0; c2 < cipher2.Length; c2++)
                {
                    if (cipher1[c1] == cipher2[c2])
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    remaining.Add(cipher1[c1]);
                }
            }
        }

        static bool HasAllCharacters(String cipher, String characters)
        {

            for (Int32 i = 0; i < characters.Length; ++i)
            {
                bool found = false;
                for(Int32 c = 0; c < cipher.Length; ++c)
                {
                    if(characters[i] == cipher[c])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return false;

            }

            return true;
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
