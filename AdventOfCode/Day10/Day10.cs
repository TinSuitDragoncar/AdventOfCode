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

        public static void Part2()
        {
            List<string> lines = File.ReadAllLines(@"Day10/input.txt").ToList();

            Dictionary<char, ulong> scoreMap = new Dictionary<char, ulong>();
            scoreMap.Add(')', 1);
            scoreMap.Add(']', 2);
            scoreMap.Add('}', 3);
            scoreMap.Add('>', 4);

            Dictionary<char, char> oppositeMap = new Dictionary<char, char>();
            oppositeMap.Add('(', ')');
            oppositeMap.Add('[', ']');
            oppositeMap.Add('{', '}');
            oppositeMap.Add('<', '>');
            Stack<char> brackets = new Stack<char>();


            List<ulong> scores = new List<ulong>();
            foreach (string l in lines)
            {
                ulong score = 0;
                bool bBroken = false;
                brackets.Clear();

                foreach (char c in l)
                {
                    if (scoreMap.ContainsKey(c))
                    {
                        if (brackets.Count == 0 ||
                            oppositeMap[brackets.Peek()] != c)
                        {
                            bBroken = true;
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

                if (bBroken)
                {
                    continue;
                }

                // brackets will still have some values if the line is incomplete
                while(brackets.Count > 0)
                {
                    char matchingBracket = oppositeMap[brackets.Pop()];
                    score *= 5;
                    score += scoreMap[matchingBracket];
                }

                if (score != 0)
                {
                    scores.Add(score);
                }
            }

            scores.Sort();


            Console.WriteLine("Day 10 Part 2: {0}", scores[scores.Count/2]);
        }
    }
}
