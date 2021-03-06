using System.Collections.Generic;
using Assets;
using Entitas;
using UnityEngine;

public class AddViewSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.Resource.OnEntityAdded(); } }


    private readonly Transform _viewsContainer;

    public AddViewSystem()
    {
        var existingViewContainer = GameObject.Find("Views");
        _viewsContainer = existingViewContainer != null ? existingViewContainer.transform : new GameObject("Views").transform;
    }

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
            AddParentIfAvailable(view, entity);
            AddAnimatorIfAvailable(view, entity);
            AddColliderIfAvailable(view, entity);

            entity.AddView(view);
            GameObjectConfigurer.AttachEntity(view, entity);
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

    private static void AddParentIfAvailable(GameObject view, Entity entity)
    {
        if (entity.hasParent && entity.parent.Value.hasView)
        {
            var parentView = entity.parent.Value.view;
            view.transform.SetParent(parentView.Value.transform, false);
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

    private static void AddColliderIfAvailable(GameObject view, Entity entity)
    {
        var collider = view.GetComponent<Collider>();
        if (collider != null)
        {
            view.AddComponent<Collidable>().SetEntity(entity);
        }
    }
}