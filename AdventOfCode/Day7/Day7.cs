using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day7
    {
        public static void Part1()
        {
            List<int> crabPositions = File.ReadAllLines(@"Day7/input.txt").First().Split(',').Select(x => Int32.Parse(x)).ToList();

            int min = crabPositions.Min();
            int max = crabPositions.Max();

            List<int> endPositions = Enumerable.Range(min, max - min).ToList();

            int bestPosition = 0;
            int minFuel = Int32.MaxValue;

            foreach (int pos in endPositions)
            {
                int fuel = 0;
                foreach(int crab in crabPositions)
                {
                    fuel += Math.Abs(crab - pos);
                }
                if (fuel < minFuel)
                {
                    bestPosition = pos;
                    minFuel = fuel;
                }
            }

            Console.WriteLine("Day 7 Part 1: The crabs should align to position {0} consuming {1} fuel", bestPosition, minFuel);
        }
    }
}
