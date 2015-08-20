using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;

public static class EntityExtensions
{
    public static void ShouldHaveUnit(this Entity entity, int squadNumber)
    {
        entity.hasUnit.Should().BeTrue("entity should have a unit component");
        entity.unit.SquadNumber.Should().Be(squadNumber);
    }

    public static void ShouldHaveParent(this Entity child, Entity parent)
    {
        child.parent.Value.Should().Be(parent);
        parent.child.Value.Should().Be(child);
    }

    public static void ShouldHaveMovement(this Entity entity, float moveSpeed)
    {
        entity.hasMovement.Should().BeTrue("entity should have movement component");
        entity.movement.ShouldBeEquivalentTo(new MovementComponent() { MoveSpeed = moveSpeed });
    }

    public static void ShouldHavePosition(this Entity entity, float x, float y, float z)
    {
        entity.hasPosition.Should().BeTrue("the entity should have a position component");
        entity.position.ShouldBeEquivalentTo(new VectorClass(x, y, z));
    }

    public static void ShouldHaveResource(this Entity entity, string resource)
    {
        entity.hasResource.Should().BeTrue("the entity should have a resource component");
        entity.resource.Name.Should().Be(resource);
    }

    public static void ShouldHaveMoveOrderTo(this Entity entity, Vector3 position)
    {
        entity.ShouldHaveMoveOrderTo(position, Quaternion.Identity);
    }

    public static void ShouldHaveMoveOrderTo(this Entity entity, Vector3 position, Quaternion orientation)
    {
        entity.hasMoveOrder.Should().BeTrue("entity should have received order");
        entity.moveOrder.Position.Should().Be(position);
        entity.moveOrder.Orientation.Should().Be(orientation);
    }

    public static void ShouldHaveAttackOrderTo(this Entity entity, Vector3 position)
    {
        entity.hasAttackOrder.Should().BeTrue("entity should have received order");
        entity.attackOrder.ToV3().Should().Be(position);
    }

    public static void ShouldBeDestroyed(this Entity entity, bool expected)
    {
        entity.isDestroy.Should().Be(expected, "entity " + entity + " should " + (expected ? "" : "not ") + "be destroyed");
    }

    public static void ShouldBeHidden(this Entity entity, bool expected)
    {
        entity.isDestroy.Should().Be(expected, "entity " + entity + " should " + (expected ? "" : "not ") + "be hidden");
    }
}