using UnityEngine;

public interface IInput
{
    bool GetMouseButtonDown(int button);
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
}

public class TestInput : IInput
{
    private int _mouseButtonDown;

    public bool GetMouseButtonDown(int button)
    {
        return button == _mouseButtonDown;
    }

    public void SetMouseButtonDown(int button)
    {
        _mouseButtonDown = button;
    }
}