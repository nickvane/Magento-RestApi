using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class WhenAuthenticating : BaseTest
    {
        [Test]
        public void WithValidCredentialsShouldReturnAuthenticatedClient()
        {
            // Arrange
            
            // Act
            var type = Client.GetType();

            // Assert
            Assert.AreEqual(typeof(MagentoApi), type);
        }

        [Test]
        public void WithInValidUrlShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize("http://some.invalid.url.123.be", "w", "x")
                .SetCustomAdminUrlPart("");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Test]
        public void WithInValidConsumerKeyShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, "w", "x")
                .SetCustomAdminUrlPart("");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Test]
        public void WithInValidConsumerSecretShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, ConsumerKey, "x")
                .SetCustomAdminUrlPart("");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Test]
        public void WithInValidAdminUrlPartShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, ConsumerKey, ConsumerSecret)
                .SetCustomAdminUrlPart("invalidcustomadminurlpart");

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Test]
        public void WithInValidUserNameShouldThrowException()
        {
            // Arrange
            var client = new MagentoApi()
                .Initialize(Url, ConsumerKey, ConsumerSecret)
                .SetCustomAdminUrlPart(CustomAdminUrlPart);

            // Act & assert
            Assert.Throws<MagentoApiException>(() => client.AuthenticateAdmin("y", "z"));
        }

        [Test]
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
