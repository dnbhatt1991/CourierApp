using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CourierApp.Services
{
    public class SorterFactory : ISorterFactory
    {
        private readonly IServiceProvider _provider;
        public SorterFactory(IServiceProvider provider) => _provider = provider;

        public ISorter GetSorter(string sorterType)
        {
            return sorterType.ToLower() switch
            {
                "bubble" => _provider.GetRequiredService<BubbleSorter>(),
                "quick" => _provider.GetRequiredService<QuickSorter>(),
                _ => _provider.GetRequiredService<QuickSorter>()
            };
        }
    }
}
