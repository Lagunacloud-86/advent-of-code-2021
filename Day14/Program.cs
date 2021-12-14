using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Day14
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
            Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2(true)}");

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
            String[] lines = input.Split(new char[] { '\n' });
            Dictionary<String, String> insertRules = new Dictionary<string, string>();
            StringBuilder polymer = new StringBuilder();
            polymer.Append(lines[0].Trim());
            for(Int32 i = 2; i < lines.Length; ++i)
            {
                String[] rule = lines[i].Split("->");
                insertRules.Add(rule[0].Trim(), rule[1].Trim());
            }


            if (verbose)
                Console.WriteLine(polymer.ToString());
            for (Int32 i = 0; i < 10; ++i)
            {
                FindPairs(insertRules, polymer);
                //if (verbose)
                //    Console.WriteLine(polymer.ToString());
            }

            IEnumerable<Char> result = polymer
                .ToString();
            var groupingResult = result
                .GroupBy(x => x)
                .Select(x => new
                {
                    Value = x,
                    Count = x.Count()
                });


            UInt64 max = (UInt64)groupingResult.Max(x => x.Count);
            UInt64 min = (UInt64)groupingResult.Min(x => x.Count);

            return max - min;
        }

        static UInt64 Part_2_Logic(String input, bool verbose = false)
        {
            String[] lines = input.Split(new char[] { '\n' });
            Dictionary<UInt16, Char> insertRules = new Dictionary<UInt16, Char>();
            Dictionary<Char, UInt64> counter = new Dictionary<Char, UInt64>();

            for (Int32 i = 0; i < lines[0].Trim().Length; ++i)
                if (!counter.ContainsKey(lines[0].Trim()[i]))
                    counter.Add(lines[0].Trim()[i], 0);

            for (Int32 i = 2; i < lines.Length; ++i)
            {
                String[] ruleData = lines[i].Split("->");
                String rule = ruleData[0].Trim();
                UInt16 code = (UInt16)((UInt16)(rule[0] << 8) | (UInt16)rule[1]);
                insertRules.Add(code, ruleData[1].Trim()[0]);

                if (!counter.ContainsKey(rule[1].ToString()[0]))
                    counter.Add(rule[1].ToString()[0], 0);
            }


            FindPairsWithDepth(
                insertRules,
                counter,
                lines[0].Trim(), 40);

            UInt64 max = (UInt64)counter.Max(x => x.Value);
            UInt64 min = (UInt64)counter.Min(x => x.Value);

            return max - min;
        }


        static void FindPairs(Dictionary<String, String> insertRules, StringBuilder stringBuilder)
        {
            Dictionary<Int32, char> pairs = new Dictionary<Int32, char>();
            for(Int32 i = 0; i < stringBuilder.Length - 1; ++i)
            {
                foreach(var rule in insertRules)
                {

                    if(stringBuilder[i + 0] == rule.Key[0] && stringBuilder[i + 1] == rule.Key[1])
                    {
                        pairs.Add(i + 1, rule.Value[0]);
                        break;
                    }
                }
            }

            foreach(var pair in pairs.OrderByDescending(x => x.Key))
            {
                stringBuilder.Insert(pair.Key, pair.Value);
            }
            
        }

        static void FindPairsWithDepth(
            Dictionary<UInt16, Char> insertRules,
            Dictionary<Char, UInt64> counter,
            String polymer,
            Int32 depth)
        {
            Stack<Node> nodes = new Stack<Node>();
            for (Int32 i = 0; i < polymer.Length - 1; ++i)
            {
                nodes.Push(new Node(in i, polymer[i], polymer[i + 1]));
                while(nodes.Count > 0)
                {
                    Node node = nodes.Pop();

                    counter[node.Left]++;
                    counter[node.Right]++;

                    UInt16 code = (UInt16)((UInt16)(node.Left << 8) | (UInt16)node.Right);
                    char insert = insertRules[code];


                    if (node.Depth <= depth)
                    {
                        nodes.Push(new Node(node.Depth + 1, node.Left, insert));
                        nodes.Push(new Node(node.Depth + 1, insert, node.Right));
                    }
                    
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
