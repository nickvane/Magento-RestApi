using System;
using System.Threading;
using Xunit;

namespace Magento.RestApi.IntegrationTests
{
    public class ProductWebsiteTests : BaseFixture
    {
        private string _validSku = "100000";

        [Fact]
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
            Assert.NotNull(product.Result);
            Assert.Null(productForStore.Result);
            Assert.False(response.HasErrors, response.ErrorString);
            Assert.NotNull(newProductForStore.Result);
        }

        [Fact]
        public void WhenAssigningInvalidWebsiteToProductShouldReturnError()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;
            if(product.Result == null) throw new Exception(product.ErrorString);
            
            // Act
            var response = Client.AssignWebsiteToProduct(product.Result.entity_id, 999).Result;

            // Assert
            Assert.True(response.HasErrors);
        }

        [Fact]
        public void WhenUnAssigningInvalidWebsiteFromProductShouldReturnError()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.UnassignWebsiteFromProduct(product.Result.entity_id, 999).Result;

            // Assert
            Assert.True(response.HasErrors);
        }

        [Fact]
        public void WhenAssigningWebsiteToInvalidProductShouldReturnError()
        {
            // Arrange
            
            // Act
            var response = Client.AssignWebsiteToProduct(999999, 1).Result;

            // Assert
            Assert.True(response.HasErrors);
        }

        [Fact]
        public void WhenUnAssigningWebsiteFromInvalidProductShouldReturnError()
        {
            // Arrange
            
            // Act
            var response = Client.UnassignWebsiteFromProduct(999999, 1).Result;

            // Assert
            Assert.True(response.HasErrors);
        }

        [Fact]
        public void WhenAssigningWebsiteToProductThatIsAlreadyAssignedShouldBeOk()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.AssignWebsiteToProduct(product.Result.entity_id, 1).Result;

            // Assert
            Assert.False(response.HasErrors);
            Assert.True(response.Result);
        }

        [Fact]
        public void WhenUnAssigningWebsiteFromProductThatIsAlreadyUnAssignedShouldBeOk()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.UnassignWebsiteFromProduct(product.Result.entity_id, 3).Result;

            // Assert
            Assert.False(response.HasErrors);
            Assert.True(response.Result);
        }

        [Fact]
        public void WhenGettingWebsitesFromProductShouldReturnIds()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result;

            // Act
            var response = Client.GetWebsitesForProduct(product.Result.entity_id).Result;

            // Assert
            Assert.False(response.HasErrors);
            var websites = response.Result;
            Assert.NotNull(websites);
            Assert.Equal(2, websites.Count);
        }
    }
}
