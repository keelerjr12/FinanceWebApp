using System.Collections.Generic;
using System.Linq;

namespace FinanceWebLib
{
    public class InverseCDF
    {
        public InverseCDF(IEnumerable<SurveyData> samples)
        {
            _samples = samples.OrderBy(s => s.Data).ToList();
        }

        public double Calculate(double p)
        {
            var totalWeight = _samples.Sum(s => s.Weight);

            var samplesWithinP = p * totalWeight;
            var prevIdx = LastIndexBeforeSum(samplesWithinP);

            if (prevIdx < 0)
                return _samples.First().Data;

            var prevData = _samples[prevIdx].Data;
            var prevWeight = _samples.Take(prevIdx + 1).Sum(s => s.Weight);
            var prevProb = prevWeight / totalWeight;

            var nextData = _samples[prevIdx + 1].Data;
            var nextWeight = _samples.Take(prevIdx + 2).Sum(s => s.Weight);
            var nextProb = nextWeight / totalWeight;

            var slope = (nextData - prevData) / (nextProb - prevProb);
            var x = slope * (p - prevProb) + prevData;

            return x;
        }

        private int LastIndexBeforeSum(double x)
        {
            var sum = 0.0;

            for (var i = 0; i < _samples.Count; i++)
            {
                sum += _samples[i].Weight;
                if (sum >= x)
                    return i - 1;
            }

            return 0;
        }

        private readonly List<SurveyData> _samples;
    }
}
