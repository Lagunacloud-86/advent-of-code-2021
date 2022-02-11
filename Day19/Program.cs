using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day19
{
    class Program
    {

        static List<Func<Point, Point>> UniqueRotations { get; }

        static Program()
        {
            UniqueRotations = new List<Func<Point, Point>>
            {
                //No rotation
                (p) => p,

                //X axis
                (p) => p.Rotate90L(EAxis.X),
                (p) => p.Rotate90R(EAxis.X),
                (p) => p.Rotate180(EAxis.X),
                
                //Y axis
                (p) => p.Rotate90L(EAxis.Y),
                (p) => p.Rotate90R(EAxis.Y),
                (p) => p.Rotate180(EAxis.Y),
                
                //Z axis
                (p) => p.Rotate90L(EAxis.Z),
                (p) => p.Rotate90R(EAxis.Z),
                (p) => p.Rotate180(EAxis.Z),

                //Extra Y axis
                (p) => p.Rotate90L(EAxis.X).Rotate90L(EAxis.Y),
                (p) => p.Rotate90L(EAxis.X).Rotate90R(EAxis.Y),
                (p) => p.Rotate90L(EAxis.X).Rotate180(EAxis.Y),
                (p) => p.Rotate90R(EAxis.X).Rotate90L(EAxis.Y),
                (p) => p.Rotate90R(EAxis.X).Rotate90R(EAxis.Y),
                (p) => p.Rotate90R(EAxis.X).Rotate180(EAxis.Y),
                (p) => p.Rotate180(EAxis.X).Rotate90L(EAxis.Y),
                (p) => p.Rotate180(EAxis.X).Rotate90R(EAxis.Y),
                (p) => p.Rotate180(EAxis.X).Rotate180(EAxis.Y),
                
                //Extra Z axis
                (p) => p.Rotate90L(EAxis.X).Rotate90L(EAxis.Z),
                (p) => p.Rotate90L(EAxis.X).Rotate90R(EAxis.Z),
                (p) => p.Rotate90L(EAxis.X).Rotate180(EAxis.Z),
                (p) => p.Rotate90R(EAxis.X).Rotate90L(EAxis.Z),
                (p) => p.Rotate90R(EAxis.X).Rotate90R(EAxis.Z),
                (p) => p.Rotate90R(EAxis.X).Rotate180(EAxis.Z),
                (p) => p.Rotate180(EAxis.X).Rotate90L(EAxis.Z),
                (p) => p.Rotate180(EAxis.X).Rotate90R(EAxis.Z),
                (p) => p.Rotate180(EAxis.X).Rotate180(EAxis.Z),
            };

            
        }


        static String Namespace { get; set; }

        static void Main(string[] args)
        {
            Namespace = "Day19";

            Console.WriteLine($"\tTest Case 1 Result: {Test_Case_1(false)}\n");
            //Console.WriteLine($"\tPuzzle 1 Result: {Puzzle_Case_1(false)}\n");
            Console.WriteLine($"\tTest Case 2 Result: {Test_Case_2(false)}\n");
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
            Dictionary<Int32, Scan> beaconScanData = BuildScannerData(input);

            if (verbose)
            {
                Console.WriteLine($"Scanners: {beaconScanData.Count}");
                for (Int32 i = 0; i < beaconScanData.Count; ++i)
                {
                    Console.WriteLine($"\tScanner: {i}");
                    foreach (Beacon beacon in beaconScanData[i].Beacons)
                    {

                        Console.WriteLine($"\t\tPosition: {beacon.WorldPosition.X}, {beacon.WorldPosition.Y}, {beacon.WorldPosition.Z}");
                    }
                }
            }


            //Step 1: Build list of known beacons
            List<Beacon> known = new List<Beacon>();
            known.AddRange(beaconScanData[0].Beacons);

            //Now compare other scanners and find comparisons against known
            List<Int32> indexesToSearch = Enumerable.Range(1, beaconScanData.Count - 1).ToList();
            while(indexesToSearch.Count > 0)
            {
                for (Int32 i = 0; i < indexesToSearch.Count; ++i)
                {
                    if (CompareScanAgainstKnown(beaconScanData[indexesToSearch[i]], known))
                    {
                        indexesToSearch.RemoveAt(i);
                        i--;
                    }
                }
            }


            var distinct = known
                .Select(x => x.WorldPosition)
                .Distinct()
                .ToList();
            foreach (var d in distinct.OrderBy(x => x.X))
            {
                Console.WriteLine($"x:{d.X}, y:{d.Y}, z:{d.Z}");
            }


            return (UInt64)distinct.Count();
        }

        static UInt64 Part_2_Logic(String input, bool verbose = false)
        {
            Dictionary<Int32, Scan> beaconScanData = BuildScannerData(input);


            //Step 1: Build list of known beacons
            List<Beacon> known = new List<Beacon>();
            known.AddRange(beaconScanData[0].Beacons);

            //Now compare other scanners and find comparisons against known
            List<Int32> indexesToSearch = Enumerable.Range(1, beaconScanData.Count - 1).ToList();
            while (indexesToSearch.Count > 0)
            {
                for (Int32 i = 0; i < indexesToSearch.Count; ++i)
                {
                    if (CompareScanAgainstKnown(beaconScanData[indexesToSearch[i]], known))
                    {
                        indexesToSearch.RemoveAt(i);
                        i--;
                    }
                }
            }

            Int64 biggestDistance = Int64.MinValue;
            Int32 resultA, resultB;
            for(Int32 i = 0; i < beaconScanData.Count - 1; ++i)
            {
                for (Int32 j = i + 1; j < beaconScanData.Count; ++j)
                {
                    resultA = Point.ManhattanDistance(
                        beaconScanData[i].Scanner.Position,
                        beaconScanData[j].Scanner.Position);

                    resultB = Point.ManhattanDistance(
                        beaconScanData[j].Scanner.Position,
                        beaconScanData[i].Scanner.Position);

                    if (biggestDistance < resultA)
                        biggestDistance = resultA;

                    if (biggestDistance < resultB)
                        biggestDistance = resultB;

                }
            }


            return (UInt64)biggestDistance;
        }


        static Dictionary<Int32, Scan> BuildScannerData(in String input)
        {
            String[] lines = input
                .Split(new char[] { '\n' });
            Dictionary<Int32, Scan> data = new Dictionary<int, Scan>();
            Scan current = null;
            Int32 scanner;
            for(Int32 i = 0; i < lines.Length; ++i)
            {
                if (String.IsNullOrWhiteSpace(lines[i]))
                    continue;

                if (lines[i].Contains("scanner"))
                {
                    int startIndex = lines[i].IndexOf("scanner") + 8;
                    int spaceIndex = lines[i].IndexOf(" ", startIndex);
                    scanner = Convert.ToInt32(lines[i].Substring(startIndex, spaceIndex - startIndex).Trim());
                    current = new Scan();
                    current.Scanner = new Scanner(lines[i], 0, 0, 0);
                    data.Add(scanner, current);
                }
                else
                {
                    String[] axes = lines[i].Split(new char[] { ',' });
                    Int32 x, y, z;
                    x = Convert.ToInt32(axes[0].Trim());
                    y = Convert.ToInt32(axes[1].Trim());
                    z = Convert.ToInt32(axes[2].Trim());
                    current.Beacons.Add(new Beacon(in x, in y, in z));
                }


            }


            return data;
        }

        static Boolean CompareScanAgainstKnown(Scan scan, List<Beacon> known)
        {
            //Int32 result = -1;
            //List<Beacon> outputBeacons = new List<Beacon>();

            //bool found = false;
            for (Int32 beaconIndex = 0; beaconIndex < scan.Beacons.Count; ++beaconIndex) //each (var b in scan.Beacons)
            {
                if (FindCommonPoints(known, scan, in beaconIndex, out List<Beacon> rotatedPoints))
                {
                    scan.Scanner.Position = -rotatedPoints[0].RelativePosition + rotatedPoints[0].WorldPosition;
                    //add uniques to known
                    known.AddRange(rotatedPoints.Select(x => new Beacon(x.WorldPosition.X, x.WorldPosition.Y, x.WorldPosition.Z)));

                    return true;
                }

            }

            return false;
        }


        static bool FindCommonPoints(
            in List<Beacon> known,
            in Scan scan,
            in Int32 originBeacon,
            out List<Beacon> rotatedBeacons)
        {
            rotatedBeacons = new List<Beacon>(50);
            foreach (Beacon k in known)
            {
                foreach (Func<Point, Point> rotationFunction in UniqueRotations)
                {
                    rotatedBeacons.Clear();
                    for (int b = 0; b < scan.Beacons.Count; ++b)
                    {
                        Point rotatedPoint = rotationFunction
                            .Invoke(scan.Beacons[b].WorldPosition - scan.Beacons[originBeacon].WorldPosition) + k.WorldPosition;
                        Beacon beacon = new (rotatedPoint.X, rotatedPoint.Y, rotatedPoint.Z);
                        beacon.RelativePosition = scan.Beacons[b].RelativePosition;
                        rotatedBeacons.Add(beacon);
                    }

                    Int32 count = known
                        .Select(x => x.WorldPosition)
                        .Intersect(rotatedBeacons.Select(x => x.WorldPosition))
                        .Count();

                    if (count >= 12)
                        return true;

                }

            }

            return false;
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
