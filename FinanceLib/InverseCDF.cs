using System.Collections.Generic;

namespace FinanceLib
{
    public class InverseCDF
    {
        public InverseCDF(IEnumerable<SurveyData> samples)
        {
            _population = new Population(samples);
        }

        public double Calculate(double p)
        {
            var nSamples = _population.NumSamplesWithinP(p);

            if (nSamples == 0)
                return _population.Min;

            var prevData = _population.GetDataOfSample(nSamples - 1);
            var nextData = _population.GetDataOfSample(nSamples);

            var prevProb = GetProbabilityThroughSamples(nSamples - 1);
            var nextProb = GetProbabilityThroughSamples(nSamples);

            var y = LinearInterpolator.SolveForY(p, prevProb, nextProb, prevData, nextData);

            return y;
        }

        private double GetProbabilityThroughSamples(int n)
        {
            var summedWeight = _population.SummedWeightOfNSamples(n + 1);
            var prob = summedWeight / _population.TotalWeight;

            return prob;
        }

        private readonly Population _population;
    }
}
