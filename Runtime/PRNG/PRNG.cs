using System;
using System.Security.Cryptography;
using System.Text;

namespace Planeted
{
    public static class PRNG
    {
        public static ulong FMix64(ulong h)
        {
            h ^= h >> 33;
            h *= 0xff51afd7ed558ccdUL;
            h ^= h >> 33;
            h *= 0xc4ceb9fe1a85ec53UL;
            h ^= h >> 33;

            return h;
        }

        public static ulong FNV64(string s)
        {
            const ulong offsetBasis = 14695981039346656037UL;
            const ulong prime = 1099511628211UL;

            ulong h = offsetBasis;

            foreach (byte b in Encoding.UTF8.GetBytes(s))
            {
                h ^= b;
                h *= prime;
            }
            return h;
        }

        public static ulong SplitMix64(ref ulong state)
        {
            state += 0x9e3779b97f4a7c15ul;

            ulong z = state;

            z = (z ^ (z >> 30)) * 0xbf58476d1ce4e5b9ul;
            z = (z ^ (z >> 27)) * 0x94d049bb133111ebul;

            return z ^ (z >> 31);
        }

        public static ulong RandomSeed()
        {
            byte[] bytes = new byte[8];
            RandomNumberGenerator.Fill(bytes);

            return BitConverter.ToUInt64(bytes, 0);
        }
        public static ulong StringToSeed64(string s)
        {
            return PRNG.FMix64(PRNG.FNV64(s));
        }
    }
}