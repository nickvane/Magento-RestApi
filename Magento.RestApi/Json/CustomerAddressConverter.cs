using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerAddressConverter : BaseConverter<CustomerAddress>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var product = value as CustomerAddress;
            
            writer.WriteStartObject();

            WriteProperty(product, p => p.entity_id, true, writer, serializer);
            WriteProperty(product, p => p.firstname, true, writer, serializer);
            WriteProperty(product, p => p.lastname, true, writer, serializer);
            WriteProperty(product, p => p.street, true, writer, serializer);
            WriteProperty(product, p => p.city, true, writer, serializer);
            WriteProperty(product, p => p.country_id, true, writer, serializer);
            WriteProperty(product, p => p.region, false, writer, serializer);
            WriteProperty(product, p => p.postcode, true, writer, serializer);
            WriteProperty(product, p => p.telephone, true, writer, serializer);
            WriteProperty(product, p => p.is_default_billing, false, writer, serializer);
            WriteProperty(product, p => p.is_default_shipping, false, writer, serializer);
            WriteProperty(product, p => p.vat_id, false, writer, serializer);
            WriteProperty(product, p => p.company, false, writer, serializer);
            WriteProperty(product, p => p.fax, false, writer, serializer);
            WriteProperty(product, p => p.middlename, false, writer, serializer);
            WriteProperty(product, p => p.prefix, false, writer, serializer);
            WriteProperty(product, p => p.suffix, false, writer, serializer);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var customerAddress = existingValue as CustomerAddress ?? new CustomerAddress();

            serializer.Populate(reader, customerAddress);

            customerAddress.StartTracking();
            return customerAddress;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(CustomerAddress));
        }
    }
}
