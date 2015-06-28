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
    }
}
