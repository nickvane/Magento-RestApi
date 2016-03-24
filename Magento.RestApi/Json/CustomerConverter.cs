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
    public class CustomerConverter : BaseConverter<Customer>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var product = value as Customer;
            
            writer.WriteStartObject();

            WriteProperty(product, p => p.entity_id, true, writer, serializer);
            WriteProperty(product, p => p.firstname, false, writer, serializer);
            WriteProperty(product, p => p.lastname, false, writer, serializer);
            WriteProperty(product, p => p.email, false, writer, serializer);
            WriteProperty(product, p => p.group_id, false, writer, serializer);
            WriteProperty(product, p => p.middlename, false, writer, serializer);
            WriteProperty(product, p => p.prefix, false, writer, serializer);
            WriteProperty(product, p => p.suffix, false, writer, serializer);
            WriteProperty(product, p => p.taxvat, false, writer, serializer);
            WriteProperty(product, p => p.password, false, writer, serializer);
            WriteProperty(product, p => p.website_id, false, writer, serializer);
            WriteProperty(product, p => p.disable_auto_group_change, false, writer, serializer);
            
            var attributes = product.GetProperty(p => p.Attributes);
            if (attributes != null)
            {
                foreach (var attribute in attributes.Value)
                {
                    if (!attributes.InitialValue.ContainsKey(attribute.Key))
                    {
                        writer.WritePropertyName(attribute.Key);
                        serializer.Serialize(writer, attribute.Value);
                    }
                    else
                    {
                        if (attribute.Value != attributes.InitialValue[attribute.Key])
                        {
                            writer.WritePropertyName(attribute.Key);
                            serializer.Serialize(writer, attribute.Value);
                        }
                    }
                }
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var customer = existingValue as Customer ?? new Customer();
            var jObject = JObject.Load(reader);

            serializer.Populate(jObject.CreateReader(), customer);
            customer.Attributes = new Dictionary<string, string>();
            
            foreach (var item in jObject.Children())
            {
                var jProperty = item as JProperty;
                if (jProperty != null)
                {
                    var name = jProperty.Name;
                    var property = customer.GetType().GetProperty(name);
                    if (property == null && name != "messages")
                    {
                        if (customer.Attributes.ContainsKey(name)) customer.Attributes.Remove(name);
                        string value = null;
                        if (jProperty.Value != null) value = jProperty.Value.Value<string>();
                        customer.Attributes.Add(name, value);
                    }
                }
            }

            customer.StartTracking();
            return customer;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Customer));
        }
    }
}
