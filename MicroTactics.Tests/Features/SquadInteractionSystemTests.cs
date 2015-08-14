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
        private readonly Systems _systemsSut;
        private readonly Entity _input;
        private readonly Entity _empty;

        public SquadInteractionSystemTests()
        {
            _systemsSut = new Systems()
                .Add(_pool.CreateSelectInteractionSystem())
                .Add(_pool.CreateSelectedInteractionSystem())
                .Add(_pool.CreateMoveInteractionSystem())
                .Add(_pool.CreateAttackInteractionSystem());

            _input = _pool.CreateEntity().AddInput(InputState.Hover, new List<EntityHit>());
            _empty = _pool.CreateEntity();
        }

        public class SelectInteraction : SquadInteractionSystemTests
        {
            [Fact]
            public void SetsSelectedOnClosestSquadInInputWhenPressing()
            {
                var squad1 = CreateSquad();
                var squad2 = CreateSquad();

                PerformPressOn(CreateSelectionArea(squad2), CreateSelectionArea(squad1));

                squad1.isSelected.Should().BeTrue("first squad should have been selected");
                squad2.isSelected.Should().BeFalse("second squad shouldn't have been selected");
            }

            [Fact]
            public void DoesNotGiveMoveOrderWhenSelecting()
            {
                var squad = CreateSquad();

                SelectSquad(squad);

                squad.hasMoveOrder.Should().BeFalse("should only have been selected, not moved");
            }

            [Fact]
            public void DoesNothingIfNoSquadsAreInInput()
            {
                var squad = CreateSquad();

                PerformPressOn(_empty);

                squad.isSelected.Should().BeFalse("squad is not in selection so shouldn't become selected");
            }

            [Fact]
            public void DeselectsAllOtherSquadsWhenPressingANewSquad()
            {
                var squad1 = CreateSquad().IsSelected(true);
                var squad2 = CreateSquad().IsSelected(false);

                SelectSquad(squad2);

                squad1.isSelected.Should().BeFalse("first squad should have been deselected");
            }

            [Fact]
            public void DeselectsAllSquadsWhenReleasingOnNotASquad()
            {
                var squad = CreateSquad();
                SelectSquad(squad);

                PerformPressOn(_empty);

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

                var pressPosition = new Vector3(0, 0, 0);
                var releasePosition = new Vector3(100, 0, 100);
                PerformDragInput(
                    new EntityHit(_empty, pressPosition),
                    new EntityHit(_empty, releasePosition));

                squad.HasMoveOrderTo(pressPosition, Quaternion.LookAt(releasePosition.Normalized()));
            }

            [Fact]
            public void DoesNotCrashWhenInteractingAndNoSquadIsSelected()
            {
                PerformDragInput(
                    new EntityHit(_empty, Vector3.Zero),
                    new EntityHit(_empty, new Vector3(100, 0, 0)));
            }
        }

        public class SquadInteractionAttack : SquadInteractionSystemTests
        {
            [Fact]
            public void GivesAttackOrderWhenPressingOnUnselectedSquadAndReleasingOnNonSquadEntity()
            {
                var squad = CreateSquad();
                var releasePosition = new Vector3(100, 0, 0);

                PerformDragInput(
                    new EntityHit(CreateSelectionArea(squad), Vector3.Zero),
                    new EntityHit(_empty, releasePosition));

                squad.HasAttackOrderTo(releasePosition);
            }

            [Fact]
            public void GivesAttackOrderWhenPressingOnSquadAndReleasingOnOtherSquad()
            {
                var squad1 = CreateSquad();
                var squad2 = CreateSquad();
                SelectSquad(squad1);

                var releasePosition = new Vector3(10, 0, 0);

                PerformDragInput(
                    new EntityHit(CreateSelectionArea(squad1), Vector3.Zero),
                    new EntityHit(CreateSelectionArea(squad2), releasePosition));

                squad1.HasAttackOrderTo(releasePosition);
            }
        }

        private Entity CreateSquad()
        {
            return _pool.CreateEntity().AddSquad(0);
        }

        private void SelectSquad(Entity squad)
        {
            var selectionArea = CreateSelectionArea(squad);
            PerformPressOn(selectionArea);
        }

        private void PerformPressOn(params Entity[] entities)
        {
            Execute(SetInput(InputState.Press, entities));
            Execute(SetInput(InputState.Release, entities));
        }

        private Entity CreateSelectionArea(Entity squad)
        {
            return _pool.CreateEntity().AddSelectionArea(squad);
        }

        private Entity SetInput(InputState state, params Entity[] hitEntities)
        {
            return SetInput(state, ToEntityHits(hitEntities));
        }

        private static EntityHit[] ToEntityHits(Entity[] hitEntities)
        {
            return hitEntities.Select(x => new EntityHit(x, new Vector3())).ToArray();
        }

        private Entity SetInput(InputState state, params EntityHit[] entityHits)
        {
            return _input.ReplaceInput(state, entityHits.ToList());
        }

        private void PerformDragInput(EntityHit pressEntityHit, EntityHit releaseEntityHit)
        {
            Execute(SetInput(InputState.Press, pressEntityHit));
            Execute(SetInput(InputState.Release, releaseEntityHit));
        }

        private void Execute(Entity input = null)
        {
            _systemsSut.Execute();
        }
    }
}