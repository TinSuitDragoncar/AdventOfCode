using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day8
    {
        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day8/input.txt").ToList();

            int count = 0;
            foreach (string l in lines)
            {
                List<string> uniqueDigits = l.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7).ToList();
                count += uniqueDigits.Count;
            }

            Console.WriteLine("Day 8 Part 1: number of unique digits is {0}", count);
        }
    }
}
