using System;
using System.Linq.Expressions;
using Magento.RestApi.Core;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseConverter<T> : JsonConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="expression"></param>
        /// <param name="isRequired"></param>
        /// <param name="writer"></param>
        /// <param name="serializer"></param>
        /// <typeparam name="O"></typeparam>
        /// <typeparam name="P"></typeparam>
        protected void WriteProperty<O, P>(O parentObject, Expression<Func<T, P>> expression, bool isRequired, JsonWriter writer, Newtonsoft.Json.JsonSerializer serializer) where O : IChangeTracking<T>
        {
            if (!parentObject.HasChanged(expression) && !isRequired) return;

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
