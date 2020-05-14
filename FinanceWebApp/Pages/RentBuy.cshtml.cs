using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceWebApp.Pages
{
    public class RentBuyModel : PageModel
    {
        public double ResidencyLength { get; set; } = 5;

        public decimal InvestmentReturnRate { get; set; } = 10.5m;

        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal InflationRate { get; set; } = 2.5m;

        [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
        public decimal CapitalGainsTaxRate { get; set; } = 15m;

        [BindProperty]
        public int HousePrice { get; set; } = 200000;

        [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
        public decimal DownPaymentPercent { get; set; } = 20m;

        [BindProperty] 
        public int MortgageTerm { get; set; } = 30;

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal BuyingClosingCostRate { get; set; } = .0m;

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal SellingClosingCostRate { get; set; } = 6.0m;

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:F3}", ApplyFormatInEditMode = true)]
        public decimal InterestRate { get; set; } = 3.875m;

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal HousePriceGrowthRate { get; set; } = 2.8m;

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal PropertyTaxRate { get; set; } = 2.35m;

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal InsuranceRate { get; set; } = .9m;

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal MaintenanceCostRate { get; set; } = 1.5m;
    }
}
