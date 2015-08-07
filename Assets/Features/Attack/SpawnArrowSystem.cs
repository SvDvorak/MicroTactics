using System.Collections.Generic;
using Assets;
using Entitas;
using UnityEngine;

public class SpawnArrowSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.FireArrow, Matcher.ArrowTemplate); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var spawnInfo = entity.fireArrow;
            var arrow = (GameObject)Object.Instantiate(entity.arrowTemplate.Template, spawnInfo.Position.ToUnityV3(), spawnInfo.Rotation.ToUnityQ());
            arrow.GetComponent<Rigidbody>().AddForce(spawnInfo.Force.ToUnityV3());
        }
    }
}