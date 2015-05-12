using Xunit;

namespace Magento.RestApi.IntegrationTests
{
    public class WhenAuthenticating : BaseFixture
    {
        [Fact]
        public void WithValidCredentialsShouldReturnAuthenticatedClient()
        {
            // Arrange
            
            // Act
            var type = Client.GetType();

            // Assert
            Assert.Equal(typeof(MagentoApi), type);
        }

        [Fact]
        public void WithInValidUrlShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize("http://some.invalid.url.123.be", "w", "x")
                .SetCustomAdminUrlPart("");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Fact]
        public void WithInValidConsumerKeyShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, "w", "x")
                .SetCustomAdminUrlPart("");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Fact]
        public void WithInValidConsumerSecretShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, ConsumerKey, "x")
                .SetCustomAdminUrlPart("");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Fact]
        public void WithInValidAdminUrlPartShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, ConsumerKey, ConsumerSecret)
                .SetCustomAdminUrlPart("invalidcustomadminurlpart");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Fact]
        public void WithInValidUserNameShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, ConsumerKey, ConsumerSecret)
                .SetCustomAdminUrlPart(CustomAdminUrlPart);

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Fact]
        public void WithInValidPasswordShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, ConsumerKey, ConsumerSecret)
                .SetCustomAdminUrlPart(CustomAdminUrlPart);

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin(UserName, "z"));
        }
    }
}
