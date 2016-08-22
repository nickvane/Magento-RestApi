using System;
using System.Configuration;
using System.Collections.Generic;

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
            var reader = new AppSettingsReader();

            Url = reader.GetValue("url", typeof(string)).ToString();
            CustomAdminUrlPart = reader.GetValue("adminurlpart", typeof(string)).ToString();
            ConsumerKey = reader.GetValue("consumerkey", typeof(string)).ToString();
            ConsumerSecret = reader.GetValue("consumersecret", typeof(string)).ToString();
            UserName = reader.GetValue("username", typeof(string)).ToString();
            Password = reader.GetValue("password", typeof(string)).ToString();
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