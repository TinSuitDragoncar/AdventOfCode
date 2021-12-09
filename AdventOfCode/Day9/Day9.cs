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
    }
}
