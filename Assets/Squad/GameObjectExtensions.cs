using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static IEnumerable<T> GetComponentsInChildren<T>(this GameObject gameObject, string name)
        where T : MonoBehaviour
    {
        var allComponents = gameObject.GetComponentsInChildren<T>();
        return allComponents.Where(x => x.name == name);
    }

    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject, string name)
    {
        return gameObject.transform
            .Cast<Transform>()
            .Where(x => x.name == name)
            .Select(x => x.gameObject);
    }
}