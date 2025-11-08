using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Models;

namespace CourierApp.Services
{
    public class PackageDiscountCalculator : IPackageDiscountCalculator
    {
        public decimal CalculateDiscount(Package package, decimal baseCost)
        {
            decimal discount = 0;
            switch (package.OfferCode)
            {
                case "OFR001":
                    if (package.Distance < 200 && package.Weight >= 70 && package.Weight <= 200)
                        discount = baseCost * 0.10m;
                    break;
                case "OFR002":
                    if (package.Distance >= 50 && package.Distance <= 150 && package.Weight >= 100 && package.Weight <= 250)
                        discount = baseCost * 0.07m;
                    break;
                case "OFR003":
                    if (package.Distance >= 50 && package.Distance <= 250 && package.Weight >= 10 && package.Weight <= 150)
                        discount = baseCost * 0.05m;
                    break;
            }
            return discount;
        }
    }
}
