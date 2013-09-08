using System.Linq;
using Magento.RestApi.Models;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class CustomerTests : BaseTest
    {
        [Test]
        public void WhenGettingCustomerWithValidIdShouldReturnCustomer()
        {
            // Arrange
            
            // Act
            var response = Client.GetCustomerById(1).Result;

            // Assert
            Assert.IsNotNull(response.Result, response.ErrorString);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual("john.doe@microsoft.com", response.Result.email);
        }

        [Test]
        public void WhenGettingProductWithInvalidIdShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetCustomerById(0).Result;

            // Assert
            Assert.IsNull(response.Result, response.ErrorString);
            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual("404", response.Errors.First().Code);
        }

        [Test]
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
                Assert.IsFalse(response.HasErrors, response.ErrorString);
                Assert.Less(0, customerId);
                var newCustomer = Client.GetCustomerById(customerId).Result;
                Assert.IsFalse(newCustomer.HasErrors, newCustomer.ErrorString);
                Assert.IsNotNull(newCustomer.Result);
                Assert.AreEqual(customer.disable_auto_group_change, newCustomer.Result.disable_auto_group_change);
                Assert.AreEqual(customer.email, newCustomer.Result.email);
                Assert.AreEqual(customer.firstname, newCustomer.Result.firstname);
                Assert.AreEqual(customer.lastname, newCustomer.Result.lastname);
                Assert.AreEqual(customer.group_id, newCustomer.Result.group_id);
                Assert.AreEqual(customer.middlename, newCustomer.Result.middlename);
                Assert.IsNull(newCustomer.Result.password);
                Assert.AreEqual(customer.prefix, newCustomer.Result.prefix);
                Assert.AreEqual(customer.suffix, newCustomer.Result.suffix);
                Assert.AreEqual(customer.taxvat, newCustomer.Result.taxvat);
                Assert.AreEqual(customer.website_id, newCustomer.Result.website_id);
            }
            finally
            {
                var deleteResponse = Client.DeleteCustomer(customerId).Result;
            }
        }

        [Test]
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
                Assert.IsFalse(response2.HasErrors, response2.ErrorString);
                Assert.AreEqual(newCustomer.disable_auto_group_change, updatedCustomer.disable_auto_group_change);
                Assert.AreEqual(newCustomer.email, updatedCustomer.email);
                Assert.AreEqual(newCustomer.firstname, updatedCustomer.firstname);
                Assert.AreEqual(newCustomer.lastname, updatedCustomer.lastname);
                Assert.AreEqual(newCustomer.group_id, updatedCustomer.group_id);
                Assert.AreEqual(newCustomer.middlename, updatedCustomer.middlename);
                Assert.IsNull(updatedCustomer.password);
                Assert.AreEqual(newCustomer.prefix, updatedCustomer.prefix);
                Assert.AreEqual(newCustomer.suffix, updatedCustomer.suffix);
                Assert.AreEqual(newCustomer.taxvat, updatedCustomer.taxvat);
                Assert.AreEqual(newCustomer.website_id, updatedCustomer.website_id);
            }
            finally
            {
                var deleteResponse = Client.DeleteCustomer(customerId).Result;
            }
        }

        [Test]
        public void WhenSearchingForCustomersFirstNameIsJohnShouldReturn1()
        {
            // Arrange
            var filter = new Filter();
            filter.FilterExpressions.Add(new FilterExpression("firstname", ExpressionOperator.@in, "John"));

            // Act
            var response = Client.GetCustomers(filter).Result;

            // Assert
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.AreEqual(1, response.Result.Count);
            Assert.AreEqual("Doe", response.Result.First().lastname);
        }
    }
}