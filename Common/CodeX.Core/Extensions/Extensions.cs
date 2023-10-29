using System.Collections.Generic;
using System.Linq;

namespace CodeX.Core.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// All factorials for a given number, except 1 and itself,
        /// as any number is evenly divisible by that.
        /// </summary>
        /// <param name="x">An unsigned integer</param>
        /// <returns>All factors, except 1 and itself</returns>
        public static IEnumerable<uint> Factors(this uint x)
        {
            for (uint i = 2; i * i <= x; i++)
            {
                if (x % i == 0)
                {
                    yield return i;
                    if (i != x / i)
                    {
                        yield return x / i;
                    }
                }
            }
        }

        public static IEnumerable<string> Split(this string value, int chunkSize)
        {
            return Enumerable
                .Range(0, value.Length / chunkSize)
                .Select(i => value.Substring(i * chunkSize, chunkSize));
        }
    }
}