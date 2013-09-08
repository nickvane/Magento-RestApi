using System.Linq;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class ProductCategoryTests : BaseTest
    {
        [Test]
        public void ShouldGetCategoriesForProduct()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;

            // Act
            var response = Client.GetCategoriesForProduct(productId).Result;

            // Assert
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(2, response.Result.Count);
        }

        [Test]
        public void UnassignAndAssignCategoryToProduct()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var lastCategory = Client.GetCategoriesForProduct(productId).Result.Result.Last();

            // Act
            var response1 = Client.UnAssignCategoryFromProduct(productId, lastCategory).Result;
            var response2 = Client.GetCategoriesForProduct(productId).Result;
            var response3 = Client.AssignCategoryToProduct(productId, lastCategory).Result;
            var response4 = Client.GetCategoriesForProduct(productId).Result;
            
            // Assert
            Assert.IsFalse(response1.HasErrors);
            Assert.AreEqual(1, response2.Result.Count);
            Assert.IsFalse(response3.HasErrors);
            Assert.AreEqual(2, response4.Result.Count);
        }

        [Test]
        public void AssigningUnknownCategoryShouldBeHandled()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;

            // Act
            var response = Client.AssignCategoryToProduct(productId, 666).Result;

            // Assert
            Assert.IsTrue(response.HasErrors);
        }
        
        [Test]
        public void AssigningCategoryToUnknownProductShouldBeHandled()
        {
            // Arrange

            // Act
            var response = Client.AssignCategoryToProduct(999999, 1).Result;

            // Assert
            Assert.IsTrue(response.HasErrors);
        }

        [Test]
        public void AssigningCategoryToProductThatAlreadyHasCategoryShouldBeOk()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var categories = Client.GetCategoriesForProduct(productId).Result.Result;

            // Act
            var response = Client.AssignCategoryToProduct(productId, categories.Last()).Result;

            // Assert
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(categories.Count, Client.GetCategoriesForProduct(productId).Result.Result.Count);
        }

        [Test]
        public void UnassigningCategoryFromProductThatIsNotAssignedShouldBeOk()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var categories = Client.GetCategoriesForProduct(productId).Result.Result;

            // Act
            var response = Client.UnAssignCategoryFromProduct(productId, 1).Result;

            // Assert
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(categories.Count, Client.GetCategoriesForProduct(productId).Result.Result.Count);
        }
    }
}
