using Entitas;
using FluentAssertions;
using MicroTactics.Tests;
using Xunit;

public class AiMoveOrderSystemTests
{
    private readonly AiMoveOrderSystem _sut;
    private readonly TestPool _testPool;
    private readonly Entity _squad;

    public AiMoveOrderSystemTests()
    {
        _sut = new AiMoveOrderSystem();
        _testPool = new TestPool();
        _sut.SetPool(_testPool);
        _squad = _testPool.CreateEntity()
            .AddSquad(0, 1, 1)
            .IsAi(true);
    }

    [Fact]
    public void GivesDifferentOrdersPerSquad()
    {
        var squad2 = _testPool.CreateEntity()
            .AddSquad(1, 1, 1)
            .IsAi(true);

        _sut.Execute();

        _squad.HasMoveOrderTo(new Vector(0, 0, 0));
        squad2.HasMoveOrderTo(new Vector(1, 0, 0));
    }

    [Fact]
    public void DoesNotAddMoveOrderIfOneAlreadyExists()
    {
        _squad.AddMoveOrder(1, 1, 1);

        _sut.Execute();

        _squad.moveOrder.ShouldBeEquivalentTo(new Vector(1, 1, 1));
    }
}