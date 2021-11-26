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
                    .GroupBy(s => s, g => 1)
                    .Select(g => new { g.Key, Quantity = g.Count(), UnitPrice = _productRepository.GetPrice(g.Key) })
                    .Where(x => x.UnitPrice.HasValue)
                    .ToDictionary(g => g.Key, g => (g.Quantity, g.UnitPrice!.Value));

            return new Basket(items);
        }


        private static string SanitizeName(string productName) => productName.Trim().ToLowerInvariant();
    }
}
