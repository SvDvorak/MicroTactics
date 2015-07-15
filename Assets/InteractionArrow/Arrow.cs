using Assets;
using UnityEngine;

public class Arrow : MonoBehaviour, IArrow
{
    public Transform ArrowBase;
    public Transform ArrowHead;
    private ArrowController _arrowController;

    public void Awake()
    {
        _arrowController = new ArrowController(this, ArrowHead.localScale.z);
    }

    public void SetArrowPoints(Vector3 start, Vector3 end)
    {
        _arrowController.SetPositions(start, end);
    }

    public bool IsVisible
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }

    public void SetPositions(Vector3 start, Vector3 end)
    {
        ArrowHead.position = end;
        ArrowBase.position = start;
        var baseScale = (end - start).magnitude - ArrowHead.localScale.z;
        ArrowBase.localScale = ArrowBase.localScale.SetZ(baseScale);
    }

    public void SetRotation(Quaternion rotation)
    {
        ArrowHead.rotation = rotation;
        ArrowBase.rotation = rotation;
    }
}

public interface IArrow
{
    void SetPositions(Vector3 start, Vector3 end);
    void SetRotation(Quaternion rotation);
    bool IsVisible { get; set; }
}

public class ArrowController
{
    private readonly IArrow _arrow;
    private readonly float _minLength;

    public ArrowController(IArrow arrow, float minLength)
    {
        _arrow = arrow;
        _minLength = minLength;
    }

    public void SetPositions(Vector3 start, Vector3 end)
    {
        var startToEnd = end - start;

        _arrow.SetPositions(start, end);
        _arrow.SetRotation(Quaternion.LookRotation(startToEnd, Vector3.up));

        if (startToEnd.magnitude <= _minLength)
        {
            _arrow.IsVisible = false;
        }
    }
}