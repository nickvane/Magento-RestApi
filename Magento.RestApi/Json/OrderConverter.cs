using System;
using System.Collections.Generic;
using Magento.RestApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderConverter : BaseConverter<Order>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var product = value as Order;
            
            writer.WriteStartObject();

            WriteProperty(product, p => p.entity_id, true, writer, serializer);
            WriteProperty(product, p => p.base_currency_code, false, writer, serializer);
            WriteProperty(product, p => p.base_discount_amount, false, writer, serializer);
            WriteProperty(product, p => p.base_shipping_amount, false, writer, serializer);
            WriteProperty(product, p => p.base_shipping_discount_amount, false, writer, serializer);
            WriteProperty(product, p => p.base_shipping_tax_amount, false, writer, serializer);
            WriteProperty(product, p => p.base_subtotal, false, writer, serializer);
            WriteProperty(product, p => p.base_subtotal_incl_tax, false, writer, serializer);
            WriteProperty(product, p => p.base_tax_amount, false, writer, serializer);
            WriteProperty(product, p => p.base_total_due, false, writer, serializer);
            WriteProperty(product, p => p.base_total_paid, false, writer, serializer);
            WriteProperty(product, p => p.base_total_refunded, false, writer, serializer);
            WriteProperty(product, p => p.customer_id, false, writer, serializer);
            WriteProperty(product, p => p.tax_amount, false, writer, serializer);
            WriteProperty(product, p => p.tax_name, false, writer, serializer);
            WriteProperty(product, p => p.tax_rate, false, writer, serializer);
            WriteProperty(product, p => p.total_due, false, writer, serializer);
            WriteProperty(product, p => p.total_paid, false, writer, serializer);
            WriteProperty(product, p => p.total_refunded, false, writer, serializer);
            
            // Lists

            var addresses = product.GetProperty(p => p.addresses);
            if (addresses != null && addresses.HasChanged())
            {
                writer.WritePropertyName("addresses");
                writer.WriteStartArray();
                foreach (var address in product.addresses)
                {
                    if (address.HasChanged())
                    {
                        serializer.Serialize(writer, address);
                    }
                }
                writer.WriteEndArray();
            }

            var order_items = product.GetProperty(p => p.order_items);
            if (order_items != null && order_items.HasChanged())
            {
                writer.WritePropertyName("order_items");
                writer.WriteStartArray();
                foreach (var item in product.order_items)
                {
                    if (item.HasChanged())
                    {
                        serializer.Serialize(writer, item);
                    }
                }
                writer.WriteEndArray();
            }

            // WriteProperty(product, p => p.addresses, false, writer, serializer);
            // WriteProperty(product, p => p.order_items, false, writer, serializer);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var Order = existingValue as Order ?? new Order();
            //var jObject = JObject.Load(reader);

            serializer.Populate(reader, Order);
            /*Order.Attributes = new Dictionary<string, string>();

            foreach (var item in jObject.Children())
            {
                var jProperty = item as JProperty;
                if (jProperty != null)
                {
                    var name = jProperty.Name;
                    var property = Order.GetType().GetProperty(name);
                    if (property == null && name != "messages")
                    {
                        if (Order.Attributes.ContainsKey(name)) Order.Attributes.Remove(name);
                        string value = null;
                        if (jProperty.Value != null) value = jProperty.Value.Value<string>();
                        Order.Attributes.Add(name, value);
                    }
                }
            }*/

            Order.StartTracking();
            return Order;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Order));
        }
    }
}
