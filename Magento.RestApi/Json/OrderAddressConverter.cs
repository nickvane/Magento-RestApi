using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderAddressConverter : BaseConverter<OrderAddress>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var product = value as OrderAddress;
            
            writer.WriteStartObject();

            WriteProperty(product, p => p.address_type, true, writer, serializer);
            WriteProperty(product, p => p.firstname, true, writer, serializer);
            WriteProperty(product, p => p.lastname, true, writer, serializer);
            WriteProperty(product, p => p.street, true, writer, serializer);
            WriteProperty(product, p => p.city, true, writer, serializer);
            WriteProperty(product, p => p.country_id, true, writer, serializer);
            WriteProperty(product, p => p.region, false, writer, serializer);
            WriteProperty(product, p => p.postcode, true, writer, serializer);
            WriteProperty(product, p => p.telephone, true, writer, serializer);
            WriteProperty(product, p => p.company, false, writer, serializer);
            WriteProperty(product, p => p.middlename, false, writer, serializer);
            WriteProperty(product, p => p.prefix, false, writer, serializer);
            WriteProperty(product, p => p.suffix, false, writer, serializer);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var OrderAddress = existingValue as OrderAddress ?? new OrderAddress();

            serializer.Populate(reader, OrderAddress);

            OrderAddress.StartTracking();
            return OrderAddress;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(OrderAddress));
        }
    }
}
