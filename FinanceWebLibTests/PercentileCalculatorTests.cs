using System.Collections.Generic;
using FinanceLib;
using Xunit;

namespace FinanceWebLibTests
{
    public class PercentileCalculatorTests
    {

        private readonly IList<SurveyData> _samples = new List<SurveyData> {new SurveyData {Data = 1000, Weight = 1}};

        [Fact]
        public void Test1()
        {
            var pc = new CDF(_samples);
            var percentile = pc.Calculate(999);

            Assert.Equal(0, percentile);
        }

        [Fact]
        public void Test2()
        {
            var pc = new CDF(_samples);
            var percentile = pc.Calculate(1000);

            Assert.Equal(0.5, percentile);
        }

        [Fact]
        public void Test3()
        {
            var pc = new CDF(_samples);
            var percentile = pc.Calculate(1001);

            Assert.Equal(1, percentile);
        }
    }
}
