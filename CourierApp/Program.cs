using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CourierApp.Models;
using CourierApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourierApp
{
    /// <summary>
    /// Entry point for the courier sorting application.
    /// </summary>
    public class Program
    {
        private const decimal BaseCost = 1000m; // You can adjust this as needed

        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<QuickSorter>();
            services.AddSingleton<BubbleSorter>();
            services.AddSingleton<ISorterFactory, SorterFactory>();
            services.AddSingleton<IPackageDiscountCalculator, PackageDiscountCalculator>();
            // Default sorter: QuickSorter
            services.AddSingleton<ISorter>(sp => sp.GetRequiredService<QuickSorter>());
            services.AddSingleton<SortService>();

            var provider = services.BuildServiceProvider();

            // Choose sorter type from args (e.g., --sorter bubble)
            string sorterType = "quick";
            if (args.Length > 1 && args[0] == "--sorter")
                sorterType = args[1];

            var sorterFactory = provider.GetRequiredService<ISorterFactory>();
            var sorter = sorterFactory.GetSorter(sorterType);
            var sortService = new SortService(sorter, provider.GetRequiredService<IPackageDiscountCalculator>());

            IEnumerable<string> inputLines;
            string inputFile = args.FirstOrDefault(a => !a.StartsWith("--"));
            if (!string.IsNullOrEmpty(inputFile) && File.Exists(inputFile))
            {
                inputLines = File.ReadLines(inputFile);
            }
            else
            {
                inputLines = ReadStdinLines();
            }

            var packages = inputLines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParsePackage)
                .ToList();

            var results = sortService.Process(packages, BaseCost);

            foreach (var result in results)
            {
                Console.WriteLine($"{result.Id} {result.DiscountedCost:F2} {result.DiscountApplied:F2}");
            }
        }

        private static IEnumerable<string> ReadStdinLines()
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                yield return line;
            }
        }

        private static Package ParsePackage(string line)
        {
            // Expected format: <PackageId> <Weight> <Distance> <OfferCode>
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4)
                throw new FormatException("Invalid input format. Expected: <PackageId> <Weight> <Distance> <OfferCode>");

            return new Package
            {
                Id = parts[0],
                Weight = decimal.Parse(parts[1]),
                Distance = decimal.Parse(parts[2]),
                OfferCode = parts[3]
            };
        }
    }
}