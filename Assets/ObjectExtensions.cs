public static class ObjectExtensions
{
    public static Maybe<T> ToMaybe<T>(this T element)
    {
        return new Maybe<T>(element);
    }
}