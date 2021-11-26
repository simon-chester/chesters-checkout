using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestersCheckout.Core.Models
{
    public class Basket
    {
        public IDictionary<string, int> Items { get; set; }

        public Basket(IDictionary<string, int> items)
        {
            Items = items;
        }
    }
}
