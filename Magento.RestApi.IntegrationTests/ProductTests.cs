using System;
using System.Collections.Generic;
using System.Linq;
using Magento.RestApi.Models;
using NUnit.Framework;

namespace Magento.RestApi.IntegrationTests
{
    [TestFixture]
    public class ProductTests : BaseTest
    {
        private string _validSku = "100000";

        [Test]
        public void WhenGettingProductWithValidIdShouldReturnProduct()
        {
            // Arrange
            
            // Act
            var response = Client.GetProductById(1).Result;

            // Assert
            Assert.IsNotNull(response.Result, response.ErrorString);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(_validSku, response.Result.sku);
        }

        [Test]
        public void WhenGettingProductWithInvalidIdShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetProductById(0).Result;

            // Assert
            Assert.IsNull(response.Result, response.ErrorString);
            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual("404", response.Errors.First().Code);
        }

        [Test]
        public void WhenGettingProductWithValidskuShouldReturnProduct()
        {
            // Arrange

            // Act
            var response = Client.GetProductBySku(_validSku).Result;

            // Assert
            Assert.IsNotNull(response.Result, response.ErrorString);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(1, response.Result.entity_id);
        }

        [Test]
        public void WhenGettingProductWithInvalidSkuShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetProductBySku("000000").Result;

