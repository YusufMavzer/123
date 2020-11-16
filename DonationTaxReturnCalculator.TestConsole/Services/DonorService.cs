using System;
using System.Collections.Generic;
using DonationTaxReturnCalculator.TestConsole.DataModels;
using DonationTaxReturnCalculator.TestConsole.Repository;

namespace DonationTaxReturnCalculator.TestConsole.Services
{
    public class DonorService
    {
        private readonly Context _ctx;

        public DonorService(Context ctx)
        {
            _ctx = ctx;
        }

        public Entity CreateEntity(string fullName, string socialSecurityNumber)
        {
            return _ctx.InsertOne(new Entity
            {
                FullName = fullName,
                SocialSecurityNumber = socialSecurityNumber
            });
        }

        public Entity GetEntity(string socialSecurityNumber)
        {
            return _ctx.FindOne<Entity>(i => i.SocialSecurityNumber == socialSecurityNumber);
        }

        public IEnumerable<Donation> ListDonations(string socialSecurityNumber)
        {
            return _ctx.Find<Donation>(i =>
                i.Entity.SocialSecurityNumber == socialSecurityNumber);
        }
    }
}