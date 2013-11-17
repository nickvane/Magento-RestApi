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
            var product = value as OrderItem;
            
            writer.WriteStartObject();

            WriteProperty(product, p => p.sku, true, writer, serializer);
            WriteProperty(product, p => p.base_discount_amount, false, writer, serializer);
            WriteProperty(product, p => p.base_original_price, false, writer, serializer);
            WriteProperty(product, p => p.base_price, false, writer, serializer);
            WriteProperty(product, p => p.base_price_incl_tax, false, writer, serializer);
            WriteProperty(product, p => p.base_row_total, false, writer, serializer);
            WriteProperty(product, p => p.base_row_total_incl_tax, false, writer, serializer);
            WriteProperty(product, p => p.base_tax_amount, false, writer, serializer);
            WriteProperty(product, p => p.price, false, writer, serializer);
            WriteProperty(product, p => p.tax_amount, false, writer, serializer);
            WriteProperty(product, p => p.tax_percent, false, writer, serializer);

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
