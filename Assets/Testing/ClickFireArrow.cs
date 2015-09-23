using Assets;
using UnityEngine;
using Quaternion = Mono.GameMath.Quaternion;

public class ClickFireArrow : MonoBehaviour
{
    private void Start()
    {
    }

#if (UNITY_EDITOR)
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            SpawnHelper.SpawnArrow(Pools.pool)
                .AddPosition(ray.origin.ToV3())
                .AddForce(ray.direction.ToV3()*1000)
                .AddRotation(Quaternion.LookAt(ray.direction.ToV3()));
        }
    }
#endif
}