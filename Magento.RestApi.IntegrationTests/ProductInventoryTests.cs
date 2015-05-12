using System;
using Magento.RestApi.Models;
using Xunit;

namespace Magento.RestApi.IntegrationTests
{
    public class ProductInventoryTests : BaseFixture
    {
        [Fact]
        public void WhenUpdatingStockQuantityByIdShouldBeCorrect()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var quantity = Math.Ceiling(new Random().NextDouble() * 100);

            // Act
            var response = Client.UpdateStockQuantityForProduct(productId, quantity).Result;

            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            var updatedProduct = Client.GetProductById(productId).Result;
            Assert.Equal(quantity, updatedProduct.Result.stock_data.qty);
        }
        [Fact]
        public void WhenUpdatingStockQuantityWith0ShouldBeCorrect()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var quantity = 0;

            // Act
            var response = Client.UpdateStockQuantityForProduct(productId, quantity).Result;

            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            var updatedProduct = Client.GetProductById(productId).Result;
            Assert.Equal(quantity, updatedProduct.Result.stock_data.qty);
        }

        [Fact]
        public void WhenUpdatingStockItemByIdShouldBeCorrect()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var quantity = Math.Ceiling(new Random().NextDouble() * 100);
            var isInStock = (quantity%2) == 1;
            var stockItem = new StockItem
                                {
                                    qty = quantity,
                                    is_in_stock = isInStock
                                };

            // Act
            var response = Client.UpdateStockItemForProduct(productId, stockItem).Result;

            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            var updatedProduct = Client.GetProductById(productId).Result;
            Assert.Equal(quantity, updatedProduct.Result.stock_data.qty);
            Assert.Equal(isInStock, updatedProduct.Result.stock_data.is_in_stock);
        }

        [Fact]
        public void WhenUpdatingFullStockItemShouldBeCorrect()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;

            // Act
            var stockItem = Client.GetStockItemForProduct(productId).Result.Result;

            var backorders = stockItem.backorders != BackOrderStatus.NoBackorders ? BackOrderStatus.NoBackorders : BackOrderStatus.AllowQtyBelow0;
            stockItem.backorders = backorders;
            var enable_qty_increments = !stockItem.enable_qty_increments;
            stockItem.enable_qty_increments = enable_qty_increments;
            var is_decimal_divided = !stockItem.is_decimal_divided;
            stockItem.is_decimal_divided = is_decimal_divided;
            var is_in_stock = !stockItem.is_in_stock;
            stockItem.is_in_stock = is_in_stock;
            var is_qty_decimal = !stockItem.is_qty_decimal;
            stockItem.is_qty_decimal = is_qty_decimal;
            var manage_stock = !stockItem.manage_stock;
            stockItem.manage_stock = manage_stock;
            var max_sale_qty =  stockItem.max_sale_qty + 1;
            stockItem.max_sale_qty = max_sale_qty;
            var min_qty = stockItem.min_qty + 1;
            stockItem.min_qty = min_qty;
            var min_sale_qty = stockItem.min_sale_qty + 1;
            stockItem.min_sale_qty = min_sale_qty;
            if (!stockItem.notify_stock_qty.HasValue) stockItem.notify_stock_qty = 0;
            var notify_stock_qty = stockItem.notify_stock_qty + 1;
            stockItem.notify_stock_qty = notify_stock_qty;
            var qty_increments = !stockItem.qty_increments.HasValue || stockItem.qty_increments == 0 ? 1 : 0;
            stockItem.qty_increments = qty_increments;
            // I have no idea what this does and can't find it in the user interface. 
            // Apparently it can't be set by the api either
            //var stock_status_changed_auto = stockItem.stock_status_changed_auto.HasValue ? !stockItem.stock_status_changed_auto : true;
            //stockItem.stock_status_changed_auto = stock_status_changed_auto;
            var use_config_backorders = !stockItem.use_config_backorders;
            stockItem.use_config_backorders = use_config_backorders;
            var use_config_enable_qty_inc = !stockItem.use_config_enable_qty_inc;
            stockItem.use_config_enable_qty_inc = use_config_enable_qty_inc;
            var use_config_manage_stock = !stockItem.use_config_manage_stock;
            stockItem.use_config_manage_stock = use_config_manage_stock;
            var use_config_max_sale_qty = !stockItem.use_config_max_sale_qty;
            stockItem.use_config_max_sale_qty = use_config_max_sale_qty;
            var use_config_min_qty = !stockItem.use_config_min_qty;
            stockItem.use_config_min_qty = use_config_min_qty;
            var use_config_min_sale_qty = !stockItem.use_config_min_sale_qty;
            stockItem.use_config_min_sale_qty = use_config_min_sale_qty;
            var use_config_notify_stock_qty = !stockItem.use_config_notify_stock_qty;
            stockItem.use_config_notify_stock_qty = use_config_notify_stock_qty;
            var use_config_qty_increments = !stockItem.use_config_qty_increments;
            stockItem.use_config_qty_increments = use_config_qty_increments;
            // make sure qty is always larger than min_qty so if managestock is false, is in stock is always true
            stockItem.qty = min_qty + 1;

            var response = Client.UpdateStockItemForProduct(productId, stockItem).Result;

            // Assert
            Assert.False(response.HasErrors, response.ErrorString);
            var updatedStockitem = Client.GetStockItemForProduct(productId).Result.Result;
            Assert.Equal(backorders, updatedStockitem.backorders);
            Assert.Equal(enable_qty_increments, updatedStockitem.enable_qty_increments);
            Assert.Equal(is_decimal_divided, updatedStockitem.is_decimal_divided);
            Assert.Equal(is_qty_decimal, updatedStockitem.is_qty_decimal);
            Assert.Equal(manage_stock, updatedStockitem.manage_stock);
            Assert.Equal(max_sale_qty, updatedStockitem.max_sale_qty);
            Assert.Equal(min_qty, updatedStockitem.min_qty);
            Assert.Equal(min_sale_qty, updatedStockitem.min_sale_qty);
            Assert.Equal(notify_stock_qty, updatedStockitem.notify_stock_qty);
            Assert.Equal(qty_increments, updatedStockitem.qty_increments);
            //Assert.Equal(stock_status_changed_auto, updatedStockitem.stock_status_changed_auto);
            Assert.Equal(use_config_backorders, updatedStockitem.use_config_backorders);
            Assert.Equal(use_config_enable_qty_inc, updatedStockitem.use_config_enable_qty_inc);
            Assert.Equal(use_config_manage_stock, updatedStockitem.use_config_manage_stock);
            Assert.Equal(use_config_max_sale_qty, updatedStockitem.use_config_max_sale_qty);
            Assert.Equal(use_config_min_qty, updatedStockitem.use_config_min_qty);
            Assert.Equal(use_config_min_sale_qty, updatedStockitem.use_config_min_sale_qty);
            Assert.Equal(use_config_notify_stock_qty, updatedStockitem.use_config_notify_stock_qty);
            Assert.Equal(use_config_qty_increments, updatedStockitem.use_config_qty_increments);
            
            Assert.Equal(is_in_stock, updatedStockitem.is_in_stock);
        }

        [Fact]
        public void WhenManagingStockShouldUpdate()
        {
            // Arrange
            var product = Client.GetProductBySku("100000").Result;
            var productId = product.Result.entity_id;
            var stockItem = new StockItem
            {
                qty = 10,
                min_qty = 15,
                is_in_stock = false,
                manage_stock = true
            };
            var response1 = Client.UpdateStockItemForProduct(productId, stockItem).Result;
            var updatedStockItem = new StockItem
                                       {
                                           qty = 10,
                                           is_in_stock = true
                                       };

            // Act
            var response2 = Client.UpdateStockItemForProduct(productId, updatedStockItem).Result;
            
            // Assert
            Assert.False(response2.HasErrors, response2.ErrorString);
            var updatedProduct = Client.GetProductById(productId).Result;
            Assert.True(updatedProduct.Result.stock_data.is_in_stock.Value);
        }
    }
}
