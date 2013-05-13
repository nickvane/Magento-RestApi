using System;

namespace Magento.RestApi.IntegrationTests
{
    public static class ExtensionMethods
    {
        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
        }
    }
}