using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day14
    {
        public static void Solve()
        {
            Console.WriteLine("Part 1 (10 steps)");
            SolveForSteps(10);
            Console.WriteLine("Part 2 (40 steps)");
            SolveForSteps(40);
        }

        public static void SolveForSteps(int steps)
        {
            List<string> lines = File.ReadAllLines(@"Day14/input.txt").ToList();
            string initialState = lines.First();

            Dictionary<string, (string, string)> polyMap = lines.Skip(2).Select(x => x.Split("->", StringSplitOptions.TrimEntries)).ToDictionary(x => x[0], x => (x[0][0] + x[1], x[1] + x[0][1]));
            Dictionary<string, ulong> pairFrequency = new Dictionary<string, ulong>();
            foreach (string s in polyMap.Keys.Distinct())
            {
                pairFrequency.Add(s, 0);
            }
            for (int i = 0; i < initialState.Count() - 1; i++)
            {
                pairFrequency[initialState.Substring(i, 2)]++;
            }

            for (int i = 0; i < steps; i++)
            {
                Dictionary<string, ulong> newFrequency = new Dictionary<string, ulong>(pairFrequency);
                foreach (var entry in pairFrequency)
                {
                    (string, string) newPairs = polyMap[entry.Key];

                    newFrequency[newPairs.Item1] += entry.Value;
                    newFrequency[newPairs.Item2] += entry.Value;
                    newFrequency[entry.Key] -= entry.Value;
                }
                pairFrequency = newFrequency;
            }

            // Count frequencies
            Dictionary<char, ulong> occurences = new Dictionary<char, ulong>();
            foreach(var entry in pairFrequency)
            {
                occurences.TryAdd(entry.Key[0], 0);
                occurences.TryAdd(entry.Key[1], 0);
                occurences[entry.Key[0]] += entry.Value;
                occurences[entry.Key[1]] += entry.Value;
            }

            ulong max = (occurences.Values.Max() + 1) / 2;
            ulong min = (occurences.Values.Min() + 1) / 2;
            Console.WriteLine("Max {0} - Min {1} = {2}", max, min, max - min);
        }
    }
}
