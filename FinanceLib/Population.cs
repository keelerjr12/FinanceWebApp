using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceLib
{
    public class Population
    {

        public Population(IEnumerable<SurveyData> samples)
        {
            _samples = samples.OrderBy(s => s.Data).ToList();
            TotalWeight = CalculateTotalWeight();
        }

        public double TotalWeight { get; }
        public double Min => _samples.First().Data;
        public double Max => _samples.Last().Data;

        public bool DoesXExist(double x)
        {
            const double eps = .000000000000001;
            return _samples.Exists(s => Math.Abs(s.Data - x) < eps);
        }

        public int NumSamplesWithinP(double p)
        {
            var weightWithinP = p * TotalWeight;
            var summedWgt = 0.0;

            for (var i = 0; i < _samples.Count; i++)
            {
                summedWgt += _samples[i].Weight;
                if (summedWgt >= weightWithinP)
                    return i;
            }

            return 0;
        }

        public double SummedWeightWithinX(double x)
        {
            return _samples.Where(s => s.Data <= x).Sum(s => s.Weight);
        }

        public double SummedWeightOfNSamples(int n)
        {
            return _samples.Take(n).Sum(s => s.Weight);
        }

        public double GetDataPrior(double x)
        {
            return _samples.Last(s => s.Data < x).Data;
        }

        public double GetDataOfSample(int sample)
        {
            return _samples[sample].Data;
        }

        public double GetDataAfter(double x)
        {
            return _samples.First(s => s.Data > x).Data;
        }

        private double CalculateTotalWeight()
        {
            return _samples.Sum(s => s.Weight);
        }

        private readonly List<SurveyData> _samples;
    }
}
