using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day4
    {
        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day4/test.txt").ToList();

            List<int> draws = lines.First().Split(',').Select(x => Int32.Parse(x)).ToList();
            lines.RemoveAt(0);

            List<int> tables = new List<int>();

            foreach (string line in lines)
            {
                tables.AddRange(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)));
            }

            HashSet<int> drawnNumbers = new HashSet<int>();

            int winningIndex = 0;
            int lastCalled = 0;
            foreach (int n in draws)
            {
                lastCalled = n;
                drawnNumbers.Add(lastCalled);
                if (HasATableWon(tables, drawnNumbers, out winningIndex))
                {
                    break;
                }
            }

            // Get winning result
            int initialVal = 25 * winningIndex;
            int sum = 0;
            for (int i = initialVal; i < initialVal + 25; ++i)
            {
                if (!drawnNumbers.Contains(tables[i]))
                {
                    sum += tables[i];
                }
            }

            Console.WriteLine("Day 4 Part 1: Table Index {0} wins with a score of {1} * {2} = {3}", winningIndex, sum, lastCalled, lastCalled * sum);
        }

        private static bool HasATableWon(List<int> tables, HashSet<int> drawnNumbers, out int winningIndex)
        {
            for (int i = 0; i < tables.Count; i += 25)
            {
                winningIndex = i / 25;
                // Check rows
                for (int j = i; j < i + 25; j += 25)
                {
                    for (int k = j; k < j + 5; ++k)
                    {
                        if (!drawnNumbers.Contains(tables[k]))
                        {
                            break;
                        }
                        else if (k == j + 4)
                        {
                            return true;
                        }
                    }
                }

                // Check columns
                for (int j = i; j < i + 5; ++j)
                {
                    for (int k = j; k < i + 25; k += 5)
                    {
                        if (!drawnNumbers.Contains(tables[k]))
                        {
                            break;
                        }
                        else if (k == k + 20)
                        {
                            return true;
                        }
                    }
                }
            }

            winningIndex = -1;
            return false;
        }
    }
}
