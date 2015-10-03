using System;
using System.Collections.Generic;
using Microsoft.Framework.Configuration;

namespace Magento.RestApi.IntegrationTests
{
    public class BaseFixture : IDisposable
    {
        private readonly Dictionary<string, IMagentoApi> magentoApis = new Dictionary<string, IMagentoApi>();
        private readonly object _object = new object();

        public IMagentoApi Client { 
            get { return InitializeClient(); }
        }

        public BaseFixture()
        {
            var configurationBuilder = new ConfigurationBuilder()
                // change to full path to make this work
                .AddJsonFile("config.json");
            var configuration = configurationBuilder.Build();

            Url = configuration["url"];
            CustomAdminUrlPart = configuration["adminurlpart"];
            ConsumerKey = configuration["consumerkey"];
            ConsumerSecret = configuration["consumersecret"];
            UserName = configuration["username"];
            Password = configuration["password"];
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

        public virtual void Dispose()
        {
            
        }
    }
}