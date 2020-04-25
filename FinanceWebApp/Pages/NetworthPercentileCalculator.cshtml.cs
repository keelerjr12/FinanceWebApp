using System.Collections.Generic;
using FinanceDataAccess;
using FinanceWebLib;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.Pages
{
    public class NetworthPercentileCalculatorModel : CalculatorModel
    {

        [BindProperty(SupportsGet = true)] 
        public NetworthPercentileCalculatorFormModel NetworthPercentileCalculatorFormModel { get; set; } = new NetworthPercentileCalculatorFormModel();


        public NetworthPercentileCalculatorModel(NetworthRepository networthRepo)
        {
            _networthRepo = networthRepo;
        }

        protected override IList<SurveyData> GetSamples()
        {
            return _networthRepo.GetSamples(
                NetworthPercentileCalculatorFormModel.MinAge,
                NetworthPercentileCalculatorFormModel.MaxAge);
        }

        protected override double GetRandomVariable()
        {
            return NetworthPercentileCalculatorFormModel.Networth;
        }

        private readonly NetworthRepository _networthRepo;
    }
}