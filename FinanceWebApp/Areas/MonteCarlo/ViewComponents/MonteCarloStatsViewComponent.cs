using System.Collections.Generic;
using FinanceWebLib;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.Areas.MonteCarlo.ViewComponents
{
    [ViewComponent]
    public class MonteCarloStatsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IDictionary<string, Trial> trials)
        {
            return View("Default", trials);
        }
    }
}
