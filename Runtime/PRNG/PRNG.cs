using System;
using System.Security.Cryptography;
using System.Text;

namespace Planeted
{
    public static class PRNG
    {  
        // MurmuHash finalizers, written by Austin Appleby:
        // https://github.com/aappleby/smhasher/blob/master/src/MurmurHash3.cpp
        public static uint FMix32(uint h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;

            return h;
        }
        public static ulong FMix64(ulong h)
        {
            h ^= h >> 33;
            h *= 0xff51afd7ed558ccdUL;
            h ^= h >> 33;
            h *= 0xc4ceb9fe1a85ec53UL;
            h ^= h >> 33;

            return h;
        }
       
        // FNV-1a hash functions:
        // https://en.wikipedia.org/wiki/Fowler-Noll-Vo_hash_function
        public static uint FNV32(string s)
        {
            uint h = 2166136261u; //FNV offset basis

            foreach (byte b in Encoding.UTF8.GetBytes(s))
            {
                h ^= b;
                h *= 16777619u; //FNV prime
            }
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

        // SplitMix64 by Sebastiano Vigna
        // https://prng.di.unimi.it/splitmix64.c
        public static ulong SplitMix64(ref ulong state)
        {
            state += 0x9e3779b97f4a7c15ul;

            ulong z = state;

            z = (z ^ (z >> 30)) * 0xbf58476d1ce4e5b9ul;
            z = (z ^ (z >> 27)) * 0x94d049bb133111ebul;

            return z ^ (z >> 31);
        }

        // Utils
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