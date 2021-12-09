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

        public static void Part2()
        {
            List<string> lines = File.ReadAllLines(@"Day9/test.txt").ToList();

            int width = lines.First().Length;
            int height = lines.Count;

            List<int> input = lines.SelectMany(x => x.ToArray()).Select(y => Int32.Parse(y.ToString())).ToList();
            HashSet<int> visitedIndices = new HashSet<int>();
            List<int> basins = new List<int>();

            int sum = 0;
            for (int i = 0; i < input.Count; ++i)
            {
                int basinSize = GetBasinSize(i, -1, input, visitedIndices, width, height);
                basins.Add(basinSize);
            }

            Console.WriteLine("Day 9 Part 2: The 3 largest basins are {0}, {1} and {2} with a product of {3}", sum);
        }

        private static int GetBasinSize(int i, int prevValue, List<int> input, HashSet<int> visitedIndices, int width, int height)
        {
            
            if (input[i] == 9 || visitedIndices.Contains(i))
            {
                return 0;
            }
            visitedIndices.Add(i);

            int columnIndex = i % width;
            int rowIndex = i / width;

            int basinCount = 1;

            if (input[i] > prevValue)
            {
                if (columnIndex > 0)
                {
                    basinCount += GetBasinSize(i - 1, input[i], input, visitedIndices, width, height);
                }
                if (columnIndex < width - 1)
                {
                    basinCount += GetBasinSize(i + 1, input[i], input, visitedIndices, width, height);
                }
                if (rowIndex > 0)
                {

                    basinCount += GetBasinSize(i - width, input[i], input, visitedIndices, width, height);
                }
                if (rowIndex < height - 1)
                {
                    basinCount += GetBasinSize(i + width, input[i], input, visitedIndices, width, height);
                }

                return basinCount;
            }   
            else
            {
                return 0;
            }
        }
    }
}
