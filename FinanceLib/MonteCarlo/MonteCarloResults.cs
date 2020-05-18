using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceLib.MonteCarlo
{
    public class MonteCarloResults
    {
        public MonteCarloResults(IEnumerable<Trial> trials)
        {
            SortTrialsByEndingBalance(trials);
        }

        public IList<Trial> GetTrialsByPercentiles(IEnumerable<double> percentiles)
        {
            return percentiles.Select(GetTrialByPercentile).ToList();
        }

        private void SortTrialsByEndingBalance(IEnumerable<Trial> trials)
        {
            _sortedTrials = trials.OrderBy(t => t.Balances.Last()).ToList();
        }

        private Trial GetTrialByPercentile(double percentile)
        {
            var trialIdx = CalculateIndexOfPercentile(percentile);
            return _sortedTrials[trialIdx];
        }

        private int CalculateIndexOfPercentile(double percentile)
        {
            var numTrials = _sortedTrials.Count;

            var trialIdx = Convert.ToInt32(percentile * numTrials);
            trialIdx = Math.Min(trialIdx, numTrials - 1);

            return trialIdx;
        }

        private IList<Trial> _sortedTrials = new List<Trial>();
    }
}
