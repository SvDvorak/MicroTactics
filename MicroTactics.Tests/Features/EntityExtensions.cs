using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;

public static class EntityExtensions
{
    public static void HasMoveOrderTo(this Entity entity, Vector3 position)
    {
        entity.hasMoveOrder.Should().BeTrue("entity should have received order");
        entity.moveOrder.Position.Should().Be(position);
    }

    public static void HasAttackOrderTo(this Entity entity, Vector3 position)
    {
        entity.hasAttackOrder.Should().BeTrue("entity should have received order");
        entity.attackOrder.ToV3().Should().Be(position);
    }
}