using ChestersCheckout.Core.Services;
using ChestersCheckout.Core.Services.Abstractions;
using ChestersCheckout.Core.Services.Discounting;
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

        [Theory]
        [InlineData(0, 75)]
        [InlineData(2, 135)]
        [InlineData(3, 195)]
        [InlineData(4, 195)]
        public void CalculateTotalCost_ReturnsDiscountedCost_WhenAppleBogofDiscounter(int appleQuantity, int expectedCost)
        {
            // Arrange
            var service = new BasketCostCalculatorService(new[] { new BogofDiscounterService("apple") });

            // Act
            var result = service.CalculateTotalCost(new Models.Basket(new Dictionary<string, (int Quantity, int UnitPrice)>
            {
                { "apple", (appleQuantity, 60) },
                { "orange", (3, 25) }
            }));

            // Assert
            Assert.Equal(expectedCost, result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 25)]
        [InlineData(2, 50)]
        [InlineData(3, 50)]
        [InlineData(4, 75)]
        [InlineData(5, 100)]
        [InlineData(6, 100)]
        public void CalculateTotalCost_ReturnsDiscountedCost_WhenThreeForTwoDiscounter(int quantity, int expectedCost)
        {
            // Arrange
            var service = new BasketCostCalculatorService(new[] { new ThreeForTwoDiscounterService("kiwi") });

            // Act
            var result = service.CalculateTotalCost(new Models.Basket(new Dictionary<string, (int Quantity, int UnitPrice)>
            {
                { "kiwi", (quantity, 25) }
            }));

            // Assert
            Assert.Equal(expectedCost, result);
        }
    }
}