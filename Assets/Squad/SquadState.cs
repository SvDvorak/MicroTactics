using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.GoodMonotoneChain;

[ExecuteInEditMode]
public class SquadState : MonoBehaviour
{
    public GameObject UnitTemplate;
    public TwoDimensionalCollection<GameObject> Units;
    public int Rows;
    public int Columns;
    public float Spacing = 3;
    public Interaction InteractState;
    public Vector3 CenterPosition;
    public Quaternion CenterRotation;
    public Vector3 FormationCenter;
    public bool IsMoving;

    void Start()
    {
        Units = new TwoDimensionalCollection<GameObject>();

        var squadSoldiers = transform
            .Cast<Transform>()
            .Where(x => x.name == "Unit")
            .ToList();

        var vectorComparer = new VectorComparer();
        FormationCenter = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        foreach (var soldier in squadSoldiers)
        {
            if (vectorComparer.Compare(soldier.position, FormationCenter) < 0)
            {
                FormationCenter = soldier.position;
            }
        }

        squadSoldiers.ForEach(soldier =>
            {
                var centeredPosition = (soldier.position - FormationCenter)/Spacing;
                var x = Convert.ToInt32(centeredPosition.x);
                var y = Convert.ToInt32(centeredPosition.z);
                Units[x, y] = soldier.gameObject;
            });

        if (Units.IsEmpty)
        {
            Debug.Log("No units in squad here!");
            Debug.DrawLine(transform.position, transform.position + Vector3.up*5);
        }
    }

    [ContextMenu("Spawn")]
    private void RespawnUnits()
    {
        if (!Units.IsEmpty)
        {
            Units.Iterate(DestroyImmediate);
            Units.Clear();
        }

        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                var unit = ((GameObject)Instantiate(UnitTemplate, new Vector3(x, 0, y) * Spacing, Quaternion.identity));
                unit.name = "Unit";
                unit.transform.SetParent(transform, false);
                //var unitCollider = unit.GetComponent<BoxCollider>();
                //unitCollider.size = unitCollider.size + new Vector3((Spacing-1)/unit.transform.localScale.x, 0, (Spacing-1)/unit.transform.localScale.z);

                Units[x, y] = unit;
            }
        }
    }

    public void PerformForEachUnit(Action<int, int> action)
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