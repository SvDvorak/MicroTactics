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

    public static GameObject GetChild(this GameObject gameObject, string name, bool isPartialName = false)
    {
        return gameObject.GetChildren(name, isPartialName).FirstOrDefault();
    }

    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject, string expectedName, bool isPartialName = false)
    {
        return gameObject.transform
            .Cast<Transform>()
            .Where(x => MatchName(x.name, expectedName, isPartialName))
            .Select(x => x.gameObject);
    }

    private static bool MatchName(string actualName, string expectedName, bool isPartialName)
    {
        return isPartialName ? actualName.Contains(expectedName) : actualName == expectedName;
    }
}