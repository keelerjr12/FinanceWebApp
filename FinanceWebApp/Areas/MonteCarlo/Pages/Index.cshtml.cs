using System;
using System.Collections.Generic;
using System.Linq;
using FinanceWebLib;
using FinanceWebLib.MonteCarlo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceWebApp.Areas.MonteCarlo.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public MonteCarloFormModel MonteCarloFormModel { get; set; } = new MonteCarloFormModel();

        public IEnumerable<int> XAxis { get; private set; } = new List<int>();

        public readonly IDictionary<string, Trial> Trials = new Dictionary<string, Trial>();

        public IndexModel(MonteCarloService monteCarloService)
        {
            _monteCarloService = monteCarloService;
        }

        public IActionResult OnGet()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            XAxis = Enumerable.Range(0, MonteCarloFormModel.NumYears + 1);

            var percentiles = new List<double> { .975, .5, .025 };
            var percentTrials = _monteCarloService.RetrieveTrialsByPercentile(percentiles, 
                MonteCarloFormModel.NumYears, 
                MonteCarloFormModel.InitialValue, 
                MonteCarloFormModel.Contribution,
                MonteCarloFormModel.InflationAdjusted);

            var max = percentTrials[0];
            var median = percentTrials[1];
            var min = percentTrials[2];

            Trials.Add("97.5%", max);
            Trials.Add("50%", median);
            Trials.Add("2.5%", min);

            return Page();
        }

        private readonly MonteCarloService _monteCarloService;
    }
}
