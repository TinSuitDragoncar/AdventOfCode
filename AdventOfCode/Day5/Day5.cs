using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public record Point(
        int x,
        int y
        );

    public class Line
    {
        public Point Start { get; init; }
        public Point End { get; init; }

        public Line(Point first, Point second)
        {
            Start = first;
            End = second;
        }

        public bool bVertical
        {
            get { return Start.x == End.x; }
        }

        public bool bHorizontal
        {
            get { return Start.y == End.y; }
        }

        public override string ToString()
        {

            return String.Format("Start: {0}, End: {1}", Start, End);
        }
    }

    class Day5
    {

        public static void Part1()
        {
            List<string> lines = File.ReadAllLines(@"Day5/input.txt").ToList();
            List<Line> squidLines = new List<Line>();

            foreach (string l in lines)
            {
                var points = l.Split("->");
                var first = points[0].Split(',').Select(x => Int32.Parse(x)).ToList();
                Point firstPoint = new(first[0], first[1]);
                var second = points[1].Split(',').Select(x => Int32.Parse(x)).ToList();
                Point secondPoint = new(second[0], second[1]);
                Line currentLine = new Line(firstPoint, secondPoint);

                if(currentLine.bHorizontal || currentLine.bVertical)
                {
                    squidLines.Add(currentLine);
                }
            }

            HashSet<Point> friendlySquids = new HashSet<Point>();
            HashSet<Point> unfriendlySquids = new HashSet<Point>();
            

            foreach(Line squid in squidLines)
            {
                if (squid.bHorizontal)
                {
                    int direction = (squid.End.x - squid.Start.x) > 0 ? 1 : -1;
                    for (int i = squid.Start.x; i != squid.End.x + direction; i += direction)
                    {
                        Point currentPoint = new Point(i, squid.Start.y);
                        
                        if (!friendlySquids.Contains(currentPoint))
                        {
                            friendlySquids.Add(currentPoint);
                        }
                        else
                        {
                            unfriendlySquids.Add(currentPoint);
                        }
                    }
                }
                else
                {
                    int direction = (squid.End.y - squid.Start.y) > 0 ? 1 : -1;
                    for (int i = squid.Start.y; i != squid.End.y + direction; i += direction)
                    {
                        Point currentPoint = new Point(squid.Start.x, i);

                        if (!friendlySquids.Contains(currentPoint))
                        {
                            friendlySquids.Add(currentPoint);
                        }
                        else
                        {
                            unfriendlySquids.Add(currentPoint);
                        }
                    }
                }
            }

            Console.WriteLine("Number of overlaps is {0}", unfriendlySquids.Count);




        }


    }
}
