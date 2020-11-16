using System;
using System.Linq;
using System.Reflection;
using DonationTaxReturnCalculator.TestConsole.DataModels;

namespace DonationTaxReturnCalculator.TestConsole
{
    public static class ValidationExtensions
    {
        public static decimal ValidateDonationAmount(this decimal amount)
        {
            return amount > 0 
                ? amount 
                : throw new ArgumentException("donation amount should be more than 0.00");
        }
    }
}