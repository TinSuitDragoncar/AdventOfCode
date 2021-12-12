using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day12
    {
        public static void Part1And2()
        {
            List<string> lines = File.ReadAllLines(@"Day12/input.txt").ToList();

            Dictionary<string, HashSet<string>> caveMap = new Dictionary<string, HashSet<string>>();

            foreach (string l in lines)
            {
                string[] caves = l.Split('-');

                string startCave = caves[0];
                string endCave = caves[1];

                if (endCave != "start" && startCave != "end")
                {
                    if (!caveMap.ContainsKey(startCave))
                    {
                        caveMap.Add(startCave, new HashSet<string>());
                    }
                    caveMap[startCave].Add(endCave);
                }

                if (startCave != "start" && endCave != "end")
                {
                    if (!caveMap.ContainsKey(endCave))
                    {
                        caveMap.Add(endCave, new HashSet<string>());
                    }
                    caveMap[endCave].Add(startCave);
                }
            }

            HashSet<string> visitedCaves = new HashSet<string>();
            Console.WriteLine("Day 12 Part 1: {0}", DepthFirstSearch("start", caveMap, visitedCaves, false));
            visitedCaves.Clear();
            Console.WriteLine("Day 12 Part 2: {0}", DepthFirstSearch("start", caveMap, visitedCaves, true));
            
        }

        private static int DepthFirstSearch(string current, Dictionary<string, HashSet<string>> caveMap, HashSet<string> visitedCaves, bool bAllowTwice)
        {
            if (current == "end")
            {
                return 1;
            }
            else if (caveMap[current] == null)
            {
                return 0;
            }
            else if (Regex.IsMatch(current, "[a-z]") &&
                    visitedCaves.Contains(current))
            {
                if (bAllowTwice)
                {
                    bAllowTwice = false;
                }
                else
                {
                    return 0;
                }
            }
            int pathCount = 0;

            visitedCaves.Add(current);
            foreach (string path in caveMap[current])
            {
                pathCount += DepthFirstSearch(path, caveMap, new HashSet<string>(visitedCaves), bAllowTwice);
            }
            return pathCount;
        }
    }
}
