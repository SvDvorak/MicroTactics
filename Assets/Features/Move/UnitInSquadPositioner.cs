using Entitas;

public class UnitInSquadPositioner
{
    public static VectorClass GetPosition(BoxFormationComponent formation, int unitNumber)
    {
        var squadPositionX = unitNumber%formation.Columns - (formation.Columns-1)/2f;
        var squadPositionZ = unitNumber/formation.Columns - (formation.Rows-1)/2f;

        return new VectorClass(squadPositionX*formation.Spacing, 0, squadPositionZ*formation.Spacing);
    }
}