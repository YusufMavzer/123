using DonationTaxReturnCalculator.TestConsole.DataModels;
using SimpleInjector;

namespace DonationTaxReturnCalculator.TestConsole
{
    public static class IoC
    {
        public static Container ServiceProvider { get; set; }
        public static Entity Session { get; set; }
    }
}