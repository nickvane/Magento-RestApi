using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Magento.RestApi.Models;
using Magento.RestApi.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;
using JsonSerializer = Magento.RestApi.Json.JsonSerializer;

namespace Magento.RestApi
{
    public class MagentoApi : IMagentoApi
    {
        private string _url;
        private string _consumerKey;
        private string _consumerSecret;
        private string _accessTokenKey;
        private string _accessTokenSecret;
        private string _adminUrlPart = "admin";
        private string _userName;
        private string _password;
        private bool _hasAuthenticatedWithAdminAuthentication;
        private bool _isAuthenticating;
    
        private JsonSerializer _jsonSerializer;
        private RestClient _client;

        private RestClient Client
        {
            get
            {
                if (_isAuthenticating)
                {
                    lock (_client)
                    {
                        // lock access to the client when authenticating
                    }
                }
                return _client;
            }
        }

        public IMagentoApi Initialize(string url, string consumerKey, string consumerSecret)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(url);
            if (string.IsNullOrEmpty(consumerKey)) throw new ArgumentNullException(consumerKey);
            if (string.IsNullOrEmpty(consumerSecret)) throw new ArgumentNullException(consumerSecret);

            _url = url.TrimEnd('/');
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            
            InitializeRestClient();

            return this;
        }

        private void InitializeRestClient()
        {
            _jsonSerializer = new JsonSerializer();
            _client = new RestClient(_url);
            _client.AddDefaultHeader("Accept", "application/json");
            _client.AddDefaultHeader("Content-type", "application/json");
            _client.AddHandler("application/json", _jsonSerializer);
        }

        #region Authentication

        public IMagentoApi SetAccessToken(string accessTokenKey, string accessTokenSecret)
        {
            _accessTokenKey = accessTokenKey;
            _accessTokenSecret = accessTokenSecret;

            InitializeRestClient();
            _client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                _consumerKey,
                _consumerSecret,
                _accessTokenKey,
                _accessTokenSecret);

