using System;
using System.Collections.Generic;
using Magento.RestApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magento.RestApi.Json
{
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
            WriteProperty(imageInfo, p => p.file_mime_type, true, writer, serializer);
            WriteProperty(imageInfo, p => p.file_name, true, writer, serializer);

            WriteProperty(imageInfo, p => p.file_content, true, writer, serializer);

            if (imageInfo.file_content != null && imageInfo.HasChanged(i => i.file_content))
            {
                writer.WritePropertyName("types");
                serializer.Serialize(writer, Convert.ToBase64String(imageInfo.GetValue(i => i.file_content), 0, imageInfo.file_content.Length));
            }

            if (imageInfo.types != null && imageInfo.HasChanged(i => i.types))
            {
                writer.WritePropertyName("types");
                writer.WriteStartArray();
                foreach (var type in imageInfo.GetValue(i => i.types))
                {
                    serializer.Serialize(writer, (int)type);
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
