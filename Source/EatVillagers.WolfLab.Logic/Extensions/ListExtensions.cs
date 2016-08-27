using System;
using System.Collections.Generic;

namespace EatVillagers.WolfLab.Logic.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random rng = new Random();

        /// <summary>
        /// Re-orders a list randomly.
        /// </summary>
        public static List<T> Shuffle<T>(this IList<T> list)
        {
            var result = new List<T>(list);

            int n = result.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = result[k];
                result[k] = result[n];
                result[n] = value;
            }

            return result;
        }

    }
}
