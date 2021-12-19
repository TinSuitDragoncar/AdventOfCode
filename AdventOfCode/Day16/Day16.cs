﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day16
    {
        // Read version
        // read type ID
            // if Type ID 4 then is literal
            // read next few literal values (5 bits each, last packet starts with a 0)
            // concatenate the bits to get the literal value

            // else

            // Is operator packet
            // read next 1 bit
                // if 0 then the next 15 bits represents the total length of sub packets which will follow
                // if 1 then the next 11 bits indicate the number of sub packets which will follow
                // sub packets could either be operator or literal types

            
            
        enum State
        {
            Version,
            Type,
            LengthType,
            Length,
            LiteralPacket,
            SubPacket
        }

        enum TypeID
        {
            Literal,
            Operator
        }

        public static void Solve()
        {
            List<int> bits = File.ReadAllLines(@"Day16/test.txt").First().Select(x => Convert.ToInt32(x.ToString(), 16)).Select(x => Convert.ToString(x, 2).PadLeft(4, '0')).SelectMany(s => s.Select(c => c == '1' ? 1 : 0)).ToList();


            State state = State.Version;
            bool bLiteral = false;
            int idx = 0;
            int versionCount = 0;

            int literalVal = 0;
            int literalIdx = 0;

            bool bLengthType = false;
            int length = 0;
            while (idx < bits.Count)
            {
                switch(state)
                {
                    case State.Version:
                        versionCount += Sum(bits, idx, 3);
                        state = State.Type;
                        idx += 3;
                        break;

                    case State.Type:
                        int typeID = Sum(bits, idx, 3);
                        bLiteral = typeID == 4;
                        state = bLiteral ? State.LiteralPacket : State.LengthType;
                        idx += 3;
                        break;

                    case State.LiteralPacket:
                        bool bLastPacket = !Convert.ToBoolean(bits[idx]);

                        literalVal <<= 4;
                        literalVal |= Sum(bits, idx + 1, 4);
                        if (bLastPacket)
                        {
                            Console.WriteLine(literalVal);
                            literalVal = 0;
                            literalIdx = 0;

                            state = State.Version;
                        }
                        else
                        {
                            ++literalIdx;
                        }

                        idx += 5;
                        break;

                    case State.LengthType:
                        bLengthType = Convert.ToBoolean(bits[idx]);
                        state = State.Length;
                        length = 0;
                        ++idx;
                        break;

                    case State.Length:
                        length = bLengthType ? 11 : 15;
                        length = Sum(bits, idx, length);
                        idx += length;
                        state = State.SubPacket;
                        break;
                }
            }

            Console.WriteLine("Day 16 Part 1: Version count is: {0}", versionCount);
        }

        public static int _versionCount = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bits"></param>
        /// <returns>Packet size</returns>
        private static int EvaluatePacket(List<int> bits)
        {
            State state = State.Version;
            bool bLiteral = false;
            int idx = 0;

            int literalVal = 0;
            int literalIdx = 0;

            bool bLengthType = false;
            int length = 0;
            while (idx < bits.Count)
            {
                switch (state)
                {
                    case State.Version:
                        _versionCount += Sum(bits, idx, 3);
                        state = State.Type;
                        idx += 3;
                        break;

                    case State.Type:
                        int typeID = Sum(bits, idx, 3);
                        bLiteral = typeID == 4;
                        state = bLiteral ? State.LiteralPacket : State.LengthType;
                        idx += 3;
                        break;

                    case State.LiteralPacket:
                        bool bLastPacket = !Convert.ToBoolean(bits[idx]);

                        literalVal <<= 4;
                        literalVal |= Sum(bits, idx + 1, 4);
                        if (bLastPacket)
                        {
                            Console.WriteLine(literalVal);
                            literalVal = 0;
                            literalIdx = 0;

                            state = State.Version;
                        }
                        else
                        {
                            ++literalIdx;
                        }

                        idx += 5;
                        break;

                    case State.LengthType:
                        bLengthType = Convert.ToBoolean(bits[idx]);
                        state = State.Length;
                        length = 0;
                        ++idx;
                        break;

                    case State.Length:
                        length = bLengthType ? 11 : 15;
                        length = Sum(bits, idx, length);
                        idx += length;
                        state = State.SubPacket;
                        break;

                    case State.SubPacket:
                        int subLength = EvaluatePacket(new List<int>(bits.GetRange(idx, bits.Count - idx)));
                        if (bLengthType)
                        {
                            length--;
                        }
                        else
                        {
                            length -= subLength;
                        }
                        if (length == 0)
                        {
                            state = State.Version;
                        }
                        idx += subLength;
                        break;
                }
            }

            return idx;
        }

        private static int Sum(List<int> bits, int idx, int count)
        {
            int sum = 0;
            for (int i = 0; i < count; ++i)
            {
                sum |= bits[idx + i] << count - i - 1;
            }

            return sum;
        }
    }
}
