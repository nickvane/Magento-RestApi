# Magento.RestApi - An async C# Magento REST API client

[Magento](http://www.magentocommerce.com/) is an open source ecommerce platform that allows external applications to interact with it by a SOAP API or REST API. [The REST API](http://www.magentocommerce.com/api/rest/introduction.html) is only available from release 1.7 of Magento. The client only uses the REST API (= no SOAP calls).

The client is specifically targeted to be used in background processes. Magento REST API uses 3-legged OAuth 1.0a protocol to authenticate the application to access the Magento service. Because it is not very useful to pop up browser windows from a background process (like a windows service) for the user to enter username and password, the client has an authentication method that simulates the login process without opening browser windows.

[Available from nuget: Magento.RestApi](https://www.nuget.org/packages/Magento.RestApi/1.0.4) (updated to 1.0.4 on 22/08/2016)

### Usage
#### Authentication

For the following code to work, **the user must be an admin** and the REST user and roles have to be configured in Magento (see http://www.magentocommerce.com/api/rest/permission_settings/roles_configuration.html ). 

```csharp
var client = new MagentoApi()
    .Initialize("http://www.yourmagentourl.com", "ConsumerKey", "ConsumerSecret")
    .AuthenticateAdmin("UserName", "Password");
```

Or if the magento installation has a custom admin path (like "myadmin"):

```csharp
var client = new MagentoApi()
    .SetCustomAdminUrlPart("myadmin")
    .Initialize("http://www.yourmagentourl.com", "ConsumerKey", "ConsumerSecret")
    .AuthenticateAdmin("UserName", "Password");
```

If you need to change the oauth callback url:

```csharp
var client = new MagentoApi()
    .SetCallbackUrl("https://domainname.com:88")
    .Initialize("http://www.yourmagentourl.com", "ConsumerKey", "ConsumerSecret")
    .AuthenticateAdmin("UserName", "Password");
```

The client can be used with a user that isn't an admin, but the oauth credentials will have to be provided to the client. And not all of the methods will work with a user that isn't an admin.

```csharp
var client = new MagentoApi()
    .Initialize("http://www.yourmagentourl.com", "ConsumerKey", "ConsumerSecret")
    .SetAccessToken("AccessTokenKey", "accessTokenSecret");
```

Some Magento configurations require to send the content-type header with an underscore. If that is the case:

```csharp
var client = new MagentoApi()
    .ContentTypeHeaderWithUnderscore()
    .Initialize("http://www.yourmagentourl.com", "ConsumerKey", "ConsumerSecret")
    .AuthenticateAdmin("UserName", "Password");
```

*If you have trouble authenticating, you can read the wiki page [authentication steps](https://github.com/nickvane/Magento-RestApi/wiki/Authentication-steps) for more information about the different steps in the authentication process. You can then compare the steps from the page with your own requests you see in [Fiddler](http://fiddler2.com).*

If you are having trouble authenticating to an SSL-secured Magento instance, one failure reason may be the security policy setup on the web server. Many web servers may block SSLv3 because of vulnerabilities like POODLE. However, .NET defaults the `System.Net.ServicePointManager.SecurityProtocol` to be `System.Net.SecurityProtocolType.Ssl3`. To fix this, simply set your application's default to a matching protocol type for your web server instance. For instance, for web servers that require TLSv1.2 for the latest security recommendations, you would set your policy before your authentication call like:

```csharp
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
```

#### Client calls

The client can then be used like this:

```csharp
// in an async method
var response = await client.GetProductBySku("123456");
// not async
response = client.GetProductBySku("123456").Result;
// The response contains the result or errors
if (!response.HasErrors)
{
    var product = response.Result;
}
```

**Usage in an ASP.net application**: 
The client call should be wrapped in a new aync task and should then be registered with the page (from a button click or Page_Load).

```
protected void ButtonGetProductInfo_Click(object sender, EventArgs e)
{
    RegisterAsyncTask(new PageAsyncTask(GetProductInfo));
}

private async Task GetProductInfo()
{
    var response = await client.GetProductBySku(textboxGetProductInfo.Text.Trim());
    var product = response.Result;
}
```

For this to work, you need to add Async="true" to the page directive

```
<%@ Page Title="Async" Language="C#" CodeBehind="Async.aspx.cs" Inherits="Whatever" Async="true" %>
```

Some good reading here:
[The Magic of using Asynchronous Methods in ASP.NET 4.5 plus an important gotcha](http://www.hanselman.com/blog/TheMagicOfUsingAsynchronousMethodsInASPNET45PlusAnImportantGotcha.aspx)

Thank you [Scotty79](https://github.com/Scotty79) for figuring that out, from [issue 11](https://github.com/nickvane/Magento-RestApi/issues/11)

### Features

* Can be used in multiple threads
* Keeps track of changed properties so only changed values are updated
* If oauth token is rejected after some time, the client re-authenticates and executes the failed request again.

Following Magento REST API features are currently implemented:

* Inventory
* Products
	* Product Categories
	* Product Images
	* Product Websites
* Customers
* Customer Addresses 
* Orders
	* Order Comments
	* Order Addresses
	* Order Items

For the supported features and usage of the library take a look at the integration tests.


### More info

The library is tested against Magento versions:

* 1.7.0.*: all actions tested
* 1.9.1.*: authentication tested

Support for .net framework 4.5 and higher due to usage of async/await keywords.
As of 13/05/2015 support for dnx451 target framework. dnxcore50 currently unavailable due to 

* dependencies not yet available for dnxcore50 (RestSharp, HtmlAgility)
* certain parts of BCL not yet ported to dnxcore50
