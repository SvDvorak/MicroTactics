using UnityEngine;

public class QuaternionClass
{
    public float x;
    public float y;
    public float z;
    public float w;

    public QuaternionClass() : this(0, 0, 0, 0) { }

    public QuaternionClass(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    #region Equality-methods
    protected bool Equals(QuaternionClass other)
    {
        return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
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
        return Equals((QuaternionClass)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = x.GetHashCode();
            hashCode = (hashCode * 397) ^ y.GetHashCode();
            hashCode = (hashCode * 397) ^ z.GetHashCode();
            hashCode = (hashCode * 397) ^ w.GetHashCode();
            return hashCode;
        }
    }
    #endregion
}