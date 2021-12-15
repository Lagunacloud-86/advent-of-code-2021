using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            Dictionary<String, Char> insertRules = new Dictionary<String, Char>();
            Dictionary<Char, UInt64> counter = new Dictionary<Char, UInt64>();

            for (Int32 i = 2; i < lines.Length; ++i)
            {
                String[] ruleData = lines[i].Split("->");
                String rule = ruleData[0].Trim();
                insertRules.Add(rule, ruleData[1].Trim()[0]);

            }

            FindPairsWithDepth(
                insertRules,
                counter,
                lines[0].Trim(), 40);

            foreach (var c in counter)
                Console.WriteLine($"'{c.Key}': {c.Value}");

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
            Dictionary<String, Char> insertRules,
            Dictionary<Char, UInt64> counter,
            String polymer,
            Int32 depth)
        {
            ReadOnlySpan<Char> polymerSpan = polymer;

                Dictionary<String, UInt64> newPairs =
                    new Dictionary<string, ulong>();
            Dictionary<String, UInt64> pairs = new Dictionary<string, ulong>();

            for (Int32 i = 0; i < polymer.Length - 1; ++i)
            {
                string key = polymerSpan.Slice(i, 2).ToString();
                if (!pairs.ContainsKey(key))
                    pairs.Add(key, 0);
                pairs[key]++;

            }
            for (Int32 i = 0; i < polymer.Length; ++i)
            {
                if (!counter.ContainsKey(polymer[i]))
                    counter.Add(polymer[i], 0);
                counter[polymer[i]]++;
            }




            for (Int32 i = 0; i < depth; ++i)
            {

                foreach (var pair in pairs)
                {
                    String entry1 = String.Concat(pair.Key[0], insertRules[pair.Key]);
                    String entry2 = String.Concat(insertRules[pair.Key], pair.Key[1]);

                    if (!counter.ContainsKey(insertRules[pair.Key]))
                        counter.Add(insertRules[pair.Key], 0);

                    counter[insertRules[pair.Key]] += pair.Value;


                    if (!newPairs.ContainsKey(entry1))
                        newPairs.Add(entry1, 0);

                    if (!newPairs.ContainsKey(entry2))
                        newPairs.Add(entry2, 0);

                    newPairs[entry1] += pair.Value;
                    newPairs[entry2] += pair.Value;

                }
                pairs.Clear();
                foreach (var np in newPairs)
                    pairs.Add(np.Key, np.Value);
                newPairs.Clear();


            }


        }
        
        //static Dictionary<Char, UInt64> IteratePair(
        //    Dictionary<UInt16, Char> insertRules, 
        //    Char left, Char right,
        //    Int32 depth)
        //{
        //    Console.WriteLine($"Starting {depth} iterations for '{left}{right}'...");

        //    Dictionary<Char, UInt64> counter = new Dictionary<char, ulong>();
        //    foreach(var ir in insertRules.Select(x => x.Value).Distinct())
        //        counter.Add(ir, 0);


        //    Stack<Node> nodes = new Stack<Node>();
        //    nodes.Push(new Node(0, in left, in right));

        //    while(nodes.Count > 0)
        //    {
        //        Node node = nodes.Pop();

        //        UInt16 code = (UInt16)((UInt16)(node.Left << 8) | (UInt16)node.Right);
        //        char insert = insertRules[code];
        //        counter[insert]++;

        //        if (node.Depth + 1 < depth)
        //        {
        //            nodes.Push(new Node(node.Depth + 1, node.Left, insert));
        //            nodes.Push(new Node(node.Depth + 1, insert, node.Right));
        //        }
        //    }

        //    Console.WriteLine($"Ending {depth} iterations for '{left}{right}'...");

        //    return counter;
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
