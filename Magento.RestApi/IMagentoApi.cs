using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestApi.Models;

namespace Magento.RestApi
{
    public interface IMagentoApi
    {
        #region fluent configuration

        /// <summary>
        /// A Magento rest api client.
        /// </summary>
        /// <param name="url">This is the root domain url. For example: https://www.yourdomain.com</param>
        /// <param name="consumerKey">The consumer key for the rest consumer that will be used to connect to the Magento rest api.</param>
        /// <param name="consumerSecret">The consumer secret for the rest consumer that will be used to connect to the Magento rest api.</param>
        /// <see cref="http://www.magentocommerce.com/api/rest/authentication/oauth_configuration.html"/>
        /// <returns>this, for fluent configuration</returns>
        IMagentoApi Initialize(string url, string consumerKey, string consumerSecret);
        /// <summary>
        /// When you want to follow and control the standard oauth procedure yourself, this lets you set the access token and secret you have received.
        /// </summary>
        /// <param name="accessTokenKey">The access token</param>
        /// <param name="accessTokenSecret">The corresponding access token secret</param>
        /// <returns>this, for fluent configuration</returns>
        IMagentoApi SetAccessToken(string accessTokenKey, string accessTokenSecret);
        /// <summary>
        /// If the Magento installation doesn't follow the default naming for the admin section, you can set it here.
        /// </summary>
        /// <param name="adminUrlPart">The part of the url that is for admin access. The default is 'admin'.</param>
        /// <returns>this, for fluent configuration</returns>
        IMagentoApi SetCustomAdminUrlPart(string adminUrlPart);
        /// <summary>
        /// This gets the access token and secret without opening a browser to let the user log in.
        /// This is primarely used for backend applications such as windows services where you can't let the user show a browser window.
        /// </summary>
        /// <param name="userName">The username of the user that is going to be used for the authentication. The user must be an administrator.</param>
        /// <param name="password">The password of the user</param>
        /// <returns>this, for fluent configuration</returns>
        IMagentoApi AuthenticateAdmin(string userName, string password);

        #endregion

        /// <summary>
        /// Allows you to retrieve the list of all products with detailed information.
        /// </summary>
        /// <returns></returns>
        Task<MagentoApiResponse<IList<Product>>> GetProducts(Filter filter);
        /// <summary>
        /// Allows you to retrieve the list of products of a specified category. These products will be returned in the product position ascending order.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<IList<Product>>> GetProductsByCategoryId(int categoryId);
        /// <summary>
        /// Get a product by id.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<Product>> GetProductById(int productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<Product>> GetProductByIdForStore(int productId, int storeId);
        /// <summary>
        /// Get a product by sku.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        /// <exception cref="Magento.RestApi.MagentoApiException">If more than 1 product is returned for sku.</exception>
        Task<MagentoApiResponse<Product>> GetProductBySku(string sku);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<Product>> GetProductBySkuForStore(string sku, int storeId);
        /// <summary>
        /// Saves the product as new product. Throws exception if product already exists.
        /// </summary>
        /// <param name="product"></param>
        Task<MagentoApiResponse<int>> SaveNewProduct(Product product);
        /// <summary>
        /// Updates an existing product. Throws exception if product doesn't exist.
        /// </summary>
        /// <param name="product"></param>
        Task<MagentoApiResponse<bool>> UpdateProduct(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="storeId"></param>
        Task<MagentoApiResponse<bool>> UpdateProductForStore(Product product, int storeId);
        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="productId"></param>
        Task<MagentoApiResponse<bool>> DeleteProduct(int productId);
        /// <summary>
        /// Allows you to retrieve information about websites assigned to a product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<IList<int>>> GetWebsitesForProduct(int productId);
        /// <summary>
        /// Allows you to assign a website to a specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="websiteId"></param>
        Task<MagentoApiResponse<bool>> AssignWebsiteToProduct(int productId, int websiteId);
        /// <summary>
        /// Allows you to unassign a website from a specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="websiteId"></param>
        Task<MagentoApiResponse<bool>> UnassignWebsiteFromProduct(int productId, int websiteId);
        /// <summary>
        /// Allows you to retrieve information about assigned categories.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<IList<int>>> GetCategoriesForProduct(int productId);
        /// <summary>
        /// Allows you to assign a category to a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        Task<MagentoApiResponse<bool>> AssignCategoryToProduct(int productId, int categoryId);
        /// <summary>
        /// Allows you to unassign a category from a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        Task<MagentoApiResponse<bool>> UnAssignCategoryFromProduct(int productId, int categoryId);
        /// <summary>
        /// Allows you to retrieve information about all images of a specified product. 
        /// If there are custom attributes with the Catalog Input Type for Store Owner option set to Media Image, these attributes will be also returned in the response as an image type.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<IList<ImageInfo>>> GetImagesForProduct(int productId);
        /// <summary>
        /// Allows you to retrieve information about product images for a specified store view.
        /// Images can have different labels for different stores. For example, image label "flower" in the English store view can be set as "fleur" in the French store view. If there are custom attributes with the Catalog Input Type for Store Owner option set to Media Image, these attributes will be also returned in the response as an image type.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<IList<ImageInfo>>> GetImagesForProductForStore(int productId, int storeId);
        /// <summary>
        /// Allows you to retrieve information about a specified product image.
        /// If there are custom attributes with the Catalog Input Type for Store Owner option set to Media Image, these attributes will be also returned in the response as an image type.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<ImageInfo>> GetImageInfoForProduct(int productId, int imageId);
        /// <summary>
        /// Allows you to retrieve information about the specified product image from a specified store.
        /// If there are custom attributes with the Catalog Input Type for Store Owner option set to Media Image, these attributes will be also returned in the response as an image type.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<ImageInfo>> GetImageInfoForProductForStore(int productId, int storeId, int imageId);
        /// <summary>
        /// Allows you to update information for the specified product image.
        /// When updating information, you need to pass only those parameters that you want to be updated. Parameters that were not passed in the request will preserve the previous values.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <param name="imageInfo"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<bool>> UpdateImageInfoForProduct(int productId, int imageId, ImageInfo imageInfo);
        /// <summary>
        /// Allows you to update the specified product image information for s specified store.
        /// When updating information, you need to pass only those parameters that you want to be updated. Parameters that were not passed in the request, will preserve the previous values.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <param name="imageId"></param>
        /// <param name="imageInfo"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<bool>> UpdateImageInfoForProductForStore(int productId, int storeId, int imageId, ImageInfo imageInfo);
        /// <summary>
        /// Allows you to add an image for the required product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<int>> AddImageToProduct(int productId, ImageFile image);
        /// <summary>
        /// Allows you to remove the specified image from a product.
        /// The image will not be deleted physically, the image parameters will be set to No Image.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<bool>> UnassignImageFromProduct(int productId, int imageId);
        /// <summary>
        /// Allows you to remove an image from the required product in the specified store.
        /// The image will not be deleted physically, the image parameters will be set to No Image for the current store.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<bool>> UnAssignImageFromProductForStore(int productId, int storeId, int imageId);
        /// <summary>
        /// Allows you to retrieve the stock item information.
        /// The list of attributes that will be returned for stock items is configured in the Magento Admin Panel.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<StockItem>> GetStockItemForProduct(int productId);
        /// <summary>
        /// Allows you to update existing stock item data.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<bool>> UpdateStockItemForProduct(int productId, StockItem stockItem);
        /// <summary>
        /// Allows you to update the quantity of the stock for a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<MagentoApiResponse<bool>> UpdateStockQuantityForProduct(int productId, double quantity);
    }
}