using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderItemConverter : BaseConverter<OrderItem>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var orderItem = value as OrderItem;
            
            writer.WriteStartObject();

            WriteProperty(orderItem, p => p.sku, true, writer, serializer);
            WriteProperty(orderItem, p => p.base_discount_amount, false, writer, serializer);
            WriteProperty(orderItem, p => p.base_original_price, false, writer, serializer);
            WriteProperty(orderItem, p => p.base_price, false, writer, serializer);
            WriteProperty(orderItem, p => p.base_price_incl_tax, false, writer, serializer);
            WriteProperty(orderItem, p => p.base_row_total, false, writer, serializer);
            WriteProperty(orderItem, p => p.base_row_total_incl_tax, false, writer, serializer);
            WriteProperty(orderItem, p => p.base_tax_amount, false, writer, serializer);
            WriteProperty(orderItem, p => p.price, false, writer, serializer);
            WriteProperty(orderItem, p => p.tax_amount, false, writer, serializer);
            WriteProperty(orderItem, p => p.tax_percent, false, writer, serializer);
            WriteProperty(orderItem, p => p.item_id, false, writer, serializer);
            WriteProperty(orderItem, p => p.parent_item_id, false, writer, serializer);
            WriteProperty(orderItem, p => p.name, false, writer, serializer);
            WriteProperty(orderItem, p => p.qty_canceled, false, writer, serializer);
            WriteProperty(orderItem, p => p.qty_invoiced, false, writer, serializer);
            WriteProperty(orderItem, p => p.qty_ordered, false, writer, serializer);
            WriteProperty(orderItem, p => p.qty_refunded, false, writer, serializer);
            WriteProperty(orderItem, p => p.qty_shipped, false, writer, serializer);
            WriteProperty(orderItem, p => p.original_price, false, writer, serializer);
            WriteProperty(orderItem, p => p.discount_amount, false, writer, serializer);
            WriteProperty(orderItem, p => p.row_total, false, writer, serializer);
            WriteProperty(orderItem, p => p.price_incl_tax, false, writer, serializer);
            WriteProperty(orderItem, p => p.row_total_incl_tax, false, writer, serializer);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var orderItem = existingValue as OrderItem ?? new OrderItem();

            serializer.Populate(reader, orderItem);

            orderItem.StartTracking();
            return orderItem;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(OrderItem));
        }
    }
}
