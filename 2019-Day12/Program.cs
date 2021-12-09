using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace _2019_Day12
{
    class Program
    {
        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "_2019_Day12";
            Console.WriteLine("Hello World!");

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1(10)}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2(100)}\n");
            Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1(1000)}\n");
            //Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2()}\n");

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_3()}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_4()}\n");
            //Console.WriteLine($"\tPuzzle 2 Result: {Puzzle_Case_2()}");

        }


        static UInt64 Test_Case_1(Int32 iterationCount)
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-1.txt");
            return Part_1_Logic(input, iterationCount);
        }
        static UInt64 Test_Case_2(Int32 iterationCount)
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-2.txt");
            return Part_1_Logic(input, iterationCount);
        }
        static UInt64 Test_Case_3()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-3.txt");
            return Part_2_Logic(input);
        }
        static UInt64 Test_Case_4()
        {
            String input = ReadEmbeddedResource($"{Namespace}.test-case-4.txt");
            return Part_2_Logic(input);
        }



        static UInt64 Puzzle_Case_1(Int32 iterationCount)
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-1.txt");

            return Part_1_Logic(input, iterationCount);
        }

        static UInt64 Puzzle_Case_2()
        {
            String input = ReadEmbeddedResource($"{Namespace}.puzzle-input-2.txt");

            return Part_2_Logic(input);
        }

        static UInt64 Part_1_Logic(String input, Int32 iterationCount, bool verbose = false)
        {
            String[] lines = input.Split(new char[] { '\n', });
            List<Moon> moons = new List<Moon>();
            for(Int32 i = 0; i < lines.Length; ++i)
            {
                Moon moon = new Moon();
                moon.Position = ParseVectorLine(in lines[i]);
                moon.Velocity = new Vector();
                moons.Add(moon);
            }

            for (Int32 iteration = 0; iteration < iterationCount; ++iteration)
            {
                ApplyGravity(moons);
                ApplyVelocity(moons);

                if (verbose)
                    PrintMoonData(moons);

                //ResetVelocity(moons);
            }

            UInt64 total = 0;
            for (Int32 i = 0; i < moons.Count; ++i)
            {
                UInt64 potentialEnergy = 0, kineticEnergy = 0;
                for(Int32 a = 0; a < 3; ++a)
                {
                    potentialEnergy += (UInt64)Math.Abs(moons[i].Position[a]);
                    kineticEnergy += (UInt64)Math.Abs(moons[i].Velocity[a]);
                }

                if (verbose)
                    Console.WriteLine($"Moon[{i}] Potential Energy: {potentialEnergy}, Kinetic Energy: {kineticEnergy}");
                total += potentialEnergy * kineticEnergy;
            }


            return total;
        }

        static UInt64 Part_2_Logic(String input)
        {
            String[] lines = input.Split(new char[] { '\n', });
            List<Moon> moons = new List<Moon>();
            for (Int32 i = 0; i < lines.Length; ++i)
            {
                Moon moon = new Moon();
                moon.Position = ParseVectorLine(in lines[i]);
                moon.Velocity = new Vector();
                moons.Add(moon);
            }

            return 0;
        }

       
        static Vector ParseVectorLine(in String line)
        {
            Int32[] entry = new int[3];
            String[] axisEntries = line.Split(new char[] { ',' });

            for (Int32 i = 0; i < axisEntries.Length; ++i)
            {
                axisEntries[i] = axisEntries[i].Replace("<", "").Replace(">", "").Trim();

                String[] equation = axisEntries[i].Split(new char[] { '=' });
                entry[i] = Convert.ToInt32(equation[1]);
            }



            return new Vector(in entry[0], in entry[1], in entry[2]);
        }

        static void ApplyGravity(List<Moon> moons)
        {
            for (Int32 p = 0; p < moons.Count - 1; ++p)
                for (Int32 j = p + 1; j < moons.Count; ++j)
                    for (Int32 a = 0; a < 3; ++a)
                    {
                        if (moons[p].Position[a] == moons[j].Position[a])
                            continue;

                        Int32 result = moons[j].Position[a] - moons[p].Position[a];
                        moons[p].Velocity[a] += Math.Sign(result);
                        moons[j].Velocity[a] += -Math.Sign(result);
                    }

        }
        static void ApplyVelocity(List<Moon> moons)
        {
            for (Int32 p = 0; p < moons.Count; ++p)
                for (Int32 a = 0; a < 3; ++a)
                {
                    moons[p].Position[a] += moons[p].Velocity[a];
                }
        }
        static void ResetVelocity(List<Moon> moons)
        {
            for (Int32 p = 0; p < moons.Count; ++p)
                for (Int32 a = 0; a < 3; ++a)
                {
                    moons[p].Velocity[a] = 0;
                }
        }

        static void PrintMoonData(List<Moon> moons)
        {
            for (Int32 p = 0; p < moons.Count; ++p)
            {
                Console.WriteLine($"Moon[{p}]Position<{moons[p].Position.X}, {moons[p].Position.Y}, {moons[p].Position.Z}> Velocity<{moons[p].Velocity.X}, {moons[p].Velocity.Y}, {moons[p].Velocity.Z}>");
            }
            Console.WriteLine();
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
