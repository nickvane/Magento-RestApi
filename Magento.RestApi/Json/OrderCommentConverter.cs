using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderCommentConverter : BaseConverter<OrderComment>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var orderComment = value as OrderComment;
            
            writer.WriteStartObject();

            WriteProperty(orderComment, p => p.is_customer_notified, false, writer, serializer);
            WriteProperty(orderComment, p => p.is_visible_on_front, false, writer, serializer);
            WriteProperty(orderComment, p => p.comment, false, writer, serializer);
            WriteProperty(orderComment, p => p.status, false, writer, serializer);
            WriteProperty(orderComment, p => p.created_at, false, writer, serializer);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var OrderComment = existingValue as OrderComment ?? new OrderComment();

            serializer.Populate(reader, OrderComment);

            OrderComment.StartTracking();
            return OrderComment;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(OrderComment));
        }
    }
}
