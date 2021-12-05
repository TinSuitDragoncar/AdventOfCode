using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day3
    {
        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day3/input.txt").ToList();

            List<int> oneCount = new List<int>(new int[lines.First().Length]);

            foreach (string l in lines)
            {
                for(int i = 0; i < l.Length; ++i)
                {
                    if (l[i] == '1')
                    {
                        ++oneCount[i];
                    }
                }
            }

            int gamma = 0;
            int epsilon = 0;
            float half = lines.Count / 2;
            for (int i = 0; i < oneCount.Count; ++i)
            {
                int valToAdd = (int) Math.Pow(2, (oneCount.Count - i - 1));
                if (oneCount[i] > half)
                {
                    gamma += valToAdd;
                }
                else
                {
                    epsilon += valToAdd;
                }
            }

            Console.WriteLine("Day 3 Part 1: Power Consumption is Gamma ({0}) * Epsilon ({1}) = {2}", gamma, epsilon, gamma * epsilon);
        }
    }
}
