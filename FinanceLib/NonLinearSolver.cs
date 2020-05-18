using System;
using System.Collections.Generic;

namespace FinanceLib
{
    public static class NonLinearSolver
    {
        private const double EPSILON = .00001;

        // Newton's method
        public static double Solve(Func<double, IList<double>, double> func,
            Func<double, IList<double>, double> funcDeriv, IList<double> data)
        {
            var newRate = 0.0;

            double error;
            do
            {
                var rate = newRate;

                var funcRet = func(rate, data);
                var funcDerivRet = funcDeriv(rate, data);

                newRate = rate - funcRet / funcDerivRet;

                error = Math.Abs(newRate - rate);
            } while (error > EPSILON);

            return newRate;
        }
    }
}
