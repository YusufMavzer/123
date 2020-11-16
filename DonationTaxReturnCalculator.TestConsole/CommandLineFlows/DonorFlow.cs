using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DonationTaxReturnCalculator.TestConsole.DataModels;
using DonationTaxReturnCalculator.TestConsole.Services;
using DonationTaxReturnCalculator.TestConsole.Util;

namespace DonationTaxReturnCalculator.TestConsole.CommandLineFlows
{
    public class DonorFlow
    {
        private readonly DonorService _donorService;
        private readonly DonationService _donationService;
       

        public DonorFlow(DonationService donationService, DonorService donorService)
        {
            _donorService = donorService;
            _donationService = donationService;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Please fill in your social security number");
            var number = Console.ReadLine();
            IoC.Session = _donorService.GetEntity(number);
            if (IoC.Session == null)
            {
                Console.WriteLine("Please enter full name: ");
                var fullname = Console.ReadLine();
                IoC.Session = _donorService.CreateEntity(fullname, number);
            }

            Menu();
        }

        private void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1: Make a donation");
                Console.WriteLine("2: Calculate your tax refund");
                Console.WriteLine("q: Quit");
                Console.Write("Choice : ");
                var choice = Console.ReadLine()?.ToLower();
                if (choice == "q") return;
                if (choice == "1")
                {
                    MakeDonation();
                }

                if (choice == "2")
                {
                    ConsultTaxRefund();
                }
            }
        }

        private void MakeDonation()
        {
            var lst = _donationService.GetTaxRates().ToList();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please Enter donation amount");
                Console.Write("Amount: ");
                if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture,
                    out decimal amount))
                    continue;
                Console.WriteLine("Please Enter donation types");
                for (var i = 0; i < lst.Count; i++)
                    Console.WriteLine($"{i}: {lst[i].Name}");
                Console.WriteLine("Choice : ");
                if(!int.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out int selectedIndex)) continue;
                var selectedRates = new List<TaxRate>()
                {
                    lst.First(i => i.IsDefault == true),
                    lst[selectedIndex]
                }.Distinct();
                _donationService.MakeDonation(amount, IoC.Session, selectedRates.ToList());
                break;
            }
        }

        private void ConsultTaxRefund()
        {
            Console.Clear();
            var donations =  _donorService.ListDonations(IoC.Session.SocialSecurityNumber);
            if (!donations.Any())
            {
                Console.WriteLine("You didn't make any donations");
                Console.Read();
            }

            Console.WriteLine(donations.ToStringTable(new[]
                {
                    "Id",
                    "Created",
                    "Ratio",
                    "Tax Return Amount",
                    "Donation Amount"
                },
                i => i.Id,
                i => i.Created.ToString("yyyy-MM-dd HH:mm"),
                i => i.Ratio.ToString("0.00"),
                i => i.TaxReturnAmount.ToString("0.00"),
                i => i.DonationAmount.ToString("0.00")));
            
            Console.WriteLine($"---------------------------------------------------------------------");
            Console.WriteLine($"Total: {donations.Sum(i => i.TaxReturnAmount):0.00}");
            Console.ReadKey();
        }
    }
}