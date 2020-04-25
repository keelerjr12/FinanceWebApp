using System.Collections.Generic;
using System.Linq;

namespace FinanceWebLib
{
    public class CDF
    {
        public CDF(IEnumerable<SurveyData> samples)
        {
            _samples = samples.OrderBy(s => s.Data).ToList();
        }

        public double Calculate(double x)
        {
            if (_samples.Exists(s => s.Data == x))
            {
                return Compute(x);
            } 

            if (x < _samples.First().Data)
            {
                return 0;
            }

            if (x > _samples.Last().Data)
            {
                return 1;
            }

            var prevX = FindPrev(x);
            var prevPercentile = Compute(prevX);

            var nextX = FindNext(x);
            var nextPercentile = Compute(nextX);

            var slope = (nextPercentile - prevPercentile) / (nextX - prevX);
            return slope * (x - prevX) + prevPercentile;
        }

        private double Compute(double x)
        {
            var lessOrEqualSamples = _samples.Where(s => s.Data <= x).Sum(s => s.Weight);
            var totalSamples = _samples.Sum(s => s.Weight);
            var percentile = (lessOrEqualSamples) / totalSamples;
            return percentile;
        }

        private double FindPrev(double x)
        {
            return _samples.Last(s => s.Data < x).Data;
        }

        private double FindNext(double x)
        {
            return _samples.First(s => s.Data > x).Data;
        }

        private readonly List<SurveyData> _samples;
    }
}
