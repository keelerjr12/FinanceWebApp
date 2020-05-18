using System.Collections.Generic;
using FinanceDataAccess;
using FinanceWebApp.Areas.Calculators.Models;
using FinanceWebLib;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.Areas.Calculators.Pages
{
    public class HousePercentileCalculatorModel : CalculatorModel
    {
        [BindProperty(SupportsGet = true)]
        public HousePercentileCalculatorFormModel HousePercentileCalculatorFormModel { get; set; } = new HousePercentileCalculatorFormModel();

        public HousePercentileCalculatorModel(HouseRepository houseRepo)
        {
            _houseRepo = houseRepo;
        }

        protected override IList<SurveyData> GetSamples()
        {
            return _houseRepo.GetSamples(
                HousePercentileCalculatorFormModel.MinAge,
                HousePercentileCalculatorFormModel.MaxAge);
        }

        protected override double GetRandomVariable()
        {
            return HousePercentileCalculatorFormModel.HouseValue;
        }

        private readonly HouseRepository _houseRepo;
    }
}
