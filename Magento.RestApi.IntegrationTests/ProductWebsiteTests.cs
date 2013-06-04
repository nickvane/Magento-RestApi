using System;
using System.Threading;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class ProductWebsiteTests : BaseTest
    {
        private string _validSku = "100000";

        [Test]
        public void WhenAssigningWebsiteToProductShouldBeAssigned()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;
            var productForStore = Client.GetProductBySkuForStore(_validSku, 1).Result;
            if (productForStore.Result != null)
            {
                Client.UnassignWebsiteFromProduct(productForStore.Result.entity_id, 1);
                Thread.Sleep(1000);
            }

            // Act
            productForStore = Client.GetProductBySkuForStore(_validSku, 1).Result;
            var response = Client.AssignWebsiteToProduct(product.Result.entity_id, 1).Result;
            Thread.Sleep(1000);
            var newProductForStore = Client.GetProductBySkuForStore(_validSku, 1).Result;

            // Assert
            Assert.IsNotNull(product.Result);
            Assert.IsNull(productForStore.Result);
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.IsNotNull(newProductForStore.Result);
        }

        [Test]
        public void WhenAssigningInvalidWebsiteToProductShouldReturnError()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;
            if(product.Result == null) throw new Exception(product.ErrorString);
            
            // Act
            var response = Client.AssignWebsiteToProduct(product.Result.entity_id, 999).Result;

            // Assert
            Assert.IsTrue(response.HasErrors);
        }

        [Test]
        public void WhenUnAssigningInvalidWebsiteFromProductShouldReturnError()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.UnassignWebsiteFromProduct(product.Result.entity_id, 999).Result;

            // Assert
            Assert.IsTrue(response.HasErrors);
        }

        [Test]
        public void WhenAssigningWebsiteToInvalidProductShouldReturnError()
        {
            // Arrange
            
            // Act
            var response = Client.AssignWebsiteToProduct(999999, 1).Result;

            // Assert
            Assert.IsTrue(response.HasErrors);
        }

        [Test]
        public void WhenUnAssigningWebsiteFromInvalidProductShouldReturnError()
        {
            // Arrange
            
            // Act
            var response = Client.UnassignWebsiteFromProduct(999999, 1).Result;

            // Assert
            Assert.IsTrue(response.HasErrors);
        }

        [Test]
        public void WhenAssigningWebsiteToProductThatIsAlreadyAssignedShouldBeOk()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.AssignWebsiteToProduct(product.Result.entity_id, 1).Result;

            // Assert
            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(response.Result);
        }

        [Test]
        public void WhenUnAssigningWebsiteFromProductThatIsAlreadyUnAssignedShouldBeOk()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.UnassignWebsiteFromProduct(product.Result.entity_id, 3).Result;

            // Assert
            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(response.Result);
        }

        [Test]
        public void WhenGettingWebsitesFromProductShouldReturnIds()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.GetWebsitesForProduct(product.Result.entity_id).Result;

            // Assert
            Assert.IsFalse(response.HasErrors);
            var websites = response.Result;
            Assert.IsNotNull(websites);
            Assert.AreEqual(2, websites.Count);
        }
    }
}
