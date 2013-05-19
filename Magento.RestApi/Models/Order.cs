using System;
using System.Collections.Generic;
using Magento.RestApi.Json;
using Newtonsoft.Json;

namespace Magento.RestApi.Models
{
    /// <summary>
    /// A product
    /// </summary>
    //[JsonConverter(typeof(OrderConverter))]
    [Serializable]
    public class Order
    {
        /// <summary>
        /// Id of the order
        /// </summary>
        public string entity_id { get; set; }
        /// <summary>
        /// Id of the customer
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_discount_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_shipping_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_shipping_tax_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_subtotal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_tax_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_total_paid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_total_refunded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double tax_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double total_paid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double total_refunded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_shipping_discount_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_subtotal_incl_tax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double base_total_due { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(DoubleConverter))]
        public double total_due { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string base_currency_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tax_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tax_rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderAddress> addresses { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderAddress> order_items { get; set; }
    }
}
