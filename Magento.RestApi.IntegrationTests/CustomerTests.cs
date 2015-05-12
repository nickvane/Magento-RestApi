using System.Linq;
using Magento.RestApi.Models;
using Xunit;

namespace Magento.RestApi.IntegrationTests
{
    public class CustomerTests : BaseFixture
    {
        [Fact]
        public void WhenGettingCustomerWithValidIdShouldReturnCustomer()
        {
            // Arrange
            
            // Act
            var response = Client.GetCustomerById(1).Result;

            // Assert
            Assert.NotNull(response.Result);
            Assert.False(response.HasErrors);
            Assert.Equal("john.doe@microsoft.com", response.Result.email);
        }

        [Fact]
        public void WhenGettingProductWithInvalidIdShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetCustomerById(0).Result;

            // Assert
            Assert.Null(response.Result);
            Assert.True(response.HasErrors);
            Assert.Equal("404", response.Errors.First().Code);
        }

        [Fact]
        public void WhenAddingANewCustomerShouldBeSaved()
        {
            var customerId = 0;
            try
            {
                // Arrange
                var customer = new Customer
                                   {
                                       disable_auto_group_change = true,
                                       email = "mary@gmail.com",
                                       firstname = "Mary",
                                       lastname = "Ann",
                                       group_id = 3,
                                       middlename = "L.",
                                       password = "321654b",
                                       prefix = "Ms.",
                                       suffix = "Sr.",
                                       taxvat = "654987321",
                                       website_id = 2
                                   };

                // Act
                var response = Client.CreateNewCustomer(customer).Result;
                customerId = response.Result;

                // Assert
                Assert.False(response.HasErrors, response.ErrorString);
                Assert.True(0 < customerId);
                var newCustomer = Client.GetCustomerById(customerId).Result;
                Assert.False(newCustomer.HasErrors, newCustomer.ErrorString);
                Assert.NotNull(newCustomer.Result);
                Assert.Equal(customer.disable_auto_group_change, newCustomer.Result.disable_auto_group_change);
                Assert.Equal(customer.email, newCustomer.Result.email);
                Assert.Equal(customer.firstname, newCustomer.Result.firstname);
                Assert.Equal(customer.lastname, newCustomer.Result.lastname);
                Assert.Equal(customer.group_id, newCustomer.Result.group_id);
                Assert.Equal(customer.middlename, newCustomer.Result.middlename);
                Assert.Null(newCustomer.Result.password);
                Assert.Equal(customer.prefix, newCustomer.Result.prefix);
                Assert.Equal(customer.suffix, newCustomer.Result.suffix);
                Assert.Equal(customer.taxvat, newCustomer.Result.taxvat);
                Assert.Equal(customer.website_id, newCustomer.Result.website_id);
            }
            finally
            {
                var deleteResponse = Client.DeleteCustomer(customerId).Result;
            }
        }

        [Fact]
        public void WhenUpdatingCustomerShouldBeSaved()
        {
            var customerId = 0;
            try
            {
                // Arrange
                var customer = new Customer
                {
                    disable_auto_group_change = true,
                    email = "mary@gmail.com",
                    firstname = "Mary",
                    lastname = "Ann",
                    group_id = 3,
                    middlename = "L.",
                    password = "321654b",
                    prefix = "Ms.",
                    suffix = "Sr.",
                    taxvat = "654987321",
                    website_id = 2
                };
                var response1 = Client.CreateNewCustomer(customer).Result;
                customerId = response1.Result;
                var newCustomer = Client.GetCustomerById(customerId).Result.Result;

                // Act
                newCustomer.disable_auto_group_change = false;
                newCustomer.email = "mary@microsoft.com";
                newCustomer.firstname = "Marie";
                newCustomer.lastname = "Annie";
                newCustomer.group_id = 2;
                newCustomer.middlename = "I.";
                newCustomer.prefix = "Mrs.";
                newCustomer.suffix = "jr.";
                newCustomer.taxvat = "147258369";
                var response2 = Client.UpdateCustomer(newCustomer).Result;

                // Assert
                var updatedCustomer = Client.GetCustomerById(customerId).Result.Result;
                Assert.False(response2.HasErrors, response2.ErrorString);
                Assert.Equal(newCustomer.disable_auto_group_change, updatedCustomer.disable_auto_group_change);
                Assert.Equal(newCustomer.email, updatedCustomer.email);
                Assert.Equal(newCustomer.firstname, updatedCustomer.firstname);
                Assert.Equal(newCustomer.lastname, updatedCustomer.lastname);
                Assert.Equal(newCustomer.group_id, updatedCustomer.group_id);
                Assert.Equal(newCustomer.middlename, updatedCustomer.middlename);
                Assert.Null(updatedCustomer.password);
                Assert.Equal(newCustomer.prefix, updatedCustomer.prefix);
                Assert.Equal(newCustomer.suffix, updatedCustomer.suffix);
                Assert.Equal(newCustomer.taxvat, updatedCustomer.taxvat);
                Assert.Equal(newCustomer.website_id, updatedCustomer.website_id);
            }
            finally
            {
                var deleteResponse = Client.DeleteCustomer(customerId).Result;
            }
        }

        [Fact]
        public void WhenSearchingForCustomersFirstNameIsJohnShouldReturn1()
        {
            // Arrange
            var filter = new Filter();
            filter.FilterExpressions.Add(new FilterExpression("firstname", ExpressionOperator.@in, "John"));

            // Act
            var response = Client.GetCustomers(filter).Result;

            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            Assert.Equal(1, response.Result.Count);
            Assert.Equal("Doe", response.Result.First().lastname);
        }
    }
}