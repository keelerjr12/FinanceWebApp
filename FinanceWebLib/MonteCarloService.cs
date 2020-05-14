using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceWebLib
{
    public class MonteCarloService
    {
        public IList<Trial> RetrieveTrialsByPercentile(IEnumerable<double> percentiles, int numYears, double initialValue, double contribution)
        {
            var trials = FinanceWebLib.MonteCarlo.Run(5000, numYears, initialValue, contribution);

            var sortedTrials = trials.OrderBy(t => t.Balances.Last()).ToList();
            var trialCount = sortedTrials.Count;

            var trialsByPercent = new List<Trial>();
            foreach (var percentile in percentiles)
            {
                var trialIdx = Convert.ToInt32(percentile * trialCount);
                trialIdx = Math.Min(trialIdx, trialCount - 1);

                var trial = sortedTrials[trialIdx];
                trialsByPercent.Add(trial);
            }

            return trialsByPercent;
        }
    }
}
