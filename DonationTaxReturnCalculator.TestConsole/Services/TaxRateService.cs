using System;
using System.Collections.Generic;
using System.Linq;
using DonationTaxReturnCalculator.TestConsole.DataModels;
using DonationTaxReturnCalculator.TestConsole.Repository;

namespace DonationTaxReturnCalculator.TestConsole.Services
{
    public class TaxRateService
    {
        private readonly Context _ctx;

        public TaxRateService(Context ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<TaxRate> ListOfAll()
        {
            return _ctx.Find<TaxRate>(i => i.IsDeleted == false);
        }
        
        public TaxRate UpsertTaxRate(TaxRate local)
        {
            //Minimal to no security, due to limited time..
            return _ctx.ReplaceOne(local, upsert: true);
        }
        
        
        public static (decimal amount, decimal ratio) CalcTaxReturn(decimal donationAmount, IEnumerable<TaxRate> rates)
        {
            var taxRate = rates.ToList().Sum(i => i.Rate);
            var ratio = taxRate / (100 - taxRate);
            var amount = Math.Min(donationAmount * ratio, 1000);
            return (amount, ratio);
        }

    }
}