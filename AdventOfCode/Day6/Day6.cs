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
            List<int> initialState = File.ReadAllLines(@"Day6/test.txt").First().Split(',').Select(x => Int32.Parse(x)).ToList();

            int count = initialState.Count;
            const int days = 18;
            foreach (int state in initialState)
            {
                int dayOffset = 8 - state;

                count += GetProduction(days + dayOffset);
            }

            Console.WriteLine("Day 6 Part 1: Total number of fish after {0} days is {1} ", count);
        }
        
        private static int GetProduction(int days)
        {
            const int initialLifespan = 9;
            const int defaultLifespan = 7;
            if (initialLifespan > days)
            {
                return 0;
            }
            int localProduction = 1;
            days -= initialLifespan;
            localProduction += days / defaultLifespan;
            int totalProduction = GetProduction(days) + localProduction;
            for (int i = 0; i < localProduction - 1; ++i)
            {
                days -= defaultLifespan;
                totalProduction += GetProduction(days);
            }
            return totalProduction;
        }
    }
}
