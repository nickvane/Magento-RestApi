using System;
using System.Collections.Generic;
using System.Linq;
using Magento.RestApi.Models;
using Xunit;

namespace Magento.RestApi.IntegrationTests
{
    public class ProductTests : BaseFixture
    {
        private string _validSku = "100000";

        [Fact]
        public void WhenGettingProductWithValidIdShouldReturnProduct()
        {
            // Arrange
            
            // Act
            var response = Client.GetProductById(1).Result;

            // Assert
            Assert.NotNull(response.Result);
            Assert.False(response.HasErrors);
            Assert.Equal(_validSku, response.Result.sku);
        }

        [Fact]
        public void WhenGettingProductWithInvalidIdShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetProductById(0).Result;

            // Assert
            Assert.Null(response.Result);
            Assert.True(response.HasErrors);
            Assert.Equal("404", response.Errors.First().Code);
        }

        [Fact]
        public void WhenGettingProductWithValidskuShouldReturnProduct()
        {
            // Arrange

            // Act
            var response = Client.GetProductBySku(_validSku).Result;

            // Assert
            Assert.NotNull(response.Result);
            Assert.False(response.HasErrors);
            Assert.Equal(1, response.Result.entity_id);
        }

        [Fact]
        public void WhenGettingProductWithInvalidSkuShouldReturnNull()
        {
            // Arrange

            // Act
            var response = Client.GetProductBySku("000000").Result;

            // Assert
            Assert.Null(response.Result);
            Assert.False(response.HasErrors, response.ErrorString);
        }

