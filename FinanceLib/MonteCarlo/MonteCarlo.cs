using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;

namespace FinanceLib.MonteCarlo
{
    public static class MonteCarlo
    {
        private const double MEAN = .115861;
        private const double STDDEV = .180662;
        private const double INFLATION_RATE = .03;

        public static MonteCarloResults Run(int numTrials, int numYears, double initialValue, double contribution, bool inflationAdjusted)
        {
            if (numTrials < 1)
                throw new ArgumentOutOfRangeException(nameof(numTrials));
            if (numYears < 1)
                throw new ArgumentOutOfRangeException(nameof(numYears));
            if (contribution < 0)
                throw new ArgumentOutOfRangeException(nameof(contribution));

            var trials = new List<Trial>();

            for (var trialNum = 0; trialNum < numTrials; trialNum++)
            {
                var trial = GenerateTrial(numYears, initialValue, contribution, inflationAdjusted);
                trials.Add(trial);
            }

            return new MonteCarloResults(trials);
        }

        private static Trial GenerateTrial(int numYears, double initialValue, double contribution, bool inflationAdjusted)
        {
            var trial = new Trial(initialValue);

            for (var year = 1; year <= numYears; year++)
            {
                var ret = GenerateReturn();

                if (inflationAdjusted)
                    ret = ((1 + ret) / (1 + INFLATION_RATE)) - 1;

                trial.CompoundAndContribute(ret, contribution);
            }

            return trial;
        }

        private static double GenerateReturn()
        {
            var prob = new Random().NextDouble();
            return normDist.InverseCumulativeDistribution(prob);
        }

        private static readonly Normal normDist = new Normal(MEAN, STDDEV);
    }
}
