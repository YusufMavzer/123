using System;
using System.Collections.Generic;
using System.Linq;
using DonationTaxReturnCalculator.TestConsole.DataModels;
using DonationTaxReturnCalculator.TestConsole.Repository;

namespace DonationTaxReturnCalculator.TestConsole.Services
{
    public sealed class DonationService
    {
        
        private readonly Context _ctx;
        
        public DonationService(Context ctx) {  _ctx = ctx; }

        public IEnumerable<TaxRate> GetTaxRates()
        {
            return _ctx.Find<TaxRate>(i => i.IsDeleted == false);
        }

        public Donation MakeDonation(decimal amount, Entity entity)
        {
            amount.ValidateDonationAmount();
            var defaultTaxRate = _ctx.FindOne<TaxRate>(i => i.IsDefault);
            if(defaultTaxRate == null) throw new Exception("Default tax rate not found");
            var donation = CreateDonation(amount, new List<TaxRate>() {defaultTaxRate}, entity);
            return _ctx.InsertOne(donation);
        }

        public Donation MakeDonation(decimal amount, Entity entity, List<TaxRate> rates)
        {
            amount.ValidateDonationAmount();
            if (!rates.Any())
                throw new Exception("No tax rates found");
            var donation = CreateDonation(amount, rates, entity);
            return _ctx.InsertOne(donation);
        }
        
        private Donation CreateDonation(decimal amount, List<TaxRate> rates, Entity entity)
        {
            var taxReturn = TaxRateService.CalcTaxReturn(amount, rates);
            return new Donation()
            {
                Created = DateTime.UtcNow,
                TaxRates = rates.Select(i => i.DeepClone()).ToList(), //For a nosql solution we don't need to deep clone for a relational we have to deep clone
                DonationAmount = amount,
                TaxReturnAmount = taxReturn.amount,
                Ratio = taxReturn.ratio,
                Entity = entity
            };
        }
        
    }
}