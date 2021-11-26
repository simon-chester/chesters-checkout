using ChestersCheckout.Core.Services;
using ChestersCheckout.Core.Services.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChestersCheckout.Core.Tests.Services
{
    public class BasketBuilderServiceTests
    {
        [Fact]
        public void BuildBasket_ReturnsBasketWithoutInvalidProducts()
        {
            // Arrange
            var productRepoMock = new Mock<IProductRepositoryService>();
            productRepoMock.Setup(e => e.GetPrice(It.IsAny<string>()))
                .Returns<string>(s => s == "apple" ? 99 : null);
            var service = new BasketBuilderService(productRepoMock.Object);

            // Act
            var result = service.BuildBasket(new[] { "apple", "InvalidProduct" });

            // Assert
            Assert.Collection(result.Items,
                item =>
                {
                    Assert.Equal("apple", item.Key);
                    Assert.Equal(1, item.Value.Quantity);
                    Assert.Equal(99, item.Value.UnitPrice);
                });
        }

        [Fact]
        public void BuildBasket_ReturnsBasketWithProductCounts_GivenOnlyValidProducts()
        {
            // Arrange
            var productRepoMock = new Mock<IProductRepositoryService>();
            productRepoMock.Setup(e => e.GetPrice(It.IsAny<string>())).Returns(1);
            var service = new BasketBuilderService(productRepoMock.Object);

            // Act
            var result = service.BuildBasket(new[] { "apple", "orange", "apple" });

            // Assert
            Assert.Collection(result.Items.OrderBy(i => i.Key),
                item =>
                {
                    Assert.Equal("apple", item.Key);
                    Assert.Equal(2, item.Value.Quantity);
                    Assert.Equal(1, item.Value.UnitPrice);
                },
                item =>
                {
                    Assert.Equal("orange", item.Key);
                    Assert.Equal(1, item.Value.Quantity);
                    Assert.Equal(1, item.Value.UnitPrice);
                });
        }

        [Theory]
        [InlineData("pepper ")]
        [InlineData(" pepper")]
        [InlineData(" pEPPer")]
        [InlineData("PEPPER ")]
        public void BuildBasket_ReturnsBasketWithProductCounts_GivenCaseVariantProductNamesWithPadding(string productName)
        {
            // Arrange
            var productRepoMock = new Mock<IProductRepositoryService>();
            productRepoMock.Setup(e => e.GetPrice("pepper")).Returns(123);
            var service = new BasketBuilderService(productRepoMock.Object);

            // Act 
            var result = service.BuildBasket(new[] { productName });

            // Assert
            Assert.Collection(result.Items,
                item =>
                {
                    Assert.Equal("pepper", item.Key);
                    Assert.Equal(1, item.Value.Quantity);
                    Assert.Equal(123, item.Value.UnitPrice);
                });
        }
    }
}
