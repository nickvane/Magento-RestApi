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
        public int? customer_id
        {
            get { return GetValue(x => x.customer_id); }
            set { SetValue(x => x.customer_id, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double? base_discount_amount
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
        public double base_grand_total
        {
            get { return GetValue(x => x.base_grand_total); }
            set { SetValue(x => x.base_grand_total, value); }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double? base_total_paid
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
        public double grand_total
        {
            get { return GetValue(x => x.grand_total); }
            set { SetValue(x => x.grand_total, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double subtotal
        {
            get { return GetValue(x => x.subtotal); }
            set { SetValue(x => x.subtotal, value); }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double shipping_amount
        {
            get { return GetValue(x => x.shipping_amount); }
            set { SetValue(x => x.shipping_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double shipping_tax_amount
        {
            get { return GetValue(x => x.shipping_tax_amount); }
            set { SetValue(x => x.shipping_tax_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double store_to_order_rate
        {
            get { return GetValue(x => x.store_to_order_rate); }
            set { SetValue(x => x.store_to_order_rate, value); }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double? total_paid
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
        public double? discount_amount
        {
            get { return GetValue(x => x.discount_amount); }
            set { SetValue(x => x.discount_amount, value); }
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
        public string discount_description
        {
            get { return GetValue(x => x.discount_description); }
            set { SetValue(x => x.discount_description, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double shipping_discount_amount
        {
            get { return GetValue(x => x.shipping_discount_amount); }
            set { SetValue(x => x.shipping_discount_amount, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double subtotal_incl_tax
        {
            get { return GetValue(x => x.subtotal_incl_tax); }
            set { SetValue(x => x.subtotal_incl_tax, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double shipping_incl_tax
        {
            get { return GetValue(x => x.shipping_incl_tax); }
            set { SetValue(x => x.shipping_incl_tax, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string payment_method
        {
            get { return GetValue(x => x.payment_method); }
            set { SetValue(x => x.payment_method, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string gift_message_from
        {
            get { return GetValue(x => x.gift_message_from); }
            set { SetValue(x => x.gift_message_from, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string gift_message_to
        {
            get { return GetValue(x => x.gift_message_to); }
            set { SetValue(x => x.gift_message_to, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string gift_message_body
        {
            get { return GetValue(x => x.gift_message_body); }
            set { SetValue(x => x.gift_message_body, value); }
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
        /// <summary>
        /// 
        /// </summary>
        public List<OrderComment> order_comments
        {
            get { return GetValue(x => x.order_comments); }
            set { SetValue(x => x.order_comments, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime created_at
        {
            get { return GetValue(x => x.created_at); }
            set { SetValue(x => x.created_at, value); }
        }
		/// <summary>
        /// 
        /// </summary>
        public string remote_ip
        {
            get { return GetValue(x => x.remote_ip); }
            set { SetValue(x => x.remote_ip, value); }
        }
		/// <summary>
        /// 
        /// </summary>
        public string store_currency_code
        {
            get { return GetValue(x => x.store_currency_code); }
            set { SetValue(x => x.store_currency_code, value); }
        }
		/// <summary>
        /// 
        /// </summary>
        public string store_name
        {
            get { return GetValue(x => x.store_name); }
            set { SetValue(x => x.store_name, value); }
        }		
		/// <summary>
        /// 
        /// </summary>
        public string increment_id
        {
            get { return GetValue(x => x.increment_id); }
            set { SetValue(x => x.increment_id, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string status
        {
            get { return GetValue(x => x.status); }
            set { SetValue(x => x.status, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string coupon_code
        {
            get { return GetValue(x => x.coupon_code); }
            set { SetValue(x => x.coupon_code, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string shipping_description
        {
            get { return GetValue(x => x.shipping_description); }
            set { SetValue(x => x.shipping_description, value); }
        }
    }
}
