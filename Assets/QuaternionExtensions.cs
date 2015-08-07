using Mono.GameMath;

namespace Assets
{
    public static class QuaternionExtensions
    {
        public static Quaternion ToQ(this QuaternionClass quaternion)
        {
            return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        public static UnityEngine.Quaternion ToUnityQ(this Quaternion quaternion)
        {
            return new UnityEngine.Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
        }
    }
}