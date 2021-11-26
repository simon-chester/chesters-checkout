using ChestersCheckout.Core.Models;
using ChestersCheckout.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestersCheckout.Core.Services
{
    public class BasketBuilderService
    {
        private readonly IProductRepositoryService _productRepository;

        public BasketBuilderService(IProductRepositoryService productRepository)
        {
            _productRepository = productRepository;
        }

        public Basket BuildBasket(string[] products)
        {
            var items = products.Select(SanitizeName)
                    .Where(_productRepository.IsValidProduct)
                    .GroupBy(s => s, g => 1)
                    .Select(g => new { g.Key, Count = g.Count() })
                    .ToDictionary(g => g.Key, g => g.Count);

            return new Basket(items);
        }


        private static string SanitizeName(string productName) => productName.Trim().ToLowerInvariant();
    }
}
