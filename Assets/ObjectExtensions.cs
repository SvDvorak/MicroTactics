
using System.Collections.Generic;

public static class ObjectExtensions
{
    public static StructMaybe<T> ToStructMaybe<T>(this T element) where T : struct 
    {
        return new StructMaybe<T>(element);
    }

    public static Maybe<T> ToMaybe<T>(this T element) where T : class
    {
        return new Maybe<T>(element);
    }

    public static List<T> AsList<T>(this T element)
    {
        return new List<T>() { element };
    }
}