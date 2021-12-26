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
        long[] As = { 1, 1, 1, 1, 26, 1, 26, 1, 26, 26, 1, 26, 26, 26};
        long[] Bs = { 11, 14, 10, 14, -8, 14, -11, 10, -6, -9, 12, -5, -4, -9};
        long[] Cs = { 7, 8, 16, 8, 3, 12, 1, 8, 8, 14, 4, 14, 15, 6};

        public static List<List<string>> GetChunks()
        {
            string[] lines = File.ReadAllLines(@"Day24/input.txt");

            //int dTemp = 0;
            //bool bCaptureNext = false;

            List<List<string>> chunks = new();

            foreach (string l in lines)
            {
                if (l.Contains("inp"))
                {
                    chunks.Add(new());
                }
                else
                {
                    chunks.Last().Add(l);
                }

                // if after 4th (idx 3) we have < 26 then get rid of

                //if (l == "add y w")
                //{
                //    bCaptureNext = true;
                //    continue;
                //}
                //else if (bCaptureNext)
                //{
                //    bCaptureNext = false;
                //    Match mq = Regex.Match(l, @"add y ([0-9-]+)");
                //    int q = Int32.Parse(mq.Groups[1].ToString());
                //    unknowns.Add((q, dTemp));
                //}

                //Match m = Regex.Match(l, @"div z ([0-9-]+)");

                //if (m.Success)
                //{
                //    dTemp = Int32.Parse(m.Groups[1].ToString());
                //}
            }

            return chunks;
        }

        public static bool TryDecodeZ(List<string> chunk, int wInput, HashSet<long> desiredValues, out List<long> validZ)
        {
            Dictionary<string, long> variables = new();
            validZ = new();

            // assume z cannot be greater than 1000
            for (int z = 0; z <= 32; z++)
            {
                variables["w"] = wInput;
                variables["x"] = 0; // x and y always set to 0
                variables["y"] = 0;
                variables["z"] = z;

                // perform logic
                foreach (string l in chunk)
                {
                    //DecodeLine(variables, l);
                }

                if (desiredValues.Contains(variables["z"]))
                {
                    validZ.Add(z);
                }
            }

            return validZ.Count > 0;
        }

        private static void DecodeLine(Dictionary<string, long> variables, string line)
        {
            Match m = Regex.Match(line, @"(\w+) (\w) ([-a-z0-9]+)");

            string op = m.Groups[1].ToString();
            string a = m.Groups[2].ToString();
            string b = m.Groups[3].ToString();

            bool bVariable = variables.ContainsKey(b);
            long valToUse = bVariable ? variables[b] : Int64.Parse(b);

            switch (op)
            {
                case "add":
                    variables[a] += valToUse;
                    break;

                case "mul":
                    variables[a] *= valToUse;
                    break;

                case "div":
                    variables[a] /= valToUse;
                    break;

                case "mod":
                    variables[a] %= valToUse;
                    break;

                case "eql":
                    variables[a] = Convert.ToInt32(variables[a] == valToUse);
                    break;
            }
        }

        public static void Solve()
        {
            var chunks = GetChunks();
            chunks.Reverse();

            //long modelNumber = DepthFirstSearch(chunks, 0, 0, 0, Int64.MinValue);

            HashSet<(long w, long z)> states = new();
            states.Add((0, 0));
            Dictionary<string, long> variables = new();
            for (int i = 0; i < chunks.Count; i++)
            {
                int idx = chunks.Count - 1 - i;

                HashSet<(long w, long z)> sCopy = new(states);
                states.Clear();

                foreach (var pair in sCopy)
                {
                    for (int w = 1; w <= 9; ++w)
                    {
                        // Try condition 1
                        long zStart = 

                        variables["w"] = w;
                        variables["x"] = 0;
                        variables["y"] = 0;
                        variables["z"] = pair.z;
                        foreach (string l in chunks[i])
                        {
                            DecodeLine(variables, l);
                        }

                        long mod = 26
                         if (i >= 3 &&
                            i < 13 &&
                            ((variables["z"] < 26 &&
                           variables["z"] > -26) ||)
                        {
                            continue;
                        }
                        else
                        {
                            lock (states)
                            {
                                long newW = pair.w + w * (long)Math.Pow(10, i);
                                states.Add((newW, variables["z"]));
                            }
                        }
                    }
                }
            }

            long modelNumber = states.Where(x => x.z == 0).Max(x => x.w);

            Console.WriteLine("Largest valid number is {0}", modelNumber);
        }

        private static long DepthFirstSearch(List<List<string>> chunks, int depth, long currentW, long currentZ, long currentBest)
        {
            if (currentW <= currentBest ||
                depth > 13)
            {
                return currentBest;
            }
            for (int w = 9; w >= 1; --w)
            {
                long newW = currentW + w * (long)Math.Pow(10, 13 - depth);
                Dictionary<string, long> variables = new();

                variables["w"] = w;
                variables["x"] = 0;
                variables["y"] = 0;
                variables["z"] = currentZ;
                foreach (string l in chunks[depth])
                {
                    DecodeLine(variables, l);
                }

                if (depth == 13 &&
                    variables["z"] == 0)
                {
                    return newW;
                }
                else if (depth >= 3 &&
                    variables["z"] == 0)
                {
                    continue;
                }
                else
                {
                    long newPotential = DepthFirstSearch(chunks, depth + 1, newW, variables["z"], currentBest);
                    if (newPotential > currentBest)
                    {
                        currentBest = newPotential;
                    }
                }
            }

            return currentBest;

        }
    }
}

// Last 4:
// zStart = (zEnd - w - q) / (26 / d)
// zEnd = zStart * (26 / d) + w + q
// All others