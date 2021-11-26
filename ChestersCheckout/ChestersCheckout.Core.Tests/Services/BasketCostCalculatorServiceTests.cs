using ChestersCheckout.Core.Services;
using ChestersCheckout.Core.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChestersCheckout.Core.Tests
{
    public class BasketCostCalculatorServiceTests
    {
        [Fact]
        public void CalculateTotalCost_ReturnsCost_GivenBasketWithValidItems()
        {
            // Arrange
            var service = new BasketCostCalculatorService(Enumerable.Empty<IDiscounterService>());

            // Act
            var result = service.CalculateTotalCost(new Models.Basket(new Dictionary<string, (int Quantity, int UnitPrice)>
            {
                { "apple", (2, 60) },
                { "orange", (3, 25) }
            }));

            // Assert
            Assert.Equal(195, result);
        }
    }
}