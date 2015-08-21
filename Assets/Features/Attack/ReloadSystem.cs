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
                var newFramesLeft = reloadable.reload.FramesLeft - 1;
                if (newFramesLeft == 0)
                {
                    reloadable.RemoveReload();
                }
                else
                {
                    reloadable.ReplaceReload(newFramesLeft);
                }
            }
        }
    }
}