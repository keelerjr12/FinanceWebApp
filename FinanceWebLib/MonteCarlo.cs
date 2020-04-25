using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;

namespace FinanceWebLib
{
    public class MonteCarlo
    {
        private const double MEAN = .115861;
        private const double STDDEV = .180662;

        public static IList<Trial> Run(int numTrials, int numYears, double initialValue, double contribution)
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
                var trial = GenerateTrial(numYears, initialValue, contribution);
                trials.Add(trial);
            }

            return trials;
        }

        private static Trial GenerateTrial(int numYears, double initialValue, double contribution)
        {
            var trial = new Trial(initialValue);

            for (var year = 1; year <= numYears; year++)
            {
                var ret = GenerateReturn();
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
