using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IInput
{
    IEnumerable<RayHit> RaycastFromMousePosition();
    InputState GetMouseState(int button);
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

    public InputState GetMouseState(int button)
    {
        if (Input.GetMouseButtonDown(0))
        {
            return InputState.Press;
        }
        if (Input.GetMouseButtonUp(0))
        {
            return InputState.Release;
        }

        return InputState.Hover;
    }
}

public class TestInput : IInput
{
    private readonly Queue<MouseAction> _mouseActions = new Queue<MouseAction>();

    public IEnumerable<RayHit> RaycastFromMousePosition()
    {
        if (!_mouseActions.Any())
        {
            return new List<RayHit>();
        }

        return _mouseActions.Peek().Hit.AsList();
    }

    public InputState GetMouseState(int button)
    {
        return _mouseActions.Dequeue().State;
    }

    public void AddMouseDown(GameObject gameObject)
    {
        _mouseActions.Enqueue(new MouseAction(InputState.Press, new RayHit(gameObject.transform, new Vector3())));
    }

    public void AddMouseUp(GameObject gameObject)
    {
        _mouseActions.Enqueue(new MouseAction(InputState.Release, new RayHit(gameObject.transform, new Vector3())));
    }

    private class MouseAction
    {
        public MouseAction(InputState state, RayHit hit)
        {
            State = state;
            Hit = hit;
        }

        public InputState State { get; private set; }
        public RayHit Hit { get; private set; }
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