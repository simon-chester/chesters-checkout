using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestersCheckout.Core.Models
{
    public class Basket
    {
        public IDictionary<string, (int Quantity, int UnitPrice)> Items { get; set; }

        public Basket(IDictionary<string, (int, int)> items)
        {
            Items = items;
        }
    }
}
