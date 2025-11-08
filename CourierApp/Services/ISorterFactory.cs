using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Services
{
    public interface ISorterFactory
    {
        ISorter GetSorter(string sorterType);
    }
}
