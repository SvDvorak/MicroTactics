using Assets;
using UnityEngine;
using Quaternion = Mono.GameMath.Quaternion;

public class ClickFireArrow : MonoBehaviour
{
#if (UNITY_EDITOR)
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            SpawnHelper.Arrow(Pools.pool)
                .ReplacePosition(ray.origin.ToV3())
                .ReplaceForce(ray.direction.ToV3()*1000)
                .ReplaceRotation(Quaternion.LookAt(ray.direction.ToV3()));
        }
    }
#endif
}