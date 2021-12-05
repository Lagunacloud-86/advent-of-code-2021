using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day05
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day05";
            Console.WriteLine("Hello World!");

            //Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1()}\n");
            //Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2()}\n");
            Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2()}");

        }


        static Int32 Test_Case_1()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-1.txt");
            return Part_1_Logic(input);
        }
        static Int32 Puzzle_Case_1()
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-1.txt");

            return Part_1_Logic(input);
        }

        static Int32 Test_Case_2()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-2.txt");

            return Part_2_Logic(input);
        }
        static Int32 Puzzle_Case_2()
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-2.txt");

            return Part_2_Logic(input);
        }

        static Int32 Part_1_Logic(String input)
        {
            ReadOnlySpan<char> spanInput = input;
            Int32 startIndex = 0;
            List<Line> lines = new List<Line>();
            while (ReadNextHyrdoVentLine(in spanInput, in startIndex, out Int32 endIndex))
            {
                lines.Add(ParseLine(spanInput.Slice(startIndex, endIndex - startIndex)));
                startIndex = endIndex;

                while (startIndex < spanInput.Length && (spanInput[startIndex] == '\n' || spanInput[startIndex] == '\r'))
                    startIndex++;
            }

            Dictionary<PointInfo, Int32> points = new Dictionary<PointInfo, Int32>();
            for (Int32 lineIndex = 0; lineIndex < lines.Count; ++lineIndex)
            {
                Line line = lines[lineIndex];
                if (line.Direction.X == 0 || line.Direction.Y == 0)
                {
                    Int32 x = line.Point1.X + -Math.Sign(line.Direction.X);
                    Int32 y = line.Point1.Y + -Math.Sign(line.Direction.Y);
                    do
                    {
                        x += Math.Sign(line.Direction.X);
                        y += Math.Sign(line.Direction.Y);

                        PointInfo point = new PointInfo(in x, in y);
                        if (!points.TryGetValue(point, out Int32 value))
                            points.Add(point, 0);
                        value++;
                        points[point] = value;



                    } while (!line.Point2.Equals(in x, in y));
                }
             
            }


            return points.Count(x => x.Value >= 2);
        }
        static Int32 Part_2_Logic(String input)
        {
            ReadOnlySpan<char> spanInput = input;
            Int32 startIndex = 0;
            List<Line> lines = new List<Line>();
            while (ReadNextHyrdoVentLine(in spanInput, in startIndex, out Int32 endIndex))
            {
                lines.Add(ParseLine(spanInput.Slice(startIndex, endIndex - startIndex)));
                startIndex = endIndex;

                while (startIndex < spanInput.Length && (spanInput[startIndex] == '\n' || spanInput[startIndex] == '\r'))
                    startIndex++;
            }

            Dictionary<PointInfo, Int32> points = new Dictionary<PointInfo, Int32>();
            for (Int32 lineIndex = 0; lineIndex < lines.Count; ++lineIndex)
            {
                Line line = lines[lineIndex];
                Int32 x = line.Point1.X + -Math.Sign(line.Direction.X);
                Int32 y = line.Point1.Y + -Math.Sign(line.Direction.Y);
                do
                {
                    x += Math.Sign(line.Direction.X);
                    y += Math.Sign(line.Direction.Y);

                    PointInfo point = new PointInfo(in x, in y);
                    if (!points.TryGetValue(point, out Int32 value))
                        points.Add(point, 0);
                    value++;
                    points[point] = value;



                } while (!line.Point2.Equals(in x, in y));

            }


            return points.Count(x => x.Value >= 2);
        }


        static Boolean ReadNextHyrdoVentLine(in ReadOnlySpan<char> input, in Int32 startIndex, out Int32 endIndex)
        {
            endIndex = -1;

            if (startIndex >= input.Length)
                return false;

            Int32 i = 0;
            for (i = startIndex; i < input.Length; ++i)
            {
                if (input[i] == '\n')
                {
                    endIndex = i + 1;
                    return true;
                }
                
            }

            endIndex = i;
            return true;
        }
     
        static Line ParseLine(in ReadOnlySpan<char> lineInput)
        {
            Int32 x1 = 0, x2 = 0, y1 = 0, y2 = 0;

            ReadOnlySpan<char> lineSeparator = "->";

            for(Int32 i = 0; i < lineInput.Length - 1; ++i)
            {
                ReadOnlySpan<char> section = lineInput.Slice(i, 2);
                if (section.SequenceEqual(lineSeparator))
                {
                    ReadOnlySpan<char> left = lineInput.Slice(0, i - 1);
                    ReadOnlySpan<char> right = lineInput.Slice(i + 3);
                    ParseVector(in left, out x1, out y1);
                    ParseVector(in right, out x2, out y2);
                    break;
                }
            }

            return new Line(in x1, in y1, in x2, in y2);
        }
        static void ParseVector(in ReadOnlySpan<char> vectorInput, out Int32 x, out Int32 y)
        {
            for(Int32 i = 0; i < vectorInput.Length; ++i)
            {
                if (vectorInput[i] == ',')
                {
                    ReadOnlySpan<char> xInput = vectorInput.Slice(0, i);
                    ReadOnlySpan<char> yInput = vectorInput.Slice(i + 1);

                    x = Int32.Parse(xInput.ToString().Trim());
                    y = Int32.Parse(yInput.ToString().Trim());

                    return;
                }
            }

            x = -1;
            y = -1;


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
