using ChestersCheckout.Core.Models;
using ChestersCheckout.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestersCheckout.Core.Services
{
    public class BasketCostCalculatorService
    {
        private readonly IProductRepositoryService _productRepository;

        public BasketCostCalculatorService(IProductRepositoryService productRepository)
        {
            _productRepository = productRepository;
        }

        public int CalculateTotalCost(Basket basket) => basket.Items
                .Select(item => new
                {
                    item.Key,
                    Quantity = item.Value,
                    Price = _productRepository.GetPrice(item.Key)
                })
                .Where(item => item.Price.HasValue)
                .Sum(item => item.Quantity * item.Price!.Value);
    }
}
