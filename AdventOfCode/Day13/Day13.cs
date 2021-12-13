using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day13
    {
        record Point
        {
            public int X { get; init; }
            public int Y { get; init; }
            public Point(int[] coords)
                => (X, Y) = (coords[0], coords[1]);
            public Point(int x, int y)
                => (X, Y) = (x, y);
        }

        public static void Part1And2()
        {
            List<string> lines = File.ReadAllLines(@"Day13/input.txt").ToList();

            List<string> folds = lines.Where(x => Regex.IsMatch(x, "[a-z]")).ToList();
            HashSet<Point> points = lines.Where(x => Regex.IsMatch(x, @"\d+,\d+")).Select(x => new Point(x.Split(',').Select(y => Int32.Parse(y)).ToArray())).ToHashSet();

            Console.WriteLine("Starting with {0} points.", points.Count());
            foreach (string fold in folds)
            {
                Match m = Regex.Match(fold, @"(\w)=(\d+)");
                string axis = m.Groups[1].ToString();
                int bXFold = Convert.ToInt32(axis == "x");
                int foldIdx = Int32.Parse(m.Groups[2].ToString());
                points = Fold(points, bXFold, foldIdx);
                Console.WriteLine("Folding at {0} = {1} results in {2} points", axis, foldIdx, points.Count);
            }
            PrintPoints(points);
        }

        private static void PrintPoints(HashSet<Point> points)
        {
            int yMin = points.Min(x => x.Y);
            int yMax = points.Max(x => x.Y);
            int xMin = points.Min(x => x.X);
            int xMax = points.Max(x => x.X);

            for (int y = yMin; y <= yMax; ++y)
            {
                string line = "";
                for (int x = xMin; x <= xMax; ++x)
                {
                    line += points.Contains(new(x, y)) ? '#' : '.';
                }
                Console.WriteLine(line);
            }
        }

        private static HashSet<Point> Fold(HashSet<Point> visiblePoints, int bXFold, int foldIdx)
        {
            HashSet<Point> newPoints = new HashSet<Point>();
            foreach (Point point in visiblePoints)
            {
                int[] p = new int[] { point.X, point.Y };
                int pIdx = 1 - bXFold;
                if (p[pIdx] == foldIdx)
                {
                    continue;
                }
                else if (p[pIdx] > foldIdx)
                {
                    p[pIdx] = 2 * foldIdx - p[pIdx];
                }
                newPoints.Add(new Point(p));
            }
            return newPoints;
        }
    }
}
