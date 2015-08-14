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
        private readonly Pool _pool = new TestPool();
        private readonly Entity[] _empty = new Entity[0];
        private readonly Systems _systemsSut;
        private readonly Entity _input;

        public SquadInteractionSystemTests()
        {
            _systemsSut = new Systems()
                .Add(_pool.CreateSelectInteractionSystem())
                .Add(_pool.CreateSelectedInteractionSystem())
                .Add(_pool.CreateMoveInteractionSystem())
                .Add(_pool.CreateAttackInteractionSystem());

            _input = _pool.CreateEntity().AddInput(InputState.Hover, new List<EntityHit>());
        }

        public class SelectInteraction : SquadInteractionSystemTests
        {
            [Fact]
            public void SetsSelectedOnClosestSquadInInputWhenPressing()
            {
                var squad1 = CreateSquad();
                var squad2 = CreateSquad();
                var selectionArea1 = CreateSelectionArea(squad1);
                var selectionArea2 = CreateSelectionArea(squad2);

                Execute(SetInput(InputState.Press, selectionArea2, selectionArea1));
                Execute(SetInput(InputState.Release, selectionArea2, selectionArea1));

                _systemsSut.Execute();

                squad1.isSelected.Should().BeTrue("first squad should have been selected");
                squad2.isSelected.Should().BeFalse("second squad shouldn't have been selected");
            }

            [Fact]
            public void DoesNotGiveMoveOrderWhenSelecting()
            {
                var squad = CreateSquad();
                var selectionArea = CreateSelectionArea(squad);

                Execute(SetInput(InputState.Press, selectionArea));
                Execute(SetInput(InputState.Release, selectionArea));

                squad.hasMoveOrder.Should().BeFalse("should only have been selected, not moved");
            }

            [Fact]
            public void DoesNothingIfNoSquadsAreInInput()
            {
                var squad = CreateSquad();

                Execute(SetInput(InputState.Press, _empty));

                squad.isSelected.Should().BeFalse("squad is not in selection so shouldn't become selected");
            }

            [Fact]
            public void DeselectsAllOtherSquadsWhenPressingANewSquad()
            {
                var squad1 = CreateSquad().IsSelected(true);
                var squad2 = CreateSquad().IsSelected(false);

                Execute(SetInput(InputState.Press, CreateSelectionArea(squad2)));

                squad1.isSelected.Should().BeFalse("first squad should have been deselected");
            }

            [Fact]
            public void DeselectsAllSquadsWhenReleasingOnNotASquad()
            {
                var squad = CreateSquad();
                SelectSquad(squad);

                var notSquad = _pool.CreateEntity();
                Execute(SetInput(InputState.Press, notSquad));
                Execute(SetInput(InputState.Release, notSquad));

                squad.isSelected.Should().BeFalse("squad should have been deselected");
            }
        }

        public class SquadInteractionMove : SquadInteractionSystemTests
        {
            [Fact]
            public void GivesMoveOrderWhenPressingOnGroundAndReleasingFurtherAway()
            {
                var squad = CreateSquad();
                SelectSquad(squad);

                var ground = _pool.CreateEntity();
                var hit1 = new EntityHit(ground, new Vector3(0, 0, 0));
                var hit2 = new EntityHit(ground, new Vector3(100, 0, 100));

                Execute(SetInput(InputState.Press, hit1));
                Execute(SetInput(InputState.Release, hit2));

                squad.HasMoveOrderTo(hit1.Position, Quaternion.LookAt(hit2.Position.Normalized()));
            }

            [Fact]
            public void DoesNotCrashWhenInteractingAndNoSquadIsSelected()
            {
                var ground = _pool.CreateEntity();
                var hit1 = new EntityHit(ground, Vector3.Zero);
                var hit2 = new EntityHit(ground, new Vector3(100, 0, 0));

                Execute(SetInput(InputState.Press, hit1));
                Execute(SetInput(InputState.Release, hit2));
            }
        }

        public class SquadInteractionAttack : SquadInteractionSystemTests
        {
            [Fact]
            public void GivesAttackOrderWhenPressingOnUnselectedSquadAndReleasingOnGround()
            {
                var squad = CreateSquad();

                var ground = _pool.CreateEntity();
                var hit1 = new EntityHit(CreateSelectionArea(squad), Vector3.Zero);
                var hit2 = new EntityHit(ground, new Vector3(100, 0, 0));

                Execute(SetInput(InputState.Press, hit1));
                Execute(SetInput(InputState.Release, hit2));

                squad.HasAttackOrderTo(hit2.Position);
            }

            [Fact]
            public void GivesAttackOrderWhenPressingOnSquadAndReleasingOnOtherSquad()
            {
                var squad1 = CreateSquad();
                var squad2 = CreateSquad();
                SelectSquad(squad1);

                var selectedSquadHit = new EntityHit(CreateSelectionArea(squad1), Vector3.Zero);
                var attackedSquadHit = new EntityHit(CreateSelectionArea(squad2), new Vector3(10, 0, 0));

                Execute(SetInput(InputState.Press, selectedSquadHit));
                Execute(SetInput(InputState.Release, attackedSquadHit));

                squad1.hasAttackOrder.Should().BeTrue("attack order should have been given to squad");
                squad1.attackOrder.ToV3().ShouldBeEquivalentTo(attackedSquadHit.Position);
            }
        }

        private Entity CreateSquad()
        {
            return _pool.CreateEntity().AddSquad(0);
        }

        private void SelectSquad(Entity squad)
        {
            var selectionArea = CreateSelectionArea(squad);
            Execute(SetInput(InputState.Press, selectionArea));
            squad.isSelected.Should().BeTrue("should have become selected after running selection input");
        }

        private Entity CreateSelectionArea(Entity squad)
        {
            return _pool.CreateEntity().AddSelectionArea(squad);
        }

        private Entity SetInput(InputState state, params Entity[] hitEntities)
        {
            return SetInput(state, ToEntityHits(hitEntities));
        }

        private Entity SetInput(InputState state, params EntityHit[] entityHits)
        {
            return _input.ReplaceInput(state, entityHits.ToList());
        }

        private static EntityHit[] ToEntityHits(Entity[] hitEntities)
        {
            return hitEntities.Select(x => new EntityHit(x, new Vector3())).ToArray();
        }

        private void Execute(Entity input)
        {
            _systemsSut.Execute();
        }
    }
}