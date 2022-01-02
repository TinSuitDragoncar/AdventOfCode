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

            List<Node> nAsTrees = snailNumbers.Select(s => ConvertToTree(s)).ToList();

            Node currentNumber = nAsTrees.First(); 
            foreach(Node n in nAsTrees.Skip(1))
            {
                currentNumber = AddSnailNumbers(currentNumber, n);
            }

            Console.WriteLine("Result is:\n{0}", currentNumber);
        }

        public class Node
        {
            public Node Parent { get; set; }
            public int? Data { get; set; }
            public Node A { get; set; }
            public Node B { get; set; }
            public Node(Node parent = null, int? data = null)
            {
                Parent = parent;
                Data = data;
                A = null;
                B = null;
            }
        }


        private static Node ConvertToTree(string s)
        {
            Node n = new();
            Match m = Regex.Match(s, @"^\[(\d+),(\d+)\]$");
            if (m.Success)
            {
                n.A = new(n, Int32.Parse(m.Groups[1].ToString()));
                n.B = new(n, Int32.Parse(m.Groups[2].ToString()));
            }
            else
            {
                s = s.Substring(1, s.Length - 2);
                int balance = 0;
                for(int i = 0; i < s.Length; i++)
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
                        string aString = s.Substring(0, i + 1);
                        string bString = s.Substring(i + 2, s.Length - i - 2);
                        int result;
                        if (Int32.TryParse(aString, out result))
                        {
                            n.A = new(n, result);
                        }
                        else
                        {
                            n.A = ConvertToTree(aString);
                        }
                        if (Int32.TryParse(bString, out result))
                        {
                            n.B = new(n, result);
                        }
                        else
                        {
                            n.B = ConvertToTree(bString);
                        }
                        break;
                    }
                }
            }

            return n;
        }

        private static Node AddSnailNumbers(Node a, Node b)
        {
            Node result = new();
            result.A = a;
            result.B = b;

            Console.WriteLine("Attempting to reduce {0}", a);
            int reduceCount = 0;
            while (Reduce(result))
            {
                reduceCount++;
                Console.WriteLine("Reduced {0}", reduceCount);
            }

            return result;
        }

        private static bool Reduce(Node n)
        {
            bool result = Explode(n);
            if (!result)
            {
                result = Split(n);
            }

            return result;
        }

        private static bool Explode(Node n, int depth = 1)
        {
            if (depth == 5)
            {
                // EXPLODE LOGIC
                n.Data = 0;
                AddLeft(n);
                AddRight(n);
                return true;
            }

            bool bExplode = false;
            // traverse A branch
            if (n.A != null)
            {
                bExplode = Explode(n.A, depth + 1);
            }
            if (!bExplode &&
                n.B != null)
            {
                bExplode = Explode(n.A, depth + 1);
            }

            return bExplode;
        }

        private static void AddLeft(Node n)
        {
            HashSet<Node> visited = new();
            visited.Add(n);

            Node current = n.Parent;
            while (current != null)
            {

            }

            return;
        }
    }

}
