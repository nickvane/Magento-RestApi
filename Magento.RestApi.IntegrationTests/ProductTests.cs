using System;
using System.Linq;
using Magento.RestApi.Models;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class ProductTests : BaseTest
    {
        private string _validSku = "100000";

        [Test]
        public void WhenGettingProductWithValidIdShouldReturnProduct()
        {
            // Arrange
            
            // Act
            var response = Client.GetProductById(1).Result;

            // Assert
            Assert.IsNotNull(response.Result, response.ErrorString);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(_validSku, response.Result.sku);
        }

        [Test]
        public void WhenGettingProductWithInvalidIdShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetProductById(0).Result;

            // Assert
            Assert.IsNull(response.Result, response.ErrorString);
            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual("404", response.Errors.First().Code);
        }

        [Test]
        public void WhenGettingProductWithValidskuShouldReturnProduct()
        {
            // Arrange

            // Act
            var response = Client.GetProductBySku(_validSku).Result;

            // Assert
            Assert.IsNotNull(response.Result, response.ErrorString);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(1, response.Result.entity_id);
        }

        [Test]
        public void WhenGettingProductWithInvalidSkuShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetProductBySku("000000").Result;

            // Assert
            Assert.IsNull(response.Result);
            Assert.IsFalse(response.HasErrors, response.ErrorString);
        }

        [Test]
        public void WhenAddingANewProductShouldBeSaved()
        {
            // Arrange
            var sku = "200000";
            // create product with minimal required fields
            var product = new Product
                              {
                                  description = "A long description of the new product",
                                  short_description = "A short description of the new product",
                                  price = 12.5,
                                  sku = sku,
                                  visibility = ProductVisibility.CatalogSearch,
                                  status = ProductStatus.Enabled,
                                  name = "New product",
                                  weight = "10",
                                  tax_class_id = 2,
                                  type_id = "simple",
                                  attribute_set_id = 4 // default
                              };
            var existingProduct = Client.GetProductBySku(sku).Result;
            if (existingProduct.Result != null)
            {
                var delete = Client.DeleteProduct(existingProduct.Result.entity_id).Result;
                if(delete.HasErrors) throw new Exception(delete.Errors.First().Message);
            }
            
            // Act
            var response = Client.SaveNewProduct(product).Result;

            // Assert
            Assert.Less(0, response.Result);
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            var newProduct = Client.GetProductBySku(sku).Result;
            Assert.IsNotNull(newProduct.Result);
            Assert.AreEqual(12.5, newProduct.Result.price);
            Assert.IsFalse(newProduct.HasErrors);
        }

        [Test]
        public void CanUpdateProductWithPrice0()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result.Result;

            // Act
            product.price = 0;
            var response = Client.UpdateProduct(product).Result;
            
            // Assert
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.IsTrue(response.Result);
            var updatedProduct = Client.GetProductBySku(_validSku).Result.Result;
            Assert.AreEqual(0, updatedProduct.price);
        }
    }
}
