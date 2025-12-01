using System;
using UnityEngine;

namespace TappyTale
{
    /// <summary>
    /// Centralised RNG with seed per run for reproducibility.
    /// </summary>
    public static class RNGService
    {
        static System.Random _random = new System.Random();

        public static void Reseed(int seed) => _random = new System.Random(seed);
        public static int NextInt(int min, int max) => _random.Next(min, max);
        public static float NextFloat() => (float)_random.NextDouble();

        /// <summary>
        /// Weighted choice from 0..weights.Length-1
        /// </summary>
        public static int WeightedIndex(params float[] weights)
        {
            float sum = 0f;
            for (int i = 0; i < weights.Length; i++) sum += Mathf.Max(0, weights[i]);
            float r = NextFloat() * sum;
            float a = 0f;
            for (int i = 0; i < weights.Length; i++)
            {
                a += Mathf.Max(0, weights[i]);
                if (r <= a) return i;
            }
            return weights.Length - 1;
        }
    }
}
