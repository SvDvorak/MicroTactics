using System;

public static class FloatExtensions
{
    public static bool IsApproximately(this float a, float b)
    {
        return a.IsApproximately(b, float.Epsilon);
    }

    public static bool IsApproximately(this float a, float b, float tolerance)
    {
        return Math.Abs(a - b) < tolerance;
    }
}