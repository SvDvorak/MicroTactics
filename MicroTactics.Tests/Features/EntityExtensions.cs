using Entitas;
using FluentAssertions;

public static class EntityExtensions
{
    public static void HasMoveOrderTo(this Entity unit, Vector position)
    {
        unit.hasMoveOrder.Should().BeTrue("unit should have received order");
        unit.moveOrder.ShouldBeEquivalentTo(position);
    }

    public static void HasAttackOrderTo(this Entity unit, Vector position)
    {
        unit.hasAttackOrder.Should().BeTrue("unit should have received order");
        unit.attackOrder.ShouldBeEquivalentTo(position);
    }
}