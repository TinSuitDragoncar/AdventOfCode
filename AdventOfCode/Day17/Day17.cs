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
            string input = File.ReadLines(@"Day17/test.txt").First();

            Match xM = Regex.Match(input, @"x=([0-9-]+)..([0-9-]+)");
            Match yM = Regex.Match(input, @"y=([0-9-]+)..([0-9-]+)");
            (int a, int b) xLims = (Int32.Parse(xM.Groups[1].ToString()), Int32.Parse(xM.Groups[2].ToString()));
            (int a, int b) yLims = (Int32.Parse(yM.Groups[2].ToString()), Int32.Parse(yM.Groups[1].ToString()));

            int xMinStart = (int) Math.Sqrt(2 * xLims.a);
            int xMaxStart = xLims.b;

            int yMinStart = yLims.b;
            int yMaxStart = yLims.b * - 1 - 1; // max start trajectory will be -yMax - 1 -> when the projectile comes back down and passes 0 its velocity will be yStart + 1

            int maxHeight = yMaxStart * (yMaxStart + 1) / 2;

            Console.WriteLine("Max height is {0}", maxHeight);

            Dictionary<int, int> frequencyMap = new();

            int xRange = xMaxStart - xMinStart;
            int yRange = yMaxStart - yMinStart;

            for (int i = 0; i < yRange; ++i)
            {
                // solve s=ut + 0.5at^2
                double a = -0.5;
                double startVelocity = yMinStart + i;

                int minSteps = (int) Math.Ceiling(QuadSolve(a, startVelocity, -yLims.a));
                int maxSteps = (int)Math.Floor(QuadSolve(a, startVelocity, -yLims.b));

                if (maxSteps < minSteps)
                {
                    maxSteps = minSteps;
                }

                for (int j = minSteps; j <= maxSteps; ++j)
                {
                    if (!frequencyMap.ContainsKey(j))
                    {
                        frequencyMap[j] = 0;
                    }
                    frequencyMap[j]++;
                }
            }

            int possibilities = 0;

            for (int i = 0; i < xRange; ++i)
            {
                // solve s=ut + 0.5at^2
                double a = 0.5 * -1;
                double startVelocity = xMinStart + i + 1;

                int minSteps = (int)Math.Ceiling(QuadSolve(a, startVelocity, -xLims.a));
                double temp = QuadSolve(a, startVelocity, -xLims.b);
                int maxSteps;
                if (Double.IsNaN(temp))
                {
                    maxSteps = frequencyMap.Max(o => o.Key);
                }
                else
                {
                    maxSteps = (int) temp;
                }

                if (maxSteps == Int32.MinValue)
                {
                    maxSteps = minSteps + 1000;
                }
                else if (maxSteps < minSteps)
                {
                    maxSteps = minSteps;
                }

                for (int j = minSteps; j <= maxSteps; ++j)
                {
                    if (frequencyMap.ContainsKey(j))
                    {
                        possibilities += frequencyMap[j];
                    }
                }
            }

            

            Console.WriteLine("{0} possible start trajectories", possibilities);
        }

        private static double QuadSolve(double a, double b, double c)
        {
            double other = Math.Sqrt(b * b - 4 * a * c);

            double ret1 = (-b + other) / (2 * a);
            double ret2 = (-b - other) / (2 * a);

            // Only the pos result is valid here
            return Math.Max(ret1, ret2);
        }
    }
}
