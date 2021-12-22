
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

            int dd = 100;
            bool p1Win = false;
            bool p2Win = false;

            int p1Score = 0;
            int p2Score = 0;

            bool p1Turn = true;
            while(!(p1Win || p2Win))
            {
                int roll = Roll3(dd, out dd);
                if (p1Turn)
                {
                    p1Pos = (p1Pos + roll) % 10;
                    p1Score += p1Pos + 1;
                    p1Win = p1Score >= 1000;
                }
                else
                {
                    p2Pos = (p2Pos + roll) % 10;
                    p2Score += p2Pos + 1;
                    p2Win = p2Score >= 1000;
                }
                p1Turn = !p1Turn;
            }

            int losingScore = p1Win ? p2Score : p1Score;
            Console.WriteLine("{0} * {1} = {2}", losingScore, _rollCount, losingScore * _rollCount);
        }

        private static int _rollCount = 0;

        private static int Roll3(int start, out int end)
        {
            int sum = 0;
            for (int i = 0; i < 3; ++i)
            {
                ++start;
                start = start % 100;
                sum += start;
                ++_rollCount;
            }
            end = start;
            return sum;
        }
    }
}
