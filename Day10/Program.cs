using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Day10
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day10";

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
            String[] entries = input
                .Split(new char[] { '\n' });

            List<ChunkTree> chunkTrees = new List<ChunkTree>();
            foreach (String entry in entries)
            {
                ChunkTree chunkTree = new ChunkTree(in entry);
                chunkTrees.Add(chunkTree);
            }

            UInt64 total = 0;
            foreach (ChunkTree entry in chunkTrees)
            {
                if(entry.FindWrongCloseBracket(out char bracket))
                {
                    switch (bracket)
                    {
                        case ')': total += 3UL; break;
                        case ']': total += 57UL; break;
                        case '}': total += 1197UL; break;
                        case '>': total += 25137UL; break;
                    }
                }

            }


            return total;
        }

        static UInt64 Part_2_Logic(String input)
        {
            String[] entries = input
              .Split(new char[] { '\n' });

            List<ChunkTree> chunkTrees = new List<ChunkTree>();
            foreach (String entry in entries)
            {
                ChunkTree chunkTree = new ChunkTree(in entry);
                chunkTrees.Add(chunkTree);
            }

            Dictionary<Char, UInt64> _scoreLookup = new Dictionary<char, ulong>
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 }
            };
            List<UInt64> chunkTotals = new List<ulong>();
            foreach (ChunkTree entry in chunkTrees)
            {
                if (!entry.FindWrongCloseBracket(out char bracket))
                {
                    String autoCompleteString = entry
                        .AutocompleteSyntaxClosers();

                    UInt64 total = 0;
                    for(Int32 i = 0; i < autoCompleteString.Length; ++i)
                    {
                        total = (total * 5) + _scoreLookup[autoCompleteString[i]];
                    }
                    chunkTotals.Add(total);
                }

            }
            chunkTotals.Sort();


            return chunkTotals[chunkTotals.Count / 2];

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
