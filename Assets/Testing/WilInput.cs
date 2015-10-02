using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IInput
{
    MouseState GetMouseState(int button);
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

    public MouseState GetMouseState(int button)
    {
        return new MouseState(GetMouseButtonState(button), RaycastFromMousePosition());
    }

    private InputState GetMouseButtonState(int button)
    {
        if (Input.GetMouseButtonDown(button))
        {
            return InputState.Press;
        }
        if (Input.GetMouseButtonUp(button))
        {
            return InputState.Release;
        }

        return InputState.Hover;
    }

    private IEnumerable<RayHit> RaycastFromMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastAll(ray, Mathf.Infinity).Select(x => new RayHit(x));
    }
}

public class TestInput : IInput
{
    private readonly Queue<MouseState> _mouseActions = new Queue<MouseState>();

    public MouseState GetMouseState(int button)
    {
        if (_mouseActions.Count != 0)
        {
            return _mouseActions.Dequeue();
        }

        return DefaultMouseState;
    }

    public void AddMouseDown(GameObject gameObject, Vector3 position)
    {
        _mouseActions.Enqueue(new MouseState(InputState.Press, new RayHit(gameObject.transform, position).AsList()));
    }

    public void AddMouseUp(GameObject gameObject, Vector3 position)
    {
        _mouseActions.Enqueue(new MouseState(InputState.Release, new RayHit(gameObject.transform, position).AsList()));
    }

    public void AddMouseHover(GameObject gameObject, Vector3 position)
    {
        _mouseActions.Enqueue(new MouseState(InputState.Hover, new RayHit(gameObject.transform, position).AsList()));
    }

    private MouseState DefaultMouseState { get { return new MouseState(InputState.Hover, new List<RayHit>()); } }
}

public class MouseState
{
    public MouseState(InputState state, IEnumerable<RayHit> hits)
    {
        State = state;
        Hits = hits;
    }

    public InputState State { get; private set; }
    public IEnumerable<RayHit> Hits { get; private set; }
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