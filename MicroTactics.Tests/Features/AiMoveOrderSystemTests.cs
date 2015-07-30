using Entitas;
using FluentAssertions;
using MicroTactics.Tests;
using Xunit;

public class AiMoveOrderSystemTests
{
    private readonly AiMoveOrderSystem _sut;
    private readonly TestPool _testPool;
    private readonly Entity _squad1;

    public AiMoveOrderSystemTests()
    {
        _sut = new AiMoveOrderSystem();
        _testPool = new TestPool();
        _sut.SetPool(_testPool);
        _squad1 = _testPool.CreateEntity()
            .AddSquad(0, 1, 1)
            .IsAi(true);
    }

    [Fact]
    public void OrdersUnitsAccordingToSquadSettings()
    {
        _squad1.ReplaceSquad(0, 2, 2);
        var unit1 = _testPool.CreateEntity().AddUnit(0).AddPosition(0, 0, 0);
        var unit2 = _testPool.CreateEntity().AddUnit(0).AddPosition(0, 0, 0);
        var unit3 = _testPool.CreateEntity().AddUnit(0).AddPosition(0, 0, 0);
        var unit4 = _testPool.CreateEntity().AddUnit(0).AddPosition(0, 0, 0);

        _sut.Execute();

        unit1.HasMoveOrderTo(new Vector(0, 0, 0));
        unit2.HasMoveOrderTo(new Vector(1, 0, 0));
        unit3.HasMoveOrderTo(new Vector(0, 0, 1));
        unit4.HasMoveOrderTo(new Vector(1, 0, 1));
    }

    [Fact]
    public void GivesDifferentOrdersPerSquad()
    {
        _testPool.CreateEntity().AddSquad(1, 1, 1).IsAi(true);

        var unit1 = _testPool.CreateEntity().AddUnit(0).AddPosition(0, 0, 0);
        var unit2 = _testPool.CreateEntity().AddUnit(1).AddPosition(0, 0, 0);

        _sut.Execute();

        unit1.HasMoveOrderTo(new Vector(0, 0, 0));
        unit2.HasMoveOrderTo(new Vector(0, 0, 0));
    }

    [Fact]
    public void DoesNotAddMoveOrderIfOneAlreadyExists()
    {
        var entity = _testPool.CreateEntity()
            .AddUnit(0)
            .AddPosition(0, 0, 0)
            .AddMoveOrder(1, 1, 1);

        _sut.Execute();

        entity.moveOrder.ShouldBeEquivalentTo(new Vector(1, 1, 1));
    }
}

public static class EntityExtensions
{
    public static void HasMoveOrderTo(this Entity unit, Vector position)
    {
        unit.hasMoveOrder.Should().BeTrue("unit should have received order");
        unit.moveOrder.ShouldBeEquivalentTo(position);
    }
}
