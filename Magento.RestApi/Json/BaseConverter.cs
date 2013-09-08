using System;
using System.Linq.Expressions;
using Magento.RestApi.Core;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    public abstract class BaseConverter<T> : JsonConverter
    {
        protected void WriteProperty<O, P>(O parentObject, Expression<Func<T, P>> expression, bool isRequired, JsonWriter writer, Newtonsoft.Json.JsonSerializer serializer) where O : IChangeTracking<T>
        {
            if (parentObject.HasChanged(expression) || isRequired)
            {
                writer.WritePropertyName((expression.Body as MemberExpression).Member.Name);
                var value = parentObject.GetValue(expression);
                var jsonconvertAttributes = (expression.Body as MemberExpression).Member.GetCustomAttributes(typeof(JsonConverterAttribute), true);
                if (jsonconvertAttributes.Length == 1)
                {
                    var jsonConverterAttribute = jsonconvertAttributes[0] as JsonConverterAttribute;
                    var converter = (JsonConverter)Activator.CreateInstance(jsonConverterAttribute.ConverterType);
                    converter.WriteJson(writer, value, serializer);
                }
                else
                {
                    serializer.Serialize(writer, value);
                }
            }
        }
    }
}
