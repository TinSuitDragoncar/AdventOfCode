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

        public static void Part2()
        {
            List<string> lines = File.ReadAllLines(@"Day8/input.txt").ToList();

            int count = 0;
            foreach (string l in lines)
            {
                Dictionary<string, int> wireToDigit = new Dictionary<string, int>();
                Dictionary<int, string> digitToWire = new Dictionary<int, string>();

                List<string> input = l.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                List<string> length5 = new List<string>();
                List<string> length6 = new List<string>();
                // Get uniques
                foreach (string wires in input)
                {
                    char[] w = wires.ToCharArray();
                    Array.Sort(w);
                    string orderedWires = String.Concat(w);
                    

                    if (orderedWires.Length == 2)
                    {
                        wireToDigit.Add(orderedWires, 1);
                        digitToWire.Add(1, orderedWires);
                    }
                    else if (orderedWires.Length == 3)
                    {
                        wireToDigit.Add(orderedWires, 7);
                        digitToWire.Add(7, orderedWires);
                    }
                    else if(orderedWires.Length == 4)
                    {
                        wireToDigit.Add(orderedWires, 4);
                        digitToWire.Add(4, orderedWires);
                    }
                    else if (orderedWires.Length == 7)
                    {
                        wireToDigit.Add(orderedWires, 8);
                        digitToWire.Add(8, orderedWires);
                    }
                    else if (orderedWires.Length == 6)
                    {
                        length6.Add(orderedWires);
                    }
                    else
                    {
                        length5.Add(orderedWires);
                    }
                }

                List<char> cf = digitToWire[1].ToList();
                string three = length5.First(x => x.Contains(cf[0]) && x.Contains(cf[1]));
                wireToDigit.Add(three, 3);

                List<char> bd = digitToWire[4].Where(x => !cf.Contains(x)).ToList();
                char d = bd.First(x => three.Contains(x));

                char b = bd.First(x => x != d);

                string five = length5.First(x => x.Contains(b));
                wireToDigit.Add(five, 5);

                string two = length5.First(x => x != five && x != three);
                wireToDigit.Add(two, 2);

                string zero = length6.First(x => !x.Contains(d));
                wireToDigit.Add(zero, 0);

                string nine = length6.First(x => x != zero && x.Contains(cf[0]) && x.Contains(cf[1]));
                wireToDigit.Add(nine, 9);

                string six = length6.First(x => x != zero && x != nine);
                wireToDigit.Add(six, 6);


                List<string> digitStrings = l.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < digitStrings.Count; ++i)
                {
                    char[] w = digitStrings[i].ToCharArray();
                    Array.Sort(w);
                    string orderedWires = String.Concat(w);

                    count += wireToDigit[orderedWires] * (int) Math.Pow(10, digitStrings.Count - i -1);
                }
            }

            Console.WriteLine("Day 8 Part 2: number of unique digits is {0}", count);
        }
    }
}
