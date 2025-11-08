using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Models;

namespace CourierApp.Services
{
    public class QuickSorter : ISorter
    {
        public IEnumerable<PackageResult> Sort(IEnumerable<PackageResult> packages)
        {
            return packages.OrderBy(p => p.DiscountedCost);
        }
    }
}
