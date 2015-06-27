using System;
using System.Collections.Generic;
using UnityEngine;

public class Formation
{
    private readonly float _columnsToRowsRatio;
    private readonly List<Unit> _units;

    public Formation(float columnsToRowsRatio)
    {
        _columnsToRowsRatio = columnsToRowsRatio;
        _units = new List<Unit>();
    }

    public void Remove(Unit unit)
    {
    }

    public void Add(Unit unit)
    {
        _units.Add(unit);
        unit.Position = GetPositionForUnit();
    }

    private Vector3Adapter GetPositionForUnit()
    {
        var growVector = new Vector3(1, _columnsToRowsRatio);
        var rows = Math.Sqrt(_units.Count / _columnsToRowsRatio);
        var columns = rows * _columnsToRowsRatio;

        return new Vector3Adapter((int)columns - 1, (int)rows - 1, 0);
    }
}

public class Unit
{
    public Vector3Adapter Position;

    public Unit(Vector3Adapter position)
    {
        Position = position;
    }
}

public struct Vector3Adapter
{
    private readonly Vector3 _vector;

    public float x { get { return _vector.x; } }
    public float y { get { return _vector.y; } }
    public float z { get { return _vector.z; } }

    public Vector3Adapter(Vector3 vector)
    {
        _vector = vector;
    }

    public Vector3Adapter(float x, float y, float z) : this(new Vector3(x, y, z))
    {
    }

    public static implicit operator Vector3Adapter(Vector3 vector)
    {
        return new Vector3Adapter(vector);
    }

    public bool Equals(Vector3Adapter other)
    {
        return _vector.Equals(other._vector);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is Vector3Adapter && Equals((Vector3Adapter) obj);
    }

    public override int GetHashCode()
    {
        return _vector.GetHashCode();
    }
}