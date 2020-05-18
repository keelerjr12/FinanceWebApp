using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FinanceWebLib;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.Areas.MonteCarlo.ViewComponents
{
    public class MonteCarloChartViewComponent : ViewComponent
    {
        public IEnumerable<int> XAxis { get; private set; } = new List<int>();

        public class ChartDataPoint
        {
            public string Label { get; set; } = "";
            public IList<double> Data { get; set; } = new List<double>();
            public string[] BackgroundColor { get; set; } = new string[1];
            public string[] BorderColor { get; set; } = new string[1];
        }

        public IList<ChartDataPoint> TrialPercentiles { get; } = new List<ChartDataPoint>();

        public IViewComponentResult Invoke(int numYears, Trial max, Trial median, Trial min)
        {
            XAxis = Enumerable.Range(0, numYears + 1);

            TrialPercentiles.Add(new ChartDataPoint { Label = "2.5%", Data = min.Balances, BackgroundColor = new[] { ConvertColorToRGBAString(Color.OrangeRed, .6f) }, BorderColor = new[] { ConvertColorToRGBAString(Color.OrangeRed) } });
            TrialPercentiles.Add(new ChartDataPoint { Label = "50%", Data = median.Balances, BackgroundColor = new[] { ConvertColorToRGBAString(Color.LightYellow, .6f) }, BorderColor = new[] { ConvertColorToRGBAString(Color.LightYellow) } });
            TrialPercentiles.Add(new ChartDataPoint { Label = "97.5%", Data = max.Balances, BackgroundColor = new[] { ConvertColorToRGBAString(Color.LightSeaGreen, .6f) }, BorderColor = new[] { ConvertColorToRGBAString(Color.LightSeaGreen) } });


            return View(this);
        }

        private static string ConvertColorToRGBAString(Color color, float alpha = 1f)
        {
            return ConvertToRGBAString(color.R, color.G, color.B, alpha);
        }

        private static string ConvertToRGBAString(int r, int g, int b, float a)
        {
            return $"rgba({r}, {g}, {b}, {a})";
        }
    }
}
