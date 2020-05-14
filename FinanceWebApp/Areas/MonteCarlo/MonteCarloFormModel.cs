using System.ComponentModel.DataAnnotations;

namespace FinanceWebApp.Areas.MonteCarlo
{
    public class MonteCarloFormModel
    {
        [Required]
        [Range(1, 100)]
        public int NumYears { get; set; } = 20;

        [Required]
        public int InitialValue { get; set; } = 200000;

        [Required]
        [Range(0, 10000000)] 
        public int Contribution { get; set; } = 18000;

        public bool InflationAdjusted { get; set; }
    }
}
