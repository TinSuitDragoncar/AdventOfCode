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
            List<string> lines = File.ReadAllLines(@"Day4/input.txt").ToList();

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

            Console.WriteLine("Day 4 Part 1: Table Index {0} wins first with a score of {1} * {2} = {3}", winningIndex, sum, lastCalled, lastCalled * sum);
        }

        private static bool HasATableWon(List<int> tables, HashSet<int> drawnNumbers, out int tableIndex)
        {
            int rowCount = 0;
            int columnCount = 0;
            for (int i = 0; i < tables.Count; ++i)
            {
                tableIndex = i / 25;
                int columnIndex = i % 5;
                int rowIndex = (i % 25) / 5;

                if (columnIndex == 0)
                {
                    rowCount = 5;
                }

                if (rowIndex == 0 &&
                    columnIndex == 0)
                {
                    columnCount = 0b11111;
                }

                if (drawnNumbers.Contains(tables[i]))
                {
                    --rowCount;
                }
                else
                {
                    columnCount &= ~(1 << columnIndex);
                }

                if (rowCount == 0 ||
                    rowIndex == 4 &&
                   (columnCount & (1 << columnIndex)) > 0)
                {
                    return true;
                }
            }

            tableIndex = -1;
            return false;
        }

        public static void Part2()
        {
            List<string> lines = File.ReadAllLines(@"Day4/input.txt").ToList();

            List<int> draws = lines.First().Split(',').Select(x => Int32.Parse(x)).ToList();
            lines.RemoveAt(0);

            List<int> tables = new List<int>();

            foreach (string line in lines)
            {
                tables.AddRange(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)));
            }

            HashSet<int> drawnNumbers = new HashSet<int>();
            HashSet<int> drawnNumbersSnapShot = new HashSet<int>();
            HashSet<int> winningTables = new HashSet<int>();

            int lastWinningTable = 0;
            int lastCalled = 0;

            foreach (int n in draws)
            {
                drawnNumbers.Add(n);
                if (HasATableWon2(tables, drawnNumbers, winningTables, out int temp))
                {
                    drawnNumbersSnapShot = new HashSet<int>(drawnNumbers);
                    lastWinningTable = temp;
                    lastCalled = n;
                }
                if (winningTables.Count == tables.Count / 25)
                {
                    break;
                }
            }

            // Get winning result
            int initialVal = 25 * lastWinningTable;
            int sum = 0;
            for (int i = initialVal; i < initialVal + 25; ++i)
            {
                if (!drawnNumbersSnapShot.Contains(tables[i]))
                {
                    sum += tables[i];
                }
            }

            Console.WriteLine("Day 4 Part 2: Table Index {0} wins last with a score of {1} * {2} = {3}", lastWinningTable, sum, lastCalled, lastCalled * sum);
        }

        private static bool HasATableWon2(List<int> tables, HashSet<int> drawnNumbers, HashSet<int> winningTables, out int lastWinIndex)
        {
            int rowCount = 0;
            int columnCount = 0;
            bool bTableWon = false;
            lastWinIndex = -1;
            int currentIndex = 0;
            for (int i = 0; i < tables.Count; ++i)
            {
                currentIndex = i / 25;
                if (winningTables.Contains(currentIndex))
                {
                    continue;
                }
                int columnIndex = i % 5;
                int rowIndex = (i % 25) / 5;

                if (columnIndex == 0)
                {
                    rowCount = 5;
                }

                if (rowIndex == 0 &&
                    columnIndex == 0)
                {
                    columnCount = 0b11111;
                }

                if (drawnNumbers.Contains(tables[i]))
                {
                    --rowCount;
                }
                else
                {
                    columnCount &= ~(1 << columnIndex);
                }

                if (rowCount == 0 ||
                    rowIndex == 4 &&
                   (columnCount & (1 << columnIndex)) > 0)
                {
                    lastWinIndex = currentIndex;
                    winningTables.Add(currentIndex);
                    bTableWon = true;
                }
            }

            return bTableWon;
        }
    }
}
