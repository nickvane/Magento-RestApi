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
    public class ProductConverter : BaseConverter<Product>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var product = value as Product;
            
            writer.WriteStartObject();

            WriteProperty(product, p => p.entity_id, true, writer, serializer);
            WriteProperty(product, p => p.sku, true, writer, serializer);
            WriteProperty(product, p => p.visibility, true, writer, serializer);
            WriteProperty(product, p => p.status, true, writer, serializer);
            WriteProperty(product, p => p.name, true, writer, serializer);
            WriteProperty(product, p => p.price, true, writer, serializer);
            WriteProperty(product, p => p.weight, true, writer, serializer);
            WriteProperty(product, p => p.tax_class_id, true, writer, serializer);
            WriteProperty(product, p => p.description, true, writer, serializer);

            // type_id is necessary for saving a new product
            WriteProperty(product, p => p.type_id, false, writer, serializer);

            WriteProperty(product, p => p.attribute_set_id, false, writer, serializer);
            WriteProperty(product, p => p.country_of_manufacture, false, writer, serializer);
            WriteProperty(product, p => p.custom_design, false, writer, serializer);
            WriteProperty(product, p => p.custom_design_from, false, writer, serializer);
            WriteProperty(product, p => p.custom_design_to, false, writer, serializer);
            WriteProperty(product, p => p.custom_layout_update, false, writer, serializer);
            WriteProperty(product, p => p.description, false, writer, serializer);
            WriteProperty(product, p => p.enable_googlecheckout, false, writer, serializer);
            WriteProperty(product, p => p.gift_message_available, false, writer, serializer);
            WriteProperty(product, p => p.meta_description, false, writer, serializer);
            WriteProperty(product, p => p.meta_keyword, false, writer, serializer);
            WriteProperty(product, p => p.meta_title, false, writer, serializer);
            WriteProperty(product, p => p.msrp, false, writer, serializer);
            WriteProperty(product, p => p.msrp_display_actual_price_type, false, writer, serializer);
            WriteProperty(product, p => p.msrp_enabled, false, writer, serializer);
            WriteProperty(product, p => p.news_from_date, false, writer, serializer);
            WriteProperty(product, p => p.news_to_date, false, writer, serializer);
            WriteProperty(product, p => p.options_container, false, writer, serializer);
            WriteProperty(product, p => p.page_layout, false, writer, serializer);
            WriteProperty(product, p => p.short_description, false, writer, serializer);
            WriteProperty(product, p => p.special_from_date, false, writer, serializer);
            WriteProperty(product, p => p.special_price, false, writer, serializer);
            WriteProperty(product, p => p.special_to_date, false, writer, serializer);
            WriteProperty(product, p => p.url_key, false, writer, serializer);
            // even if stock_data hasn't changed, the empty property needs to be written or some use_config values are set to true
            WriteProperty(product, p => p.stock_data, false, writer, serializer);

            var groupPrice = product.GetProperty(p => p.group_price);
            if (product.group_price != null && groupPrice.HasChanged())
            {
                writer.WritePropertyName("group_price");
                writer.WriteStartArray();
                foreach (var price in product.group_price)
                {
                    if (price.HasChanged())
                    {
                        serializer.Serialize(writer, price);
                    }
                }
                writer.WriteEndArray();
            }

            var tierPrice = product.GetProperty(p => p.tier_price);
            if (product.tier_price != null && tierPrice.HasChanged())
            {
                writer.WritePropertyName("tier_price");
                writer.WriteStartArray();
                foreach (var price in product.tier_price)
                {
                    if (price.HasChanged())
                    {
                        serializer.Serialize(writer, price);
                    }
                }
                writer.WriteEndArray();
            }

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
            var product = existingValue as Product ?? new Product();
            var jObject = JObject.Load(reader);

            serializer.Populate(jObject.CreateReader(), product);
            product.Attributes = new Dictionary<string, string>();

            foreach (var item in jObject.Children())
            {
                var jProperty = item as JProperty;
                if (jProperty != null)
                {
                    var name = jProperty.Name;
                    var property = product.GetType().GetProperty(name);
                    if (property == null && name != "messages")
                    {
                        if (product.Attributes.ContainsKey(name)) product.Attributes.Remove(name);
                        string value = null;
                        if (jProperty.Value != null) value = jProperty.Value.Value<string>();
                        product.Attributes.Add(name, value);
                    }
                }
            }

            product.StartTracking();
            return product;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Product));
        }
    }
}
