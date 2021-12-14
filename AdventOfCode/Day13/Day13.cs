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
        public static void Part1And2()
        {
            List<string> lines = File.ReadAllLines(@"Day13/input.txt").ToList();

            List<string> folds = lines.Where(x => Regex.IsMatch(x, "[a-z]")).ToList();
            HashSet<(int X, int Y)> points = lines.Where(x => Regex.IsMatch(x, @"\d+,\d+")).Select(x => x.Split(',').Select(y => Int32.Parse(y)).ToArray()).Select(x => (x[0], x[1])).ToHashSet();

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
        private static void PrintPoints(HashSet<(int X, int Y)> points)
        {
            for (int y = points.Min(x => x.Y); y <= points.Max(x => x.Y); ++y)
            {
                string line = "";
                for (int x = points.Min(x => x.X); x <= points.Max(x => x.X); ++x)
                {
                    line += points.Contains((x, y)) ? '#' : '.';
                }
                Console.WriteLine(line);
            }
        }
        private static HashSet<(int X, int Y)> Fold(HashSet<(int X, int Y)> visiblePoints, int bXFold, int foldIdx)
        {
            HashSet<(int X, int Y)> newPoints = new HashSet<(int X, int Y)>();
            foreach ((int X, int Y) point in visiblePoints)
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
                newPoints.Add((p[0], p[1]));
            }
            return newPoints;
        }
    }
}
