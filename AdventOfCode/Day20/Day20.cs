using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day20
    {
        public static void Solve()
        {
            string[] lines = File.ReadAllLines(@"Day20/input.txt");

            List<int> algorithm = lines.First().Select(x => Convert.ToInt32(x == '#')).ToList();

            List<List<int>> input = new();

            foreach (string l in lines.Skip(2))
            {
                input.Add(l.Select(x => Convert.ToInt32(x == '#')).ToList());
            }

            int steps = 50;
            for (int i = 0; i < steps; ++i)
            {
                input = ExpandInput(input, algorithm, i);

                if (i == 1) // Part 1
                {
                    PrintInput(input);
                }
            }

            PrintInput(input);
        }

        private static List<List<int>> ExpandInput(List<List<int>> input, List<int> algorithm, int step)
        {
            // expand input
            int[] arr = new int[input.Count];
            int initialValue = 0;
            if (algorithm.First() == 1)
            {
                initialValue = step % 2 == 1 ? algorithm.First() : algorithm.Last();
                arr = arr.Select(x => initialValue).ToArray();
            }
            input.Insert(0, new(arr));
            input.Insert(0, new(arr));
            input.Add(new(arr));
            input.Add(new(arr));

            foreach(var inner in input)
            {
                inner.Insert(0, initialValue);
                inner.Insert(0, initialValue);
                inner.Add(initialValue);
                inner.Add(initialValue);
            }

            List<List<int>> newInput = new();
            foreach(var inner in input)
            {
                newInput.Add(new(inner));
            }

            for (int i = 0; i < input.Count; ++i)
            {
                for ( int j = 0; j < input.Count; ++j)
                {
                    int pixelValue = 0;
                    int shiftCount = 8;
                    foreach (int k in new int[] { -1, 0, 1 })
                    {
                        foreach (int l in new int[] { -1, 0, 1 })
                        {
                            int xIdx = j + l;
                            int yIdx = i + k;

                            if (xIdx >= 0 &&
                                yIdx >= 0 &&
                                xIdx < input.Count &&
                                yIdx < input.Count)
                            {
                                pixelValue |= input[yIdx][xIdx] << shiftCount;
                            }
                            else
                            {
                                pixelValue |= initialValue << shiftCount;
                            }

                            --shiftCount;
                        }
                    }
                    newInput[i][j] = algorithm[pixelValue];
                }
            }

            return newInput;
        }

        private static void PrintInput(List<List<int>> input)
        {
            foreach (var inner in input)
            {
                Console.WriteLine(String.Join(' ', inner.Select(x => x == 1 ? '#' : '.')));
            }

            Console.WriteLine("{0} Lit Pixels", input.SelectMany(l => l.Where(x => x == 1)).Count());
        }
    }
}
