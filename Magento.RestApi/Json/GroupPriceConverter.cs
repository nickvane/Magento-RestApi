using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupPriceConverter : BaseConverter<GroupPrice>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var groupPrice = value as GroupPrice;
            writer.WriteStartObject();
            WriteProperty(groupPrice, p => p.cust_group, true, writer, serializer);
            WriteProperty(groupPrice, p => p.price, true, writer, serializer);
            WriteProperty(groupPrice, p => p.website_id, true, writer, serializer);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var price = existingValue as GroupPrice ?? new GroupPrice();
            serializer.Populate(reader, price);
            price.StartTracking();

            return price;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof (GroupPrice));
        }
    }
}