        [Fact]
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
            Assert.False(response.HasErrors, response.ErrorString);
            Assert.True(0 < response.Result);
            var newProduct = Client.GetProductBySku(sku).Result;
            Assert.NotNull(newProduct.Result);
            Assert.Equal(12.5, newProduct.Result.price);
            Assert.False(newProduct.HasErrors);
        }

        [Fact]
        public void CanUpdateProductWithPrice0()
        {
            // Arrange
            var product = Client.GetProductBySku(_validSku).Result.Result;

            // Act
            product.price = 0;
            var response = Client.UpdateProduct(product).Result;
            
            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            Assert.True(response.Result);
            var updatedProduct = Client.GetProductBySku(_validSku).Result.Result;
            Assert.Equal(0, updatedProduct.price);
        }

        [Fact]
        public void WhenGettingProductsByCategoryShouldReturnProducts()
        {
            // Arrange

            // Act
            var response = Client.GetProductsByCategoryId(3).Result;
            
            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            Assert.Equal(1, response.Result.Count);
            Assert.Equal(_validSku, response.Result.First().sku);
        }

        [Fact]
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
            Assert.False(response1.HasErrors, response1.ErrorString);
            Assert.True(1 < response1.Result);
            Assert.False(response2.HasErrors, response1.ErrorString);

            Assert.Equal(product.attribute_set_id, updatedProduct.attribute_set_id);
            Assert.Equal(product.country_of_manufacture, updatedProduct.country_of_manufacture);
            Assert.Equal(product.custom_design, updatedProduct.custom_design);
            Assert.Equal(product.custom_design_from, updatedProduct.custom_design_from);
            Assert.Equal(product.custom_design_to, updatedProduct.custom_design_to);
            Assert.Equal(product.custom_layout_update, updatedProduct.custom_layout_update);
            Assert.Equal(product.description, updatedProduct.description);
            Assert.Equal(product.enable_googlecheckout, updatedProduct.enable_googlecheckout);
            Assert.Equal(product.gift_message_available, updatedProduct.gift_message_available);
            Assert.Equal(product.meta_description, updatedProduct.meta_description);
            Assert.Equal(product.meta_keyword, updatedProduct.meta_keyword);
            Assert.Equal(product.meta_title, updatedProduct.meta_title);
            Assert.Equal(product.msrp, updatedProduct.msrp);
            Assert.Equal(product.msrp_display_actual_price_type, updatedProduct.msrp_display_actual_price_type);
            Assert.Equal(product.msrp_enabled, updatedProduct.msrp_enabled);
            Assert.Equal(product.name + "2", updatedProduct.name);
            Assert.Equal(product.news_from_date, updatedProduct.news_from_date);
            Assert.Equal(product.news_to_date, updatedProduct.news_to_date);
            Assert.Equal(product.options_container, updatedProduct.options_container);
            Assert.Equal(product.page_layout, updatedProduct.page_layout);
            Assert.Equal(product.price, updatedProduct.price);
            Assert.Equal(product.short_description, updatedProduct.short_description);
            Assert.Equal(product.sku, updatedProduct.sku);
            Assert.Equal(product.special_from_date, updatedProduct.special_from_date);
            Assert.Equal(product.special_price, updatedProduct.special_price);
            Assert.Equal(product.special_to_date, updatedProduct.special_to_date);
            Assert.Equal(product.status, updatedProduct.status);
            Assert.Equal(product.tax_class_id, updatedProduct.tax_class_id);
            Assert.Equal(product.type_id, updatedProduct.type_id);
            Assert.Equal(product.url_key, updatedProduct.url_key);
            Assert.Equal(product.visibility, updatedProduct.visibility);
            Assert.Equal(product.weight, updatedProduct.weight);

            Assert.Equal(product.stock_data.backorders, updatedProduct.stock_data.backorders);
            Assert.Equal(product.stock_data.enable_qty_increments, updatedProduct.stock_data.enable_qty_increments);
            Assert.Equal(product.stock_data.is_decimal_divided, updatedProduct.stock_data.is_decimal_divided);
            Assert.Equal(product.stock_data.is_in_stock, updatedProduct.stock_data.is_in_stock);
            Assert.Equal(product.stock_data.is_qty_decimal, updatedProduct.stock_data.is_qty_decimal);
            Assert.Equal(product.stock_data.manage_stock, updatedProduct.stock_data.manage_stock);
            Assert.Equal(product.stock_data.max_sale_qty, updatedProduct.stock_data.max_sale_qty);
            Assert.Equal(product.stock_data.min_qty, updatedProduct.stock_data.min_qty);
            Assert.Equal(product.stock_data.min_sale_qty, updatedProduct.stock_data.min_sale_qty);
            Assert.Equal(product.stock_data.notify_stock_qty, updatedProduct.stock_data.notify_stock_qty);
            Assert.Equal(product.stock_data.qty, updatedProduct.stock_data.qty);
            Assert.Equal(product.stock_data.qty_increments, updatedProduct.stock_data.qty_increments);
            Assert.Equal(product.stock_data.use_config_backorders, updatedProduct.stock_data.use_config_backorders);
            Assert.Equal(product.stock_data.use_config_enable_qty_inc, updatedProduct.stock_data.use_config_enable_qty_inc);
            Assert.Equal(product.stock_data.use_config_manage_stock, updatedProduct.stock_data.use_config_manage_stock);
            Assert.Equal(product.stock_data.use_config_max_sale_qty, updatedProduct.stock_data.use_config_max_sale_qty);
            Assert.Equal(product.stock_data.use_config_min_qty, updatedProduct.stock_data.use_config_min_qty);
            Assert.Equal(product.stock_data.use_config_min_sale_qty, updatedProduct.stock_data.use_config_min_sale_qty);
            Assert.Equal(product.stock_data.use_config_notify_stock_qty, updatedProduct.stock_data.use_config_notify_stock_qty);
            Assert.Equal(product.stock_data.use_config_qty_increments, updatedProduct.stock_data.use_config_qty_increments);

            Assert.Equal(1, updatedProduct.group_price.Count);
            Assert.Equal(product.group_price.First().cust_group, updatedProduct.group_price.First().cust_group);
            Assert.Equal(product.group_price.First().price, updatedProduct.group_price.First().price);
            Assert.Equal(product.group_price.First().website_id, updatedProduct.group_price.First().website_id);

            Assert.Equal(1, updatedProduct.tier_price.Count);
            Assert.Equal(product.tier_price.First().cust_group, updatedProduct.tier_price.First().cust_group);
            Assert.Equal(product.tier_price.First().price, updatedProduct.tier_price.First().price);
            Assert.Equal(product.tier_price.First().website_id, updatedProduct.tier_price.First().website_id);
            Assert.Equal(product.tier_price.First().price_qty, updatedProduct.tier_price.First().price_qty);
        }

        [Fact]
        public void WhenGettingProductsWithLikeFilterShouldReturnProducts()
        {
            // Arrange
            var filter = new Filter();
            filter.FilterExpressions.Add(new FilterExpression("name", ExpressionOperator.like, "Melting%"));
            filter.PageSize = 5;
            filter.Page = 0;

            // Act
            var response = Client.GetProducts(filter).Result;

            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            Assert.Equal(1, response.Result.Count);
        }
    }
}