using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Models;

namespace CourierApp.Services
{
    public class BubbleSorter : ISorter
    {
        public IEnumerable<PackageResult> Sort(IEnumerable<PackageResult> packages)
        {
            var list = new List<PackageResult>(packages);
            int n = list.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j].DiscountedCost > list[j + 1].DiscountedCost)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            return list;
        }
    }
}
