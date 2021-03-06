﻿using System.Collections.Generic;
using System.Linq;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SquadMoveOrderSystemTests
    {
        private readonly SquadMoveOrderSystem _sut;
        private readonly Entity _squad1;
        private readonly Entity _squad2;

        public SquadMoveOrderSystemTests()
        {
            _sut = new SquadMoveOrderSystem();

            _squad1 = new TestEntity()
                .AddSquad(0)
                .AddBoxFormation(1, 1, 0)
                .AddMoveOrder(new Vector3(0, 1, 0), Quaternion.Identity)
                .ReplaceUnitsCache(new List<Entity>());

            _squad2 = new TestEntity()
                .AddSquad(1)
                .AddBoxFormation(1, 1, 0)
                .AddMoveOrder(new Vector3(0, 2, 0), Quaternion.Identity)
                .ReplaceUnitsCache(new List<Entity>());
        }

        [Fact]
        public void TriggersOnAddedMoveOrder()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation, Matcher.MoveOrder).OnEntityAdded());
        }

        [Fact]
        public void OrdersUnitsAccordingToFormationAndRotation()
        {
            var unit1 = CreateUnit(0);
            var unit2 = CreateUnit(0);
            var unit3 = CreateUnit(0);
            var unit4 = CreateUnit(0);

            var orientation = Quaternion.LookAt(Vector3.Right);

            _squad1
                .ReplaceBoxFormation(2, 2, 2)
                .ReplaceMoveOrder(new Vector3(0, 1, 0), orientation)
                .ReplaceUnitsCache(new [] { unit1, unit2, unit3, unit4 }.ToList());

            _sut.Execute(_squad1.AsList());

            unit1.ShouldHaveMoveOrderTo(new Vector3(-1, 1, 1), orientation);
            unit2.ShouldHaveMoveOrderTo(new Vector3(-1, 1, -1), orientation);
            unit3.ShouldHaveMoveOrderTo(new Vector3(1, 1, 1), orientation);
            unit4.ShouldHaveMoveOrderTo(new Vector3(1, 1, -1), orientation);
        }

        [Fact]
        public void SetsSquadOrientationFromOrder()
        {
            var orientation = Quaternion.LookAt(Vector3.Right);

            _squad1
                .ReplaceMoveOrder(Vector3.Zero, orientation);

            _sut.Execute(_squad1.AsList());

            _squad1.ShouldHaveRotation(orientation);
        }

        [Fact]
        public void GivesUnitOrdersFromMultipleSquads()
        {
            var unit1 = CreateUnit(0);
            var unit2 = CreateUnit(1);
            _squad1.ReplaceUnitsCache(unit1.AsList());
            _squad2.ReplaceUnitsCache(unit2.AsList());

            _sut.Execute(new List<Entity>() { _squad1, _squad2});

            unit1.ShouldHaveMoveOrderTo(new Vector3(0, 1, 0));
            unit2.ShouldHaveMoveOrderTo(new Vector3(0, 2, 0));
        }

        [Fact]
        public void ReplacesOrderIfOneAlreadyExistsOnUnit()
        {
            var unit = CreateUnit(0).AddMoveOrder(new Vector3(1, 1, 1), Quaternion.Identity);
            _squad1.ReplaceUnitsCache(unit.AsList());

            _sut.Execute(_squad1.AsList());

            unit.ShouldHaveMoveOrderTo(new Vector3(0, 1, 0));
        }

        private Entity CreateUnit(int squadNumber)
        {
            return new TestEntity().AddUnit(squadNumber).AddPosition(0, 0, 0);
        }
    }
}
