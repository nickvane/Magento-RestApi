using System;
using System.Globalization;
using System.Threading;
using Magento.RestApi.Models;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class ProductInventoryTests : BaseTest
    {
        [Test]
        public void WhenUpdatingStockQuantityByIdShouldBeCorrect()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var quantity = Math.Ceiling(new Random().NextDouble() * 100);

            // Act
            var response = Client.UpdateStockQuantityForProduct(productId, quantity).Result;
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(response.HasErrors);
            var updatedProduct = Client.GetProductById(productId).Result;
            Assert.AreEqual(quantity, updatedProduct.Result.stock_data.qty);
        }
        [Test]
        public void WhenUpdatingStockQuantityWith0ShouldBeCorrect()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var quantity = 0;

            // Act
            var response = Client.UpdateStockQuantityForProduct(productId, quantity).Result;
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(response.HasErrors);
            var updatedProduct = Client.GetProductById(productId).Result;
            Assert.AreEqual(quantity, updatedProduct.Result.stock_data.qty);
        }

        [Test]
        public void WhenUpdatingStockItemByIdShouldBeCorrect()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var quantity = Math.Ceiling(new Random().NextDouble() * 100);
            var isInStock = (quantity%2) == 1;
            var stockItem = new StockItem
                                {
                                    qty = quantity,
                                    is_in_stock = isInStock
                                };

            // Act
            var response = Client.UpdateStockItemForProduct(productId, stockItem).Result;
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(response.HasErrors);
            var updatedProduct = Client.GetProductById(productId).Result;
            Assert.AreEqual(quantity, updatedProduct.Result.stock_data.qty);
            Assert.AreEqual(isInStock, updatedProduct.Result.stock_data.is_in_stock);
        }
    }
}
