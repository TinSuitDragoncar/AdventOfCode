using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day1
    {
        public static void Part1()
        {
            List<int> lines = File.ReadAllLines(@"Day1/input.txt").Select(x => Int32.Parse(x)).ToList();
            int increaseCount = 0;
            for (int i = 1; i < lines.Count; ++i)
            {
                if (lines[i] > lines[i - 1])
                {
                    ++increaseCount;
                }
            }

            Console.WriteLine("Day 1: Depth increased {0} times!", increaseCount);
        }
    }
}
