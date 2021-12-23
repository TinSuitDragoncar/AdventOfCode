
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day21
    {
        public static void Solve()
        {
            string[] lines = File.ReadAllLines(@"Day21/input.txt");

            int p1Pos = Int32.Parse(lines[0].Last().ToString()) - 1;
            int p2Pos = Int32.Parse(lines[1].Last().ToString()) - 1;

            Part1(p1Pos, p2Pos);
            Part2(p1Pos, p2Pos);
        }

        private static void Part2(int p1Pos, int p2Pos)
        {
            // Find possible roll combinations
            Dictionary<long, long> possibleSums = new();

            long[] possibleRolls = new long[] { 1, 2, 3 };

            foreach(long a in possibleRolls)
            {
                foreach(long b in possibleRolls)
                {
                    foreach(long c in possibleRolls)
                    {
                        long sum = a + b + c;
                        if (!possibleSums.ContainsKey(sum))
                        {
                            possibleSums[sum] = 0;
                        }
                        possibleSums[sum]++;
                    }
                }
            }

            Dictionary<(long score, long pos), long> p1StateFrequency = new();
            Dictionary<(long score, long pos), long> p2StateFrequency = new();

            const long winningScore = 21;

            p1StateFrequency[(0, p1Pos)] = 1;
            p2StateFrequency[(0, p2Pos)] = 1;

            bool p1Win = false;
            bool p2Win = false;
            bool p1Turn = true;

            long p1WinCount = 0;
            long p2WinCount = 0;
            while (!(p1Win || p2Win))
            {
                Dictionary<(long score, long pos), long> currentFrequency = p1Turn ? p1StateFrequency : p2StateFrequency;
                Dictionary<(long score, long pos), long> currentFrequencyCopy = new(currentFrequency);

                long currentWinCount = 0;
                foreach (var statePair in currentFrequencyCopy)
                {
                    if (statePair.Value == 0)
                    {
                        continue;
                    }

                    // remove occurences of old state in currentFrequency
                    currentFrequency[statePair.Key] -= statePair.Value;

                    foreach (var rollPair in possibleSums)
                    {
                        long newPos = (statePair.Key.pos + rollPair.Key) % 10;
                        long newScore = statePair.Key.score + newPos + 1;

                        if (newScore >= winningScore) // if win increment win count
                        {
                            currentWinCount += statePair.Value * rollPair.Value;
                        }
                        else // update the appropriate state
                        {
                            var key = (newScore, newPos);
                            if (!currentFrequency.ContainsKey(key))
                            {
                                currentFrequency.Add(key, 0);
                            }
                            currentFrequency[(newScore, newPos)] += statePair.Value * rollPair.Value;
                        }
                    }
                }

                // Update win count
                if (p1Turn)
                {
                    p1WinCount += p2StateFrequency.Values.Sum() * currentWinCount;
                    p1Win = p1StateFrequency.Values.Sum() == 0;
                }
                else
                {
                    p2WinCount += p1StateFrequency.Values.Sum() * currentWinCount;
                    p2Win = p2StateFrequency.Values.Sum() == 0;
                }

                p1Turn = !p1Turn;
            }

            long mostWins = p1Win ? p1WinCount : p2WinCount;
            long leastWins = p1Win ? p2WinCount : p1WinCount;
            Console.WriteLine("Part 2: Winning count: {0} Losing count: {1}", mostWins, leastWins);
        }

        private static void Part1(int p1Pos, int p2Pos)
        {
            int dd = 100;
            int rollCount = 0;
            bool p1Win = false;
            bool p2Win = false;

            int p1Score = 0;
            int p2Score = 0;

            bool p1Turn = true;

            int winningScore = 1000;
            while (!(p1Win || p2Win))
            {
                int roll = Roll3(dd, out dd);
                rollCount += 3;
                if (p1Turn)
                {
                    p1Pos += roll;
                    p1Score += p1Pos % 10 + 1;
                    p1Win = p1Score >= winningScore;
                }
                else
                {
                    p2Pos += roll;
                    p2Score += p2Pos % 10 + 1;
                    p2Win = p2Score >= winningScore;
                }
                p1Turn = !p1Turn;
            }

            int losingScore = p1Win ? p2Score : p1Score;
            Console.WriteLine("Part 1: {0} * {1} = {2}", losingScore, rollCount, losingScore * rollCount);
        }

        private static int Roll3(int start, out int end)
        {
            int sum = 0;
            for (int i = 0; i < 3; ++i)
            {
                ++start;
                start = start % 100;
                sum += start;
            }
            end = start;
            return sum;
        }
    }
}
