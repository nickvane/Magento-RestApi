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
            var customerAddress = value as CustomerAddress;
            
            writer.WriteStartObject();

            WriteProperty(customerAddress, p => p.entity_id, true, writer, serializer);
            WriteProperty(customerAddress, p => p.firstname, true, writer, serializer);
            WriteProperty(customerAddress, p => p.lastname, true, writer, serializer);
            WriteProperty(customerAddress, p => p.street, true, writer, serializer);
            WriteProperty(customerAddress, p => p.city, true, writer, serializer);
            WriteProperty(customerAddress, p => p.country_id, true, writer, serializer);
            WriteProperty(customerAddress, p => p.region, false, writer, serializer);
            WriteProperty(customerAddress, p => p.postcode, true, writer, serializer);
            WriteProperty(customerAddress, p => p.telephone, true, writer, serializer);
            WriteProperty(customerAddress, p => p.is_default_billing, false, writer, serializer);
            WriteProperty(customerAddress, p => p.is_default_shipping, false, writer, serializer);
            WriteProperty(customerAddress, p => p.vat_id, false, writer, serializer);
            WriteProperty(customerAddress, p => p.company, false, writer, serializer);
            WriteProperty(customerAddress, p => p.fax, false, writer, serializer);
            WriteProperty(customerAddress, p => p.middlename, false, writer, serializer);
            WriteProperty(customerAddress, p => p.prefix, false, writer, serializer);
            WriteProperty(customerAddress, p => p.suffix, false, writer, serializer);

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
