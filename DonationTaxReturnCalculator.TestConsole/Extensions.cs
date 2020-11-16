using System;
using System.Linq;
using System.Reflection;
using DonationTaxReturnCalculator.TestConsole.DataModels;

namespace DonationTaxReturnCalculator.TestConsole
{
    public static class Extensions
    {
        public static TaxRate DeepClone(this TaxRate rate)
        {
            return new TaxRate()
            {
                Id = rate.Id,
                Name = rate.Name,
                Rate = rate.Rate
            };
        }
    }
}