using System.Collections.Generic;
using System.Linq;

namespace FinanceLib
{
    public class Trial
    {
        public IList<double> Balances { get; } = new List<double>();
        public IList<double> CashFlows { get; } = new List<double>();

        public double IRR => CalculateIRR();

        public Trial(double initialValue)
        {
            Balances.Add(initialValue);
            CashFlows.Add(-1 * initialValue);
        }

        internal void CompoundAndContribute(double ret, double contribution)
        {
            var prevValue = Balances.Last();
            var currValue = prevValue * (1 + ret) + contribution;

            Balances.Add(currValue);
            CashFlows.Add(-1 * contribution);
        }

        private double CalculateIRR()
        {
            var cf = CashFlows;

            var totalBalance = Balances.Last();
            cf[^1] += totalBalance;

            return Financials.IRR(cf);
        }
    }
}
