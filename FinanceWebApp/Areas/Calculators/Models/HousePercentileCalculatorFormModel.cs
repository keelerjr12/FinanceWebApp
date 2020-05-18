using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceWebApp.Areas.Calculators.Models
{
    public class HousePercentileCalculatorFormModel
    {
        private const int InitialMinAge = 18;
        private const int InitialMaxAge = 85;
        private const int ConstMinAge = 18;
        private const int ConstMaxAge = int.MaxValue;

        private const int InitialHouseValue = 190000;
        private const int ConstMinHouseValue = 0;
        private const int ConstMaxHouseValue = int.MaxValue;

        [Required]
        [Range(ConstMinAge, ConstMaxAge)]
        [DisplayName("minimum age")]
        public int MinAge { get; set; } = InitialMinAge;

        [Required]
        [Range(ConstMinAge, ConstMaxAge)]
        [DisplayName("maximum age")]
        public int MaxAge { get; set; } = InitialMaxAge;

        [Required]
        [Range(ConstMinHouseValue, ConstMaxHouseValue)]
        [DisplayName("house value")]
        public int HouseValue { get; set; } = InitialHouseValue;
    }
}
