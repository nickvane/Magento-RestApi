using System;
using Magento.RestApi.Core;
using Magento.RestApi.Json;
using Newtonsoft.Json;

namespace Magento.RestApi.Models
{
    [JsonConverter(typeof(OrderAddressConverter))]
    [Serializable]
    public class OrderAddress : ChangeTracking<OrderAddress>
    {
        /// <summary>
        /// 
        /// </summary>
        public string firstname
        {
            get { return GetValue(x => x.firstname); }
            set { SetValue(x => x.firstname, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastname
        {
            get { return GetValue(x => x.lastname); }
            set { SetValue(x => x.lastname, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string middlename
        {
            get { return GetValue(x => x.middlename); }
            set { SetValue(x => x.middlename, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string prefix
        {
            get { return GetValue(x => x.prefix); }
            set { SetValue(x => x.prefix, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string suffix
        {
            get { return GetValue(x => x.suffix); }
            set { SetValue(x => x.suffix, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string email
        {
            get { return GetValue(x => x.email); }
            set { SetValue(x => x.email, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string company
        {
            get { return GetValue(x => x.company); }
            set { SetValue(x => x.company, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postcode
        {
            get { return GetValue(x => x.postcode); }
            set { SetValue(x => x.postcode, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string city
        {
            get { return GetValue(x => x.city); }
            set { SetValue(x => x.city, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string street
        {
            get { return GetValue(x => x.street); }
            set { SetValue(x => x.street, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string telephone
        {
            get { return GetValue(x => x.telephone); }
            set { SetValue(x => x.telephone, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string address_type
        {
            get { return GetValue(x => x.address_type); }
            set { SetValue(x => x.address_type, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string region
        {
            get { return GetValue(x => x.region); }
            set { SetValue(x => x.region, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string country_id
        {
            get { return GetValue(x => x.country_id); }
            set { SetValue(x => x.country_id, value); }
        }
    }
}
