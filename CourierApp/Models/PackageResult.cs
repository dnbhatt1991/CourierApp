using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Models
{
    public class PackageResult
    {
        public string Id { get; set; }
        public decimal DiscountedCost { get; set; }
        public decimal DiscountApplied { get; set; }
    }
}
