using Entitas;

namespace Assets.Features.Attack
{
    public class ReloadSystem : IExecuteSystem, ISetPool
    {
        private Group _reloadables;

        public void SetPool(Pool pool)
        {
            _reloadables = pool.GetGroup(Matcher.Reload);
        }

        public void Execute()
        {
            foreach (var reloadable in _reloadables.GetEntities())
            {
                reloadable.ReplaceReload(reloadable.reload.FramesLeft - 1);

                if (reloadable.reload.FramesLeft == 0)
                {
                    reloadable.RemoveReload();
                }
            }
        }
    }
}