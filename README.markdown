# Magento.RestApi - An async C# Magento REST API client

[Magento](http://www.magentocommerce.com/) is an open source ecommerce platform that allows external applications to interact with it by a SOAP API or REST API. [The REST API](http://www.magentocommerce.com/api/rest/introduction.html) has only been available of release 1.7 of Magento. This client only uses the REST API.

The client is specifically targeted to be used in background processes. Magento REST API uses 3-legged OAuth 1.0a protocol to authenticate the application to access the Magento service. Because it is not very useful to pop up browser windows from a background process (like a windows service) for the user to enter username and password, the client has an authentication method that simulates the login process without opening browser windows. Important caveat: **The user must be an admin to use the client.** 

### Usage

```csharp
var api = new MagentoApi()
    .SetCustomAdminUrlPart("myadmin")
    .Initialize("http://www.yourmagentourl.com", "ConsumerKey", "ConsumerSecret")
    .AuthenticateAdmin("UserName", "Password");
```

If the standard admin path of the Magento installation is used ("admin"), the SetCustomAdminUrlPart can be omitted.

The client can then be used like this:

```csharp
// async
var response1 = await api.GetProductBySku("123456");
// not async
var response2 = api.GetProductBySku("123456").Result;
// The response contains the result or errors
if (!response.HasErrors)
{
    var product = response.Result;
}
```

### Features

* Can be used in multiple threads
* Should be able to be used in monotouch/monodroid (not provided yet)

Not all Magento REST API features are currently implemented.
