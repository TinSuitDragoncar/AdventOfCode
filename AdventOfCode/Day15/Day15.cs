using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day15
    {
         

        public static void Solve()
        {
            List<string> lines = File.ReadAllLines(@"Day15/input.txt").ToList();

            int width = lines.First().Count();
            List<int> input = lines.SelectMany(x => x.ToArray().Select(y => Int32.Parse(y.ToString()))).ToList();

            int minRisk = Dijkstra(0, input.Count - 1, input, width);
            Console.WriteLine("Day 15 Part 1: {0}", minRisk);

            minRisk = Dijkstra(0, input.Count - 1, input, width);
            Console.WriteLine("Day 15 Part 2: {0}", minRisk);
        }

        
        private static int Dijkstra(int start, int target, List<int> input, int width)
        {
            List<int> dist = new();
            List<int> prev = new();
            dist.Add(0);
            prev.Add(start);
            PriorityQueue<int, int> Q = new();
            Q.Enqueue(0, input[0]);
            for (int i = 0; i < input.Count; ++i)
            {
                dist.Add(Int32.MaxValue);
                prev.Add(-1);
            }

            while (Q.Count > 0)
            {
                int u = Q.Dequeue();
                foreach (int v in GetNeighbours(u, width, input.Count / width))
                {
                    int alt = dist[u] + input[v];
                    if (v == target)
                    {
                        return alt;
                    }
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                        Q.Enqueue(v, alt);
                    }
                }
            }

            return -1;
        }

        private static List<int> GetNeighbours(int idx, int width, int height)
        {
            List<int> ret = new();
            int xIdx = idx % width;
            int yIdx = idx / width;

            if (xIdx > 0)
            {
                ret.Add(idx - 1); // left
            }
            if (xIdx < width - 1)
            {
                ret.Add(idx + 1); // right
            }
            if (yIdx > 0)
            {
                ret.Add(idx - width); // up
            }
            if (yIdx < height - 1)
            {
                ret.Add(idx + width); // down
            }
            return ret;
        }
    }
}
