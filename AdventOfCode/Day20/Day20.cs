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
            string[] lines = File.ReadAllLines(@"Day20/test.txt");

            List<int> algorithm = lines.First().Select(x => Convert.ToInt32(x == '#')).ToList();

            int width = lines[2].Length;

            List<int> input = lines.Skip(2).SelectMany(l => l.Select(x => Convert.ToInt32(x == '#'))).ToList();

            PrintInput(input, width);
            input = ExpandInput(input, width, algorithm);
            PrintInput(input, width + 2);
            //input = ExpandInput(input, width + 2, algorithm);
            //PrintInput(input, width + 4);
        }

        private static List<int> ExpandInput(List<int> input, int width, List<int> algorithm)
        {
            int newWidth = width + 2;

            List<int> newInput = new();
            int newInputLength = input.Count + 4 * width + 4;

            for(int i = 0; i < newInputLength; ++i)
            {
                int newColumnIdx = i % newWidth;
                int newRowIdx = i / newWidth;

                int oldColumnIdx = newColumnIdx - 1;
                int oldRowIdx = newRowIdx - 1;

                int oldIdx = i - 1 - newWidth;
                //if (oldRowIdx >= 0 &&
                //    oldColumnIdx >= 0 &&
                //    oldRowIdx < width &&
                //    oldColumnIdx < width)
                //{
                //    oldIdx = 
                //}

                // Calculate new value
                int pixelVal = 0;
                int topIdx = oldIdx - newWidth;
                int botIdx = oldIdx + newWidth;
                int leftIdx = oldIdx - 1;
                int rightIdx = oldIdx + 1;
                int topLeft = topIdx - 1;
                int topRight = topIdx + 1;
                int botLeft = botIdx - 1;
                int botRight = botIdx + 1;
                List<int> indices = new();
                if (oldColumnIdx > 0 &&
                    oldRowIdx > 0)
                {
                    indices.Add(topLeft); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldRowIdx > 0)
                {
                    indices.Add(topIdx); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldColumnIdx < width - 1 &&
                    oldRowIdx > 0)
                {
                    indices.Add(topRight); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldColumnIdx > 0)
                {
                    indices.Add(leftIdx); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldColumnIdx >= 0 &&
                    oldColumnIdx < width &&
                    oldRowIdx >= 0 &&
                    oldRowIdx < width)
                {
                    indices.Add(oldIdx); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldColumnIdx < width - 1)
                {
                    indices.Add(rightIdx); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldColumnIdx > 0 &&
                    oldRowIdx < width - 1)
                {
                    indices.Add(botLeft); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldRowIdx < width - 1)
                {
                    indices.Add(botIdx); //
                }
                else
                {
                    indices.Add(-1); //
                }
                if (oldColumnIdx < width - 1 &&
                    oldRowIdx < width - 1)
                {
                    indices.Add(botRight); //
                }
                else
                {
                    indices.Add(-1); //
                }



                //indices.Add(topIdx); // 
                //indices.Add(topRight); // 
                //indices.Add(leftIdx); // 
                //indices.Add(oldIdx); // 
                //indices.Add(rightIdx); // 
                //indices.Add(botLeft); // 
                //indices.Add(botIdx); // 
                //indices.Add(botRight); // 
                for(int j = 0; j < indices.Count; ++j)
                {
                    int idxToCheck = indices[j];
                    if (idxToCheck >= 0 &&
                        idxToCheck < input.Count)
                    {
                        pixelVal |= input[idxToCheck] << (8 - j);
                    }
                }

                newInput.Add(algorithm[pixelVal]);
            }

            return newInput;
        }
        private static void PrintInput(List<int> input, int width)
        {
            for (int i = 0; i < width; ++i)
            {
                string line = "";
                for (int j = 0; j < width; ++j)
                {
                    line += input[i * width + j] == 1 ? '#' : '.';
                }
                Console.WriteLine(line);
            }

            Console.WriteLine("{0} Lit Pixels", input.Where(x => x == 1).Count());
        }
    }
}
