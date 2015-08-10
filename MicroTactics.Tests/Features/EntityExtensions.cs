using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;

public static class EntityExtensions
{
    public static void HasMoveOrderTo(this Entity unit, Vector3 position)
    {
        unit.hasMoveOrder.Should().BeTrue("unit should have received order");
        unit.moveOrder.ToV3().Should().Be(position);
    }

    public static void HasAttackOrderTo(this Entity unit, Vector3 position)
    {
        unit.hasAttackOrder.Should().BeTrue("unit should have received order");
        unit.attackOrder.ToV3().Should().Be(position);
    }
}