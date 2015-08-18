using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderConverter : BaseConverter<Order>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var order = value as Order;
            
            writer.WriteStartObject();

            WriteProperty(order, p => p.entity_id, true, writer, serializer);
            WriteProperty(order, p => p.base_currency_code, false, writer, serializer);
            WriteProperty(order, p => p.base_discount_amount, false, writer, serializer);
            WriteProperty(order, p => p.base_shipping_amount, false, writer, serializer);
            WriteProperty(order, p => p.base_shipping_discount_amount, false, writer, serializer);
            WriteProperty(order, p => p.base_shipping_tax_amount, false, writer, serializer);
            WriteProperty(order, p => p.base_subtotal, false, writer, serializer);
            WriteProperty(order, p => p.base_subtotal_incl_tax, false, writer, serializer);
            WriteProperty(order, p => p.base_tax_amount, false, writer, serializer);
            WriteProperty(order, p => p.base_total_due, false, writer, serializer);
            WriteProperty(order, p => p.base_total_paid, false, writer, serializer);
            WriteProperty(order, p => p.base_total_refunded, false, writer, serializer);
            WriteProperty(order, p => p.customer_id, false, writer, serializer);
            WriteProperty(order, p => p.tax_amount, false, writer, serializer);
            WriteProperty(order, p => p.tax_name, false, writer, serializer);
            WriteProperty(order, p => p.tax_rate, false, writer, serializer);
            WriteProperty(order, p => p.total_due, false, writer, serializer);
            WriteProperty(order, p => p.total_paid, false, writer, serializer);
            WriteProperty(order, p => p.total_refunded, false, writer, serializer);
            WriteProperty(order, p => p.created_at, false, writer, serializer);
            WriteProperty(order, p => p.remote_ip, false, writer, serializer);
            WriteProperty(order, p => p.store_currency_code, false, writer, serializer);
            WriteProperty(order, p => p.store_name, false, writer, serializer);
            WriteProperty(order, p => p.increment_id, false, writer, serializer);
            WriteProperty(order, p => p.status, false, writer, serializer);
            WriteProperty(order, p => p.coupon_code, false, writer, serializer);
            WriteProperty(order, p => p.shipping_description, false, writer, serializer);
            WriteProperty(order, p => p.gift_message_from, false, writer, serializer);
            WriteProperty(order, p => p.gift_message_to, false, writer, serializer);
            WriteProperty(order, p => p.gift_message_body, false, writer, serializer);
            WriteProperty(order, p => p.payment_method, false, writer, serializer);
            WriteProperty(order, p => p.shipping_incl_tax, false, writer, serializer);
            WriteProperty(order, p => p.subtotal_incl_tax, false, writer, serializer);
            WriteProperty(order, p => p.shipping_discount_amount, false, writer, serializer);
            WriteProperty(order, p => p.discount_description, false, writer, serializer);
            WriteProperty(order, p => p.discount_amount, false, writer, serializer);
            WriteProperty(order, p => p.store_to_order_rate, false, writer, serializer);
            WriteProperty(order, p => p.shipping_tax_amount, false, writer, serializer);
            WriteProperty(order, p => p.shipping_amount, false, writer, serializer);
            WriteProperty(order, p => p.subtotal, false, writer, serializer);
            WriteProperty(order, p => p.grand_total, false, writer, serializer);
            WriteProperty(order, p => p.base_grand_total, false, writer, serializer);

            // Lists

            var addresses = order.GetProperty(p => p.addresses);
            if (addresses != null && addresses.HasChanged())
            {
                writer.WritePropertyName("addresses");
                writer.WriteStartArray();
                foreach (var address in order.addresses)
                {
                    if (address.HasChanged())
                    {
                        serializer.Serialize(writer, address);
                    }
                }
                writer.WriteEndArray();
            }

            var orderItems = order.GetProperty(p => p.order_items);
            if (orderItems != null && orderItems.HasChanged())
            {
                writer.WritePropertyName("order_items");
                writer.WriteStartArray();
                foreach (var item in order.order_items)
                {
                    if (item.HasChanged())
                    {
                        serializer.Serialize(writer, item);
                    }
                }
                writer.WriteEndArray();
            }

            var orderComments = order.GetProperty(p => p.order_comments);
            if (orderComments != null && orderComments.HasChanged())
            {
                writer.WritePropertyName("order_comments");
                writer.WriteStartArray();
                foreach (var item in order.order_comments)
                {
                    if (item.HasChanged())
                    {
                        serializer.Serialize(writer, item);
                    }
                }
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var order = existingValue as Order ?? new Order();

            serializer.Populate(reader, order);

            order.StartTracking();
            return order;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Order));
        }
    }
}
