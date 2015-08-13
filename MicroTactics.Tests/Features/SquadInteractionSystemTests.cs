using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SquadInteractionSystemTests
    {
        private readonly SquadInteractionSystem _sut = new SquadInteractionSystem();
        private readonly Pool _pool = new TestPool();
        private readonly Entity[] _empty = new Entity[0];

        public SquadInteractionSystemTests()
        {
            _sut.SetPool(_pool);
        }

        public class SquadInteractionTrigger : SquadInteractionSystemTests
        {
            [Fact]
            public void TriggersOnAddedInput()
            {
                _sut.trigger.Should().Be(Matcher.Input);
                _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
            }
        }

        public class SquadInteractionSelect : SquadInteractionSystemTests
        {
            [Fact]
            public void SetsSelectedOnClosestSquadInInputWhenPressing()
            {
                var squad1 = CreateSquad();
                var squad2 = CreateSquad();
                var selectionArea1 = CreateSelectionArea(squad1);
                var selectionArea2 = CreateSelectionArea(squad2);

                _sut.Execute(CreateInput(InputState.Press, selectionArea2, selectionArea1).AsList());

                squad1.isSelected.Should().BeTrue("first squad should have been selected");
                squad2.isSelected.Should().BeFalse("second squad shouldn't have been selected");
            }

            [Fact]
            public void DoesNotGiveMoveOrderWhenSelecting()
            {
                var squad = CreateSquad();
                var selectionArea = CreateSelectionArea(squad);

                _sut.Execute(CreateInput(InputState.Press, selectionArea).AsList());
                _sut.Execute(CreateInput(InputState.Release, selectionArea).AsList());

                squad.hasMoveOrder.Should().BeFalse("should only have been selected, not moved");
            }

            [Fact]
            public void DoesNothingIfNoSquadsAreInInput()
            {
                var squad = CreateSquad();

                _sut.Execute(CreateInput(InputState.Press, _empty).AsList());

                squad.isSelected.Should().BeFalse("squad is not in selection so shouldn't become selected");
            }

            [Fact]
            public void DeselectsAllOtherSquadsWhenPressingANewSquad()
            {
                var squad1 = CreateSquad().IsSelected(true);
                var squad2 = CreateSquad().IsSelected(false);

                _sut.Execute(CreateInput(InputState.Press, CreateSelectionArea(squad2)).AsList());

                squad1.isSelected.Should().BeFalse("first squad should have been deselected");
            }

            [Fact]
            public void DeselectsAllSquadsWhenReleasingOnNotASquad()
            {
                var squad = CreateSquad().IsSelected(true);

                var notSquad = _pool.CreateEntity();
                _sut.Execute(CreateInput(InputState.Press, notSquad).AsList());
                _sut.Execute(CreateInput(InputState.Release, notSquad).AsList());

                squad.isSelected.Should().BeFalse("squad should have been deselected");
            }
        }

        public class SquadInteractionMove : SquadInteractionSystemTests
        {
            [Fact]
            public void GivesMoveOrderWhenPressingOnGroundAndReleasingFurtherAway()
            {
                var squad = CreateSquad().IsSelected(true);

                var ground = _pool.CreateEntity();
                var hit1 = new EntityHit(ground, Vector3.Zero);
                var hit2 = new EntityHit(ground, new Vector3(100, 0, 0));

                _sut.Execute(CreateInput(InputState.Press, hit1).AsList());
                _sut.Execute(CreateInput(InputState.Release, hit2).AsList());

                squad.HasMoveOrderTo(hit1.Position);
            }

            [Fact]
            public void DoesNothingWhenNoSquadIsSelected()
            {
                var ground = _pool.CreateEntity();
                var hit1 = new EntityHit(ground, Vector3.Zero);
                var hit2 = new EntityHit(ground, new Vector3(100, 0, 0));

                _sut.Execute(CreateInput(InputState.Press, hit1).AsList());
                _sut.Execute(CreateInput(InputState.Release, hit2).AsList());
            }
        }

        public class SquadInteractionAttack : SquadInteractionSystemTests
        {
            [Fact]
            public void GivesAttackOrderWhenPressingOnSquadAndReleasingOnGround()
            {
                var squad = CreateSquad();

                var ground = _pool.CreateEntity();
                var hit1 = new EntityHit(CreateSelectionArea(squad), Vector3.Zero);
                var hit2 = new EntityHit(ground, new Vector3(100, 0, 0));

                _sut.Execute(CreateInput(InputState.Press, hit1).AsList());
                _sut.Execute(CreateInput(InputState.Release, hit2).AsList());

                squad.HasAttackOrderTo(hit2.Position);
            }

            [Fact]
            public void GivesAttackOrderWhenPressingOnSquadAndReleasingOnOtherSquad()
            {
                var squad1 = CreateSquad();
                var squad2 = CreateSquad();

                var selectedSquadHit = new EntityHit(CreateSelectionArea(squad1), Vector3.Zero);
                var attackedSquadHit = new EntityHit(CreateSelectionArea(squad2), new Vector3(10, 0, 0));

                _sut.Execute(CreateInput(InputState.Press, selectedSquadHit).AsList());
                _sut.Execute(CreateInput(InputState.Release, attackedSquadHit).AsList());

                squad1.hasAttackOrder.Should().BeTrue("attack order should have been given to squad");
                squad1.attackOrder.ToV3().ShouldBeEquivalentTo(attackedSquadHit.Position);
            }
        }

        private Entity CreateSquad()
        {
            return _pool.CreateEntity().AddSquad(0);
        }

        private Entity CreateSelectionArea(Entity squad)
        {
            return _pool.CreateEntity().AddSelectionArea(squad);
        }

        private Entity CreateInput(InputState state, params Entity[] hitEntities)
        {
            return CreateInput(state, ToEntityHits(hitEntities));
        }

        private Entity CreateInput(InputState state, params EntityHit[] entityHits)
        {
            return _pool.CreateEntity().AddInput(state, entityHits.ToList());
        }

        private static EntityHit[] ToEntityHits(Entity[] hitEntities)
        {
            return hitEntities.Select(x => new EntityHit(x, new Vector3())).ToArray();
        }
    }
}