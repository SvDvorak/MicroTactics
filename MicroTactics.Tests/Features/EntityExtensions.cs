using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;

public static class EntityExtensions
{

    public static void HasMoveOrderTo(this Entity entity, Vector3 position)
    {
        entity.HasMoveOrderTo(position, Quaternion.Identity);
    }

    public static void HasMoveOrderTo(this Entity entity, Vector3 position, Quaternion orientation)
    {
        entity.hasMoveOrder.Should().BeTrue("entity should have received order");
        entity.moveOrder.Position.Should().Be(position);
        entity.moveOrder.Orientation.Should().Be(orientation);
    }

    public static void HasAttackOrderTo(this Entity entity, Vector3 position)
    {
        entity.hasAttackOrder.Should().BeTrue("entity should have received order");
        entity.attackOrder.ToV3().Should().Be(position);
    }
}