using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageInfoConverter : BaseConverter<ImageInfo>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var imageInfo = value as ImageInfo;

            writer.WriteStartObject();

            WriteProperty(imageInfo, p => p.id, false, writer, serializer);
            WriteProperty(imageInfo, p => p.exclude, false, writer, serializer);
            WriteProperty(imageInfo, p => p.position, false, writer, serializer);
            WriteProperty(imageInfo, p => p.label, false, writer, serializer);
            WriteProperty(imageInfo, p => p.file_content, false, writer, serializer);
            WriteProperty(imageInfo, p => p.file_mime_type, false, writer, serializer);
            WriteProperty(imageInfo, p => p.file_name, false, writer, serializer);

            if (imageInfo.types != null && imageInfo.HasChanged(i => i.types))
            {
                writer.WritePropertyName("types");
                writer.WriteStartArray();
                foreach (var type in imageInfo.GetValue(i => i.types))
                {
                    serializer.Serialize(writer, type.ToString());
                }
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var imageInfo = existingValue as ImageInfo ?? new ImageInfo();
            var jObject = JObject.Load(reader);
            serializer.Populate(jObject.CreateReader(), imageInfo);
            imageInfo.StartTracking();
            return imageInfo;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(ImageInfo));
        }
    }
}
