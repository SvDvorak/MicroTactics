using System.Collections.Generic;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SelectionAreaUpdateSystemTests
    {
        private readonly SelectionAreaUpdateSystem _sut = new SelectionAreaUpdateSystem();
        private readonly TestPool _pool = new TestPool();

        public SelectionAreaUpdateSystemTests()
        {
            _sut.Padding = 0;
            _sut.SetPool(_pool);
        }

        [Fact]
        public void EmptyBoundingMeshWhenSquadHasNoUnits()
        {
            var selectionArea = CreateSelectionArea(CreateSquad());

            _sut.Execute();

            selectionArea.hasBoundingMesh.Should().BeTrue("should have bounding mesh after updating selection area");
            selectionArea.boundingMesh.Points.Should().HaveCount(0);
        }

        [Fact]
        public void BoundingMeshSurroundsSquadUnits()
        {
            var units = new List<Entity>()
                {
                    CreateUnitAt(-1, -1),
                    CreateUnitAt(1, -1),
                    CreateUnitAt(1, 1),
                    CreateUnitAt(-1, 1)
                };

            var selectionArea = CreateSelectionArea(CreateSquad(units));

            _sut.Execute();

            selectionArea.boundingMesh.Points.ShouldAllBeEquivalentTo(new List<Vector2>()
                {
                    new Vector2(-1, -1),
                    new Vector2(1, -1),
                    new Vector2(1, 1),
                    new Vector2(-1, 1)
                });
        }

        [Fact]
        public void PointsInsideConvexHullAreIgnored()
        {
            var units = new List<Entity>()
                {
                    CreateUnitAt(-1, -1),
                    CreateUnitAt(1, -1),
                    CreateUnitAt(0, 0),
                    CreateUnitAt(-1, 1)
                };

            var selectionArea = CreateSelectionArea(CreateSquad(units));

            _sut.Execute();

            selectionArea.boundingMesh.Points.ShouldAllBeEquivalentTo(new List<Vector2>()
                {
                    new Vector2(-1, -1),
                    new Vector2(1, -1),
                    new Vector2(-1, 1)
                });
        }

        [Fact]
        public void AddsPaddingToMeshPoints()
        {
            _sut.Padding = 1;

            var units = new List<Entity>()
                {
                    CreateUnitAt(0, -2),
                    CreateUnitAt(4, -2),
                    CreateUnitAt(0, 2)
                };

            var squad = CreateSquad(units).ReplacePosition(2, 0, 0);
            var selectionArea = CreateSelectionArea(squad);

            _sut.Execute();

            selectionArea.boundingMesh.Points.ShouldAllBeEquivalentTo(new[]
                {
                    new Vector2(-0.707107f, -2.707107f),
                    new Vector2(4.707107f, -2.707107f),
                    new Vector2(-0.707107f, 2.707107f)
                });
        }

        [Fact]
        public void HandlesMultipleSelectionAreas()
        {
            var selectionArea1 = CreateSelectionArea(CreateSquad());
            var selectionArea2 = CreateSelectionArea(CreateSquad());

            _sut.Execute();

            (selectionArea1.hasBoundingMesh && selectionArea2.hasBoundingMesh)
                .Should()
                .BeTrue("both selection areas should have a bounding mesh");
        }

        private Entity CreateUnitAt(int x, int z)
        {
            return _pool.CreateEntity().AddPosition(x, 0, z);
        }

        private Entity CreateSquad(List<Entity> units = null)
        {
            return _pool.CreateEntity().AddPosition(0, 0, 0).AddUnitsCache(units ?? new List<Entity>());
        }

        private Entity CreateSelectionArea(Entity squad)
        {
            return _pool.CreateEntity().AddSelectionArea(squad);
        }
    }
}