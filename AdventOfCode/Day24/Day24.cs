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
        public static void Solve()
        {
            // arrays determined from input, see test.txt for input
            long[] As = { 1, 1, 1, 1, 26, 1, 26, 1, 26, 26, 1, 26, 26, 26 };
            long[] Bs = { 11, 14, 10, 14, -8, 14, -11, 10, -6, -9, 12, -5, -4, -9 };
            long[] Cs = { 7, 8, 16, 8, 3, 12, 1, 8, 8, 14, 4, 14, 15, 6 };

            HashSet<(long w, long z)> states = new();
            states.Add((0, 0));
            Dictionary<string, long> variables = new();
            for (int i = 0; i < As.Count(); i++)
            {
                int idx = As.Count() - 1 - i;

                HashSet<(long w, long z)> sCopy = new(states);
                states.Clear();

                foreach (var pair in sCopy)
                {
                    for (int w = 9; w >= 1; --w)
                    {
                        long newW = pair.w + w * (long)Math.Pow(10, i);

                        long zEnd = pair.z;
                        long A = As[idx];
                        long B = Bs[idx];
                        long C = Cs[idx];

                        // see test.txt for reasoning here
                        long q = zEnd - w - C;
                        long zStart = q / 26 * A;
                        if (q % 26 == 0)
                        {
                            states.Add((newW, zStart));
                        }

                        if (0 <= w - B &&
                            w - B < 26)
                        {
                            long z0 = zEnd * A + w - B;
                            states.Add((newW, z0));
                        }
                    }
                }
            }

            Console.WriteLine("Largest valid number is {0}", states.Max(x => x.w));
            Console.WriteLine("Smallest valid number is {0}", states.Min(x => x.w));
        }
    }
}
