
using System.Collections.Generic;

public static class ObjectExtensions
{
    public static Maybe<T> ToMaybe<T>(this T element)
    {
        return new Maybe<T>(element);
    }

    public static List<T> AsList<T>(this T element)
    {
        return new List<T>() { element };
    }
}