            // Assert
            Assert.IsNull(response.Result);
            Assert.IsFalse(response.HasErrors, response.ErrorString);
        }

        [Test]
        public void WhenAddingANewProductShouldBeSaved()
        {
            // Arrange
            var sku = "200000";
            // create product with minimal required fields
            var product = new Product
                              {
                                  description = "A long description of the new product",
                                  short_description = "A short description of the new product",
                                  price = 12.5,
                                  sku = sku,
                                  visibility = ProductVisibility.CatalogSearch,
                                  status = ProductStatus.Enabled,
                                  name = "New product",
                                  weight = 10,
                                  tax_class_id = 2,
                                  type_id = "simple",
                                  attribute_set_id = 4 // default
                              };
            var existingProduct = Client.GetProductBySku(sku).Result;
            if (existingProduct.Result != null)
            {
                var delete = Client.DeleteProduct(existingProduct.Result.entity_id).Result;
                if(delete.HasErrors) throw new Exception(delete.Errors.First().Message);
            }
            
            // Act
            var response = Client.CreateNewProduct(product).Result;

            // Assert
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.Less(0, response.Result);
            var newProduct = Client.GetProductBySku(sku).Result;
            Assert.IsNotNull(newProduct.Result);
            Assert.AreEqual(12.5, newProduct.Result.price);
            Assert.IsFalse(newProduct.HasErrors);
        }

        [Test]
        public void CanUpdateProductWithPrice0()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result.Result;

            // Act
            product.price = 0;
            var response = Client.UpdateProduct(product).Result;
            
            // Assert
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.IsTrue(response.Result);
            var updatedProduct = Client.GetProductBySku(_validSku).Result.Result;
            Assert.AreEqual(0, updatedProduct.price);
        }

        [Test]
        public void WhenGettingProductsByCategoryShouldReturnProducts()
        {
            // Arrange

            // Act
            var response = Client.GetProductsByCategoryId(3).Result;
            
            // Assert
            Assert.IsFalse(response.HasErrors, response.ErrorString);
            Assert.AreEqual(1, response.Result.Count);
            Assert.AreEqual(_validSku, response.Result.First().sku);
        }

        [Test]
        public void CanCreateAndUpdateFullProduct()
        {
            // Arrange
            var sku = "200002";
            // create product with minimal required fields
            var product = new Product
            {
                description = "A long description of the new product",
                short_description = "A short description of the new product",
                price = 12.5,
                sku = sku,
                visibility = ProductVisibility.CatalogSearch,
                status = ProductStatus.Enabled,
                name = "New product",
                weight = 10.5,
                tax_class_id = 2,
                type_id = "simple",
                attribute_set_id = 4, // default
                country_of_manufacture = "BE",
                custom_design = "default/default",
                custom_design_from = DateTime.Now.TrimMilliseconds(),
                custom_design_to = DateTime.Now.AddMonths(1).TrimMilliseconds(),
                custom_layout_update = "<test></test>",
                enable_googlecheckout = true,
                gift_message_available = true,
                meta_description = "A meta description of the new product",
                meta_keyword = "meta keyword",
                meta_title = "meta title",
                msrp = 121.50,
                msrp_display_actual_price_type = PriceTypeDisplay.BeforeOrderConfirmation,
                msrp_enabled = ManufacturerPriceEnablement.Yes,
                news_from_date = DateTime.Now.TrimMilliseconds(),
                news_to_date = DateTime.Now.AddDays(7).TrimMilliseconds(),
                options_container = "container2",
                page_layout = "two_columns_left",
                special_from_date = DateTime.Now.TrimMilliseconds(),
                special_to_date = DateTime.Now.AddDays(14).TrimMilliseconds(),
                special_price = 110.99,
                url_key = "a-new-product"
            };
            product.group_price = new List<GroupPrice>
                                      {
                                          new GroupPrice
                                              {
                                                  cust_group = 3,
                                                  price = 125.63,
                                                  website_id = 2
                                              }
                                      };
            product.stock_data = new StockData
            {
                backorders = BackOrderStatus.AllowQtyBelow0,
                enable_qty_increments = false,
                is_decimal_divided = false,
                is_in_stock = true,
                is_qty_decimal = false,
                manage_stock = true,
                max_sale_qty = 10,
                min_qty = 2,
                min_sale_qty = 1,
                notify_stock_qty = 5,
                qty = 20,
                qty_increments = 1,
                use_config_backorders = false,
                use_config_enable_qty_inc = false,
                use_config_manage_stock = false,
                use_config_max_sale_qty = false,
                use_config_min_qty = false,
                use_config_min_sale_qty = false,
                use_config_notify_stock_qty = false,
                use_config_qty_increments = false
            };
            product.tier_price = new List<TierPrice>
                                     {
                                         new TierPrice
                                             {
                                                 cust_group = 3,
                                                 price = 109.99,
                                                 price_qty = 5,
                                                 website_id = 2
                                             }
                                           
                                     };

            var existingProduct = Client.GetProductBySku(sku).Result;
            if (existingProduct.Result != null)
            {
                var delete = Client.DeleteProduct(existingProduct.Result.entity_id).Result;
                if (delete.HasErrors) throw new Exception(delete.Errors.First().Message);
            }

            // act
            var response1 = Client.CreateNewProduct(product).Result;
            var productUpdate = Client.GetProductById(response1.Result).Result.Result;
            productUpdate.name += "2";
            var response2 = Client.UpdateProduct(productUpdate).Result;
            var updatedProduct = Client.GetProductById(response1.Result).Result.Result;

            // assert
            Assert.IsFalse(response1.HasErrors, response1.ErrorString);
            Assert.Less(1, response1.Result);
            Assert.IsFalse(response2.HasErrors, response1.ErrorString);

            Assert.AreEqual(product.attribute_set_id, updatedProduct.attribute_set_id);
            Assert.AreEqual(product.country_of_manufacture, updatedProduct.country_of_manufacture);
            Assert.AreEqual(product.custom_design, updatedProduct.custom_design);
            Assert.AreEqual(product.custom_design_from, updatedProduct.custom_design_from);
            Assert.AreEqual(product.custom_design_to, updatedProduct.custom_design_to);
            Assert.AreEqual(product.custom_layout_update, updatedProduct.custom_layout_update);
            Assert.AreEqual(product.description, updatedProduct.description);
            Assert.AreEqual(product.enable_googlecheckout, updatedProduct.enable_googlecheckout);
            Assert.AreEqual(product.gift_message_available, updatedProduct.gift_message_available);
            Assert.AreEqual(product.meta_description, updatedProduct.meta_description);
            Assert.AreEqual(product.meta_keyword, updatedProduct.meta_keyword);
            Assert.AreEqual(product.meta_title, updatedProduct.meta_title);
            Assert.AreEqual(product.msrp, updatedProduct.msrp);
            Assert.AreEqual(product.msrp_display_actual_price_type, updatedProduct.msrp_display_actual_price_type);
            Assert.AreEqual(product.msrp_enabled, updatedProduct.msrp_enabled);
            Assert.AreEqual(product.name + "2", updatedProduct.name);
            Assert.AreEqual(product.news_from_date, updatedProduct.news_from_date);
            Assert.AreEqual(product.news_to_date, updatedProduct.news_to_date);
            Assert.AreEqual(product.options_container, updatedProduct.options_container);
            Assert.AreEqual(product.page_layout, updatedProduct.page_layout);
            Assert.AreEqual(product.price, updatedProduct.price);
            Assert.AreEqual(product.short_description, updatedProduct.short_description);
            Assert.AreEqual(product.sku, updatedProduct.sku);
            Assert.AreEqual(product.special_from_date, updatedProduct.special_from_date);
            Assert.AreEqual(product.special_price, updatedProduct.special_price);
            Assert.AreEqual(product.special_to_date, updatedProduct.special_to_date);
            Assert.AreEqual(product.status, updatedProduct.status);
            Assert.AreEqual(product.tax_class_id, updatedProduct.tax_class_id);
            Assert.AreEqual(product.type_id, updatedProduct.type_id);
            Assert.AreEqual(product.url_key, updatedProduct.url_key);
            Assert.AreEqual(product.visibility, updatedProduct.visibility);
            Assert.AreEqual(product.weight, updatedProduct.weight);

            Assert.AreEqual(product.stock_data.backorders, updatedProduct.stock_data.backorders);
            Assert.AreEqual(product.stock_data.enable_qty_increments, updatedProduct.stock_data.enable_qty_increments);
            Assert.AreEqual(product.stock_data.is_decimal_divided, updatedProduct.stock_data.is_decimal_divided);
            Assert.AreEqual(product.stock_data.is_in_stock, updatedProduct.stock_data.is_in_stock);
            Assert.AreEqual(product.stock_data.is_qty_decimal, updatedProduct.stock_data.is_qty_decimal);
            Assert.AreEqual(product.stock_data.manage_stock, updatedProduct.stock_data.manage_stock);
            Assert.AreEqual(product.stock_data.max_sale_qty, updatedProduct.stock_data.max_sale_qty);
            Assert.AreEqual(product.stock_data.min_qty, updatedProduct.stock_data.min_qty);
            Assert.AreEqual(product.stock_data.min_sale_qty, updatedProduct.stock_data.min_sale_qty);
            Assert.AreEqual(product.stock_data.notify_stock_qty, updatedProduct.stock_data.notify_stock_qty);
            Assert.AreEqual(product.stock_data.qty, updatedProduct.stock_data.qty);
            Assert.AreEqual(product.stock_data.qty_increments, updatedProduct.stock_data.qty_increments);
            Assert.AreEqual(product.stock_data.use_config_backorders, updatedProduct.stock_data.use_config_backorders);
            Assert.AreEqual(product.stock_data.use_config_enable_qty_inc, updatedProduct.stock_data.use_config_enable_qty_inc);
            Assert.AreEqual(product.stock_data.use_config_manage_stock, updatedProduct.stock_data.use_config_manage_stock);
            Assert.AreEqual(product.stock_data.use_config_max_sale_qty, updatedProduct.stock_data.use_config_max_sale_qty);
            Assert.AreEqual(product.stock_data.use_config_min_qty, updatedProduct.stock_data.use_config_min_qty);
            Assert.AreEqual(product.stock_data.use_config_min_sale_qty, updatedProduct.stock_data.use_config_min_sale_qty);
            Assert.AreEqual(product.stock_data.use_config_notify_stock_qty, updatedProduct.stock_data.use_config_notify_stock_qty);
            Assert.AreEqual(product.stock_data.use_config_qty_increments, updatedProduct.stock_data.use_config_qty_increments);

            Assert.AreEqual(1, updatedProduct.group_price.Count);
            Assert.AreEqual(product.group_price.First().cust_group, updatedProduct.group_price.First().cust_group);
            Assert.AreEqual(product.group_price.First().price, updatedProduct.group_price.First().price);
            Assert.AreEqual(product.group_price.First().website_id, updatedProduct.group_price.First().website_id);

            Assert.AreEqual(1, updatedProduct.tier_price.Count);
            Assert.AreEqual(product.tier_price.First().cust_group, updatedProduct.tier_price.First().cust_group);
            Assert.AreEqual(product.tier_price.First().price, updatedProduct.tier_price.First().price);
            Assert.AreEqual(product.tier_price.First().website_id, updatedProduct.tier_price.First().website_id);
            Assert.AreEqual(product.tier_price.First().price_qty, updatedProduct.tier_price.First().price_qty);
        }
    }
}