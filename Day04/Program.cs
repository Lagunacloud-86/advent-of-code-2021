using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Day04
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day04";
            Console.WriteLine("Hello World!");

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1()}\n");
            Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1()}\n");
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
            List<BingoBoard> bingoBoards =
                new List<BingoBoard>();

            String[] rows = input.Split("\n");

            String numberString = rows[0].Trim();
            String[] numberSplit = numberString.Split(",");
            Int32[] numbers = new Int32[numberSplit.Length];

            for (Int32 i = 0; i < numberSplit.Length; ++i)
                numbers[i] = Convert.ToInt32(numberSplit[i].Trim());

            for (Int32 i = 1; i < rows.Length; ++i)
            {
                if (String.IsNullOrWhiteSpace(rows[i].Trim()))
                    continue;

                Int32 endIndex = i + 5;
                Int32 startIndex = i;
                BingoBoard bingoBoard = new BingoBoard();
                for (; i < endIndex; ++i)
                {
                    String[] lineEntries = rows[i]
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (Int32 j = 0; j < 5; j++)
                    {
                        Int32 number = Convert.ToInt32(lineEntries[j].Trim());
                        bingoBoard
                            .SetBoard(i - startIndex, j, number);
                    }
                }


                bingoBoards.Add(bingoBoard);
            }

            List<Int32> boardsWithBingo = new List<int>();
            Int32 lastNumber = 0;
            for (Int32 i = 0; i < numbers.Length; ++i)
            {
                lastNumber = numbers[i];
                for (Int32 mb = 0; mb < bingoBoards.Count; ++mb)
                {
                    bingoBoards[mb]
                        .Mark(numbers[i]);

                    if (bingoBoards[mb].HasBingo(false))
                        boardsWithBingo.Add(mb);

                }
                if (boardsWithBingo.Count > 0)
                    break;

            }

            if (boardsWithBingo.Count > 0)
            {
                Int32 result = 0;
                for (Int32 column = 0; column < 5; ++column)
                {
                    for (Int32 row = 0; row < 5; ++row)
                    {
                        if (!bingoBoards[boardsWithBingo[0]].Marks[column, row])
                        {
                            result += bingoBoards[boardsWithBingo[0]].Board[column, row];
                        }
                    }
                }

                return result * lastNumber;
            }



            return 0;
        }
        static Int32 Part_2_Logic(String input)
        {
            List<BingoBoard> bingoBoards =
                new List<BingoBoard>();

            String[] rows = input.Split("\n");

            String numberString = rows[0].Trim();
            String[] numberSplit = numberString.Split(",");
            Int32[] numbers = new Int32[numberSplit.Length];

            for (Int32 i = 0; i < numberSplit.Length; ++i)
                numbers[i] = Convert.ToInt32(numberSplit[i].Trim());

            for (Int32 i = 1; i < rows.Length; ++i)
            {
                if (String.IsNullOrWhiteSpace(rows[i].Trim()))
                    continue;

                Int32 endIndex = i + 5;
                Int32 startIndex = i;
                BingoBoard bingoBoard = new BingoBoard();
                for (; i < endIndex; ++i)
                {
                    String[] lineEntries = rows[i]
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (Int32 j = 0; j < 5; j++)
                    {
                        Int32 number = Convert.ToInt32(lineEntries[j].Trim());
                        bingoBoard
                            .SetBoard(i - startIndex, j, number);
                    }
                }


                bingoBoards.Add(bingoBoard);
            }

            List<Int32> boardsWithBingo = new List<int>();
            Int32 lastNumber = 0;
            for (Int32 i = 0; i < numbers.Length; ++i)
            {
                Boolean allDone = true;
                for (Int32 mb = 0; mb < bingoBoards.Count; ++mb)
                {
                    if (bingoBoards[mb].HasBingo(false))
                        continue;
                    allDone = false;

                    bingoBoards[mb]
                        .Mark(numbers[i]);

                    if (bingoBoards[mb].HasBingo(false))
                        boardsWithBingo.Add(mb);

                }
                if (allDone)
                    break;
                lastNumber = numbers[i];
            }

            Int32 result = 0;
            BingoBoard last = bingoBoards[boardsWithBingo[boardsWithBingo.Count - 1]];
            for (Int32 column = 0; column < 5; ++column)
            {
                for (Int32 row = 0; row < 5; ++row)
                {
                    if (!last.Marks[column, row])
                    {
                        result += last.Board[column, row];
                    }
                }
            }

            return result * lastNumber;
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
