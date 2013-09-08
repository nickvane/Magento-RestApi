using System;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    public class BaseTest
    {
        private readonly Dictionary<string, IMagentoApi> magentoApis = new Dictionary<string, IMagentoApi>();
        private readonly object _object = new object();

        protected IMagentoApi Client { 
            get { return InitializeClient(); }
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            Url = ConfigurationManager.AppSettings["url"];
            CustomAdminUrlPart = ConfigurationManager.AppSettings["adminurlpart"];
            ConsumerKey = ConfigurationManager.AppSettings["consumerkey"];
            ConsumerSecret = ConfigurationManager.AppSettings["consumersecret"];
            UserName = ConfigurationManager.AppSettings["username"];
            Password = ConfigurationManager.AppSettings["password"];
        }

        protected string Url { get; set; }
        protected string CustomAdminUrlPart { get; set; }
        protected string ConsumerKey { get; set; }
        protected string ConsumerSecret { get; set; }
        protected string UserName { get; set; }
        protected string Password { get; set; }

        protected IMagentoApi InitializeClient()
        {
            lock (_object)
            {
                var uri = new Uri(Url);
                if (!magentoApis.ContainsKey(uri.Host))
                {
                    var api = new MagentoApi()
                        .SetCustomAdminUrlPart(CustomAdminUrlPart)
                        .Initialize(Url, ConsumerKey, ConsumerSecret)
                        .AuthenticateAdmin(UserName, Password);
                    magentoApis.Add(uri.Host, api);
                }
                return magentoApis[uri.Host];
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            
        }
    }
}