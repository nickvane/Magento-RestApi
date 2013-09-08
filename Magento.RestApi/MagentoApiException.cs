using System;

namespace Magento.RestApi
{
    [Serializable]
    public class MagentoApiException : Exception
    {
        public MagentoApiException()
        {
            
        }

        public MagentoApiException(string message) : base(message)
        {
            
        }

        public MagentoApiException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
