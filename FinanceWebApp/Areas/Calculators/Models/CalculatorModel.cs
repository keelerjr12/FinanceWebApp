using System;
using System.Collections.Generic;
using FinanceLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceWebApp.Areas.Calculators.Models
{
    public abstract class CalculatorModel : PageModel
    {
        public double Percentile { get; private set; }

        public IList<double> Percentiles { get; } = new List<double>();

        public IList<int> XAxis { get; } = new List<int>();

        public IActionResult OnGet()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var samples = GetSamples();

            var cdf = new CDF(samples);
            Percentile = cdf.Calculate(GetRandomVariable());

            var cdfInv = new InverseCDF(samples);

            const double eps = .0001;
            for (var perc = 0.0; perc <= .95 + eps; perc += .05)
            {
                var inv = cdfInv.Calculate(perc);
                Percentiles.Add(inv);
                XAxis.Add(Convert.ToInt32(perc * 100));
            }

            return Page();
        }

        protected abstract IList<SurveyData> GetSamples();

        protected abstract double GetRandomVariable();
    }
}
