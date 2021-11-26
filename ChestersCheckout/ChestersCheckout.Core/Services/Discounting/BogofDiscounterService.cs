using ChestersCheckout.Core.Models;
using ChestersCheckout.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestersCheckout.Core.Services.Discounting
{
    public class BogofDiscounterService : IDiscounterService
    {
        private readonly string _productName;

        public BogofDiscounterService(string productName)
        {
            _productName = productName;
        }

        public int CalculateDiscount(Basket basket)
        {
            if (!basket.Items.TryGetValue(_productName, out var item))
                return 0;

            return item.UnitPrice * (item.Quantity / 2);
        }
    }
}
