using System;
using System.Collections.Generic;

namespace Magento.RestApi
{
    public class MagentoApiResponse<T>
    {
        public T Result { get; set; }
        public List<MagentoError> Errors { get; set; }
        public Uri RequestUrl { get; set; }
        public string ErrorString { get; set; }
        public string RequestContent { get; set; }

        public bool HasErrors
        {
            get { return Errors != null && Errors.Count > 0; }
        }
    }
}
