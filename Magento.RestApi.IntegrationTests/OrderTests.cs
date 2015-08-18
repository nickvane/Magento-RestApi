using Xunit;

namespace Magento.RestApi.IntegrationTests
{
    public class OrderTests : BaseFixture
    {
        [Fact]
        public void WhenGettingOrderWithValidIdShouldReturnOrder()
        {
            // Arrange

            // Act
            var response = Client.GetOrderById(100).Result;

            // Assert
            Assert.NotNull(response.Result);
            Assert.False(response.HasErrors);
            Assert.Equal("EUR", response.Result.base_currency_code);
        }
    }
}
