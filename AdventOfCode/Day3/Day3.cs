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
                for (int i = 0; i < l.Length; ++i)
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
                int valToAdd = (int)Math.Pow(2, (oneCount.Count - i - 1));
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

        public static void Part2()
        {
            List<string> lines = File.ReadAllLines(@"Day3/input.txt").ToList();

            int oxygen = GetDiagnostics(lines, true);
            int co2 = GetDiagnostics(lines, false);

            Console.WriteLine("Day 3 Part 2: Life Support Rating is Oxygen ({0}) * CO2 ({1}) = {2}", oxygen, co2, oxygen * co2);
        }

        private static int GetDiagnostics(List<string> lines, bool bIsCommonCriteria)
        {
            List<string> dLines = new List<string>(lines);

            for (int i = 0; i < dLines.First().Length; ++i)
            {
                int oneCount = 0;
                foreach (string l in dLines)
                {
                    if (l[i] == '1')
                    {
                        ++oneCount;
                    }
                }

                float half = (float)dLines.Count / 2;
                char leftChar = bIsCommonCriteria ? '1' : '0';
                char rightChar = bIsCommonCriteria ? '0' : '1';
                char keepChar = oneCount >= half ? leftChar : rightChar;

                dLines = dLines.Where(x => x[i] == keepChar).ToList();

                if (dLines.Count == 1)
                {
                    break;
                }
            }

            return Convert.ToInt32(dLines.First(), 2);
        }
    }
}
