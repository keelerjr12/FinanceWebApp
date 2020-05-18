using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.Areas.Calculators.ViewComponents
{
    public class PercentileChartViewComponent : ViewComponent
    {
        public IList<double> Percentiles { get; private set; } = new List<double>();

        public IList<int> XAxis { get; private set; } = new List<int>();

        public IViewComponentResult Invoke(IList<int> xAxis, IList<double> percentiles)
        {
            XAxis = xAxis;
            Percentiles = percentiles;

            return View(this);
        }
    }
}
