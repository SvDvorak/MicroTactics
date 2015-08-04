using System.Collections.Generic;
using Entitas;

public class SquadAttackOrderSystem : IReactiveSystem, ISetPool
{
    private Group _units;

    public IMatcher trigger { get { return Matcher.Unit; } }
    public GroupEventType eventType { get; set; }

    public void SetPool(Pool pool)
    {
        _units = pool.GetGroup(Matcher.Unit);
    }

    public void Execute(List<Entity> entities)
    {
        var squadEntity = entities.SingleEntity();

        for (var i = 0; i < _units.GetEntities().Length; i++)
        {
            var unit = _units.GetEntities()[i];

            var squadPosition = UnitInSquadPositioner.GetPosition(squadEntity.boxFormation, i);

            unit.ReplaceAttackOrder(
                squadPosition.x + squadEntity.attackOrder.x,
                squadPosition.y + squadEntity.attackOrder.y,
                squadPosition.z + squadEntity.attackOrder.z);
        }
    }
}