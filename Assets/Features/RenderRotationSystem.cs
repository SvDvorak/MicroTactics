using Assets;
using Entitas;

public class RenderRotationSystem : IExecuteSystem, ISetPool
{
    private Group _renderables;

    public void SetPool(Pool pool)
    {
        _renderables = pool.GetGroup(Matcher.AllOf(Matcher.Rotation, Matcher.View));
    }

    public void Execute()
    {
        foreach (var e in _renderables.GetEntities())
        {
            var transform = e.view.GameObject.transform;
            transform.rotation = e.rotation.ToUnityQ();
        }
    }
}