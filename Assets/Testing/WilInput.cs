using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IInput
{
    bool GetMouseButtonDown(int button);
    bool GetMouseButtonUp(int button);
    IEnumerable<RayHit> RaycastFromMousePosition();
}

public class WilInput
{
    private static IInput _instance;

    public static IInput Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UnityInput();
            }
            return _instance;
        }
        set { _instance = value; }
    }
}

public class UnityInput : IInput
{
    public bool GetMouseButtonDown(int button)
    {
        return Input.GetMouseButtonDown(button);
    }

    public bool GetMouseButtonUp(int button)
    {
        return Input.GetMouseButtonUp(button);
    }

    public IEnumerable<RayHit> RaycastFromMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastAll(ray, Mathf.Infinity).Select(x => new RayHit(x));
    }
}

public class TestInput : IInput
{
    private readonly List<RayHit> _hits = new List<RayHit>();

    public bool GetMouseButtonDown(int button)
    {
        return true;
    }

    public bool GetMouseButtonUp(int button)
    {
        return true;
    }

    public IEnumerable<RayHit> RaycastFromMousePosition()
    {
        return _hits;
    }

    public void AddGameObjectClick(GameObject gameObject)
    {
        _hits.Add(new RayHit(gameObject.transform, new Vector3()));
    }
}

public struct RayHit
{
    public RayHit(Transform transform, Vector3 point)
    {
        Transform = transform;
        Point = point;
    }

    public RayHit(RaycastHit hit)
    {
        Transform = hit.transform;
        Point = hit.point;
    }

    public Transform Transform { get; private set; }
    public Vector3 Point { get; private set; }
}