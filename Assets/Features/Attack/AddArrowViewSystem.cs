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

            var rigidBody = arrowGameObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(entity.arrow.Force.ToUnityV3());

            arrowGameObject.AddComponent<CollisionPublisher>().SetEntity(entity);

            arrowGameObject.transform.SetParent(_arrowContainer);

            entity.AddView(arrowGameObject);
            entity.AddPhysics(rigidBody);
        }
    }
}