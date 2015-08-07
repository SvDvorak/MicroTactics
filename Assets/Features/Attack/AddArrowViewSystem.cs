using System.Collections.Generic;
using Assets;
using Entitas;
using UnityEngine;

public class AddArrowViewSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Arrow, Matcher.ArrowTemplate); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    private readonly Transform _arrowContainer = new GameObject("Arrows").transform;

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var arrowGameObject = (GameObject)Object.Instantiate(
                entity.arrowTemplate.Template,
                entity.arrow.Position.ToUnityV3(),
                entity.arrow.Rotation.ToUnityQ());

            arrowGameObject
                .GetComponent<Rigidbody>()
                .AddForce(entity.arrow.Force.ToUnityV3());

            arrowGameObject.transform.SetParent(_arrowContainer);
        }
    }
}