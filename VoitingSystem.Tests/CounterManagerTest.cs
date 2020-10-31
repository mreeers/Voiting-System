using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoitingSystem;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Tests
{
    public class CounterManagerTest
    {
        public const string CounterName = "Counter Name";
        public Counter _counter = new Counter { Name = CounterName, Count = 5 };

        [Fact]
        public void GetStatistics_IncludesCounterName()
        {
            var statistics = new CounterManager().GetStatistics(new[] { _counter }).First();
            Assert.Equal(CounterName, statistics.Name);
        }

        [Fact]
        public void GetStatistics_IncludesCounterCount()
        {
            var statistics = new CounterManager().GetStatistics(new[] { _counter }).First();
            Assert.Equal(5, statistics.Count);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(1, 3, 33.33)]
        [InlineData(2, 8, 25)]
        public void GetStatistics_ShowPercentageBasedOnDecimalBasedOnTotalCount(int count, int total, double expected)
        {
            _counter.Count = count;
            var counter = new Counter { Count = total - count };
            var statistics = new CounterManager().GetStatistics(new[] { _counter, counter }).First(); ;
            Assert.Equal(expected, statistics.Percent);
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcessWhenAllCountersAreEqual()
        {
            var counter1 = new CounterStatistics { Percent = 33.33 };
            var counter2 = new CounterStatistics { Percent = 33.33 };
            var counter3 = new CounterStatistics { Percent = 33.33 };
            var counters = new List<CounterStatistics> { counter1, counter2, counter3 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(33.33, counter1.Percent);
            Assert.Equal(33.33, counter2.Percent);
            Assert.Equal(33.33, counter3.Percent);
        }

        [Theory]
        [InlineData(66.66, 66.67, 33.33)]
        [InlineData(66.65, 66.67, 33.33)]
        [InlineData(66.66, 66.68, 33.32)]
        public void ResolveExcess_AddsExcessToHighestCounter(double initial, double expected, double lowest)
        {
            var counter1 = new CounterStatistics { Percent = initial };
            var counter2 = new CounterStatistics { Percent = lowest };
            var counters = new List<CounterStatistics> { counter1, counter2 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(expected, counter1.Percent);
            Assert.Equal(lowest, counter2.Percent);

            var counter3 = new CounterStatistics { Percent = initial };
            var counter4 = new CounterStatistics { Percent = lowest };
            counters = new List<CounterStatistics> { counter4, counter3 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(expected, counter3.Percent);
            Assert.Equal(lowest, counter4.Percent);
        }

        [Theory]
        [InlineData(11.11, 11.12, 44.44)]
        [InlineData(11.10, 11.12, 44.44)]
        public void ResolveExcess_AddsToLowestCounterWhenMoreThanOneHighestCounters(double initial, double expected, double highest)
        {
            var counter1 = new CounterStatistics { Percent = highest };
            var counter2 = new CounterStatistics { Percent = highest };
            var counter3 = new CounterStatistics { Percent = initial };
            var counters = new List<CounterStatistics> { counter1, counter2, counter3 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(highest, counter1.Percent);
            Assert.Equal(highest, counter2.Percent);
            Assert.Equal(expected, counter3.Percent);
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcessIfTotalPercentIs100()
        {
            var counter1 = new CounterStatistics { Percent = 80 };
            var counter2 = new CounterStatistics { Percent = 20 };
            var counters = new List<CounterStatistics> { counter1, counter2 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(80, counter1.Percent);
            Assert.Equal(20, counter2.Percent);
        }
    }
}
