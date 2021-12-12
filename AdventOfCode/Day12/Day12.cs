using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day12
    {
        class Node
        {
            public Node parent { get; }
            public string Data { get; set; }
            public List<Node> Children { get; set; }

            public Node(string data, Node parent = null)
            {
                this.parent = parent;
                this.Data = data;
                Children = new List<Node>();
            }

            public bool ContainsData(string data)
            {
                Node searchNode = parent;
                while(searchNode != null)
                {
                    if (searchNode.Data == data)
                    {
                        return true;
                    }
                    else
                    {
                        searchNode = searchNode.parent;
                    }
                }

                return false;
            }
        }

        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day12/input.txt").ToList();

            Dictionary<string, List<string>> caveMap = new Dictionary<string, List<string>>();

            foreach (string l in lines)
            {
                string[] caves = l.Split('-');

                string startCave = caves[0];
                string endCave = caves[1];

                if (startCave != "end" && endCave != "start")
                {
                    if (!caveMap.ContainsKey(startCave))
                    {
                        caveMap.Add(startCave, new List<string>());
                    }

                    caveMap[startCave].Add(endCave);
                }


                if (endCave != "end" && startCave != "start")
                {
                    if (!caveMap.ContainsKey(endCave))
                    {
                        caveMap.Add(endCave, new List<string>());
                    }

                    caveMap[endCave].Add(startCave);
                }
            }

            Node root = new Node("start");
            HashSet<List<string>> uniquePaths = new HashSet<List<string>>();
            PopulateTree(root, caveMap, uniquePaths);
            foreach (List<string> path in uniquePaths)
            {
                Console.WriteLine("{0}", String.Join(',', path));
            }
            Console.WriteLine("{0} possible paths", uniquePaths.Count);
            
        }
        private static void PopulateTree(Node node, Dictionary<string, List<string>> caveMap, HashSet<List<string>> uniquePaths)
        {
            if (node.Data == "end")
            {
                List<string> pathTaken = new List<string>();
                Node searchNode = node;
                while(searchNode != null)
                {
                    pathTaken.Add(searchNode.Data);
                    searchNode = searchNode.parent;
                }
                pathTaken.Reverse();
                uniquePaths.Add(pathTaken);
                return;
            }

            List<string> possibleDirections;
            if (caveMap.TryGetValue(node.Data, out possibleDirections))
            {
                node.Children = possibleDirections.Where(x => Regex.IsMatch(x, "[A-Z]") || !node.ContainsData(x)).Select(x => new Node(x, node)).ToList();
                foreach(Node n in node.Children)
                {
                    PopulateTree(n, caveMap, uniquePaths);
                }
            }
        }
    }
}
