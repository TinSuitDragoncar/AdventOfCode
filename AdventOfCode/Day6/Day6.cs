using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day6
    {
        public static void Part1()
        {
            int days = 80;
            ulong pop = CalculatePopulation(days);

            Console.WriteLine("Day 6 Part 1: Total number of fish after {0} days is {1} ", days, pop);
        }

        public static void Part2()
        {
            int days = 256;
            ulong pop = CalculatePopulation(days);

            Console.WriteLine("Day 6 Part 2: Total number of fish after {0} days is {1} ", days, pop);
        }

        private static ulong CalculatePopulation(int days)
        {
            List<int> initialState = File.ReadAllLines(@"Day6/input.txt").First().Split(',').Select(x => Int32.Parse(x)).ToList();

            ulong[] fishLifespan = new ulong[9];

            foreach(int t in initialState)
            {
                ++fishLifespan[t];
            }

            for(int i = 0; i < days; ++i)
            {
                ulong duplicatingFish = fishLifespan[0];

                for (int j = 0; j < 8; ++j)
                {
                    fishLifespan[j] = fishLifespan[j + 1];
                }

                fishLifespan[8] = duplicatingFish;
                fishLifespan[6] += duplicatingFish;
            }

            ulong sum = 0;
            foreach(ulong population in fishLifespan)
            {
                sum += population;
            }
            return sum;
        }
    }
}
