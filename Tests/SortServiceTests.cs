using System.Collections.Generic;
using CourierApp.Models;
using CourierApp.Services;
using Xunit;

namespace Tests
{
    public class SortServiceTests
    {
        [Fact]
        public void SortService_SortsByDiscountedCost_QuickSorter()
        {
            var sorter = new QuickSorter();

            var discountCalculator = new PackageDiscountCalculator();
            var service = new SortService(sorter, discountCalculator);
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 100, Distance = 100, OfferCode = "OFR002" },
                new Package { Id = "PKG2", Weight = 50, Distance = 50, OfferCode = "OFR003" }
            };
            decimal baseCost = 1000;

            var results = service.Process(packages, baseCost);

            var resultList = new List<PackageResult>(results);
            Assert.True(resultList[0].DiscountedCost <= resultList[1].DiscountedCost);
        }

        [Fact]
        public void SortService_SortsByDiscountedCost_BubbleSorter()
        {
            var sorter = new BubbleSorter();
            var discountCalculator = new PackageDiscountCalculator();
            var service = new SortService(sorter, discountCalculator);
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 100, Distance = 100, OfferCode = "OFR002" },
                new Package { Id = "PKG2", Weight = 50, Distance = 50, OfferCode = "OFR003" }
            };
            decimal baseCost = 1000;

            var results = service.Process(packages, baseCost);

            var resultList = new List<PackageResult>(results);
            Assert.True(resultList[0].DiscountedCost <= resultList[1].DiscountedCost);
        }

        [Fact]
        public void DiscountCalculator_NoOffer_AppliesZeroDiscount()
        {
            var discountCalculator = new PackageDiscountCalculator();
            var package = new Package { Id = "PKG3", Weight = 300, Distance = 300, OfferCode = "OFR001" };
            decimal baseCost = 1000;

            var discount = discountCalculator.CalculateDiscount(package, baseCost);

            Assert.Equal(0, discount);
        }

        [Fact]
        public void DiscountCalculator_ValidOffer_AppliesCorrectDiscount()
        {
            var discountCalculator = new PackageDiscountCalculator();
            var package = new Package { Id = "PKG4", Weight = 80, Distance = 150, OfferCode = "OFR001" };
            decimal baseCost = 1000;

            var discount = discountCalculator.CalculateDiscount(package, baseCost);

            Assert.Equal(100, discount); // 10% of 1000
        }
    }
}