using ChestersCheckout.Core.Services;
using System.Collections.Generic;
using Xunit;

namespace ChestersCheckout.Core.Tests
{
    public class BasketCostCalculatorServiceTests
    {
        [Fact]
        public void CalculateTotalCost_ReturnsCost_GivenBasketWithInvalidItems()
        {
            // Arrange
            var service = new BasketCostCalculatorService(new StaticProductRepositoryService());

            // Act
            var result = service.CalculateTotalCost(new Models.Basket(new Dictionary<string, int>
            {
                { "InvalidProduct", 2 }
            }));

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateTotalCost_ReturnsCost_GivenBasketWithValidItems()
        {
            // Arrange
            var service = new BasketCostCalculatorService(new StaticProductRepositoryService());

            // Act
            var result = service.CalculateTotalCost(new Models.Basket(new Dictionary<string, int>
            {
                { "apple", 2 },
                { "orange", 3 }
            }));

            // Assert
            Assert.Equal(195, result);
        }
    }
}