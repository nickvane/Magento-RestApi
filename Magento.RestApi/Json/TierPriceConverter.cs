using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class TierPriceConverter : BaseConverter<TierPrice>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var tierPrice = value as TierPrice;
            writer.WriteStartObject();
            WriteProperty(tierPrice, p => p.cust_group, true, writer, serializer);
            WriteProperty(tierPrice, p => p.price, true, writer, serializer);
            WriteProperty(tierPrice, p => p.website_id, true, writer, serializer);
            WriteProperty(tierPrice, p => p.price_qty, true, writer, serializer);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var price = existingValue as TierPrice ?? new TierPrice();
            serializer.Populate(reader, price);
            price.StartTracking();

            return price;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(TierPrice));
        }
    }
}
