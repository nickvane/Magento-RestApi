using Magento.RestApi.Json;
using Newtonsoft.Json;
using System;

namespace Magento.RestApi.Models
{
    internal class CustomOptionConverter : BaseConverter<CustomOption>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var product = value as CustomOption;

            writer.WriteStartObject();

            WriteProperty(product, p => p.custom_view, false, writer, serializer);
            WriteProperty(product, p => p.label, false, writer, serializer);
            WriteProperty(product, p => p.option_id, false, writer, serializer);
            WriteProperty(product, p => p.option_type, false, writer, serializer);
            WriteProperty(product, p => p.option_value, false, writer, serializer);
            WriteProperty(product, p => p.print_value, false, writer, serializer);
            WriteProperty(product, p => p.value, false, writer, serializer);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var option = existingValue as CustomOption ?? new CustomOption();

            serializer.Populate(reader, option);

            option.StartTracking();
            return option;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Customer));
        }
    }
}