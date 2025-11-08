using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Models;

namespace CourierApp.Services
{
    public class SortService
    {
        private readonly ISorter _sorter;
        private readonly IPackageDiscountCalculator _discountCalculator;

        public SortService(ISorter sorter, IPackageDiscountCalculator discountCalculator)
        {
            _sorter = sorter;
            _discountCalculator = discountCalculator;
        }

        public IEnumerable<PackageResult> Process(IEnumerable<Package> packages, decimal baseCost)
        {
            var results = packages.Select(pkg =>
            {
                var discount = _discountCalculator.CalculateDiscount(pkg, baseCost);
                return new PackageResult
                {
                    Id = pkg.Id,
                    DiscountedCost = baseCost - discount,
                    DiscountApplied = discount
                };
            });
            return _sorter.Sort(results);
        }
    }
}
