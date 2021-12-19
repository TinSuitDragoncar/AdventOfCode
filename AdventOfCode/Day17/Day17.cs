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

            Match xM = Regex.Match(input, "x=(\d+)..(\d+)");
            Match yM = Regex.Match(input, "y=(\d+)..(\d+)");
            (int a, int b) xLims = (Int32.Parse(xM.Groups[1].ToString()), Int32.Parse(xM.Groups[2].ToString()));
            (int a, int b) yLims = (Int32.Parse(yM.Groups[1].ToString()), Int32.Parse(yM.Groups[2].ToString()));

            int xMin = 
        }
    }
}
