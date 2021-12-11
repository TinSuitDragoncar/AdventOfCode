using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day9
    {
        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day9/input.txt").ToList();

            int width = lines.First().Length;
            int height = lines.Count;

            List<int> input = lines.SelectMany(x => x.ToArray()).Select(y => Int32.Parse(y.ToString())).ToList();

            int sum = 0;
            for (int i = 0; i < input.Count; ++i)
            {
                if (input[i] == 9)
                {
                    continue;
                }

                int columnIndex = i % width;
                int rowIndex = i / width;

                if (columnIndex > 0)
                {
                    if (input[i] >= input[i - 1])
                    {
                        continue;
                    }
                }
                if (columnIndex < width - 1)
                {
                    if (input[i] >= input[i + 1])
                    {
                        continue;
                    }
                }
                if (rowIndex > 0)
                {
                    if (input[i] >= input[i - width])
                    {
                        continue;
                    }
                }
                if (rowIndex < height - 1)
                {
                    if (input[i] >= input[i + width])
                    {
                        continue;
                    }
                }

                sum += input[i] + 1;
            }

            Console.WriteLine("Day 9 Part 1: danger count is {0}", sum);
        }

        private static List<int> GetBasinIndices()
        {
            List<string> lines = File.ReadAllLines(@"Day9/input.txt").ToList();

            int width = lines.First().Length;
            int height = lines.Count;

            List<int> input = lines.SelectMany(x => x.ToArray()).Select(y => Int32.Parse(y.ToString())).ToList();

            List<int> indices = new List<int>();
            for (int i = 0; i < input.Count; ++i)
            {
                if (input[i] == 9)
                {
                    continue;
                }

                int columnIndex = i % width;
                int rowIndex = i / width;

                if (columnIndex > 0)
                {
                    if (input[i] >= input[i - 1])
                    {
                        continue;
                    }
                }
                if (columnIndex < width - 1)
                {
                    if (input[i] >= input[i + 1])
                    {
                        continue;
                    }
                }
                if (rowIndex > 0)
                {
                    if (input[i] >= input[i - width])
                    {
                        continue;
                    }
                }
                if (rowIndex < height - 1)
                {
                    if (input[i] >= input[i + width])
                    {
                        continue;
                    }
                }

                indices.Add(i);
            }

            return indices;
        }

        public static void Part2()
        {
            List<string> lines = File.ReadAllLines(@"Day9/input.txt").ToList();

            int width = lines.First().Length;
            int height = lines.Count;

            List<int> input = lines.SelectMany(x => x.ToArray()).Select(y => Int32.Parse(y.ToString())).ToList();
            HashSet<int> visitedIndices = new HashSet<int>();
            List<int> basins = new List<int>();

            List<int> indicies = GetBasinIndices();
            foreach (int i in indicies)
            {
                visitedIndices.Clear();
                GetBasinSize(i, input, visitedIndices, width);
                basins.Add(visitedIndices.Count);
            }

            basins.Sort();
            basins.Reverse();

            Console.WriteLine("Day 9 Part 2: The 3 largest basins are {0}, {1} and {2} with a product of {3}", basins[0], basins[1], basins[2], basins[0] * basins[1] * basins[2]);
        }

        private static void GetBasinSize(int i, List<int> input, HashSet<int> visitedIndices, int width)
        {
            int thisValue = input[i];

            int height = input.Count / width;
            int columnIndex = i % width;
            int rowIndex = i / width;

            if (thisValue < 9 && !visitedIndices.Contains(i))
            {
                visitedIndices.Add(i);

                if (columnIndex > 0)
                {
                    GetBasinSize(i - 1, input, visitedIndices, width);
                }
                if (columnIndex < width - 1)
                {
                    GetBasinSize(i + 1, input, visitedIndices, width);
                }
                if (rowIndex > 0)
                {
                    GetBasinSize(i - width, input, visitedIndices, width);
                }
                if (rowIndex < height - 1)
                {
                    GetBasinSize(i + width, input, visitedIndices, width);
                }
            }   
        }
    }
}
