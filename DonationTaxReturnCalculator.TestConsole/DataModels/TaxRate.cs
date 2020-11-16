using System;

namespace DonationTaxReturnCalculator.TestConsole.DataModels
{
    public class TaxRate: IDataModel
    {
        public Guid Id { get; set; }
        public decimal Rate { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}