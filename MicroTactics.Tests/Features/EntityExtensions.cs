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

    public static void ShouldHaveHealth(this Entity entity, float health)
    {
        entity.hasHealth.Should().BeTrue("entity should have a health component");
        entity.health.Value.Should().Be(health);
    }

    public static void ShouldHaveParent(this Entity child, Entity parent)
    {
        child.parent.Value.Should().Be(parent);
        parent.children.Value.ShouldAllBeEquivalentTo(child.AsList());
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

    public static void ShouldHaveDelayedDestroy(this Entity entity, int frames)
    {
        entity.hasDelayedDestroy.Should().BeTrue("entity should have a delayed destroy");
        entity.delayedDestroy.Frames.Should().Be(frames);
    }

    public static void ShouldBeStickable(this Entity entity, float requiredVelocity)
    {
        entity.hasStickable.Should().BeTrue("entity should be stickable");
        entity.stickable.RequiredVelocity.Should().Be(requiredVelocity);
    }

    public static void ShouldBeHidden(this Entity entity, bool expected)
    {
        entity.isDestroy.Should().Be(expected, "entity " + entity + " should " + (expected ? "" : "not ") + "be hidden");
    }

    public static void ShouldAttachTo(this Entity attacher, Entity attachee)
    {
        attacher.hasAttachTo.Should().BeTrue(attacher + " should have attach component");
        attacher.attachTo.Entity.Should().Be(attachee, attacher + " should be attached to " + attachee);
    }
}