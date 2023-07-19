using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BitSetIndex = System.Numerics.BigInteger;
namespace OpenRA.Primitives
{

    static class BitSetAllocator<T> where T : class
    {
        private static readonly Cache<string, BitSetIndex> Bits = new Cache<string, BitSetIndex>(Allocate);

        private static BitSetIndex nextBits = 1;
        static BitSetIndex Allocate(string value)
        {
            lock (Bits)
            {
                var bits = nextBits;
                nextBits <<= 1;
                return bits;
            }
        }

        public static BitSetIndex GetBits(string[] values)
        {
            BitSetIndex bits = 0;
            lock (Bits)
            {
                foreach (var value in values)
                {
                    bits |= Bits[value];
                }
            }
            return bits;
        }

        public static BitSetIndex GetBitsNoAlloc(string[] values)
        {
            BitSetIndex bits = 0;
            lock (Bits)
            {
                foreach (var value in values)
                {
                    if (Bits.TryGetValue(value,out var valueBit))
                    {
                        bits |= valueBit;
                    }
                }
            }
            return bits;
        }


        public static IEnumerable<string> GetStrings(BitSetIndex bits)
        {
            var values = new List<string>();
            lock (Bits)
            {
                foreach (var kvp in Bits)
                {
                    if ((kvp.Value & bits) != 0)
                    {
                        values.Add(kvp.Key);
                    }
                }
            }
            return values;
        }

        public static bool BitsContainString(BitSetIndex bits, string value)
        {
            BitSetIndex valueBit;
            lock (Bits)
            {
                if (!Bits.TryGetValue(value,out valueBit))
                {
                    return false;
                }
            }
            return (bits & valueBit) != 0;
        }
    }
    
    public readonly struct BitSet<T> : IEnumerable<string>,IEquatable<BitSet<T>> where T : class
    {
        private readonly BitSetIndex bits;

        public BitSet(params string[] values) : this(BitSetAllocator<T>.GetBits(values)){}

        public BitSet(BitSetIndex bits)
        {
            this.bits = bits;
        }

        public static BitSet<T> FromStringsNoAlloc(string[] values)
        {
            return new BitSet<T>(BitSetAllocator<T>.GetBitsNoAlloc(values)) { };
        }

        public override string ToString()
        {
            return BitSetAllocator<T>.GetStrings(bits).JoinWith(",");
        }

        public static bool operator ==(BitSet<T> me, BitSet<T> other)
        {
            return me.bits == other.bits;
        }
        public static bool operator !=(BitSet<T> me, BitSet<T> other)
        {
            return !(me == other);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return BitSetAllocator<T>.GetStrings(bits).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool Equals(BitSet<T> other)
        {
            return other == this;
        }

        public override bool Equals(object obj)
        {
            return obj is BitSet<T> bitSet && Equals(bitSet);
        }

        public override int GetHashCode()
        {
            return bits.GetHashCode();
        }

        public bool IsEmpty => bits == 0;

        public bool IsProperSubsetOf(BitSet<T> other)
        {
            return IsSubsetOf(other) && !SetEquals(other);
        }

        public bool IsProperSupersetOf(BitSet<T> other)
        {
            return IsSupersetOf(other) && !SetEquals(other);
        }

        public bool IsSubsetOf(BitSet<T> other)
        {
            return (bits | other.bits) == other.bits;
        }

        public bool IsSupersetOf(BitSet<T> other)
        {
            return (bits | other.bits) == bits;
        }

        public bool Overlaps(BitSet<T> other)
        {
            return (bits | other.bits) != 0;
        }

        public bool SetEquals(BitSet<T> other)
        {
            return bits == other.bits;
        }

        public bool Contains(string value)
        {
            return BitSetAllocator<T>.BitsContainString(bits, value);
        }

        public BitSet<T> Except(BitSet<T> other)
        {
            return new BitSet<T>(bits & ~ other.bits);
        }

        public BitSet<T> Intersect(BitSet<T> other)
        {
            return new BitSet<T>(bits & other.bits);
        }

        /// <summary>
        /// 对位求异或 也就是保存各自独有的位
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public BitSet<T> SymmetricExcept(BitSet<T> other)
        {
            return new BitSet<T>(bits ^ other.bits);
        }


        public BitSet<T> Union(BitSet<T> other)
        {
            return new BitSet<T>(bits | other.bits);
        }

    }

}
