using System;
using System.Collections.Generic;
using System.Linq;
using Mono.GameMath;

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
        var vectors = source.ToList();
        if (!vectors.Any())
        {
            return new Vector3();
        }

        return vectors.Select(selector).Aggregate((x, y) => x + y);
    }

    public static Vector2 Sum(this IEnumerable<Vector2> source)
    {
        return source.Aggregate((x, y) => x + y);
    }

    public static Vector2 Sum<T>(this IEnumerable<T> source, Func<T, Vector2> selector)
    {
        var vectors = source.ToList();
        if (!vectors.Any())
        {
            return new Vector2();
        }

        return vectors.Select(selector).Aggregate((x, y) => x + y);
    }
}