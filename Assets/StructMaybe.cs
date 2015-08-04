public struct StructMaybe<T> where T : struct
{
    private readonly T _element;
    private readonly bool _isSet;

    public StructMaybe(T element)
    {
        _element = element;
        _isSet = true;
    }

    public static StructMaybe<T> Nothing { get { return new StructMaybe<T>(); } } 

    public T Value { get { return _element; } }
    public bool HasValue { get { return _isSet; } }
}