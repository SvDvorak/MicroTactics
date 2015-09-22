using Entitas;

namespace Assets.Features.Destruction
{
    public class DelayedDestroySystem : IExecuteSystem, ISetPool
    {
        private Group _delayedDestroyGroup;

        public void SetPool(Pool pool)
        {
            _delayedDestroyGroup = pool.GetGroup(Matcher.DelayedDestroy);
        }

        public void Execute()
        {
            foreach (var destroyEntity in _delayedDestroyGroup.GetEntities())
            {
                destroyEntity.ReplaceDelayedDestroy(destroyEntity.delayedDestroy.Frames - 1);

                if (destroyEntity.delayedDestroy.Frames == 0)
                {
                    destroyEntity.RemoveDelayedDestroy();
                    destroyEntity.RecursiveDestroy();
                }
            }
        }
    }
}