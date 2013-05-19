using System;

namespace Magento.RestApi.Models
{
    [Serializable]
    public class OrderAddress
    {
        /// <summary>
        /// 
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string middlename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string prefix { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string suffix { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string postcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country_id { get; set; }
    }
}
