using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Magento.RestApi.Core
{
    [Serializable]
    public class ChangeTracking<T> : IChangeTracking<T>
    {
        private readonly Dictionary<string, IProperty> _properties = new Dictionary<string, IProperty>();

        [JsonIgnore]
        public bool HasStartedTracking { get; private set; }

        public virtual bool HasChanged()
        {
            return _properties.Aggregate(false, (current, property) => current | property.Value.HasChanged());
        }

        public bool HasChanged<P>(Expression<Func<T, P>> expression)
        {
            var name = (expression.Body as MemberExpression).Member.Name;
            return _properties.ContainsKey(name) && _properties[name].HasChanged();
        }

        public virtual void StartTracking()
        {
            HasStartedTracking = true;
            foreach (var property in _properties)
            {
                property.Value.SetValueAsInitial();
            }
        }

        public P GetValue<P>(Expression<Func<T, P>> expression)
        {
            var name = (expression.Body as MemberExpression).Member.Name;
            if (_properties.ContainsKey(name)) return (_properties[name] as Property<P>).Value;
            var property = new Property<P>();
            if (HasStartedTracking) property.SetValueAsInitial();
            _properties.Add(name, property);
            return (_properties[name] as Property<P>).Value;
        }

        public void SetValue<P>(Expression<Func<T, P>> expression, P value)
        {
            var name = (expression.Body as MemberExpression).Member.Name;
            if (!_properties.ContainsKey(name))
            {
                var property = new Property<P>();
                if (HasStartedTracking) property.SetValueAsInitial();
                _properties.Add(name, property);

            }
            (_properties[name] as Property<P>).Value = value;
        }

        public Property<P> GetProperty<P>(Expression<Func<T, P>> expression)
        {
            var name = (expression.Body as MemberExpression).Member.Name;
            if (_properties.ContainsKey(name))
            {
                return _properties[name] as Property<P>;
            }
            return null;
        }
    }
}
