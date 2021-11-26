using ChestersCheckout.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestersCheckout.Core.Services
{
    public class StaticProductRepositoryService : IProductRepositoryService
    {
        private readonly Dictionary<string, int> _products = new()
        {
            { "apple", 60 },
            { "orange", 25 }
        };

        public int? GetPrice(string productName)
            => _products.TryGetValue(productName, out var price) ? price : null;

        public bool IsValidProduct(string productName) => _products.ContainsKey(productName);
    }
}
