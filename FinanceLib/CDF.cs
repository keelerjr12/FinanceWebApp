using System.Collections.Generic;

namespace FinanceLib
{
    public class CDF
    {
        public CDF(IEnumerable<SurveyData> samples)
        {
            _population = new Population(samples);
        }

        public double Calculate(double x)
        {
            if (_population.DoesXExist(x))
                return Compute(x);

            if (x < _population.Min)
                return 0;

            if (x > _population.Max)
                return 1;

            var y = SolveByInterpolation(x);

            return y;
        }

        private double Compute(double x)
        {
            var summedWeight = _population.SummedWeightWithinX(x);
            var totalWeight = _population.TotalWeight;
            var percentile = (summedWeight) / totalWeight;

            return percentile;
        }

        private double SolveByInterpolation(double x)
        {
            var prevX = _population.GetDataPrior(x);
            var prevPercentile = Compute(prevX);

            var nextX = _population.GetDataAfter(x);
            var nextPercentile = Compute(nextX);

            var y = LinearInterpolator.SolveForY(x, prevX, nextX, prevPercentile, nextPercentile);
            return y;
        }

        private readonly Population _population;
    }
}
