using UnityEngine;

public class SoldierMoveOrder
{
    public Vector3 MovePosition { get; private set; }
    public Quaternion SquadOrientation { get; private set; }

    public SoldierMoveOrder(Vector3 movePosition, Quaternion squadOrientation)
    {
        MovePosition = movePosition;
        SquadOrientation = squadOrientation;
    }
}