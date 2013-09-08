using System;
using Magento.RestApi.Core;
using Magento.RestApi.Json;
using Newtonsoft.Json;

namespace Magento.RestApi.Models
{
    /// <summary>
    /// 
    /// </summary>
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
        /// Image file content.
        /// </summary>
        [JsonConverter(typeof(Base64Converter))]
        public byte[] file_content
        {
            get { return GetValue(x => x.file_content); }
            set { SetValue(x => x.file_content, value); }
        }

        /// <summary>
        /// Image file name.
        /// If the file_name parameter is not defined, the original file name is set for the image. The first created image will be called "image", the second created image will be called "image_2", etc.
        /// </summary>
        public string file_name
        {
            get { return GetValue(x => x.file_name); }
            set { SetValue(x => x.file_name, value); }
        }
    }
}
