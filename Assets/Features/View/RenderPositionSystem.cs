using System.Collections.Generic;
using Assets;
using Entitas;
using UnityEngine;

public class RenderPositionSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.View).OnEntityAdded(); } }


    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var transform = e.view.Value.transform;

            var newPosition = e.position.ToUnityV3();
            var rigidbody = transform.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.MovePosition(newPosition);
            }
            else
            {
                transform.position = newPosition;
            }
        }
    }
}
