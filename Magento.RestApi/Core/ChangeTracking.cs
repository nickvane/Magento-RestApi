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
        private bool _hasStartedTracking;

        [JsonIgnore]
        public bool HasStartedTracking
        {
            get { return _hasStartedTracking; }
        }

        public virtual bool HasChanged()
        {
            return _properties.Aggregate(false, (current, property) => current | property.Value.HasChanged());
        }

        public bool HasChanged<P>(Expression<Func<T, P>> expression)
        {
            var name = (expression.Body as MemberExpression).Member.Name;
            if (_properties.ContainsKey(name))
            {
                return _properties[name].HasChanged();
            }
            return false;
        }

        public virtual void StartTracking()
        {
            _hasStartedTracking = true;
            foreach (var property in _properties)
            {
                property.Value.SetValueAsInitial();
            }
        }

        public P GetValue<P>(Expression<Func<T, P>> expression)
        {
            var name = (expression.Body as MemberExpression).Member.Name;
            if (!_properties.ContainsKey(name))
            {
                var property = new Property<P>();
                if (_hasStartedTracking) property.SetValueAsInitial();
                _properties.Add(name, property);
                
            }
            return (_properties[name] as Property<P>).Value;
        }

        public void SetValue<P>(Expression<Func<T, P>> expression, P value)
        {
            var name = (expression.Body as MemberExpression).Member.Name;
            if (!_properties.ContainsKey(name))
            {
                var property = new Property<P>();
                if (_hasStartedTracking) property.SetValueAsInitial();
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
