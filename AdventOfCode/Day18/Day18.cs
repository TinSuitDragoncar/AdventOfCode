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
            string[] snailNumbers = File.ReadAllLines(@"Day18/test.txt");

            string currentNumber = snailNumbers.First(); 
            foreach(string n in snailNumbers.Skip(1))
            {
                currentNumber = AddSnailNumbers(currentNumber, n);
            }

            Console.WriteLine("Result is:\n{0}", currentNumber);
        }

        public class Node
        {
            public object Data { get; init; }
            public Node A { get; set; }
            public Node B { get; set; }
            public Node(object data)
            {
                Data = data;
                A = null;
                B = null;
            }
        }


        private static Node ConvertToTree(string s)
        {
            Node n = new();
            n.A = new();
            n.B = new();
            Match m = Regex.Match(s, @"^\[(\d+),(\d+)\]$");
            if (m.Success)
            {
                
            }
        }

        private static string AddSnailNumbers(string a, string b)
        {
            a = String.Format("[{0},{1}]", a, b);

            Console.WriteLine("Attempting to reduce {0}", a);
            while (Reduce(a))
            {
                Console.WriteLine("Reduced to {0}", a);
            }

            return a;
        }

        private static bool Reduce(string n)
        {
            // Check for explode -> nested in 4 pairs
            int nestCount = 0;
            bool bExplode = false;
            string explodingPair = null;
            int a, b;
            for (int i = 0; i < n.Length; i++)
            {
                char c = n[i];
                if (c == '[')
                {
                    nestCount++;

                    if (nestCount > 4)
                    {
                        Match explM = Regex.Match(n.Substring(i), @"(\[(\d+),(\d+)\])");
                        explodingPair = explM.Groups[1].ToString();
                        a = Int32.Parse(explM.Groups[2].ToString());
                        b = Int32.Parse(explM.Groups[3].ToString());
                        bExplode = true;
                        break;
                    }
                }
                else if (c == ']')
                {
                    nestCount--;
                }
            }

            if (bExplode)
            {
                string rightPattern = Regex.Escape(explodingPair) + @".+?(\d+)";
                string leftPattern = @"(\d+).+?" + Regex.Escape(explodingPair);

                return true;
            }
            // check for split -> any regular number is 10 or greater
            if (Regex.IsMatch(n, @"\d{2,}"))
            {

                return true;
            }
               
            return false;
        }
    }
}
