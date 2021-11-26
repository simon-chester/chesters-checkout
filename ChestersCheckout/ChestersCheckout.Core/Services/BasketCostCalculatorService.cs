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
        private readonly IEnumerable<IDiscounterService> _discounters;

        public BasketCostCalculatorService(IEnumerable<IDiscounterService> discounters)
        {
            _discounters = discounters;
        }

        public int CalculateTotalCost(Basket basket)
        {
            var cost = basket.Items
                .Select(item => new
                {
                    item.Key,
                    item.Value.Quantity,
                    item.Value.UnitPrice
                })
                .Sum(item => item.Quantity * item.UnitPrice);

            var discount = _discounters.Sum(d => d.CalculateDiscount(basket));

            return cost - discount;
        }
    }
}
