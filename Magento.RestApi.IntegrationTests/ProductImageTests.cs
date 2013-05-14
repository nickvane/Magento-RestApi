using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Magento.RestApi.Models;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class ProductImageTests : BaseTest
    {
        private int productId;
        private int websiteId = 2;
        private int storeId1 = 2;

        [TestFixtureSetUp]
        public void Setup()
        {
            var sku = "100001";
            // create product with minimal required fields
            var product = new Product
            {
                description = "Guitar picks are small, and we have to admit, sometimes we lose 'em. And sometimes, you just discover you need one at the strangest times (impromptu flashmob jam sessions, raucous children's parties, boring company meetings, etc.). Well, the good news is: if you have a DIY Guitar Pick Punch and some imagination, you'll never be without a guitar pick again. Just insert the material you want a pick out of into the DIY Guitar Pick Punch, and . . . well . . . squeeze. Instant guitar pick! Make them out of expired credit cards, those grocery store club cards, plastic packaging material, and more! Hey, want to get super DIY-y? Then punch a few thin picks and glue them together into the ultimate pick of density. Keep a DIY Guitar Pick Punch in your gigbag, and you'll never be without a pick again. Now, if only someone can invent a guitar punch for punching out fully functional guitars!",
                short_description = "Punch your own picks!",
                price = 24.99,
                sku = sku,
                visibility = ProductVisibility.CatalogSearch,
                status = ProductStatus.Enabled,
                name = "DIY Guitar Pick Punch",
                weight = 10,
                tax_class_id = 2,
                type_id = "simple",
                attribute_set_id = 4 // default
            };

            var existingProduct = Client.GetProductBySku(sku).Result;
            if (existingProduct.Result != null)
            {
                var delete = Client.DeleteProduct(existingProduct.Result.entity_id).Result;
                if (delete.HasErrors) throw new Exception(delete.Errors.First().Message);
            }

            var response1 = Client.CreateNewProduct(product).Result;
            var response2 = Client.GetProductBySku(sku).Result;
            productId = response2.Result.entity_id;
            var response3 = Client.AssignWebsiteToProduct(productId, websiteId).Result;
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            var response = Client.DeleteProduct(productId).Result;
            if (response.HasErrors) throw new Exception(response.Errors.First().Message);
        }

        [Test]
        public void AddImageToProduct()
        {
            // Arrange
            var images = Client.GetImagesForProduct(productId).Result;
            var imageCount = images.Result == null ? 0 : images.Result.Count;

            // Act
            var filePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Images\\100001\\") + "01.jpg";
            var imageFile = new ImageFile
                                {
                                    file_content = File.ReadAllBytes(filePath), 
                                    file_mime_type = "image/jpeg",
                                    file_name = "100001_01"
                                };
            var response = Client.AddImageToProduct(productId, imageFile).Result;

            // Assert
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.Less(0, response.Result);
            var newImages = Client.GetImagesForProduct(productId).Result;
            Assert.AreEqual(imageCount + 1, newImages.Result.Count);
        }

        [Test]
        public void UnassignImageFromProduct()
        {
            // Arrange
            var images = Client.GetImagesForProduct(productId).Result;
            var imageCount = images.Result == null ? 0 : images.Result.Count;

            // Act
            var filePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Images\\100001\\") + "03.jpg";
            var imageFile = new ImageFile
            {
                file_content = File.ReadAllBytes(filePath),
                file_mime_type = "image/jpeg",
                file_name = "100001_03"
            };
            var response = Client.AddImageToProduct(productId, imageFile).Result;
            var newImages = Client.GetImagesForProduct(productId).Result;
            var response2 = Client.UnassignImageFromProduct(productId, response.Result).Result;
            var updatedImages = Client.GetImagesForProduct(productId).Result;
            
            // Assert
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.AreEqual(imageCount + 1, newImages.Result.Count);
            // Only when the image is not used in any stores, then it gets deleted
            Assert.AreEqual(imageCount, updatedImages.Result.Count);
        }

        [Test]
        public void UnAssignImageFromProductForStore()
        {
            // Arrange
            var images = Client.GetImagesForProduct(productId).Result;
            var imageCount = images.Result == null ? 0 : images.Result.Count;
            var imagesForStore = Client.GetImagesForProductForStore(productId, storeId1).Result;
            var filePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Images\\100001\\") + "04.jpg";
            var imageFile = new ImageFile
            {
                file_content = File.ReadAllBytes(filePath),
                file_mime_type = "image/jpeg",
                file_name = "100001_04"
            };
            var response1 = Client.AddImageToProduct(productId, imageFile).Result;
            var imageId = response1.Result;
            var imageInfo = Client.GetImageInfoForProduct(productId, imageId).Result.Result;
            imageInfo.types.Add(ImageType.image);
            imageInfo.types.Add(ImageType.small_image);
            imageInfo.types.Add(ImageType.thumbnail);
            var response2 = Client.UpdateImageInfoForProduct(productId, imageId, imageInfo).Result;

            // Act
            var response3 = Client.UnAssignImageFromProductForStore(productId, storeId1, imageId).Result;
            var updatedImages = Client.GetImagesForProduct(productId).Result;
            var updatedImagesForStore1 = Client.GetImagesForProductForStore(productId, storeId1).Result;

            // Assert
            Assert.IsFalse(response3.HasErrors, response3.ErrorString);
            Assert.AreEqual(imageCount + 1, updatedImagesForStore1.Result.Count);
            Assert.AreEqual(imageCount + 1, updatedImages.Result.Count);
            var imageInfo2 = Client.GetImageInfoForProduct(productId, imageId).Result.Result;
            Assert.AreEqual(3, imageInfo2.types.Count);
            var imageInfo3 = Client.GetImageInfoForProductForStore(productId, storeId1, imageId).Result.Result;
            Assert.AreEqual(0, imageInfo3.types.Count);
        }

        [Test]
        public void GetAndUpdateImageInfoForProduct()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result.Result;
            var images = Client.GetImagesForProduct(product.entity_id).Result.Result;
            
            // Act
            var response1 = Client.GetImageInfoForProduct(product.entity_id, images.First().id).Result;
            var imageInfo = response1.Result;
            imageInfo.label = Guid.NewGuid().ToString();
            var response2 = Client.UpdateImageInfoForProduct(product.entity_id, imageInfo.id, imageInfo).Result;

            // Assert
            Assert.IsFalse(response1.HasErrors, response1.ErrorString);
            Assert.IsFalse(response2.HasErrors, response2.ErrorString);
            var response3 = Client.GetImageInfoForProduct(product.entity_id, images.First().id).Result;
            Assert.IsFalse(response3.HasErrors, response3.ErrorString);
            Assert.AreEqual(imageInfo.label, response3.Result.label);
        }

        [Test]
        public void GetAndUpdateImageInfoForProductForStore()
        {
            // Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Images\\100001\\") + "02.jpg";
            var imageFile = new ImageFile
            {
                file_content = File.ReadAllBytes(filePath),
                file_mime_type = "image/jpeg",
                file_name = "100001_02"
            };
            var response = Client.AddImageToProduct(productId, imageFile).Result;
            var imageId = response.Result;
            var response1 = Client.GetImageInfoForProduct(productId, imageId).Result;
            var imageInfo = response1.Result;
            var websiteLabel = "Label1";
            imageInfo.label = websiteLabel;
            imageInfo.types = new List<ImageType>
                                  {
                                      ImageType.image,
                                      ImageType.small_image,
                                      ImageType.thumbnail
                                  };
            imageInfo.position = 10;
            var response2 = Client.UpdateImageInfoForProduct(productId, imageId, imageInfo).Result;

            // Act
            imageInfo = new ImageInfo();
            var store2Label = "Label2";
            imageInfo.label = store2Label;
            imageInfo.position = 11;
            imageInfo.types = new List<ImageType>
                                  {
                                      ImageType.image
                                  };
            var response3 = Client.UpdateImageInfoForProductForStore(productId, 2, imageId, imageInfo).Result;
            imageInfo = new ImageInfo();
            var store3Label = "Label3";
            imageInfo.label = store3Label;
            imageInfo.position = 12;
            imageInfo.exclude = true;
            var response4 = Client.UpdateImageInfoForProductForStore(productId, 3, imageId, imageInfo).Result;
            var response5 = Client.GetImageInfoForProductForStore(productId, 2, imageId).Result;
            var response6 = Client.GetImageInfoForProductForStore(productId, 3, imageId).Result;
            var response7 = Client.GetImageInfoForProduct(productId, imageId).Result;

            // Assert
            Assert.IsFalse(response3.HasErrors, response1.ErrorString);
            Assert.IsFalse(response4.HasErrors, response2.ErrorString);
            Assert.IsFalse(response5.HasErrors, response1.ErrorString);
            Assert.IsFalse(response6.HasErrors, response2.ErrorString);
            Assert.IsFalse(response7.HasErrors, response2.ErrorString);

            Assert.AreEqual(websiteLabel, response7.Result.label);
            Assert.AreEqual(store2Label, response5.Result.label);
            Assert.AreEqual(store3Label, response6.Result.label);

            Assert.AreEqual(3, response7.Result.types.Count);
            Assert.AreEqual(1, response5.Result.types.Count);
            Assert.AreEqual(3, response6.Result.types.Count);

            Assert.AreEqual(10, response7.Result.position);
            Assert.AreEqual(11, response5.Result.position);
            Assert.AreEqual(12, response6.Result.position);

            Assert.IsFalse(response7.Result.exclude.Value);
            Assert.IsFalse(response5.Result.exclude.Value);
            Assert.IsTrue(response6.Result.exclude.Value);
        }
    }
}
