using UnityEngine;

namespace Assets
{
    public static class QuaternionExtensions
    {
        public static Quaternion ToQ(this QuaternionClass quaternion)
        {
            return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }
    }
}