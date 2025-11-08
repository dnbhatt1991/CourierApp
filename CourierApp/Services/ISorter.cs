using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Models;

namespace CourierApp.Services
{
    public interface ISorter
    {
        IEnumerable<PackageResult> Sort(IEnumerable<PackageResult> packages);
    }
}
