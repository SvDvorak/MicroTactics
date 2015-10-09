using Entitas;
using UnityEngine;

namespace Assets.Testing
{
    public class SpawnArrow : MonoBehaviour
    {
        private Entity _arrow;

        public void Start()
        {
            _arrow = SpawnHelper.Arrow(Pools.pool)
                .ReplacePosition(transform.position.ToV3());
        }

        public void OnDisable()
        {
            _arrow.IsDestroy(true);
        }
    }
}
