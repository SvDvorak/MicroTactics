public struct Maybe<T> where T : class
{
    private readonly T _element;

    public Maybe(T element)
    {
        _element = element;
    }

    public static Maybe<T> Nothing { get { return new Maybe<T>(); } }

    public T Value { get { return _element; } }
    public bool HasValue { get { return _element != null; } }
}