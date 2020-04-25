using System.Collections.Generic;
using FinanceDataAccess;
using FinanceWebLib;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.Pages
{
    public class IncomePercentileCalculatorModel : CalculatorModel
    {
        [BindProperty(SupportsGet = true)]
        public IncomePercentileCalculatorFormModel IncomePercentileCalculatorFormModel { get; set; } = new IncomePercentileCalculatorFormModel();

        public IncomePercentileCalculatorModel(IncomeRepository incomeRepo)
        {
            _incomeRepo = incomeRepo;
        }

        protected override IList<SurveyData> GetSamples()
        {
            return _incomeRepo.GetSamples(
                IncomePercentileCalculatorFormModel.MinAge,
                IncomePercentileCalculatorFormModel.MaxAge);
        }

        protected override double GetRandomVariable()
        {
            return IncomePercentileCalculatorFormModel.Income;
        }

        private readonly IncomeRepository _incomeRepo;
    }
}
 