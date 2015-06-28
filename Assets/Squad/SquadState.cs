using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;

[ExecuteInEditMode]
public class SquadState : MonoBehaviour
{
    public GameObject UnitTemplate;
    public List<GameObject> Units;
    public int Rows;
    public int Columns;
    public float Spacing = 3;
    public Interaction InteractState;
    public Vector3 CenterPosition;
    public Quaternion CenterRotation;
    public bool IsMoving;

    void Start()
    {
        RespawnUnits();
        Units = new List<GameObject>();

        var squadSoldiers = transform
            .Cast<Transform>()
            .Where(x => x.name == "Unit")
            .ToList();

        squadSoldiers.ForEach(soldier =>
            {
                Units.Add(soldier.gameObject);
            });

        CalculateCenterPosition();

        if (Units.Count == 0)
        {
            Debug.Log("No units in squad here!");
            Debug.DrawLine(transform.position, transform.position + Vector3.up*5);
        }
    }

    public void Update()
    {
        CalculateCenterPosition();
    }

    private void CalculateCenterPosition()
    {
        CenterPosition = SquadCenterPosition.GetCenterPoint(Units.Select(x => x.transform.position));
    }

    [ContextMenu("Spawn")]
    private void RespawnUnits()
    {
        if (Units.Count != 0)
        {
            Units.ForEach(DestroyImmediate);
            Units.Clear();
        }

        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                var unit = ((GameObject)Instantiate(UnitTemplate, new Vector3(x, 0, y) * Spacing, Quaternion.identity));
                unit.name = "Unit";
                unit.transform.SetParent(transform, false);

                const float centerOffset = 0.5f;
                unit.GetComponent<SoldierAI>().SquadPosition = new Vector3(x-Columns/2f+centerOffset, 0, y-Rows/2f+centerOffset)*Spacing;
                Units.Add(unit);
            }
        }
    }

    public void UnitDied(GameObject unit)
    {
        Units.Remove(unit);

        var formation = new Formation(0);
        formation.Remove(null);
    }

    public Vector3 GetCongestionMovementFor(GameObject unit)
    {
        return Units
            .Where(x => x != unit)
            .Select(x => unit.transform.position - x.transform.position)
            .Select(x => x.ZeroY())
            .Aggregate(new Vector3(), AddToCongestionMoveVector);
    }

    private static Vector3 AddToCongestionMoveVector(Vector3 current, Vector3 moveAwayVector)
    {
        return current + moveAwayVector.normalized * (1 / Mathf.Pow(moveAwayVector.magnitude, 3));
    }
}