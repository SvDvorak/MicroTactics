using Entitas;
using FluentAssertions;
using Mono.GameMath;
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
            .AddPosition(0, 0, 0)
            .AddSquad(0)
            .AddAi(0);
    }

    [Fact]
    public void DoesGiveAddMoveOrderIfNoEnemiesArePresent()
    {
        _sut.Execute();

        _squad.hasMoveOrder.Should().BeFalse("no enemy is present so no move order should be given");
    }

    [Fact]
    public void DoesNotGiveMoveOrderIfEnemyIsFarAway()
    {
        _testPool.CreateEntity()
            .AddPosition(float.PositiveInfinity, 0, 0)
            .IsEnemy(true);

        _sut.Execute();

        _squad.hasMoveOrder.Should().BeFalse("enemy is far away so no move order should be given");
    }

    [Fact]
    public void GivesDefaultMoveOrderWhenEnemyIsOnSamePosition()
    {
        _testPool.CreateEntity()
            .AddPosition(0, 0, 0)
            .IsEnemy(true);

        _sut.Execute();

        _squad.HasMoveOrderTo(new Vector3(1, 0, 0));
    }

    [Fact]
    public void GivesMoveOrdersToMoveAwayFromEnemy()
    {
        _squad.ReplacePosition(-0.5f, 0, 0);
        var squad2 = _testPool.CreateEntity()
            .AddPosition(0, 0, 0.5f)
            .AddSquad(1)
            .AddAi(0);

        _testPool.CreateEntity()
            .AddPosition(0, 0, 0)
            .IsEnemy(true);

        _sut.Execute();

        _squad.HasMoveOrderTo(new Vector3(-1f, 0, 0));
        squad2.HasMoveOrderTo(new Vector3(0, 0, 1));
    }
}