using Entitas;
using FluentAssertions;
using MicroTactics.Tests;
using Xunit;

public class AiMoveSystemTests
{
    [Fact]
    public void TriggersOnRemovedOrders()
    {
        var sut = new AiMoveSystem();

        sut.trigger.Should().Be(Matcher.MoveOrder);
        sut.eventType.Should().Be(GroupEventType.OnEntityRemoved);
    }

    [Fact]
    public void AddsNewMoveOrder()
    {
        var sut = new AiMoveSystem();

        var entity = new TestEntity().AddPosition(new Vector(0, 0, 0));
        sut.Execute(entity.AsList());

        entity.hasMoveOrder.Should().BeTrue("unit should have received order");
        entity.moveOrder.Position.Should().Be(new Vector(1, 0, 0));
    }
}
