using Entitas;
using FluentAssertions;

public static class EntityExtensions
{
    public static void HasMoveOrderTo(this Entity unit, VectorClass position)
    {
        unit.hasMoveOrder.Should().BeTrue("unit should have received order");
        unit.moveOrder.ShouldBeEquivalentTo(position);
    }

    public static void HasAttackOrderTo(this Entity unit, VectorClass position)
    {
        unit.hasAttackOrder.Should().BeTrue("unit should have received order");
        unit.attackOrder.ShouldBeEquivalentTo(position);
    }
}