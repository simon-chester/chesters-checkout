using ChestersCheckout.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChestersCheckout.Core.Services.Abstractions
{
    public interface IDiscounterService
    {
        int CalculateDiscount(Basket basket);
    }
}
