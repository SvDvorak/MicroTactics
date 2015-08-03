using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public static Vector3 Sum(this IEnumerable<Vector3> source)
    {
        return source.Aggregate((x, y) => x + y);
    }

    public static Vector3 Sum<T>(this IEnumerable<T> source, Func<T, Vector3> selector)
    {
        return source.Select(selector).Aggregate((x, y) => x + y);
    }
}