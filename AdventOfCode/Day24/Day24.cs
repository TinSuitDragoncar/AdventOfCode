using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day24
    {
        public static void CreateFunction()
        {
            string[] lines = File.ReadAllLines(@"Day24/input.txt");

            foreach (string l in lines)
            {
                Match m = Regex.Match(l, @"(\w+) (\w) ([-a-z0-9]+)");

                if (m.Success)
                {
                    string op = m.Groups[1].ToString();
                    string a = m.Groups[2].ToString();
                    string b = m.Groups[3].ToString();

                    switch (op)
                    {
                        case "add":
                            Console.WriteLine("{0} += {1};", a, b);
                            break;

                        case "mul":
                            Console.WriteLine("{0} *= {1};", a, b);
                            break;

                        case "div":
                            Console.WriteLine("{0} /= {1};", a, b);
                            break;

                        case "mod":
                            Console.WriteLine("{0} %= {1};", a, b);
                            break;

                        case "eql":
                            Console.WriteLine("{0} = Convert.ToInt64({0} == {1});", a, b);
                            break;
                    }
                }
                else // read input
                {
                    string varName = l.Last().ToString();
                    Console.WriteLine("{0} = digits.Pop();", varName);
                }
            }
        }

        private static bool Check(long modelNumber)
        {
            long x, y, z, w = 0;


            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 1;
            x += 11;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 7;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 1;
            x += 14;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 8;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 1;
            x += 10;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 16;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 1;
            x += 14;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 8;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 26;
            x += -8;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 3;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 1;
            x += 14;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 12;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 26;
            x += -11;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 1;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 1;
            x += 10;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 8;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 26;
            x += -6;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 8;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 26;
            x += -9;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 14;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 1;
            x += 12;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 4;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 26;
            x += -5;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 14;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 26;
            x += -4;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 15;
            y *= x;
            z += y;
            w = digits.Pop();
            x *= 0;
            x += z;
            x %= 26;
            z /= 26;
            x += -9;
            x = Convert.ToInt64(x == w);
            x = Convert.ToInt64(x == 0);
            y *= 0;
            y += 25;
            y *= x;
            y += 1;
            z *= y;
            y *= 0;
            y += w;
            y += 6;
            y *= x;
            z += y;
        }

        public static void Solve()
        {
            CreateFunction();
            return;

            string[] lines = File.ReadAllLines(@"Day24/input.txt");

            long maxVal = 100000000000000;
            bool bSuccess = false;

            Dictionary<string, long> variables = new()
            {
                { "w", 0 },
                { "x", 0 },
                { "y", 0 },
                { "z", 0 }
            };


            // read in the program

            Stack<long> digits = new();
            while (!bSuccess)
            {
                maxVal--;
                Console.WriteLine("Checking {0}", maxVal);

                // parse digits
                digits.Clear();
                long temp = maxVal;
                for(int i = 0; i < 14; ++i) // we must always have 14 digits
                {
                    digits.Push(temp % 10);
                    temp /= 10;
                }

                foreach (string l in lines)
                {
                    Match m = Regex.Match(l, @"(\w+) (\w) ([-a-z0-9]+)");

                    if (m.Success)
                    {
                        string op = m.Groups[1].ToString();
                        string a = m.Groups[2].ToString();
                        string b = m.Groups[3].ToString();

                        long intVal = 0;
                        bool bVar = variables.ContainsKey(b);
                        if (!bVar)
                        {
                            intVal = Int64.Parse(b);
                        }

                        switch (op)
                        {
                            case "add":
                                variables[a] += bVar ? variables[b] : intVal;
                                break;

                            case "mul":
                                variables[a] *= bVar ? variables[b] : intVal;
                                break;

                            case "div":
                                variables[a] /= bVar ? variables[b] : intVal;
                                break;

                            case "mod":
                                variables[a] %= bVar ? variables[b] : intVal;
                                break;

                            case "eql":
                                variables[a] = Convert.ToInt64(variables[a] == (bVar ? variables[b] : intVal));
                                break;
                        }
                    }
                    else // read input
                    {
                        string varName = l.Last().ToString();
                        variables[varName] = digits.Pop();
                    }
                }

                // Check for success
                bSuccess = variables["z"] == 0;
            }

            Console.WriteLine("Largest valid number is {0}", maxVal);
        }
    }
}
