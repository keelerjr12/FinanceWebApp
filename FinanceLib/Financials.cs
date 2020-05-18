using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceLib
{
    public static class Financials
    {
        public static double IRR(IList<double> cashflows)
        {
            return NonLinearSolver.Solve(NPV, NPVDeriv, cashflows);
        }

        public static double NPV(double rate, IList<double> cashflows)
        {
            return cashflows.Select((t, i) => t / Math.Pow(1 + rate, i)).Sum();
        }

        private static double NPVDeriv(double rate, IList<double> cashflows)
        {
            return cashflows.Select((t, i) => (t * -i * Math.Pow((1 + rate), -i - 1))).Sum();
        }
    }
}
