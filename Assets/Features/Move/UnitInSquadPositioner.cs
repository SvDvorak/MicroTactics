using Entitas;

public class UnitInSquadPositioner
{
    public static Vector GetPosition(BoxFormationComponent formation, int unitNumber)
    {
        var squadPositionX = unitNumber%formation.Columns;
        var squadPositionZ = unitNumber/formation.Columns;

        return new Vector(squadPositionX*formation.Spacing, 0, squadPositionZ*formation.Spacing);
    }
}