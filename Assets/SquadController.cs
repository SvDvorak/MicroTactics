using System;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public GameObject UnitTemplate;
    public int Rows;
    public int Columns;

    private TwoDimensionalCollection<GameObject> _units;
    private Vector3 _dragStartPoint;
    private const float Spacing = 3;

    void Start()
    {
        _units = new TwoDimensionalCollection<GameObject>();
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                _units[x, y] = ((GameObject)Instantiate(UnitTemplate, new Vector3(x, 0, y) * Spacing, Quaternion.identity));
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var possibleHit = RaycastUsingCamera();
            if (possibleHit.HasValue)
            {
                _dragStartPoint = possibleHit.Value.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var possibleHit = RaycastUsingCamera();
            if (possibleHit.HasValue)
            {
                var lookDirection = (possibleHit.Value.point - _dragStartPoint).normalized;
                var squadTowardsBackRotation = Quaternion.LookRotation(-lookDirection, Vector3.up);
                transform.position = _dragStartPoint;

                PerformForEachUnit((x, y) => SetUnitPositionInSquad(x, y, squadTowardsBackRotation));
            }
        }
    }

    private Maybe<RaycastHit> RaycastUsingCamera()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.ToMaybe();
        }

        return Maybe<RaycastHit>.Nothing;
    }

    private void SetUnitPositionInSquad(int x, int y, Quaternion squadTowardsBackRotation)
    {
        var centerHitpoint = new Vector3((Columns - 1)/2f, 0, 0)*Spacing;
        var unitInSquadPosition = new Vector3(x, 0, y)*Spacing;
        _units[x, y].SendMessage("SetSquadPosition",
            transform.position + squadTowardsBackRotation*(unitInSquadPosition - centerHitpoint));
    }

    private void PerformForEachUnit(Action<int, int> action)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                action(x, y);
            }
        }
    }
}

public struct Maybe<T>
{
    private T _element;

    public Maybe(T element)
    {
        _element = element;
    }

    public static Maybe<T> Nothing { get { return new Maybe<T>(); } } 

    public T Value { get { return _element; } }
    public bool HasValue { get { return _element != null; } }
}

public static class ObjectExtensions
{
    public static Maybe<T> ToMaybe<T>(this T element)
    {
        return new Maybe<T>(element);
    }
}