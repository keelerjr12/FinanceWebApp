using System;
using System.Collections.Generic;

namespace FinanceWebLib
{
    public class Financials
    {
        public static double IRR(IList<double> cashflows)
        {
            var newRate = 0.0;
            const double epsilon = .00001;

            double error;
            do
            {
                var rate = newRate;

                var npv = NPV(rate, cashflows);
                var npvDeriv = NPVDeriv(rate, cashflows);
                // Newton's method
                newRate = rate - npv / npvDeriv;

                error = Math.Abs(newRate - rate);
            } while (error > epsilon);

            return newRate;
        }

        public static double NPV(double rate, IList<double> cashflows)
        {
            var npv = cashflows[0];

            for (var i = 1; i < cashflows.Count; i++)
            {
                npv += (cashflows[i] / Math.Pow((1 + rate), i));
            }

            return npv;
        }

        private static double NPVDeriv(double rate, IList<double> cashflows)
        {
            var npv = 0.0;

            for (var i = 0; i < cashflows.Count; i++)
            {
                npv += (cashflows[i] * -i * Math.Pow((1 + rate), -i - 1));
            }

            return npv;
        }
    }
}
