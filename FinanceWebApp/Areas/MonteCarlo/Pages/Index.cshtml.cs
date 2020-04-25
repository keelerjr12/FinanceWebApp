using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FinanceWebLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceWebApp.Areas.MonteCarlo.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public MonteCarloFormModel MonteCarloFormModel { get; set; } = new MonteCarloFormModel();

        public class ChartDataPoint
        {
            public string Label { get; set; } = "";
            public IList<double> Data { get; set; } = new List<double>();
            public string[] BackgroundColor { get; set; } = new string[1];

            public string[] BorderColor { get; set; } = new string[1];
        }

        public IList<ChartDataPoint> TrialPercentiles { get; } = new List<ChartDataPoint>();

        public IEnumerable<int> XAxis { get; private set; } = new List<int>();

        public IDictionary<string, double> IRRs = new Dictionary<string, double>();

        public IActionResult OnGet()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var trials = FinanceWebLib.MonteCarlo.Run(5000, MonteCarloFormModel.NumYears, MonteCarloFormModel.InitialValue, MonteCarloFormModel.Contribution);

            XAxis = Enumerable.Range(0, MonteCarloFormModel.NumYears + 1);

            var percentiles = new List<double> { 1.0, .5, 0 };
            var percentTrials = RetrieveTrialsByPercentile(trials, percentiles);

            var max = percentTrials[0];
            var median = percentTrials[1];
            var min = percentTrials[2];

            TrialPercentiles.Add(new ChartDataPoint { Label = "Min", Data = min.Balances, BackgroundColor = new[] { ConvertColorToRGBAString(Color.OrangeRed, .6f) }, BorderColor = new[] { ConvertColorToRGBAString(Color.OrangeRed) } });
            TrialPercentiles.Add(new ChartDataPoint { Label = "Median", Data = median.Balances, BackgroundColor = new[] { ConvertColorToRGBAString(Color.LightYellow, .6f) }, BorderColor = new[] { ConvertColorToRGBAString(Color.LightYellow) } });
            TrialPercentiles.Add(new ChartDataPoint { Label = "Max", Data = max.Balances, BackgroundColor = new[] { ConvertColorToRGBAString(Color.LightSeaGreen, .6f) }, BorderColor = new[] { ConvertColorToRGBAString(Color.LightSeaGreen) } });

            IRRs.Add("Max", percentTrials[0].IRR);
            IRRs.Add("Median", percentTrials[1].IRR);
            IRRs.Add("Min", percentTrials[2].IRR);

            return Page();
        }

        private static string ConvertColorToRGBAString(Color color, float alpha = 1f)
        {
            return ConvertToRGBAString(color.R, color.G, color.B, alpha);
        }

        private static string ConvertToRGBAString(int r, int g, int b, float a)
        {
            return $"rgba({r}, {g}, {b}, {a})";
        }

        private static IList<Trial> RetrieveTrialsByPercentile(IEnumerable<Trial> trials, IEnumerable<double> percentiles)
        {
            var trialsByPercent = new List<Trial>();

            var sortedTrials = trials.OrderBy(t => t.Balances.Last()).ToList();
            var trialCount = sortedTrials.Count();

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
