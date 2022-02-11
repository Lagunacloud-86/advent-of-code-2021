using System;
using System.IO;
using System.Reflection;

namespace Day15
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day15";

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1(true)}\n");
            Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            //Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2(true)}\n");
            //Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2()}");

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
            RiskMap riskMap = new RiskMap(input);
            Path path = Path.MapPath(
                riskMap, 
                new PointInfo(0, 0),
                new PointInfo(riskMap.Width - 1, riskMap.Height - 1)
            );

            UInt64 riskTotal = 0UL;
            for (Int32 i = 1; i < path.PathPoints.Length; ++i) 
                riskTotal += riskMap[path.PathPoints[i].X, path.PathPoints[i].Y];




            return riskTotal;
        }

        static UInt64 Part_2_Logic(String input, bool verbose = false)
        {
            return 0UL;
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
