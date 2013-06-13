# Magento.RestApi - An async C# Magento REST API client

[Magento](http://www.magentocommerce.com/) is an open source ecommerce platform that allows external applications to interact with it by a SOAP API or REST API. [The REST API](http://www.magentocommerce.com/api/rest/introduction.html) is only available from release 1.7 of Magento. The client only uses the REST API (= no SOAP calls).

The client is specifically targeted to be used in background processes. Magento REST API uses 3-legged OAuth 1.0a protocol to authenticate the application to access the Magento service. Because it is not very useful to pop up browser windows from a background process (like a windows service) for the user to enter username and password, the client has an authentication method that simulates the login process without opening browser windows.

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

The client can be used with a user that isn't an admin, but the oauth credentials will have to be provided to the client. And not all of the methods will work with a user that isn't an admin.

```csharp
var client = new MagentoApi()
    .Initialize("http://www.yourmagentourl.com", "ConsumerKey", "ConsumerSecret")
    .SetAccessToken("AccessTokenKey", "accessTokenSecret");
```

*If you have trouble authenticating, you can read the wiki page [authentication steps](https://github.com/nickvane/Magento-RestApi/wiki/Authentication-steps) for more information about the different steps in the authentication process. You can then compare the steps from the page with your own requests you see in [Fiddler](http://fiddler2.com).*

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

### Features

* Can be used in multiple threads
* Should be able to be used in monotouch/monodroid (not provided yet)
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

For the supported features and usage of the library take a look at the integration tests.

Todo features:

* Orders
	* Order Addresses
	* Order Comments
	* Order Items

