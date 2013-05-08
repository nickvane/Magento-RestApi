using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class ThreadingTests : BaseTest
    {
        [Test]
        public void WhenUsingClientFromDifferentThreadsShouldDoEverythingSuccesfully()
        {
            var count = 0;
            var tasks = new Task[10];
            var _cancellationSource = new CancellationTokenSource();
            for (var i = 0; i < 10; i++)
            {
                int i1 = i;
                tasks[i] = Task.Factory.StartNew(() =>
                                                     {
                                                         for (int j = 0; j < 5; j++)
                                                         {
                                                             // get the product
                                                             var response = Client.GetProductBySku("100000").Result;
                                                             response.Result.price = (i1*10) + j;
                                                             // This simulates a token rejected 1 request could get. If this happens the client should authenticate again and retry the request 
                                                             // while locking the client for the other threads so that only 1 authentication process is ongoing
                                                             if (response.Result.price == 51) Client.AuthenticateAdmin(UserName, Password);
                                                             // update the product
                                                             var response2 = Client.UpdateProduct(response.Result).Result;
                                                             if (response2.HasErrors) throw new Exception(response2.Errors.First().Message);
                                                             count++;
                                                         }
                                                     }, _cancellationSource.Token);
            }
            Task.WaitAll(tasks);

            // Assert
            Assert.AreEqual(50, count);
        }
    }
}
