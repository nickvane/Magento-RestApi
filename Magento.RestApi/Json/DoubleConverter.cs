using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class DoubleConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value != null ? ((double) value).ToString(CultureInfo.GetCultureInfo("en-US")) : string.Empty);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var value = reader.Value == null ? string.Empty : reader.Value.ToString();
            double result;
            if (double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result))
            {
                return result;
            }
            if (objectType == typeof(double?)) return null;
            return 0.0;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(double?);
        }
    }
}
