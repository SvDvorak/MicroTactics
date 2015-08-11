using Assets;
using Entitas;

public class RenderPositionSystem : IExecuteSystem, ISetPool
{
    private Group _renderables;

    public void SetPool(Pool pool)
    {
        _renderables = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.View));
    }

    public void Execute()
    {
        foreach (var e in _renderables.GetEntities())
        {
            var transform = e.view.GameObject.transform;
            transform.position = e.position.ToUnityV3();
        }
    }
}
