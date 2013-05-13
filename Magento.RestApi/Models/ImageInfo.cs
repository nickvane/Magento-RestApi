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
    }
}
