using System;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(((int) value));
            }
            else
            {
                writer.WriteValue(string.Empty);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var value = reader.Value == null ? string.Empty : reader.Value.ToString();
            int enumInt;
            return int.TryParse(value, out enumInt) ? Enum.ToObject(objectType, enumInt) : null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == null || objectType.IsEnum || IsNullableEnum(objectType);
        }

        private bool IsNullableEnum(Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }
    }
}