            return this;
        }

        public IMagentoApi SetCustomAdminUrlPart(string adminUrlPart)
        {
            if (!string.IsNullOrEmpty(adminUrlPart)) _adminUrlPart = adminUrlPart;
            return this;
        }

        public IMagentoApi AuthenticateAdmin(string userName, string password)
        {
            lock (_client)
            {
                try
                {
                    _isAuthenticating = true;
                    if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(userName);
                    if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(password);
                    _userName = userName;
                    _password = password;

                    InitializeRestClient();
                    _client.Authenticator = OAuth1Authenticator.ForRequestToken(
                        _consumerKey,
                        _consumerSecret,
                        "http://localhost:8888" // Value for the oauth_callback parameter, we provide a value, but it won't be used.
                        );

                    // PART 1: Getting an Unauthorized Request Token
                    var request = new RestRequest("/oauth/initiate", Method.POST);
                    var response = _client.Execute(request);

                    if (response.ErrorException != null) throw new MagentoApiException(string.Format("Unable to get unauthorized access token for url: '{0}'. This usually indicates the provided url is not correct or a connection issue.", _url + "/oauth/initiate"), response.ErrorException);
                    if (response.Content.Contains("oauth_problem=consumer_key_rejected")) throw new MagentoApiException(string.Format("The provided consumer key was rejected by the server at url '{0}' for consumer key '{1}'.", _url + "/oauth/initiate", _consumerKey));
                    if (response.Content.Contains("oauth_problem=signature_invalid")) throw new MagentoApiException(string.Format("The provided consumer secret was rejected by the server at url '{0}' for consumer key '{1}'.", _url + "/oauth/initiate", _consumerKey));
                    if (response.Content.Contains("oauth_problem=timestamp_refused")) throw new MagentoApiException(string.Format("The timestamp is incorrect at '{0}' for consumer key '{1}'. This usually indicates a big gap between client and server time.", _url + "/oauth/initiate", _consumerKey));
                    if (response.Content.Contains("oauth_problem=")) throw new MagentoApiException(string.Format("There was a problem with oauth at '{0}' for consumer key '{1}'. Message: '{2}'", _url + "/oauth/initiate", _consumerKey, response.Content));

                    var queryStringValues = HttpUtility.ParseQueryString(response.Content);

                    var oauthToken = queryStringValues["oauth_token"];
                    var oauthTokenSecret = queryStringValues["oauth_token_secret"];

                    var queryString = string.Format("oauth_token={0}", oauthToken);
                    var authorizeUrl = _url + "/" + _adminUrlPart + "/oauth_authorize?" + queryString;

                    var webClient = new MagentoWebClient();
                    // PART 2: Log in the application
                    var confirmAuthorizeUrl = Login(webClient, authorizeUrl, _userName, _password, oauthToken);

                    // PART 3: Authorize the application for the logged in user
                    var requestTokenVerifier = Authorize(webClient, confirmAuthorizeUrl, oauthToken);

                    // PART 4: Getting an Access Token
                    _client.Authenticator = OAuth1Authenticator.ForAccessToken(
                        _consumerKey,
                        _consumerSecret,
                        oauthToken,
                        oauthTokenSecret,
                        requestTokenVerifier
                        );

                    request = new RestRequest("/oauth/token", Method.POST);
                    response = _client.Execute(request);

                    if (response.ErrorException != null) throw new MagentoApiException("Cannot retrieve accestoken", response.ErrorException);

                    queryStringValues = HttpUtility.ParseQueryString(response.Content);

                    _accessTokenKey = queryStringValues["oauth_token"];
                    _accessTokenSecret = queryStringValues["oauth_token_secret"];
                    _hasAuthenticatedWithAdminAuthentication = true;

                    InitializeRestClient();
                    _client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                        _consumerKey,
                        _consumerSecret,
                        _accessTokenKey,
                        _accessTokenSecret);

                    return this;
                }
                finally
                {
                    _isAuthenticating = false;
                }
            }
        }

        /// <summary>
        /// Logs the user in Magento
        /// </summary>
        /// <param name="webClient">The webclient to excecute get requests</param>
        /// <param name="loginUrl">The url where the user can log in the Magento application</param>
        /// <param name="userName">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <param name="oauthToken">The token received from the unauthorized request</param>
        /// <returns>The authorization url where the logged in user can authorize the application</returns>
        private string Login(MagentoWebClient webClient, string loginUrl, string userName, string password, string oauthToken)
        {
            // Get the login page and find the form post action url and the formkey
            var loginPage = new HtmlDocument();
            try
            {
                using (var responseStream = webClient.OpenRead(loginUrl))
                {
                    loginPage.Load(responseStream);
                }
            }
            catch (WebException ex)
            {
                if(ex.Response is HttpWebResponse && ((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound) throw new MagentoApiException(string.Format("Unable to load admin page: '{0}'. This usually indicates the admin section of the magento installation is at a different url and a customadminurlpart was not set.", loginUrl), ex);
                throw new MagentoApiException(string.Format("Unable to load admin page: '{0}'.", loginUrl), ex);
            }

            var loginForm = loginPage.GetElementbyId("loginForm");
            var postUrl = loginForm.GetAttributeValue("action", string.Empty);
            var formKey = loginPage.DocumentNode.Descendants("input").Single(node => node.Attributes.Any(a => a.Name == "name" && a.Value == "form_key")).GetAttributeValue("value", string.Empty);

            // Post the user credentials to the post action url and get the adminhtml cookie for future requests and the authorize url
            var postRequest = (HttpWebRequest) WebRequest.Create(postUrl);
            postRequest.Method = "POST";
            postRequest.ContentType = "application/x-www-form-urlencoded";
            postRequest.AllowAutoRedirect = false;
			
            var cookieContainer = new CookieContainer();
            if (!string.IsNullOrEmpty(webClient.AdminHtml))
                cookieContainer.Add(new Uri(postRequest.RequestUri.GetLeftPart(UriPartial.Authority)), new Cookie("adminhtml", webClient.AdminHtml));
            postRequest.CookieContainer = cookieContainer;
			
            var postData = string.Format("form_key={0}&login%5busername%5d={1}&login%5bpassword%5d={2}&oauth_token={3}", formKey, userName, password, oauthToken);
            byte[] postDataBytes = new ASCIIEncoding().GetBytes(postData);
            postRequest.ContentLength = postDataBytes.Length;
            using (var requestStream = postRequest.GetRequestStream())
            {
                requestStream.Write(postDataBytes, 0, postDataBytes.Length);
            }
            using (var response = postRequest.GetResponse())
            {
                var cookieheader = response.Headers["Set-Cookie"];
                webClient.SetAdminHtmlFromCookie(cookieheader);

                var location = response.Headers["location"];
                return location;
            }
        }

        /// <summary>
        /// Authorizes the application for the logged in user
        /// </summary>
        /// <param name="webClient">The webclient to excecute get requests</param>
        /// <param name="confirmAuthorizeUrl">The url to confirm the authorization</param>
        /// <param name="oauthToken">The token received from the unauthorized request</param>
        /// <returns>The oauth verifier that is used to retrieve the access token</returns>
        private string Authorize(MagentoWebClient webClient, string confirmAuthorizeUrl, string oauthToken)
        {
            // Get the authorize page where the user confirms the application authorization and gets the form get action url
            var authorizePage = new HtmlDocument();
            using (var responseStream = webClient.OpenRead(confirmAuthorizeUrl))
            {
                authorizePage.Load(responseStream);
            }
            var formElement = authorizePage.GetElementbyId("oauth_authorize_confirm");
            if(authorizePage.DocumentNode.InnerText.Contains("Invalid User Name or Password") || formElement == null) throw new MagentoApiException(string.Format("The provided admin username '{0}' or password is invalid. The user needs to be a Magento admin.", _userName));
            var actionUrl = formElement.GetAttributeValue("action", string.Empty);

            // Submit the form by a get
            var getRequest = (HttpWebRequest) WebRequest.Create(actionUrl + "?oauth_token=" + oauthToken);
            getRequest.Method = "GET";
            getRequest.AllowAutoRedirect = false;
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri(getRequest.RequestUri.GetLeftPart(UriPartial.Authority)), new Cookie("adminhtml", webClient.AdminHtml));
            getRequest.CookieContainer = cookieContainer;
            using (var response = getRequest.GetResponse())
            {
                // The location contains the callback url and the oauth verifier
                var location = response.Headers["location"];
                return location.Substring(location.IndexOf("oauth_verifier=", StringComparison.Ordinal)).Replace("oauth_verifier=", "");
            }
        }

        /// <summary>
        /// Helper for the Magento.RestApi.Client: adds functionality for retrieving and setting the adminhtml cookie
        /// </summary>
        private class MagentoWebClient : WebClient
        {
            public string AdminHtml { get; private set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                if (request is HttpWebRequest)
                {
                    var cookieContainer = new CookieContainer();
                    if (!string.IsNullOrEmpty(AdminHtml))
                        cookieContainer.Add(new Uri(request.RequestUri.GetLeftPart(UriPartial.Authority)), new Cookie("adminhtml", AdminHtml));
                    (request as HttpWebRequest).CookieContainer = cookieContainer;
                }
                return request;
            }

            protected override WebResponse GetWebResponse(WebRequest request)
            {
                var response = base.GetWebResponse(request);
                if (response != null)
                {
                    var setCookieHeader = response.Headers[HttpResponseHeader.SetCookie];
                    SetAdminHtmlFromCookie(setCookieHeader);
                }
                return response;
            }

            public void SetAdminHtmlFromCookie(string cookieString)
            {
                AdminHtml = cookieString.Substring(cookieString.LastIndexOf("adminhtml=", StringComparison.Ordinal)).Replace("adminhtml=", "").Split(';')[0];
            }
        }

        #endregion

        #region Boilerplate

        private IRestRequest CreateRequest(string url, Method method = Method.GET)
        {
            var request = new RestRequest
                              {
                                  Resource = url,
                                  Method = method,
                                  RequestFormat = DataFormat.Json,
                                  JsonSerializer = _jsonSerializer
                              };
            return request;
        }

        private async Task<MagentoApiResponse<T>> Execute<T>(IRestRequest request, bool isSecondTry = false) where T : new()
        {
            Client.FollowRedirects = request.Method != Method.POST;
            var response = await Client.GetResponseAsync<T>(request);
            return await HandleResponse(response, request, isSecondTry);
        }

        private async Task<MagentoApiResponse<T>> HandleResponse<T>(IRestResponse<T> response, IRestRequest request, bool isSecondTry) where T : new()
        {
            if (response.ErrorException != null)
            {
                throw new MagentoApiException("The request was not succesfully completed.", response.ErrorException);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (!isSecondTry 
                    && response.StatusCode == HttpStatusCode.Unauthorized
                    && response.Content.Contains("oauth_problem=")
                    && _hasAuthenticatedWithAdminAuthentication)
                {
                    AuthenticateAdmin(_userName, _password);
                    return await Execute<T>(request, true);
                }

                var errors = GetErrorsFromResponse(response);
                return new MagentoApiResponse<T> { Errors = errors, RequestUrl = Client.BuildUri(request), ErrorString = response.Content };
            }

            return new MagentoApiResponse<T> { Result = response.Data, RequestUrl = Client.BuildUri(request) };
        }

        private async Task<IRestResponse> Execute(IRestRequest request, bool isSecondTry = false)
        {
            Client.FollowRedirects = request.Method != Method.POST;
            var response = await Client.GetResponseAsync(request);
            return await HandleResponse(response, request, isSecondTry);
        }

        private async Task<IRestResponse> HandleResponse(IRestResponse response, IRestRequest request, bool isSecondTry)
        {
            if (response.ErrorException != null)
            {
                throw new MagentoApiException("The request was not succesfully completed.", response.ErrorException);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (!isSecondTry
                    && response.StatusCode == HttpStatusCode.Unauthorized
                    && response.Content.Contains("oauth_problem=")
                    && _hasAuthenticatedWithAdminAuthentication)
                {
                    AuthenticateAdmin(_userName, _password);
                    return await Execute(request, true);
                }
            }
            return response;
        }

        private MagentoApiResponse<bool> CreateMagentoResponse(IRestResponse restResponse)
        {
            return new MagentoApiResponse<bool> { 
                Result = true,
                RequestUrl = Client.BuildUri(restResponse.Request),
                Errors = GetErrorsFromResponse(restResponse),
                ErrorString = restResponse.Content};
        } 

        private List<MagentoError> GetErrorsFromResponse(IRestResponse restResponse)
        {
            var list = new List<MagentoError>();
            if (!string.IsNullOrEmpty(restResponse.Content))
            {
                JObject result;
                try
                {
                    result = JObject.Parse(restResponse.Content);
                }
                catch (JsonReaderException)
                {
                    // if it can't be parsed, then it doesn't contain messages.
                    return list;
                }   
                var messages = result["messages"];
                if (messages != null)
                {
                    var errors = messages["error"];
                    if (errors != null)
                    {
                        list.AddRange(errors.Select(error => new MagentoError
                                                                 {
                                                                     Code = (string) error["code"], Message = (string) error["message"]
                                                                 }));
                        return list;
                    }
                }
            }
            
            return list;
        }

        private void AddFilterToRequest(Filter filter, IRestRequest request)
        {
            if (filter == null) return;
            if (filter.Page > 1) request.AddParameter("page", filter.Page);
            if (filter.PageSize > 1) request.AddParameter("limit", filter.PageSize);
            if (!string.IsNullOrEmpty(filter.SortField))
            {
                request.AddParameter("order", filter.SortField);
                request.AddParameter("dir", filter.SortDirection);
            }
            var index = 0;
            foreach (var expression in filter.FilterExpressions)
            {
                request.AddParameter("filter[" + index + "][attribute]", expression.FieldName);
                request.AddParameter("filter[" + index + "][" + expression.ExpressionOperator + "]", expression.FieldValue);
                index++;
            }
        }

        #endregion

        #region API products

        #region products

        public async Task<MagentoApiResponse<IList<Product>>> GetProducts(Filter filter)
        {
            var request = CreateRequest("/api/rest/products");
            AddFilterToRequest(filter, request);

            var response = await Execute<Dictionary<int, Product>>(request);
            if (!response.HasErrors)
            {
                if(response.Result == null) response.Result = new Dictionary<int, Product>();
                return new MagentoApiResponse<IList<Product>> { Result = response.Result.Select(product => product.Value).ToList(), RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
            }
            return new MagentoApiResponse<IList<Product>>{ Errors = response.Errors, RequestUrl = response.RequestUrl };
        }

        public async Task<MagentoApiResponse<IList<Product>>> GetProductsByCategoryId(int categoryId, Filter filter = null)
        {
            var request = CreateRequest("/api/rest/products");
            request.AddParameter("category_id", categoryId);

            var response = await Execute<Dictionary<int, Product>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new Dictionary<int, Product>();
                return new MagentoApiResponse<IList<Product>> { Result = response.Result.Select(product => product.Value).ToList(), RequestUrl = response.RequestUrl };
            }
            return new MagentoApiResponse<IList<Product>> { Errors = response.Errors, RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
        }

        public async Task<MagentoApiResponse<Product>> GetProductById(int productId)
        {
            var request = CreateRequest("/api/rest/products/{productId}");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);

            return await Execute<Product>(request);
        }

        public async Task<MagentoApiResponse<Product>> GetProductByIdForStore(int productId, int storeId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/store/{storeId}");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("storeId", storeId, ParameterType.UrlSegment);

            return await Execute<Product>(request);
        }

        public async Task<MagentoApiResponse<Product>> GetProductBySku(string sku)
        {
            var request = CreateRequest("/api/rest/products");
            request.AddParameter("filter[0][attribute]", "sku");
            request.AddParameter("filter[0][in][0]", sku);

            var response = await Execute<Dictionary<int, Product>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new Dictionary<int, Product>();
                if (response.Result.Count == 0) return new MagentoApiResponse<Product> { RequestUrl = response.RequestUrl, Result = null };
                if (response.Result.Count == 1) return new MagentoApiResponse<Product> { RequestUrl = response.RequestUrl, Result = response.Result.First().Value };
                throw new MagentoApiException(string.Format("More than 1 product was found with sku '{0}'", sku));
            }
            return new MagentoApiResponse<Product> { RequestUrl = response.RequestUrl, Errors = response.Errors, ErrorString = response.ErrorString };
        }

        public async Task<MagentoApiResponse<Product>> GetProductBySkuForStore(string sku, int storeId)
        {
            var response = await GetProductBySku(sku);
            if (!response.HasErrors && response.Result != null)
            {
                return await GetProductByIdForStore(response.Result.entity_id, storeId);
            }
            return response;
        }

        public async Task<MagentoApiResponse<int>> CreateNewProduct(Product product)
        {
            if (product.entity_id != 0) throw new MagentoApiException("A new product can't have an entity_id.");

            var request = CreateRequest("/api/rest/products", Method.POST);
            request.AddBody(product);

            var response = await Execute(request);
            int productId = 0;
            var location = response.Headers.FirstOrDefault(h => h.Name.Equals("Location"));
            if (location != null)
            {
                int.TryParse(location.Value.ToString().Split('/').Last(), out productId);
            }
            return new MagentoApiResponse<int>
                       {
                           Result = productId,
                           RequestUrl = Client.BuildUri(response.Request),
                           Errors = GetErrorsFromResponse(response),
                           ErrorString = response.Content
                       };
        }

        public async Task<MagentoApiResponse<bool>> UpdateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException("product");
            if (product.HasChanged())
            {
                var request = CreateRequest("/api/rest/products/{productId}", Method.PUT);
                request.AddParameter("productId", product.entity_id, ParameterType.UrlSegment);
                request.AddBody(product);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> {Result = true};
        }

        public async Task<MagentoApiResponse<bool>> UpdateProductForStore(Product product, int storeId)
        {
            if (product == null) throw new ArgumentNullException("product");
            if (product.HasChanged())
            {
                var request = CreateRequest("/api/rest/products/{productId}/store/{storeId}", Method.PUT);
                request.AddParameter("productId", product.entity_id, ParameterType.UrlSegment);
                request.AddParameter("storeId", storeId, ParameterType.UrlSegment);
                request.AddBody(product);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> { Result = true };
        }

        public async Task<MagentoApiResponse<bool>> DeleteProduct(int productId)
        {
            var request = CreateRequest("/api/rest/products/{productId}", Method.DELETE);
            request.AddParameter("productId", productId, ParameterType.UrlSegment);

            var response = await Execute(request);
            return CreateMagentoResponse(response);
        }

        #endregion

        #region websites

        public async Task<MagentoApiResponse<IList<int>>> GetWebsitesForProduct(int productId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/websites");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);

            var response = await Execute(request);
            var errors = GetErrorsFromResponse(response);
            if (errors == null || errors.Count == 0)
            {
                var result = JsonConvert.DeserializeObject(response.Content);
                return new MagentoApiResponse<IList<int>>
                           {
                               RequestUrl = _client.BuildUri(request),
                               Result = (from object item in result as IEnumerable select (item as JObject)["website_id"].Value<int>()).ToList()
                           };
            }
            return new MagentoApiResponse<IList<int>>
                       {
                           Errors = errors,
                           RequestUrl = _client.BuildUri(request)
                       };
        }

        public async Task<MagentoApiResponse<bool>> AssignWebsiteToProduct(int productId, int websiteId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/websites", Method.POST);
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddBody(new {website_id = websiteId});

            var response = await Execute(request);
            var magentoResponse = CreateMagentoResponse(response);
            // if the response contains the error that the product is already assigned, then ignore it and remove the errors.
            if (magentoResponse.Errors != null && magentoResponse.Errors.Count(e => e.Message.Contains("Product #" + productId + " is already assigned to website #" + websiteId)) > 0)
            {
                magentoResponse.Errors = new List<MagentoError>();
            }
            return magentoResponse;
        }

        public async Task<MagentoApiResponse<bool>> UnassignWebsiteFromProduct(int productId, int websiteId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/websites/{websiteId}", Method.DELETE);
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("websiteId", websiteId, ParameterType.UrlSegment);

            var response = await Execute(request);
            var magentoResponse = CreateMagentoResponse(response);
            // if the response contains the error that the product is already assigned, then ignore it and remove the errors.
            if (magentoResponse.Errors != null && magentoResponse.Errors.Count(e => e.Message.Contains("Product #" + productId + " isn't assigned to website #" + websiteId)) > 0)
            {
                magentoResponse.Errors = new List<MagentoError>();
            }
            return magentoResponse;
        }

        #endregion

        #region categories

        public async Task<MagentoApiResponse<IList<int>>> GetCategoriesForProduct(int productId, Filter filter = null)
        {
            var request = CreateRequest("/api/rest/products/{productId}/categories");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);

            var response = await Execute(request);
            var errors = GetErrorsFromResponse(response);
            if (errors == null || errors.Count == 0)
            {
                var result = JsonConvert.DeserializeObject(response.Content);
                return new MagentoApiResponse<IList<int>>
                {
                    RequestUrl = _client.BuildUri(request),
                    Result = (from object item in result as IEnumerable select (item as JObject)["category_id"].Value<int>()).ToList()
                };
            }
            return new MagentoApiResponse<IList<int>>
            {
                Errors = errors,
                RequestUrl = _client.BuildUri(request)
            };
        }

        public async Task<MagentoApiResponse<bool>> AssignCategoryToProduct(int productId, int categoryId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/categories", Method.POST);
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddBody(new { category_id = categoryId });

            var response = await Execute(request);
            var magentoResponse = CreateMagentoResponse(response);
            // if the response contains the error that the product is already assigned, then ignore it and remove the errors.
            if (magentoResponse.Errors != null && magentoResponse.Errors.Count(e => e.Message.Contains("Product #" + productId + " is already assigned to category #" + categoryId)) > 0)
            {
                magentoResponse.Errors = new List<MagentoError>();
            }
            return magentoResponse;
        }

        public async Task<MagentoApiResponse<bool>> UnAssignCategoryFromProduct(int productId, int categoryId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/categories/{categoryId}", Method.DELETE);
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("categoryId", categoryId, ParameterType.UrlSegment);

            var response = await Execute(request);
            var magentoResponse = CreateMagentoResponse(response);
            // if the response contains the error that the product is already assigned, then ignore it and remove the errors.
            if (magentoResponse.Errors != null && magentoResponse.Errors.Count(e => e.Message.Contains("Product #" + productId + " isn't assigned to category #" + categoryId)) > 0)
            {
                magentoResponse.Errors = new List<MagentoError>();
            }
            return magentoResponse;
        }

        #endregion

        #region images

        public async Task<MagentoApiResponse<IList<ImageInfo>>> GetImagesForProduct(int productId, Filter filter = null)
        {
            var request = CreateRequest("/api/rest/products/{productId}/images");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            
            var response = await Execute<List<ImageInfo>>(request);
            return !response.HasErrors 
                ? new MagentoApiResponse<IList<ImageInfo>> { RequestUrl = response.RequestUrl, Result = response.Result ?? new List<ImageInfo>() }
                : new MagentoApiResponse<IList<ImageInfo>> { RequestUrl = response.RequestUrl, Errors = response.Errors, ErrorString = response.ErrorString };
        }

        public async Task<MagentoApiResponse<IList<ImageInfo>>> GetImagesForProductForStore(int productId, int storeId, Filter filter = null)
        {
            var request = CreateRequest("/api/rest/products/{productId}/images/store/{storeId}");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("storeId", storeId, ParameterType.UrlSegment);

            var response = await Execute<List<ImageInfo>>(request);
            return !response.HasErrors
                ? new MagentoApiResponse<IList<ImageInfo>> { RequestUrl = response.RequestUrl, Result = response.Result ?? new List<ImageInfo>() }
                : new MagentoApiResponse<IList<ImageInfo>> { RequestUrl = response.RequestUrl, Errors = response.Errors, ErrorString = response.ErrorString };
        }

        public async Task<MagentoApiResponse<ImageInfo>> GetImageInfoForProduct(int productId, int imageId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/images/{imageId}");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("imageId", imageId, ParameterType.UrlSegment);

            return await Execute<ImageInfo>(request);
        }

        public async Task<MagentoApiResponse<ImageInfo>> GetImageInfoForProductForStore(int productId, int storeId, int imageId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/images/{imageId}/store/{storeId}");
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("imageId", imageId, ParameterType.UrlSegment);
            request.AddParameter("storeId", storeId, ParameterType.UrlSegment);

            return await Execute<ImageInfo>(request);
        }

        public async Task<MagentoApiResponse<bool>> UpdateImageInfoForProduct(int productId, int imageId, ImageInfo imageInfo)
        {
            if (imageInfo == null) throw new ArgumentNullException("imageInfo");
            if (imageInfo.HasChanged())
            {
                var request = CreateRequest("/api/rest/products/{productId}/images/{imageId}", Method.PUT);
                request.AddParameter("productId", productId, ParameterType.UrlSegment);
                request.AddParameter("imageId", imageId, ParameterType.UrlSegment);
                request.AddBody(imageInfo);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> { Result = true };
        }

        public async Task<MagentoApiResponse<bool>> UpdateImageInfoForProductForStore(int productId, int storeId, int imageId, ImageInfo imageInfo)
        {
            if (imageInfo == null) throw new ArgumentNullException("imageInfo");
            if (imageInfo.HasChanged())
            {
                var request = CreateRequest("/api/rest/products/{productId}/images/{imageId}/store/{storeId}", Method.PUT);
                request.AddParameter("productId", productId, ParameterType.UrlSegment);
                request.AddParameter("storeId", storeId, ParameterType.UrlSegment);
                request.AddParameter("imageId", imageId, ParameterType.UrlSegment);
                request.AddBody(imageInfo);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> { Result = true };
        }

        public async Task<MagentoApiResponse<int>> AddImageToProduct(int productId, ImageFile image)
        {
            if (image == null) throw new ArgumentNullException("image");
            if (image.HasChanged())
            {
                var request = CreateRequest("/api/rest/products/{productId}/images", Method.POST);
                request.AddParameter("productId", productId, ParameterType.UrlSegment);
                request.AddBody(image);

                var response = await Execute(request);
                var location = response.Headers.FirstOrDefault(h => h.Name.Equals("Location"));
                var imageId = 0;
                if (location != null)
                {
                    int.TryParse(location.Value.ToString().Split('/').Last(), out imageId);
                }
                var magentoResponse = new MagentoApiResponse<int>
                {
                    Result = imageId,
                    RequestUrl = Client.BuildUri(response.Request),
                    Errors = GetErrorsFromResponse(response),
                    ErrorString = response.Content
                };
                if (imageId == 0)
                {
                    magentoResponse.Errors.Add(new MagentoError
                                                   {
                                                       Code = response.StatusCode.ToString(),
                                                       Message = "Could not get image id from location from header"
                                                   });
                }
                return magentoResponse;
            }
            return new MagentoApiResponse<int> { Result = 0 };
        }

        public async Task<MagentoApiResponse<bool>> UnassignImageFromProduct(int productId, int imageId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/images/{imageId}", Method.DELETE);
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("imageId", imageId, ParameterType.UrlSegment);

            var response = await Execute(request);
            return CreateMagentoResponse(response);
        }

        public async Task<MagentoApiResponse<bool>> UnAssignImageFromProductForStore(int productId, int storeId, int imageId)
        {
            var request = CreateRequest("/api/rest/products/{productId}/images/{imageId}/store/{storeId}", Method.DELETE);
            request.AddParameter("productId", productId, ParameterType.UrlSegment);
            request.AddParameter("imageId", imageId, ParameterType.UrlSegment);
            request.AddParameter("storeId", storeId, ParameterType.UrlSegment);

            var response = await Execute(request);
            return CreateMagentoResponse(response);
        }

        #endregion

        #region Inventory

        public async Task<MagentoApiResponse<StockItem>> GetStockItemForProduct(int productId)
        {
            var request = CreateRequest("/api/rest/stockitems");
            request.AddParameter("filter[0][attribute]", "product_id");
            request.AddParameter("filter[0][in][0]", productId);

            var response = await Execute<List<StockItem>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new List<StockItem>();
                if (response.Result.Count == 0) return new MagentoApiResponse<StockItem> { RequestUrl = response.RequestUrl, Result = null };
                if (response.Result.Count == 1) return new MagentoApiResponse<StockItem> { RequestUrl = response.RequestUrl, Result = response.Result.First() };
                throw new MagentoApiException(string.Format("More than 1 stock item was found with productid '{0}'", productId));
            }
            return new MagentoApiResponse<StockItem> { RequestUrl = response.RequestUrl, Errors = response.Errors, ErrorString = response.ErrorString };
        }

        public async Task<MagentoApiResponse<bool>> UpdateStockItemForProduct(int productId, StockItem stockItem)
        {
            if (stockItem == null) throw new ArgumentNullException("stockItem");
            if (stockItem.HasChanged())
            {
                var serverStockItem = await GetStockItemForProduct(productId);
                var request = CreateRequest("/api/rest/stockitems/{itemId}", Method.PUT);
                request.AddParameter("itemId", serverStockItem.Result.item_id, ParameterType.UrlSegment);
                request.AddBody(stockItem);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> { Result = true };
        }

        public async Task<MagentoApiResponse<bool>> UpdateStockQuantityForProduct(int productId, double quantity)
        {
            var serverStockItem = await GetStockItemForProduct(productId);
            if (!serverStockItem.HasErrors)
            {
                var request = CreateRequest("/api/rest/stockitems/{itemId}", Method.PUT);
                request.AddParameter("itemId", serverStockItem.Result.item_id, ParameterType.UrlSegment);
                var stockItem = new StockItem { qty = quantity };
                request.AddBody(stockItem);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> { Errors = serverStockItem.Errors, ErrorString = serverStockItem.ErrorString, RequestUrl = serverStockItem.RequestUrl };
        }

        #endregion

        #region Customers

        public async Task<MagentoApiResponse<IList<Customer>>> GetCustomers(Filter filter)
        {
            var request = CreateRequest("/api/rest/customers");
            AddFilterToRequest(filter, request);

            var response = await Execute<Dictionary<int, Customer>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new Dictionary<int, Customer>();
                return new MagentoApiResponse<IList<Customer>> { Result = response.Result.Select(customer => customer.Value).ToList(), RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
            }
            return new MagentoApiResponse<IList<Customer>> { Errors = response.Errors, RequestUrl = response.RequestUrl };
        }

        public async Task<MagentoApiResponse<int>> CreateNewCustomer(Customer customer)
        {
            if (customer.entity_id != 0) throw new MagentoApiException("A new customer can't have an entity_id.");

            var request = CreateRequest("/api/rest/customers", Method.POST);
            request.AddBody(customer);

            var response = await Execute(request);
            int productId = 0;
            var location = response.Headers.FirstOrDefault(h => h.Name.Equals("Location"));
            if (location != null)
            {
                int.TryParse(location.Value.ToString().Split('/').Last(), out productId);
            }
            return new MagentoApiResponse<int>
            {
                Result = productId,
                RequestUrl = Client.BuildUri(response.Request),
                Errors = GetErrorsFromResponse(response),
                ErrorString = response.Content
            };
        }

        public async Task<MagentoApiResponse<Customer>> GetCustomerById(int customerId)
        {
            var request = CreateRequest("/api/rest/customers/{customerId}");
            request.AddParameter("customerId", customerId, ParameterType.UrlSegment);

            return await Execute<Customer>(request);
        }

        public async Task<MagentoApiResponse<bool>> UpdateCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException("customer");
            if (customer.HasChanged())
            {
                var request = CreateRequest("/api/rest/customers/{customerId}", Method.PUT);
                request.AddParameter("customerId", customer.entity_id, ParameterType.UrlSegment);
                request.AddBody(customer);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> { Result = true };
        }

        public async Task<MagentoApiResponse<bool>> DeleteCustomer(int customerId)
        {
            var request = CreateRequest("/api/rest/customers/{customerId}", Method.DELETE);
            request.AddParameter("customerId", customerId, ParameterType.UrlSegment);

            var response = await Execute(request);
            return CreateMagentoResponse(response);
       } 
        public async Task<MagentoApiResponse<IList<CustomerAddress>>> GetAddressesForCustomer(int customerId)
        {
            var request = CreateRequest("/api/rest/customers/{customerId}/addresses");
            request.AddParameter("customerId", customerId, ParameterType.UrlSegment);

            var response = await Execute<List<CustomerAddress>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new List<CustomerAddress>();
                return new MagentoApiResponse<IList<CustomerAddress>> { Result = response.Result, RequestUrl = response.RequestUrl };
            }
            return new MagentoApiResponse<IList<CustomerAddress>> { Errors = response.Errors, RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
        }

        public async Task<MagentoApiResponse<int>> CreateNewCustomerAddress(int customerId, CustomerAddress address)
        {
            if (address.entity_id != 0) throw new MagentoApiException("A new address can't have an entity_id.");

            var request = CreateRequest("/api/rest/customers/{customerId}/addresses", Method.POST);
            request.AddBody(address);

            var response = await Execute(request);
            int productId = 0;
            var location = response.Headers.FirstOrDefault(h => h.Name.Equals("Location"));
            if (location != null)
            {
                int.TryParse(location.Value.ToString().Split('/').Last(), out productId);
            }
            return new MagentoApiResponse<int>
            {
                Result = productId,
                RequestUrl = Client.BuildUri(response.Request),
                Errors = GetErrorsFromResponse(response),
                ErrorString = response.Content
            };
        }

        public async Task<MagentoApiResponse<CustomerAddress>> GetCustomerAddressById(int addressId)
        {
            var request = CreateRequest("/api/rest/customers/addresses/{addressId}");
            request.AddParameter("addressId", addressId, ParameterType.UrlSegment);

            return await Execute<CustomerAddress>(request);
        }

        public async Task<MagentoApiResponse<bool>> UpdateCustomerAddress(CustomerAddress address)
        {
            if (address == null) throw new ArgumentNullException("address");
            if (address.HasChanged())
            {
                var request = CreateRequest("/api/rest/customers/addresses/{addressId}", Method.PUT);
                request.AddParameter("addressId", address.entity_id, ParameterType.UrlSegment);
                request.AddBody(address);

                var response = await Execute(request);
                return CreateMagentoResponse(response);
            }
            return new MagentoApiResponse<bool> { Result = true };
        }

        public async Task<MagentoApiResponse<bool>> DeleteCustomerAddress(int addressId)
        {
            var request = CreateRequest("/api/rest/customers/addresses/{addressId}", Method.DELETE);
            request.AddParameter("addressId", addressId, ParameterType.UrlSegment);

            var response = await Execute(request);
            return CreateMagentoResponse(response);
        }

        #endregion

        #region orders

        public async Task<MagentoApiResponse<IList<Order>>> GetOrders(Filter filter)
        {
            var request = CreateRequest("/api/rest/orders");
            AddFilterToRequest(filter, request);

            var response = await Execute<List<Order>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new List<Order>();
                return new MagentoApiResponse<IList<Order>> { Result = response.Result, RequestUrl = response.RequestUrl };
            }
            return new MagentoApiResponse<IList<Order>> { Errors = response.Errors, RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
        }

        public async Task<MagentoApiResponse<Order>> GetOrderById(int orderId)
        {
            var request = CreateRequest("/api/rest/orders/{orderId}");
            request.AddParameter("orderId", orderId, ParameterType.UrlSegment);

            return await Execute<Order>(request);
        }

        #endregion

        #endregion

    }
}