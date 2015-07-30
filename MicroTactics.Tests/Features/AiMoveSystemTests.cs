using Entitas;
using FluentAssertions;
using MicroTactics.Tests;
using Xunit;

public class AiMoveSystemTests
{
    private AiMoveOrderSystem _sut;
    private TestPool _testPool;

    public AiMoveSystemTests()
    {
        _sut = new AiMoveOrderSystem();
        _testPool = new TestPool();
        _sut.SetPool(_testPool);
    }

    [Fact]
    public void AddsNewMoveOrder()
    {
        var entity = _testPool.CreateEntity().AddPosition(0, 0, 0).IsAi(true);

        _sut.Execute();

        entity.hasMoveOrder.Should().BeTrue("unit should have received order");
        entity.moveOrder.ShouldBeEquivalentTo(new Vector(1, 0, 0));
    }

    [Fact]
    public void DoesNotAddMoveOrderIfOneAlreadyExists()
    {
        var entity = _testPool.CreateEntity().AddPosition(0, 0, 0).AddMoveOrder(1, 1, 1).IsAi(true);

        _sut.Execute();

        entity.moveOrder.ShouldBeEquivalentTo(new Vector(1, 1, 1));
    }
}
