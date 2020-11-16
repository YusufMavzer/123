using System;
using System.Collections.Generic;
using System.Linq;
using DonationTaxReturnCalculator.TestConsole.DataModels;
using DonationTaxReturnCalculator.TestConsole.Services;
using DonationTaxReturnCalculator.TestConsole.Util;

namespace DonationTaxReturnCalculator.TestConsole.CommandLineFlows
{
    public class AdminFlow
    {
        private readonly TaxRateService _taxRateService;

        public AdminFlow(TaxRateService taxRateService)
        {
            IoC.Session = null;
            _taxRateService = taxRateService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                PrintTaxRates();
                Console.WriteLine("'I' INSERT | 'E' EDIT | 'D' DELETE | 'Q' QUITE ");
                Console.Write("Choice : ");
                var input = Console.ReadKey();
                var insert = input.Key == ConsoleKey.I;
                var edit = input.Key == ConsoleKey.E;
                var delete = input.Key == ConsoleKey.D;
                var quite = input.Key == ConsoleKey.Q;

                if (quite) return;
                if (insert)
                {
                    Console.WriteLine("\nName : ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Rate : ");
                    var rate = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("IsDefault y / n : ");
                    var isDefault = Console.ReadKey().Key == ConsoleKey.Y;
                    _taxRateService.UpsertTaxRate(new TaxRate
                    {
                        Name = name,
                        Rate = rate,
                        IsDefault = isDefault,
                        IsDeleted = false
                    });
                }

                if (edit)
                {
                    Console.Write("Select row number : ");
                    var rowNo =int.Parse(Console.ReadLine());
                    Console.WriteLine("\nName : ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Rate : ");
                    var rate = decimal.Parse(Console.ReadLine());
                    var lst = _taxRateService.ListOfAll().ToArray();
                    lst[rowNo].Name = name;
                    lst[rowNo].Rate = rate;
                    //This does actually nothing since they're just pointing to same data but let
                    //imagine it's using an nosql data instead of fixed array
                    _taxRateService.UpsertTaxRate(lst[rowNo]);
                }
                
                if (delete)
                {
                    Console.WriteLine("Select row number : ");
                    var rowNo =int.Parse(Console.ReadLine());
                    var lst = _taxRateService.ListOfAll().ToArray();
                    lst[rowNo].IsDeleted = true;
                    _taxRateService.UpsertTaxRate(lst[rowNo]);
                }
            }
        }

        void PrintTaxRates()
        {
            Console.WriteLine("Tax Rates");
            var lst = _taxRateService.ListOfAll().ToArray();
            var table = new List<Tuple<int, string, string, string>>();
            for (int i = 0; i < lst.Length; i++)
            {
                table.Add(Tuple.Create(i, lst[i].Id.ToString(), lst[i].Name, lst[i].Rate.ToString("0.00")));
            }

            Console.WriteLine(table.ToStringTable(new[] {"#", "Id", "Name", "Rate"},
                a => a.Item1,
                a => a.Item2,
                a => a.Item3,
                a => a.Item4)
            );
        }
    }
}