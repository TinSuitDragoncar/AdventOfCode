using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day25
    {
        public static void Solve()
        {
            string[] lines = File.ReadAllLines(@"Day25/input.txt");

            int width = lines.First().Length;
            int height = lines.Count();

            List<char> input = lines.SelectMany(x => x).ToList();
            int steps = 0;
            bool bStopped = false;
            while(!bStopped)
            {
                steps++;
                bStopped = !Step(input, width, height);
            }

            Console.WriteLine("Stops moving after {0} steps", steps);
        }

        private static void PrintInput(List<char> input, int width, int step)
        {
            Console.WriteLine("Step {0}", step);
            for (int i = 0; i < input.Count; ++i)
            {
                Console.Write(input[i]);
                if (i % width == width - 1)
                {
                    Console.Write('\n');
                }
            }
        }

        private static bool Step(List<char> input, int width, int height)
        {
            bool bMoved = false;
            List<char> copy = new(input);

            // east loop
            for (int i = 0; i < input.Count; ++i)
            {
                if (copy[i] != '>')
                {
                    continue;
                }

                int x = i % width;
                int y = i / width;

                int newX = (x + 1) % width;

                int newIdx = (y * width) + newX;

                if (copy[newIdx] == '.')
                {
                    input[i] = '.';
                    input[newIdx] = '>';
                    bMoved = true;
                }
            }

            copy = new(input);

            // south loop
            for (int i = 0; i < input.Count; ++i)
            {
                if (copy[i] != 'v')
                {
                    continue;
                }

                int x = i % width;
                int y = i / width;

                int newY = (y + 1) % height;

                int newIdx = newY * width + x;

                if (copy[newIdx] == '.')
                {
                    input[i] = '.';
                    input[newIdx] = 'v';
                    bMoved = true;
                }
            }
            return bMoved;
        }
    }
}
