using System;
using System.Collections.Generic;

namespace DonationTaxReturnCalculator.TestConsole.DataModels
{
    public class Donation : IDataModel
    {
        public Guid Id { get; set; }
        public decimal DonationAmount { get; set; }
        public decimal TaxReturnAmount { get; set; }
        public decimal Ratio { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<TaxRate> TaxRates { get; set; }
        public Entity Entity { get; set; }
    }
}