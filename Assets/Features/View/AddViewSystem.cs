using System.Collections.Generic;
using Assets;
using Entitas;
using UnityEngine;

public class AddViewSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.Resource; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    private readonly Transform _viewsContainer = new GameObject("Views").transform;

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var resourceObject = Resources.Load<GameObject>(entity.resource.Name);
            if (resourceObject == null)
            {
                throw new MissingReferenceException("Resource " + entity.resource.Name + " not found.");
            }

            var view = Object.Instantiate(resourceObject);
            view.transform.SetParent(_viewsContainer);

            SetTransformIfAvailable(view, entity);
            AddAnimatorIfAvailable(view, entity);
            AddPhysicsIfAvailable(view, entity);

            entity.AddView(view);
        }
    }

    private static void SetTransformIfAvailable(GameObject view, Entity entity)
    {
        if (entity.hasPosition)
        {
            view.transform.position = entity.position.ToUnityV3();
        }
        if (entity.hasRotation)
        {
            view.transform.rotation = entity.rotation.ToUnityQ();
        }
    }

    private static void AddAnimatorIfAvailable(GameObject view, Entity entity)
    {
        var animator = view.GetComponent<Animator>();
        if (animator != null)
        {
            entity.AddAnimate(animator);
        }
    }

    private static void AddPhysicsIfAvailable(GameObject view, Entity entity)
    {
        var rigidbody = view.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            if (!rigidbody.isKinematic)
            {
                view.AddComponent<CollisionPublisher>().SetEntity(entity);
            }
            entity.AddPhysics(rigidbody);
        }
    }
}