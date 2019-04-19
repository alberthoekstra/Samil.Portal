using System;
using System.Linq;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Samil.Power.Tests
{
    public class SamilHelperTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private const string StationName = "plant five";

        public SamilHelperTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public void TryExportYearValues()
        {
            var helper = new SamilHelper(StationName);

            for (var i = 2015; i <= 2019; i++)
            {
                var tryGetValues = helper.TryGetValues(x=>x.GetYearValues(i));
                tryGetValues.ShouldNotBeNull();
                tryGetValues.Year.ShouldBe(i);
            }
        }

        [Fact]
        public void GetThisYearValues()
        {
            var currentDate = DateTime.Now;
            var result = new SamilHelper(StationName).GetYearValues(currentDate);
            _testOutputHelper.WriteLine($"{JsonConvert.SerializeObject(result)}");

            result.ShouldNotBeNull();
            result.DataRecords.ShouldNotBeNull();
            result.DataRecords.Count.ShouldBe(12);
        }

        [Fact]
        public void GetPreviousYearValues()
        {
            var previousYearDate = DateTime.Now.AddYears(-1);
            var result = new SamilHelper(StationName).GetYearValues(previousYearDate);
            _testOutputHelper.WriteLine($"{JsonConvert.SerializeObject(result)}");

            result.ShouldNotBeNull();
            result.DataRecords.ShouldNotBeNull();
            result.DataRecords.Count.ShouldBe(12);
        }

        [Fact]
        public void GetThisMonthValues()
        {
            var currentDate = DateTime.Now;
            var result = new SamilHelper(StationName).GetMonthValues(currentDate);
            _testOutputHelper.WriteLine($"{JsonConvert.SerializeObject(result)}");

            result.ShouldNotBeNull();
            result.DataRecords.ShouldNotBeNull();
            result.DataRecords.All(x => x.Value > 0.0).ShouldBeTrue();
            result.Year.ShouldBe(currentDate.Year);
            result.Month.ShouldBe(currentDate.Month);
        }

        [Fact]
        public void GetTodayValues()
        {
            var currentDate = DateTime.Now;
            var result = new SamilHelper(StationName).GetDayValues(currentDate);
            _testOutputHelper.WriteLine($"{JsonConvert.SerializeObject(result)}");

            result.ShouldNotBeNull();
            result.DataRecords.ShouldNotBeNull();
            result.Year.ShouldBe(currentDate.Year);
            result.Month.ShouldBe(currentDate.Month);
            result.Day.ShouldBe(currentDate.Day);
        }
    }
}