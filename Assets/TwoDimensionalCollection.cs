﻿using System;
using System.Collections.Generic;
using System.Linq;

public interface ITwoDimensionalCollection<TValue> where TValue : class
{
    TValue this[int x, int y] { get; set; }
}

public class TwoDimensionalCollection<TValue> : ITwoDimensionalCollection<TValue> where TValue : class
{
    private readonly Dictionary<int, Dictionary<int, TValue>> _collection;

    public TwoDimensionalCollection()
    {
        _collection = new Dictionary<int, Dictionary<int, TValue>>();
    }

    public TValue this[int x, int y]
    {
        get
        {
            if (!_collection.ContainsKey(x) || !_collection[x].ContainsKey(y))
            {
                return null;
            }

            return _collection[x][y];
        }
        set
        {
            if (!_collection.ContainsKey(x))
            {
                _collection[x] = new Dictionary<int, TValue>();
            }

            _collection[x][y] = value;
        }
    }

    public bool IsEmpty { get { return _collection.Count == 0; } }

    public void Clear()
    {
        _collection.Clear();
    }

    public void Iterate(Action<TValue> action)
    {
        foreach (var item in GetAllValues())
        {
            action(item);
        }
    }

    public IEnumerable<TValue> GetAllValues()
    {
        return _collection.Values.SelectMany(subCollection => subCollection.Values);
    }
}