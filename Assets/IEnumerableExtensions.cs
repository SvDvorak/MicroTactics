using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class IEnumerableExtensions
    {
        public static T Second<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Skip(1).First();
        }

        public static T Third<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Skip(2).First();
        }

        public static T Fourth<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Skip(3).First();
        }
    }
}
