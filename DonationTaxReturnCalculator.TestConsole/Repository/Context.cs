using System;
using System.Collections.Generic;
using System.Linq;
using DonationTaxReturnCalculator.TestConsole.DataModels;

namespace DonationTaxReturnCalculator.TestConsole.Repository
{
    public class Context
    {
        public ICollection<Entity> Entities { get; set; } = new List<Entity>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<TaxRate> Rates { get; set; } = new List<TaxRate>()
        {
            new TaxRate
            {
                Id = Guid.NewGuid(),
                IsDefault = true,
                Name = "default",
                Rate = 15.3m
            },
            new TaxRate
            {
                Id = Guid.NewGuid(),
                IsDefault = false,
                Name = "HumanRights",
                Rate = 5m
            },
            new TaxRate
            {
                Id = Guid.NewGuid(),
                IsDefault = false,
                Name = "Environmental",
                Rate = 3m
            }
        };
        
        public ICollection<T> GetCollection<T>() where T : IDataModel, new ()
        {
            var collection = this.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.PropertyType == typeof(ICollection<T>))
                ?.GetValue(this, null);

            return (ICollection<T>) collection;
        }
        
        public IEnumerable<T> Find<T>(Func<T, bool> predicate) where T: IDataModel, new ()
        {
            var collection = GetCollection<T>();
            return collection.Where(predicate).AsEnumerable();
        }
        
        public T FindOne<T>(Func<T, bool> predicate) where T: IDataModel, new ()
        {
            return Find(predicate).FirstOrDefault();
        }

        public T InsertOne<T>(T obj) where T: IDataModel, new ()
        {
            var collection = GetCollection<T>();
            obj.Id = Guid.NewGuid();
            collection.Add(obj);
            return obj;
        }
        
        public void InsertMany<T>(IEnumerable<T> obj) where T: IDataModel, new ()
        {
            var collection = GetCollection<T>();
            obj.ToList().ForEach(i =>
            {
                i.Id = Guid.NewGuid();
                collection.Add(i);
            });
        }
        
        public T ReplaceOne<T>(T obj, bool upsert) where T: IDataModel, new ()
        {
            var collection = GetCollection<T>();
            var remote = collection.FirstOrDefault(i => i.Id == obj.Id);
            
            if(remote == null && !upsert)
                throw new Exception("Item not found");
            
            if(remote != null)
                collection.Remove(remote);

            return InsertOne(obj);
        }
    }
}