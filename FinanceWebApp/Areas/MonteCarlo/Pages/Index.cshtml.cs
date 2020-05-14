using System;
using System.Collections.Generic;
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

            var percentiles = new List<double> { 1.0, .5, 0 };
            var percentTrials = _monteCarloService.RetrieveTrialsByPercentile(percentiles, MonteCarloFormModel.NumYears, MonteCarloFormModel.InitialValue, MonteCarloFormModel.Contribution);

            var max = percentTrials[0];
            var median = percentTrials[1];
            var min = percentTrials[2];

            Trials.Add("Max", max);
            Trials.Add("Median", median);
            Trials.Add("Min", min);

            return Page();
        }

        private readonly MonteCarloService _monteCarloService;
    }
}
