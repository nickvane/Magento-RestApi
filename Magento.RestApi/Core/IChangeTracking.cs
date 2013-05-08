using System;
using System.Linq.Expressions;

namespace Magento.RestApi.Core
{
    public interface IChangeTracking<T> : IChangeTracking
    {
        bool HasChanged<P>(Expression<Func<T, P>> expression);
        P GetValue<P>(Expression<Func<T, P>> expression);
        void SetValue<P>(Expression<Func<T, P>> expression, P value);
        Property<P> GetProperty<P>(Expression<Func<T, P>> expression);
    }

    public interface IChangeTracking
    {
        bool HasChanged();
        void StartTracking();
    }
}
