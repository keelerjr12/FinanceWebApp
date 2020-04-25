using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceWebApp.Pages
{
    public class NetworthPercentileCalculatorFormModel
    {
        private const int InitialMinAge = 18;
        private const int InitialMaxAge = 85;
        private const int ConstMinAge = 18;
        private const int ConstMaxAge = int.MaxValue;

        private const int InitialNetworth = 100000;
        private const int ConstMinNetworth = int.MinValue;
        private const int ConstMaxNetworth = int.MaxValue;

        [Required]
        [Range(ConstMinAge, ConstMaxAge)]
        [DisplayName("minimum age")]
        public int MinAge { get; set; } = InitialMinAge;

        [Required]
        [Range(ConstMinAge, ConstMaxAge)]
        [DisplayName("maximum age")]
        public int MaxAge { get; set; } = InitialMaxAge;

        [Required]
        [Range(ConstMinNetworth, ConstMaxNetworth)]
        [DisplayName("networth")]
        public int Networth { get; set; } = InitialNetworth;
    }
}
