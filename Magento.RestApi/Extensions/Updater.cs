namespace Magento.RestApi.Extensions
{
    public class Updater
    {
        public static bool UpdateChanges<T>(T original, T modified, T changed)
        {
            var hasChanges = false;
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.PropertyType.IsAssignableFrom(typeof(string)) ||
                    propertyInfo.PropertyType.IsAssignableFrom(typeof(int)) ||
                    propertyInfo.PropertyType.IsAssignableFrom(typeof(int?)) ||
                    propertyInfo.PropertyType.IsAssignableFrom(typeof(double?)) ||
                    propertyInfo.PropertyType.IsAssignableFrom(typeof(bool?)))
                {
                    var value1 = propertyInfo.GetValue(modified) as string;
                    var value2 = propertyInfo.GetValue(original) as string;
                    if (!Equals(value1, value2))
                    {
                        propertyInfo.SetValue(changed, value1);
                        hasChanges = true;
                    }
                }
            }
            return hasChanges;
        }
    }
}
