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
    [JsonConverter(typeof(CustomerAddressConverter))]
    [Serializable]
    public class CustomerAddress : ChangeTracking<CustomerAddress>
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerAddress()
        {
            this.StartTracking();
        }

        /// <summary>
        /// Id of the customeraddress
        /// </summary>
        public int entity_id
        {
            get { return GetValue(x => x.entity_id); }
            set { SetValue(x => x.entity_id, value); }
        }
        /// <summary>
        /// Customer first name
        /// </summary>
        public string firstname
        {
            get { return GetValue(x => x.firstname); }
            set { SetValue(x => x.firstname, value); }
        }
        /// <summary>
        /// Customer last name
        /// </summary>
        public string lastname
        {
            get { return GetValue(x => x.lastname); }
            set { SetValue(x => x.lastname, value); }
        }
        /// <summary>
        /// Name of the city
        /// </summary>
        public string city
        {
            get { return GetValue(x => x.city); }
            set { SetValue(x => x.city, value); }
        }
        /// <summary>
        /// Region name or code
        /// </summary>
        public string region
        {
            get { return GetValue(x => x.region); }
            set { SetValue(x => x.region, value); }
        }
        /// <summary>
        /// Customer ZIP/postal code
        /// </summary>
        public string postcode
        {
            get { return GetValue(x => x.postcode); }
            set { SetValue(x => x.postcode, value); }
        }
        /// <summary>
        /// Name of the country
        /// </summary>
        public string country_id
        {
            get { return GetValue(x => x.country_id); }
            set { SetValue(x => x.country_id, value); }
        }
        /// <summary>
        /// Customer phone number
        /// </summary>
        public string telephone
        {
            get { return GetValue(x => x.telephone); }
            set { SetValue(x => x.telephone, value); }
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
        public string middlename
        {
            get { return GetValue(x => x.middlename); }
            set { SetValue(x => x.middlename, value); }
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
        public string company
        {
            get { return GetValue(x => x.company); }
            set { SetValue(x => x.company, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fax
        {
            get { return GetValue(x => x.fax); }
            set { SetValue(x => x.fax, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vat_id
        {
            get { return GetValue(x => x.vat_id); }
            set { SetValue(x => x.vat_id, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? is_default_billing
        {
            get { return GetValue(x => x.is_default_billing); }
            set { SetValue(x => x.is_default_billing, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? is_default_shipping
        {
            get { return GetValue(x => x.is_default_shipping); }
            set { SetValue(x => x.is_default_shipping, value); }
        }
        /// <summary>
        /// Customer street address. There can be more than one street address.
        /// </summary>
        public List<string> street
        {
            get { return GetValue(x => x.street); }
            set { SetValue(x => x.street, value); }
        }
    }
}
