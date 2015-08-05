using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class VectorExtensions
    {
        public static Vector3 ZeroY(this Vector3 vector)
        {
            return new Vector3(vector.x, 0, vector.z);
        }

        public static Vector3 SetX(this Vector3 vector, float value)
        {
            return new Vector3(value, vector.y, vector.z);
        }

        public static Vector3 SetY(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, value, vector.z);
        }

        public static Vector3 SetZ(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, vector.y, value);
        }



        public static Vector3 ToUnityV3(this Mono.GameMath.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Vector3 ToUnityV3(this VectorClass vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        public static Mono.GameMath.Vector3 ToV3(this VectorClass vector)
        {
            return new Mono.GameMath.Vector3(vector.x, vector.y, vector.z);
        }
    }
}
