using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class BoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value != null ? (((bool)value) ? 1 : 0).ToString(CultureInfo.InvariantCulture) : string.Empty);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var value = reader.Value == null ? string.Empty : reader.Value.ToString();
            if (value == "1") return true;
            if (value == "0") return false;
            if (objectType == typeof (bool?)) return null;
            return false;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?);
        }
    }
}
