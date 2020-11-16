using System;
using System.Collections.Generic;
using System.Linq;
using DonationTaxReturnCalculator.TestConsole.CommandLineFlows;
using DonationTaxReturnCalculator.TestConsole.Repository;
using DonationTaxReturnCalculator.TestConsole.Services;
using SimpleInjector;

namespace DonationTaxReturnCalculator.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           var container = new Container();
           container.RegisterSingleton<Context>();
           container.Register<DonationService>();
           container.Register<DonorService>();
           container.Register<TaxRateService>();
           container.Register<DonorFlow>();
           container.Register<AdminFlow>();
           container.Verify();
           IoC.ServiceProvider = container;
           Start();
        }
        
        static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("To quit press 'q'");
                Console.Write("Donor type 'd' / admin type 'a' : ");
                var userType = Console.ReadLine();
                if (userType == "q") break;
                if (userType == "d") IoC.ServiceProvider.GetInstance<DonorFlow>().Run();
                if (userType == "a") IoC.ServiceProvider.GetInstance<AdminFlow>().Run();
            }
        }
    }
}
