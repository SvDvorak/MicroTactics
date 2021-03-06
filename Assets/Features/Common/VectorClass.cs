﻿using Entitas;
using Mono.GameMath;

public class VectorClass
{
    public float x;
    public float y;
    public float z;

    public VectorClass() : this(0, 0, 0) { }

    public VectorClass(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    #region Equality-methods
    protected bool Equals(VectorClass other)
    {
        return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj.GetType() != this.GetType())
        {
            return false;
        }
        return Equals((VectorClass)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = x.GetHashCode();
            hashCode = (hashCode * 397) ^ y.GetHashCode();
            hashCode = (hashCode * 397) ^ z.GetHashCode();
            return hashCode;
        }
    }
    #endregion    
}