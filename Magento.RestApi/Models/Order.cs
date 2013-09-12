using System;
using Magento.RestApi.Core;
using Magento.RestApi.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Magento.RestApi.Models
{
    /// <summary>
    /// A product
    /// </summary>
    [JsonConverter(typeof(OrderConverter))]
    [Serializable]
    public class Order : ChangeTracking<Order>
    {
        public Order()
        {
            this.StartTracking();
        }

        /// <summary>
        /// Id of the order
        /// </summary>
        public int entity_id
        {
            get { return GetValue(x => x.entity_id); }
            set { SetValue(x => x.entity_id, value); }
        }
        /// <summary>
        /// Id of the customer
        /// </summary>
        public string customer_id
        {
            get { return GetValue(x => x.customer_id); }
            set { SetValue(x => x.customer_id, value); }
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
        public double base_shipping_amount
        {
            get { return GetValue(x => x.base_shipping_amount); }
            set { SetValue(x => x.base_shipping_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_shipping_tax_amount
        {
            get { return GetValue(x => x.base_shipping_tax_amount); }
            set { SetValue(x => x.base_shipping_tax_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_subtotal
        {
            get { return GetValue(x => x.base_subtotal); }
            set { SetValue(x => x.base_subtotal, value); }
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
        public double base_total_paid
        {
            get { return GetValue(x => x.base_total_paid); }
            set { SetValue(x => x.base_total_paid, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double? base_total_refunded
        {
            get { return GetValue(x => x.base_total_refunded); }
            set { SetValue(x => x.base_total_refunded, value); }
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
        public double total_paid
        {
            get { return GetValue(x => x.total_paid); }
            set { SetValue(x => x.total_paid, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double? total_refunded
        {
            get { return GetValue(x => x.total_refunded); }
            set { SetValue(x => x.total_refunded, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_shipping_discount_amount
        {
            get { return GetValue(x => x.base_shipping_discount_amount); }
            set { SetValue(x => x.base_shipping_discount_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_subtotal_incl_tax
        {
            get { return GetValue(x => x.base_subtotal_incl_tax); }
            set { SetValue(x => x.base_subtotal_incl_tax, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_total_due
        {
            get { return GetValue(x => x.base_total_due); }
            set { SetValue(x => x.base_total_due, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double total_due
        {
            get { return GetValue(x => x.total_due); }
            set { SetValue(x => x.total_due, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string base_currency_code
        {
            get { return GetValue(x => x.base_currency_code); }
            set { SetValue(x => x.base_currency_code, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tax_name
        {
            get { return GetValue(x => x.tax_name); }
            set { SetValue(x => x.tax_name, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tax_rate
        {
            get { return GetValue(x => x.tax_rate); }
            set { SetValue(x => x.tax_rate, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        
        public List<OrderAddress> addresses
        {
            get { return GetValue(x => x.addresses); }
            set { SetValue(x => x.addresses, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        
        public List<OrderItem> order_items
        {
            get { return GetValue(x => x.order_items); }
            set { SetValue(x => x.order_items, value); }
        }
    }
}
