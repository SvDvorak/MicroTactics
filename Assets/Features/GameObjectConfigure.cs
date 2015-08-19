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