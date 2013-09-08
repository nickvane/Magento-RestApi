using System;
using Magento.RestApi.Core;
using Magento.RestApi.Json;
using Newtonsoft.Json;

namespace Magento.RestApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StockDataConverter))]
    public class StockData : ChangeTracking<StockData>, IEquatable<StockData>
    {
        /// <summary>
        /// 
        /// </summary>
        public StockData()
        {
            StartTracking();
        }

        /// <summary>
        /// Quantity of stock items for the current product
        /// </summary>
        /// <remarks>optional</remarks>
        public double? qty
        {
            get { return GetValue(x => x.qty); }
            set { SetValue(x => x.qty, value); }
        }
        /// <summary>
        /// Quantity for stock items to become out of stock
        /// </summary>
        /// <remarks>optional</remarks>
        public double? min_qty
        {
            get { return GetValue(x => x.min_qty); }
            set { SetValue(x => x.min_qty, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Qty for Item's Status to Become Out of Stock option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? use_config_min_qty
        {
            get { return GetValue(x => x.use_config_min_qty); }
            set { SetValue(x => x.use_config_min_qty, value); }
        }
        /// <summary>
        /// Choose whether the product can be sold using decimals (e.g., you can buy 2.5 product)
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? is_qty_decimal
        {
            get { return GetValue(x => x.is_qty_decimal); }
            set { SetValue(x => x.is_qty_decimal, value); }
        }
        /// <summary>
        /// Defines whether the customer can place the order for products that are out of stock at the moment.
        /// </summary>
        /// <remarks>optional</remarks>
        public BackOrderStatus? backorders
        {
            get { return GetValue(x => x.backorders); }
            set { SetValue(x => x.backorders, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Backorders option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? use_config_backorders
        {
            get { return GetValue(x => x.use_config_backorders); }
            set { SetValue(x => x.use_config_backorders, value); }
        }
        /// <summary>
        /// Minimum number of items in the shopping cart to be sold
        /// </summary>
        /// <remarks>optional</remarks>
        public double? min_sale_qty
        {
            get { return GetValue(x => x.min_sale_qty); }
            set { SetValue(x => x.min_sale_qty, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Minimum Qty Allowed in Shopping Cart option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? use_config_min_sale_qty
        {
            get { return GetValue(x => x.use_config_min_sale_qty); }
            set { SetValue(x => x.use_config_min_sale_qty, value); }
        }
        /// <summary>
        /// Maximum number of items in the shopping cart to be sold
        /// </summary>
        /// <remarks>optional</remarks>
        public double? max_sale_qty
        {
            get { return GetValue(x => x.max_sale_qty); }
            set { SetValue(x => x.max_sale_qty, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Maximum Qty Allowed in Shopping Cart option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? use_config_max_sale_qty
        {
            get { return GetValue(x => x.use_config_max_sale_qty); }
            set { SetValue(x => x.use_config_max_sale_qty, value); }
        }
        /// <summary>
        /// Defines whether the product is available for selling
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? is_in_stock
        {
            get { return GetValue(x => x.is_in_stock); }
            set { SetValue(x => x.is_in_stock, value); }
        }
        /// <summary>
        /// The number of inventory items below which the customer will be notified
        /// </summary>
        /// <remarks>optional</remarks>
        public double? notify_stock_qty
        {
            get { return GetValue(x => x.notify_stock_qty); }
            set { SetValue(x => x.notify_stock_qty, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Notify for Quantity Below option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? use_config_notify_stock_qty
        {
            get { return GetValue(x => x.use_config_notify_stock_qty); }
            set { SetValue(x => x.use_config_notify_stock_qty, value); }
        }
        /// <summary>
        /// Choose whether to view and specify the product quantity and availability and whether the product is in stock management
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? manage_stock
        {
            get { return GetValue(x => x.manage_stock); }
            set { SetValue(x => x.manage_stock, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Manage Stock option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? use_config_manage_stock
        {
            get { return GetValue(x => x.use_config_manage_stock); }
            set { SetValue(x => x.use_config_manage_stock, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Qty Increments option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool use_config_qty_increments
        {
            get { return GetValue(x => x.use_config_qty_increments); }
            set { SetValue(x => x.use_config_qty_increments, value); }
        }
        /// <summary>
        /// The product quantity increment value
        /// </summary>
        /// <remarks>optional</remarks>
        public double qty_increments
        {
            get { return GetValue(x => x.qty_increments); }
            set { SetValue(x => x.qty_increments, value); }
        }
        /// <summary>
        /// Choose whether the Config settings will be applied for the Enable Qty Increments option
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? use_config_enable_qty_inc
        {
            get { return GetValue(x => x.use_config_enable_qty_inc); }
            set { SetValue(x => x.use_config_enable_qty_inc, value); }
        }
        /// <summary>
        /// Defines whether the customer can add products only in increments to the shopping cart
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? enable_qty_increments
        {
            get { return GetValue(x => x.enable_qty_increments); }
            set { SetValue(x => x.enable_qty_increments, value); }
        }
        /// <summary>
        /// Defines whether the stock items can be divided into multiple boxes for shipping
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? is_decimal_divided
        {
            get { return GetValue(x => x.is_decimal_divided); }
            set { SetValue(x => x.is_decimal_divided, value); }
        }

        public bool Equals(StockData other)
        {
            if (other == null) return true;
            var isEqual = true;
            isEqual &= backorders == other.backorders;
            isEqual &= enable_qty_increments == other.enable_qty_increments;
            isEqual &= is_decimal_divided == other.is_decimal_divided;
            isEqual &= is_in_stock == other.is_in_stock;
            isEqual &= is_qty_decimal == other.is_qty_decimal;
            isEqual &= manage_stock == other.manage_stock;
            isEqual &= max_sale_qty == other.max_sale_qty;
            isEqual &= min_qty == other.min_qty;
            isEqual &= min_sale_qty == other.min_sale_qty;
            isEqual &= notify_stock_qty == other.notify_stock_qty;
            isEqual &= qty == other.qty;
            isEqual &= qty_increments == other.qty_increments;
            isEqual &= use_config_backorders == other.use_config_backorders;
            isEqual &= use_config_enable_qty_inc == other.use_config_enable_qty_inc;
            isEqual &= use_config_manage_stock == other.use_config_manage_stock;
            isEqual &= use_config_max_sale_qty == other.use_config_max_sale_qty;
            isEqual &= use_config_min_qty == other.use_config_min_qty;
            isEqual &= use_config_min_sale_qty == other.use_config_min_sale_qty;
            isEqual &= use_config_notify_stock_qty == other.use_config_notify_stock_qty;
            isEqual &= use_config_qty_increments == other.use_config_qty_increments;

            return isEqual;
        }
    }
}
