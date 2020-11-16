using System;

namespace DonationTaxReturnCalculator.TestConsole.DataModels
{
    public class Entity : IDataModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SocialSecurityNumber { get; set; }
    }
}