using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day10
    {
        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day10/input.txt").ToList();

            Dictionary<char, int> scoreMap = new Dictionary<char, int>();
            scoreMap.Add(')', 3);
            scoreMap.Add(']', 57);
            scoreMap.Add('}', 1197);
            scoreMap.Add('>', 25137);

            Dictionary<char, char> oppositeMap = new Dictionary<char, char>();
            oppositeMap.Add('(', ')');
            oppositeMap.Add('[', ']');
            oppositeMap.Add('{', '}');
            oppositeMap.Add('<', '>');
            Stack<char> brackets = new Stack<char>();

            int score = 0;
            foreach (string l in lines)
            {
                brackets.Clear();

                foreach (char c in l)
                {
                    if (scoreMap.ContainsKey(c))
                    {
                        if (brackets.Count == 0 ||
                            oppositeMap[brackets.Peek()] != c)
                        {
                            score += scoreMap[c];
                            break;
                        }
                        else
                        {
                            brackets.Pop();
                        }
                    }
                    else
                    {
                        brackets.Push(c);
                    }
                }
            }

            Console.WriteLine("Day 10 Part 1: {0}", score);
        }
    }
}
