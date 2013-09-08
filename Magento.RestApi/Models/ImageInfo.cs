using System;
using System.Collections.Generic;
using Magento.RestApi.Core;
using Magento.RestApi.Json;
using Newtonsoft.Json;

namespace Magento.RestApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(ImageInfoConverter))]
    public class ImageInfo : ChangeTracking<ImageInfo>
    {
        /// <summary>
        /// Id of the image
        /// </summary>
        public int id
        {
            get { return GetValue(x => x.id); }
            set { SetValue(x => x.id, value); }
        }

        /// <summary>
        /// A label that will be displayed on the frontend when pointing to the image
        /// </summary>
        public string label
        {
            get { return GetValue(x => x.label); }
            set { SetValue(x => x.label, value); }
        }

        /// <summary>
        /// The Sort Order option. The order in which the images are displayed in the MORE VIEWS section.
        /// </summary>
        public int? position
        {
            get { return GetValue(x => x.position); }
            set { SetValue(x => x.position, value); }
        }

        /// <summary>
        /// Defines whether the image will associate only to one of the three image types.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        public bool? exclude
        {
            get { return GetValue(x => x.exclude); }
            set { SetValue(x => x.exclude, value); }
        }

        /// <summary>
        /// The url of the image
        /// </summary>
        public string url
        {
            get { return GetValue(x => x.url); }
            set { SetValue(x => x.url, value); }
        }

        /// <summary>
        /// Array of image types. Can have the following values: image, small_image, and thumbnail.
        /// </summary>
        public List<ImageType> types
        {
            get { return GetValue(x => x.types); }
            set { SetValue(x => x.types, value); }
        }

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
