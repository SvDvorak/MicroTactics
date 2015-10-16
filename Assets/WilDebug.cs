using UnityEngine;

public static class WilDebug
{
    public static void DrawQuaternion(Vector3 position, Quaternion quaternion, Color color, float duration)
    {
        Debug.DrawLine(position, position + quaternion*Vector3.forward*3, color, duration);
    }
}