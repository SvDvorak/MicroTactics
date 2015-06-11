using UnityEngine;

public class SoldierMoveOrder
{
    public Vector3 NewSoldierPosition { get; private set; }
    public Quaternion SquadOrientation { get; private set; }

    public SoldierMoveOrder(Vector3 newSoldierPosition, Quaternion squadOrientation)
    {
        NewSoldierPosition = newSoldierPosition;
        SquadOrientation = squadOrientation;
    }
}