using System.Collections.Generic;
using System.Linq;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class ShowSelectedIndicatorForSquadSystemTests
    {
        private readonly ShowSelectedIndicatorForSquadSystem _sut = new ShowSelectedIndicatorForSquadSystem();

        [Fact]
        public void TriggersOnAddedOrRemovedSelectedWithUnitsCache()
        {
            _sut.trigger.Should().Be(Matcher.Selected);
            _sut.ensureComponents.Should().Be(Matcher.UnitsCache);
            _sut.eventType.Should().Be(GroupEventType.OnEntityAddedOrRemoved);
        }

        [Fact]
        public void HidesSelectedIndicatorWhenSquadIsNotSelected()
        {
            var selectedIndicator = CreateSelectedIndicator(false);

            _sut.Execute(
                CreateUnitsAndCacheFor(selectedIndicator)
                    .IsSelected(false)
                    .AsList());

            selectedIndicator.ShouldBeHidden(true);
        }

        [Fact]
        public void ShowsSelectedIndicatorWhenSquadIsSelected()
        {
            var selectedIndicator1 = CreateSelectedIndicator(true);
            var selectedIndicator2 = CreateSelectedIndicator(true);

            _sut.Execute(
                CreateUnitsAndCacheFor(selectedIndicator1, selectedIndicator2)
                    .IsSelected(true)
                    .AsList());

            selectedIndicator1.ShouldBeHidden(false);
            selectedIndicator1.ShouldBeHidden(false);
        }

        [Fact]
        public void HandlesMultipleSquadsChangingSelection()
        {
            var selectedIndicator1 = CreateSelectedIndicator(true);
            var selectedIndicator2 = CreateSelectedIndicator(true);

            _sut.Execute(new[]
                {
                    CreateUnitsAndCacheFor(selectedIndicator1).IsSelected(true),
                    CreateUnitsAndCacheFor(selectedIndicator2).IsSelected(false)
                }.ToList());

            selectedIndicator1.ShouldBeHidden(false);
            selectedIndicator2.ShouldBeHidden(true);
        }

        private static Entity CreateSelectedIndicator(bool isHidden)
        {
            return new TestEntity().IsHidden(isHidden);
        }

        private static Entity CreateUnitsAndCacheFor(params Entity[] selectedIndicators)
        {
            var units = selectedIndicators.Select(x => new TestEntity().AddChild(x)).ToList();
            return new TestEntity().AddUnitsCache(units);
        }
    }

    public static class EntityExtensions
    {
        public static void ShouldBeHidden(this Entity entity, bool expected)
        {
            entity.isHidden.Should().Be(expected, "selection indicator should be " + (expected ? "hidden" : "visible"));
        }
    }
}