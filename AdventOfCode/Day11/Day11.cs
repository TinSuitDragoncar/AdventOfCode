using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day11
    {
        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day11/input.txt").ToList();

            int width = lines.First().Length;

            List<int> input = lines.SelectMany(x => x.ToArray()).Select(x => Int32.Parse(x.ToString())).ToList();

            int steps = 100;
            HashSet<int> flashedIndices = new HashSet<int>();
            int flashCount = 0;
            for (int i = 0; i < steps; ++i)
            {
                flashedIndices.Clear();
                for (int j = 0; j < input.Count; ++j)
                {
                    flashCount += Step(input, flashedIndices, j, width);
                }
            }

            Console.WriteLine("Day 11 Part 1: {0}", flashCount);
        }

        public static void Part2()
        {
            List<string> lines = File.ReadAllLines(@"Day11/input.txt").ToList();

            int width = lines.First().Length;

            List<int> input = lines.SelectMany(x => x.ToArray()).Select(x => Int32.Parse(x.ToString())).ToList();

            int steps = 0;
            HashSet<int> flashedIndices = new HashSet<int>();
            while (flashedIndices.Count != input.Count)
            {
                ++steps;
                flashedIndices.Clear();
                for (int j = 0; j < input.Count; ++j)
                {
                    Step(input, flashedIndices, j, width);
                }
            }

            Console.WriteLine("Day 11 Part 1: {0}", steps);
        }

        private static int Step(List<int> input, HashSet<int> flashedIndices, int Idx, int width)
        {
            if (input[Idx] == 0 && flashedIndices.Contains(Idx))
            {
                return 0;
            }
            else
            {
                ++input[Idx];
            }
            
            if (input[Idx] < 10)
            {
                return 0;
            }
            else
            {
                input[Idx] = 0;
            }

            if (flashedIndices.Contains(Idx))
            {
                return 0;
            }
            else
            {
                flashedIndices.Add(Idx);
            }

            // Handle flash
            int rowIndex = Idx / width;
            int columnIndex = Idx % width;
            int flashCount = 1;
            int height = input.Count / width;

            // Adjacents
            if (rowIndex > 0)
            {
                flashCount += Step(input, flashedIndices, Idx - width, width);
            }
            if (rowIndex < height - 1)
            {
                flashCount += Step(input, flashedIndices, Idx + width, width);
            }
            if (columnIndex > 0)
            {
                flashCount += Step(input, flashedIndices, Idx - 1, width);
            }
            if (columnIndex < width - 1)
            {
                flashCount += Step(input, flashedIndices, Idx + 1, width);
            }
            // Diagonals
            // Top Left
            if (rowIndex > 0 && columnIndex > 0)
            {
                flashCount += Step(input, flashedIndices, Idx - width - 1, width);
            }
            // Top Right
            if (rowIndex > 0 && columnIndex < width - 1)
            {
                flashCount += Step(input, flashedIndices, Idx - width + 1, width);
            }
            // Bottom Left
            if (columnIndex > 0 && rowIndex < height - 1)
            {
                flashCount += Step(input, flashedIndices, Idx + width - 1, width);
            }
            // Bottom Right
            if (rowIndex < height - 1 && columnIndex < width - 1)
            {
                flashCount += Step(input, flashedIndices, Idx + width + 1, width);
            }

            return flashCount;
        }
    }
}
