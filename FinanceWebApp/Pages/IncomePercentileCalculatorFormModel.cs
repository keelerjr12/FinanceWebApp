using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceWebApp.Pages
{
    public class IncomePercentileCalculatorFormModel
    {

        private const int InitialMinAge = 18;
        private const int InitialMaxAge = 85;
        private const int ConstMinAge = 18;
        private const int ConstMaxAge = int.MaxValue;

        private const int InitialIncome = 53000;
        private const int ConstMinIncome = 0;
        private const int ConstMaxIncome = int.MaxValue;

        [Required]
        [Range(ConstMinAge, ConstMaxAge)]
        [DisplayName("minimum age")]
        public int MinAge { get; set; } = InitialMinAge;

        [Required]
        [Range(ConstMinAge, ConstMaxAge)]
        [DisplayName("maximum age")]
        public int MaxAge { get; set; } = InitialMaxAge;

        [Required]
        [Range(ConstMinIncome, ConstMaxIncome)]
        [DisplayName("income")]
        public int Income { get; set; } = InitialIncome;
    }
}
