using Entitas;
using Mono.GameMath;

public class UnitInSquadPositioner
{
    public static Vector3 GetPosition(BoxFormationComponent formation, int unitNumber)
    {
        var squadPositionX = unitNumber%formation.Columns - (formation.Columns-1)/2f;
        var squadPositionZ = unitNumber/formation.Columns - (formation.Rows-1)/2f;

        return new Vector3(squadPositionX*formation.Spacing, 0, squadPositionZ*formation.Spacing);
    }
}