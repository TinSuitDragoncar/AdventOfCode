using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day2
    {
        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day2/input.txt").ToList();
            int horizontalPos = 0;
            int depth = 0;
            foreach (string line in lines)
            {
                string[] values = line.Split(' ');
                string direction = values[0];
                int distance = Int32.Parse(values[1]);
                switch (direction)
                {
                    case "forward":
                        horizontalPos += distance;
                        break;

                    case "up":
                        depth -= distance;
                        break;

                    case "down":
                        depth += distance;
                        break;
                }
            }

            Console.WriteLine("Day 2 Part 1: Depth ({0}) * Horizontal Position ({1}) = {2}", depth, horizontalPos, horizontalPos * depth);
        }
    }
}
