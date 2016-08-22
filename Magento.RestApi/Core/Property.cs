using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Magento.RestApi.Core
{
    [Serializable]
    public class Property<T> : IProperty
    {
        private T _initialValue;
        private T _value;

        public T InitialValue
        {
            get { return _initialValue; }
        }

        public T Value { get { return _value; } set { _value = value; } }

        public bool HasChanged()
        {
            var hasChanged = false;
            if (typeof (T).IsAssignableFrom(typeof (IChangeTracking<T>)))
            {
                hasChanged = (Value as IChangeTracking).HasChanged();
            }
            else if (typeof(T).IsAssignableFrom(typeof(Dictionary<string, string>)))
            {
                if (_initialValue == null)
                {
                    hasChanged = _value != null;
                }
                else
                {
                    if (_value == null) return hasChanged;
                    var initialValue = _initialValue as IDictionary<string, string>;
                    var value = _value as IDictionary<string, string>;
                    hasChanged = initialValue.Count != value.Count;
                    if (hasChanged) return hasChanged;
                    foreach (var pair in initialValue)
                    {
                        hasChanged = !value.ContainsKey(pair.Key) || pair.Value != value[pair.Key];
                        if (hasChanged) break;
                    }
                }
            }
            else if (typeof(T).GetInterfaces().Any(x =>
                                                   x.IsGenericType &&
                                                   x.GetGenericTypeDefinition() == typeof(IList<>)) &&
                !typeof(T).IsAssignableFrom(typeof(byte[])))
            {
                var genericType = typeof (T).GetGenericArguments()[0];
                var initialValue = _initialValue as IList;
                var value = _value as IList;
                if (!(value == null && initialValue == null))
                {
                    if ((value == null && initialValue != null) || (value != null && initialValue == null))
                    {
                        hasChanged = true;
                    }
                    else
                    {
                        if (initialValue.Count != value.Count)
                        {
                            hasChanged = true;
                        }
                        else
                        {
                            if (genericType.GetInterfaces().Any(x =>
                                                   x.IsGenericType &&
                                                   x.GetGenericTypeDefinition() == typeof(IChangeTracking<>)))
                            {
                                hasChanged = value.Cast<object>().Aggregate(hasChanged, (current, item) => current | (item as IChangeTracking).HasChanged());
                            }
                            else
                            {
                                for (var i = 0; i < initialValue.Count; i++)
                                {
                                    if (!Equals(initialValue[i], value[i]))
                                    {
                                        hasChanged = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                hasChanged |= !Equals(_initialValue, _value);
            }
            return hasChanged;
        }

        public void SetValueAsInitial()
        {
            _initialValue = DeepClone(_value);
        }

        private C DeepClone<C>(C obj)
        {
            if (obj == null) return default(C);
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (C)formatter.Deserialize(ms);
            }
        }
    }
}
