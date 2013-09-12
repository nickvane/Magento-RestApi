using System;
using Magento.RestApi.Json;
using Newtonsoft.Json;
using Magento.RestApi.Core;

namespace Magento.RestApi.Models
{
    /// <summary>
    /// A product
    /// </summary>
    [JsonConverter(typeof(OrderItemConverter))]
    [Serializable]
    public class OrderItem : ChangeTracking<OrderItem>
    {
        public OrderItem()
        {
            this.StartTracking();
        }
        /// <summary>
        /// 
        /// </summary>
        public string sku
        {
            get { return GetValue(x => x.sku); }
            set { SetValue(x => x.sku, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double price
        {
            get { return GetValue(x => x.price); }
            set { SetValue(x => x.price, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_price
        {
            get { return GetValue(x => x.base_price); }
            set { SetValue(x => x.base_price, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_original_price
        {
            get { return GetValue(x => x.base_original_price); }
            set { SetValue(x => x.base_original_price, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double tax_percent
        {
            get { return GetValue(x => x.tax_percent); }
            set { SetValue(x => x.tax_percent, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double tax_amount
        {
            get { return GetValue(x => x.tax_amount); }
            set { SetValue(x => x.tax_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_tax_amount
        {
            get { return GetValue(x => x.base_tax_amount); }
            set { SetValue(x => x.base_tax_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_discount_amount
        {
            get { return GetValue(x => x.base_discount_amount); }
            set { SetValue(x => x.base_discount_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_row_total
        {
            get { return GetValue(x => x.base_row_total); }
            set { SetValue(x => x.base_row_total, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_price_incl_tax
        {
            get { return GetValue(x => x.base_price_incl_tax); }
            set { SetValue(x => x.base_price_incl_tax, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_row_total_incl_tax
        {
            get { return GetValue(x => x.base_row_total_incl_tax); }
            set { SetValue(x => x.base_row_total_incl_tax, value); }
        }
    }
}
