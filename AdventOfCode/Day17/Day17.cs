using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day17
    {
        public static void Solve()
        {
            string input = File.ReadLines(@"Day17/input.txt").First();

            Match xM = Regex.Match(input, @"x=([0-9-]+)..([0-9-]+)");
            Match yM = Regex.Match(input, @"y=([0-9-]+)..([0-9-]+)");
            (int a, int b) xLims = (Int32.Parse(xM.Groups[1].ToString()), Int32.Parse(xM.Groups[2].ToString()));
            (int a, int b) yLims = (Int32.Parse(yM.Groups[2].ToString()), Int32.Parse(yM.Groups[1].ToString()));

            int xMinStart = 1;
            HashSet<int> result = GetStepRange(xMinStart, xLims);
            while (result.Count == 0)
            {
                xMinStart++;
                result = GetStepRange(xMinStart, xLims);
            }
            int xMaxStart = xLims.b;

            int yMinStart = yLims.b;
            int yMaxStart = yLims.b * - 1 - 1; // max start trajectory will be -yMax - 1 -> when the projectile comes back down and passes 0 its velocity will be yStart + 1

            int maxHeight = yMaxStart * (yMaxStart + 1) / 2;

            Console.WriteLine("Max height is {0}", maxHeight);

            Dictionary<int, HashSet<int>> frequencyMap = new();

            for (int i = yMinStart; i <= yMaxStart; ++i)
            {
                result = GetStepRange(i, yLims);
                foreach(int step in result)
                {
                    if (!frequencyMap.ContainsKey(step))
                    {
                        frequencyMap[step] = new();
                    }
                    frequencyMap[step].Add(i);
                }
            }

            int possibilities = 0;

            int maxSteps = frequencyMap.Keys.Max();

            for (int i = xMinStart; i <= xMaxStart; ++i)
            {
                result = GetStepRange(i, xLims, maxSteps, true);
                HashSet<int> possibleYCombos = new();
                foreach (int step in result)
                {
                    if (frequencyMap.ContainsKey(step))
                    {
                        possibleYCombos.UnionWith(frequencyMap[step]);
                    }
                }
                possibilities += possibleYCombos.Count;
            }

            Console.WriteLine("{0} possible start trajectories", possibilities);
        }

        private static HashSet<int> GetStepRange(int startVelocity, (int a, int b) lims, int stepLimit = 1000, bool bStopAtZero = false)
        {
            HashSet<int> stepsSet = new();
            int vel = startVelocity;
            int pos = vel;
            int steps = 1;

            if (lims.b > lims.a)
            {
                while (pos <= lims.b && steps <= stepLimit)
                {
                    if (pos >= lims.a &&
                        pos <= lims.b)
                    {
                        stepsSet.Add(steps);
                    }

                    steps++;
                    if (!(vel == 0 && bStopAtZero))
                    {
                        vel--;
                    }
                    pos += vel;
                }
            }
            else
            {
                while (pos >= lims.b && steps < stepLimit)
                {
                    if (pos <= lims.a &&
                        pos >= lims.b)
                    {
                        stepsSet.Add(steps);
                    }

                    steps++;
                    vel--;
                    pos += vel;
                }
            }

            return stepsSet;
        }
    }
}
