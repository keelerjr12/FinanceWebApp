using System.Collections.Generic;

namespace FinanceLib.MonteCarlo
{
    public class MonteCarloService
    {
        public IList<Trial> RetrieveTrialsByPercentile(IEnumerable<double> percentiles, int numYears, double initialValue, double contribution, bool inflationAdjusted)
        {
            var results = MonteCarlo.Run(5000, numYears, initialValue, contribution, inflationAdjusted);
            return results.GetTrialsByPercentiles(percentiles);
        }
    }
}
