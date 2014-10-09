using System;
using System.Linq.Expressions;

namespace Magento.RestApi.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChangeTracking<T> : IChangeTracking
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="P"></typeparam>
        /// <returns></returns>
        bool HasChanged<P>(Expression<Func<T, P>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="P"></typeparam>
        /// <returns></returns>
        P GetValue<P>(Expression<Func<T, P>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <typeparam name="P"></typeparam>
        void SetValue<P>(Expression<Func<T, P>> expression, P value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="P"></typeparam>
        /// <returns></returns>
        Property<P> GetProperty<P>(Expression<Func<T, P>> expression);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IChangeTracking
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool HasChanged();
        /// <summary>
        /// 
        /// </summary>
        void StartTracking();
    }
}
