using System;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public GameObject UnitTemplate;
    public int Rows;
    public int Columns;
    public Arrow MoveArrow;

    private TwoDimensionalCollection<GameObject> _units;
    private Vector3 _dragStartPoint;
    private bool _isDragging;
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
        WilDebug.DrawQuaternion(transform.position, transform.rotation, Color.red);
        var possibleHit = RaycastUsingCamera();

        if (!possibleHit.HasValue)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _dragStartPoint = possibleHit.Value.point;
            _isDragging = true;
            MoveArrow.SetVisible(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var lookDirection = (possibleHit.Value.point - _dragStartPoint).normalized;
            var squadTowardsBackRotation = Quaternion.LookRotation(-lookDirection, Vector3.up);
            transform.position = possibleHit.Value.point;
            transform.rotation = squadTowardsBackRotation;

            PerformForEachUnit((x, y) => SetUnitPositionInSquad(x, y, squadTowardsBackRotation));
            _isDragging = false;
            MoveArrow.SetVisible(false);
        }

        if (_isDragging)
        {
            MoveArrow.SetPositions(_dragStartPoint, possibleHit.Value.point);
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
            _dragStartPoint + squadTowardsBackRotation*(unitInSquadPosition - centerHitpoint));
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

public static class WilDebug
{
    public static void DrawQuaternion(Vector3 position, Quaternion quaternion, Color color)
    {
        Debug.DrawLine(position, position + quaternion*Vector3.forward*3, color);
    }
}