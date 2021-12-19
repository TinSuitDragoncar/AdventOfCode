using System;
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
            Sum = 0,
            Product = 1,
            Min = 2,
            Max = 3,
            Literal = 4,
            GreaterThan = 5,
            LessThan = 6,
            Equal = 7
        }

        public static void Solve()
        {
            string[] lines = File.ReadAllLines(@"Day16/input.txt");
            foreach (string l in lines)
            {
                List<int> bits = l.Select(x => Convert.ToInt32(x.ToString(), 16)).Select(x => Convert.ToString(x, 2).PadLeft(4, '0')).SelectMany(s => s.Select(c => c == '1' ? 1 : 0)).ToList();
                _versionCount = 0;
                Console.WriteLine("Decoding {0}", l);
                EvaluatePacket(bits, out long finalValue);
                Console.WriteLine("Day 16 Part 1: Version count is: {0}", _versionCount);
                Console.WriteLine("Day 16 Part 2: Operation result is: {0}", finalValue);
            }
        }

        public static long _versionCount = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bits">bits to evaluate, each int should be 1 or 0</param>
        /// <param name="bSinglePacketOnly">If this flag is true the function exits after the first packet is evaluated</param>
        /// <returns>Packet size</returns>
        private static int EvaluatePacket(List<int> bits, out long opVal, bool bSinglePacketOnly = false)
        {
            opVal = 0;

            State state = State.Version;
            int idx = 0;

            long literalVal = 0;
            int literalIdx = 0;

            bool bPacketFound = false;

            bool bLengthType = false;
            long length = 0;

            TypeID t = TypeID.Sum;

            List<long> operatorParams = new();

            while (idx < bits.Count)
            {
                if (state == State.Version &&
                    bPacketFound &&
                    bSinglePacketOnly)
                {
                    break;
                }
                switch (state)
                {
                    case State.Version:
                        bPacketFound = true;
                        // minimum packet size is 11 so if size is smaller than that we've reached the end of the packet
                        if (bits.Count - idx >= 11)
                        {
                            long ver = Sum(bits, idx, 3);
                            Console.WriteLine("Version: {0}", ver);
                            _versionCount += ver;
                            state = State.Type;
                            idx += 3;
                        }
                        else
                        {
                            return idx;
                        }
                        
                        break;

                    case State.Type:
                        t = (TypeID) Sum(bits, idx, 3);
                        state = t == TypeID.Literal ? State.LiteralPacket : State.LengthType;
                        idx += 3;
                        break;

                    case State.LiteralPacket:
                        bool bLastPacket = !Convert.ToBoolean(bits[idx]);

                        literalVal <<= 4;
                        literalVal |= Sum(bits, idx + 1, 4);
                        if (bLastPacket)
                        {
                            Console.WriteLine("Literal Value: {0}", literalVal);
                            if (bSinglePacketOnly)
                            {
                                opVal = literalVal;
                            }   
                            else
                            {
                                literalVal = 0;
                                literalIdx = 0;
                            }
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
                        int lengthLength = bLengthType ? 11 : 15;
                        length = Sum(bits, idx, lengthLength);
                        idx += lengthLength;
                        state = State.SubPacket;
                        break;

                    case State.SubPacket:
                        int subLength = EvaluatePacket(new List<int>(bits.GetRange(idx, bits.Count - idx)), out long newOpVal, true);
                        operatorParams.Add(newOpVal);

                        if (bLengthType)
                        {
                            length--;
                        }
                        else
                        {
                            length -= subLength;
                        }
                        if (length <= 0)
                        {
                            opVal = EvaluateOpVal(operatorParams, t);
                            state = State.Version;
                        }
                        idx += subLength;
                        break;
                }
            }

            return idx;
        }

        private static long EvaluateOpVal(List<long> operatorParams, TypeID t)
        {
            long result = -1;
            if (operatorParams.Count == 1)
            {
                return operatorParams.First();
            }

            switch(t)
            {
                case TypeID.Sum:
                    result = operatorParams.Sum();
                    break;

                case TypeID.Product:
                    result = operatorParams.Aggregate((total, next) => total * next);
                    break;

                case TypeID.Min:
                    result = operatorParams.Min();
                    break;
                case TypeID.Max:
                    result = operatorParams.Max();
                    break;
                case TypeID.GreaterThan:
                    result = operatorParams[0] > operatorParams[1] ? 1 : 0;
                    break;
                case TypeID.LessThan:
                    result = operatorParams[0] < operatorParams[1] ? 1 : 0;
                    break;
                case TypeID.Equal:
                    result = operatorParams[0] == operatorParams[1] ? 1 : 0;
                    break;
            }
            return result;
        }

        private static long Sum(List<int> bits, int idx, int count)
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
