using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeConverter : JsonConverter
    {
        private const string dateFormat = "yyyy-MM-dd HH:mm:ss";

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(((DateTime)value).ToUniversalTime().ToString(dateFormat));
            }
            else
            {
                writer.WriteValue(string.Empty);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var value = reader.Value == null ? string.Empty : reader.Value.ToString();
            DateTime result;
            if (DateTime.TryParseExact(value, dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return result.ToLocalTime();
            }
            if (objectType == typeof(DateTime?)) return null;
            return new DateTime();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}
