using Entitas;

public class UnitInSquadPositioner
{
    public static Vector GetPosition(SquadComponent squad, int unitNumber)
    {
        var squadPositionX = unitNumber%squad.Columns;
        var squadPositionZ = unitNumber/squad.Columns;

        return new Vector(squadPositionX, 0, squadPositionZ);
    }
}