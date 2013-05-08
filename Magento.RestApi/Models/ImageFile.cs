using System;
using Magento.RestApi.Core;

namespace Magento.RestApi.Models
{
    [Serializable]
    public class ImageFile : ChangeTracking<ImageFile>
    {
        /// <summary>
        /// File mime type. Can have the following values: image/jpeg, image/png, etc.
        /// </summary>
        public string file_mime_type
        {
            get { return GetValue(x => x.file_mime_type); }
            set { SetValue(x => x.file_mime_type, value); }
        }

        /// <summary>
        /// Image file content (base_64 encoded).
        /// </summary>
        public byte[] file_content
        {
            get { return GetValue(x => x.file_content); }
            set { SetValue(x => x.file_content, value); }
        }

        /// <summary>
        /// Image file name.
        /// </summary>
        public string file_name
        {
            get { return GetValue(x => x.file_name); }
            set { SetValue(x => x.file_name, value); }
        }
    }
}
