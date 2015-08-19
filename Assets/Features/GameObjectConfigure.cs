using System.Collections.Generic;
using Entitas;
using UnityEngine;

public interface IEntityConfigurer
{
    void OnConfigureEntity(Entity entity);
}

public static class GameObjectConfigure
{
    private static readonly List<IEntityConfigurer> CachedEntityList;
    static GameObjectConfigure()
    {
        CachedEntityList = new List<IEntityConfigurer>(16);
    }

    public static void ConfigureGameObject(GameObject unityObject, Entity entity)
    {
        CachedEntityList.Clear();
        unityObject.GetComponents(CachedEntityList);
        foreach (var t in CachedEntityList)
        {
            t.OnConfigureEntity(entity);
        }
    }
}

public class ConfigurationLoader : MonoBehaviour
{
    public void Start()
    {
        GameObjectConfigure.ConfigureGameObject(gameObject, Pools.pool.CreateEntity());
    }
}

public interface ISpawnUnitCommand
{
    void Spawn(Entity unit);
    void Despawn(Entity unit);
}

public class SpawnUnitCommand : ISpawnUnitCommand
{
    private readonly Transform _unitsContainer = new GameObject("Units").transform;

    public void Spawn(Entity unit)
    {
        var selectedIndicatorResource = Resources.Load<GameObject>(Res.SelectedIndicator);
        var selectedIndicatorView = Object.Instantiate(selectedIndicatorResource);
        var selectedIndicator = Pools.pool.CreateEntity()
            .AddView(selectedIndicatorView);

        GameObjectConfigure.ConfigureGameObject(selectedIndicatorView, selectedIndicator);

        var unitResource = Resources.Load<GameObject>(Res.Unit);
        var unitView = Object.Instantiate(unitResource);
        unit
            .AddView(unitView)
            .AddAnimate(unitView.GetComponent<Animator>());

        GameObjectConfigure.ConfigureGameObject(unitView, unit);

        selectedIndicatorView.transform.SetParent(unitView.transform);
        unitView.transform.SetParent(_unitsContainer);
    }

    public void Despawn(Entity unit)
    {
        unit.IsDestroy(true);
        unit.child.Value.IsDestroy(true);
    }
}