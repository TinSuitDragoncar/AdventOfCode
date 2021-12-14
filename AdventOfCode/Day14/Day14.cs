using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day14
    {
        public static void Solve()
        {
            List<string> lines = File.ReadAllLines(@"Day14/input.txt").ToList();


            string initialState = lines.First();
            lines.RemoveRange(0, 2);
            Console.WriteLine("Template: {0}", initialState);

            Dictionary<string, string> polyMap = lines.Select(x => x.Split("->", StringSplitOptions.TrimEntries)).ToDictionary(x => x[0], x => x[1]);
            const int steps = 10;
            for (int i = 0; i < steps; ++i)
            {
                initialState = Step(initialState, polyMap);
                //Console.WriteLine("After Step {0}: {1}", i + 1, initialState);
            }
            int minCount = Int32.MaxValue;
            int maxCount = Int32.MinValue;
            foreach (char c in initialState.Distinct())
            {
                int count = initialState.Where(x => x == c).ToList().Count;
                if (count < minCount)
                {
                    minCount = count;
                }
                else if (count > maxCount)
                {
                    maxCount = count;
                }
            }
            Console.WriteLine("Max {0} - Min {1} = {2}", maxCount, minCount, maxCount - minCount);
            
        }

        private static string Step(string input, Dictionary<string, string> polyMap)
        {
            string ret = input;
            int insertionOffset = 0;
            for (int i = 1; i < input.Count; ++i)
            {
                string sub = input.Substring(i - 1, 2);
                if (polyMap.TryGetValue(sub, out string  monomer))
                {
                    ret = ret.Insert(i + insertionOffset, monomer);
                    ++insertionOffset;
                }
            }
            return ret;
        }
    }
}
