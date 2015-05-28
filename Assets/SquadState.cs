using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SquadState : MonoBehaviour
{
    public GameObject UnitTemplate;
    public TwoDimensionalCollection<GameObject> Units;
    public int Rows;
    public int Columns;
    public float Spacing = 3;
    public State InteractState;
    public Vector3 CenterPosition { get; set; }

    void Start()
    {
        Units = new TwoDimensionalCollection<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.name != "Unit")
            {
                continue;
            }

            var x = Convert.ToInt32(child.position.x/Spacing);
            var y = Convert.ToInt32(child.position.z/Spacing);
            Units[x, y] = child.gameObject;
        }

        if (Units.IsEmpty)
        {
            Debug.Log("No units in squad here!");
            Debug.DrawLine(transform.position, transform.position + Vector3.up*5);
        }
    }

    public enum State
    {
        Idle,
        Attack,
        Move
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
                unit.transform.SetParent(transform);
                var unitCollider = unit.GetComponent<BoxCollider>();
                unitCollider.size = unitCollider.size + new Vector3(Spacing/2, 0, Spacing/2);

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