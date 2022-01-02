using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day18
    {
        public static void Solve()
        {
           string[] snailNumbers = File.ReadAllLines(@"Day18/input.txt");

           string currentNumber = snailNumbers.First(); 
            foreach(string n in snailNumbers.Skip(1))
            {
                currentNumber = AddSnailNumbers(currentNumber, n);
            }

            int magnitude = FindMagnitude(currentNumber);

            Console.WriteLine("Part 1 Result is:\n{0}", currentNumber);
            Console.WriteLine("Magnitude is: {0}", magnitude);

            // Part 2
        }

        private static int FindMagnitude(string s)
        {
            // trim outer brackets
            s = s.Substring(1, s.Length - 2);
            int magnitude = 0;

            // check both vals
            Match m = Regex.Match(s, @"^(\d+),(\d+)$");
            if (m.Success)
            {
                magnitude += 3 * Int32.Parse(m.Groups[1].ToString());
                magnitude += 2 * Int32.Parse(m.Groups[2].ToString());
                return magnitude;
            }
            // check left val
            m = Regex.Match(s, @"^(\d+),");
            if (m.Success)
            {
                magnitude += 3 * Int32.Parse(m.Groups[1].ToString());
                magnitude += 2 * FindMagnitude(s.Substring(m.Groups[0].Length));
                return magnitude;
            }
            // check right val
            m = Regex.Match(s, @",(\d+)$");
            if (m.Success)
            {
                magnitude += 3 * FindMagnitude(s.Substring(0, m.Groups[0].Index));
                magnitude += 2 * Int32.Parse(m.Groups[1].ToString());
                return magnitude;
            }

            // otherwise balance brackets to determine where to split the string
            int balance = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                {
                    balance++;
                }
                else if (s[i] == ']')
                {
                    balance--;
                }
                if (balance == 0)
                {
                    string lString = s.Substring(0, i + 1);
                    string rString = s.Substring(i + 2, s.Length - i - 2);
                    magnitude += 3 * FindMagnitude(lString);
                    magnitude += 2 * FindMagnitude(rString);
                    return magnitude;
                }
            }

            return -1;
        }

        private static string AddSnailNumbers(string a, string b)
        {
            a = String.Format("[{0},{1}]", a, b);
            Console.WriteLine("Reducing {0}", a);
            while (Reduce(a, out string result))
            {
                a = result;
            }

            return a;
        }

        private static bool Reduce(string n, out string result)
        {
            bool bResult = Explode(n, out result);
            if (!bResult)
            {
                bResult = Split(n, out result);
            }

            return bResult;
        }

        private static bool Explode(string n, out string result)
        {
            int balance = 0;
            for (int i = 0; i < n.Length; ++i)
            {
                if (n[i] == '[')
                {
                    balance++;
                }
                else if (n[i] == ']')
                {
                    balance--;
                }

                if (balance == 5)
                {
                    Match m = Regex.Match(n.Substring(i), @"\[(\d+),(\d+)\]");
                    result = n.Remove(i, m.Groups[0].Length);
                    result = result.Insert(i, "x");
                    int left = Int32.Parse(m.Groups[1].ToString());
                    int right = Int32.Parse(m.Groups[2].ToString());
                    // Find right if it exists
                    m = Regex.Match(result, @"x\D+?(\d+)");
                    if (m.Success)
                    {
                        int val = Int32.Parse(m.Groups[1].ToString());
                        val += right;
                        result = result.Remove(m.Groups[1].Index, m.Groups[1].Length);
                        result = result.Insert(m.Groups[1].Index, val.ToString());
                    }
                    // Find left if it exists
                    m = Regex.Match(result, @"(\d+)\D+?x");
                    if (m.Success)
                    {
                        int val = Int32.Parse(m.Groups[1].ToString());
                        val += left;
                        result = result.Remove(m.Groups[1].Index, m.Groups[1].Length);
                        result = result.Insert(m.Groups[1].Index, val.ToString());
                    }

                    result = result.Replace('x', '0');
                    return true;
                }
            }

            result = null;
            return false;
        }

        private static bool Split(string n, out string result)
        {
            Match m = Regex.Match(n, @"(\d{2,})");
            if (m.Success)
            {
                int val = Int32.Parse(m.Groups[0].ToString());
                int left = val / 2;
                int right = left + val % 2;
                result = n.Remove(m.Groups[0].Index, m.Groups[0].Length);
                result = result.Insert(m.Groups[0].Index, String.Format("[{0},{1}]", left, right));
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }

}
