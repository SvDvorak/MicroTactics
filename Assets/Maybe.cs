public struct Maybe<T>
{
    private readonly T _element;
    private readonly bool _isSet;

    public Maybe(T element)
    {
        _element = element;
        _isSet = true;
    }

    public static Maybe<T> Nothing { get { return new Maybe<T>(); } } 

    public T Value { get { return _element; } }
    public bool HasValue { get { return _isSet; } }
}