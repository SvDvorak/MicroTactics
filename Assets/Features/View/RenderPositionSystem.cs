using System.Collections.Generic;
using Assets;
using Entitas;

public class RenderPositionSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.View).OnEntityAdded(); } }


    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var newPosition = e.position.ToUnityV3();
            if (e.hasPhysics)
            {
                e.physics.RigidBody.MovePosition(newPosition);
            }
            else
            {
                e.view.Value.transform.position = newPosition;
            }
        }
    }
}
