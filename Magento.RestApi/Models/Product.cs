using System;
using System.Collections.Generic;
using Magento.RestApi.Core;
using Magento.RestApi.Json;
using Newtonsoft.Json;

namespace Magento.RestApi.Models
{
    /// <summary>
    /// A product
    /// </summary>
    [JsonConverter(typeof(ProductConverter))]
    [Serializable]
    public class Product : ChangeTracking<Product>
    {
        /// <summary>
        /// 
        /// </summary>
        public Product()
        {
            StartTracking();
        }

        public override void StartTracking()
        {
            Attributes = new Dictionary<string, string>();
            base.StartTracking();
            if (group_price != null)
            {
                foreach (var groupPrice in group_price)
                {
                    groupPrice.StartTracking();
                }
            }
            if (tier_price != null)
            {
                foreach (var tierPrice in tier_price)
                {
                    tierPrice.StartTracking();
                }
            }
            if (stock_data != null) stock_data.StartTracking();
        }

        /// <summary>
        /// Id of the product
        /// </summary>
        public int entity_id
        {
            get { return GetValue(x => x.entity_id); }
            set { SetValue(x => x.entity_id, value); }
        }

        /// <summary>
        /// Product type. Can have the "simple" value
        /// </summary>
        /// <remarks>required</remarks>
        public string type_id
        {
            get { return GetValue(x => x.type_id); }
            set { SetValue(x => x.type_id, value); }
        }
        /// <summary>
        /// Attribute set for the product
        /// </summary>
        /// <remarks>required</remarks>
        public int attribute_set_id
        {
            get { return GetValue(x => x.attribute_set_id); }
            set { SetValue(x => x.attribute_set_id, value); }
        }
        /// <summary>
        /// Product SKU	
        /// </summary>
        /// <remarks>required</remarks>
        public string sku
        {
            get { return GetValue(x => x.sku); }
            set { SetValue(x => x.sku, value); }
        }
        /// <summary>
        /// Product name
        /// </summary>
        /// <remarks>required</remarks>
        public string name
        {
            get { return GetValue(x => x.name); }
            set { SetValue(x => x.name, value); }
        }
        /// <summary>
        /// Product meta title
        /// </summary>
        /// <remarks>optional</remarks>
        public string meta_title
        {
            get { return GetValue(x => x.meta_title); }
            set { SetValue(x => x.meta_title, value); }
        }
        /// <summary>
        /// Product meta description
        /// </summary>
        /// <remarks>optional</remarks>
        public string meta_description
        {
            get { return GetValue(x => x.meta_description); }
            set { SetValue(x => x.meta_description, value); }
        }
        /// <summary>
        /// A friendly URL path for the product
        /// </summary>
        /// <remarks>optional</remarks>
        public string url_key
        {
            get { return GetValue(x => x.url_key); }
            set { SetValue(x => x.url_key, value); }
        }
        /// <summary>
        /// Custom design applied for the product page
        /// </summary>
        /// <remarks>optional</remarks>
        public string custom_design
        {
            get { return GetValue(x => x.custom_design); }
            set { SetValue(x => x.custom_design, value); }
        }
        /// <summary>
        /// Page template that can be applied to the product page
        /// </summary>
        /// <remarks>optional</remarks>
        public string page_layout
        {
            get { return GetValue(x => x.page_layout); }
            set { SetValue(x => x.page_layout, value); }
        }
        /// <summary>
        /// Defines how the custom options for the product will be displayed. Can have the following values: Block after Info Column or Product Info Column
        /// </summary>
        /// <remarks>optional</remarks>
        public string options_container
        {
            get { return GetValue(x => x.options_container); }
            set { SetValue(x => x.options_container, value); }
        }
        /// <summary>
        /// Product country of manufacture. This is the 2 letter ISO code of the country.
        /// </summary>
        /// <remarks>optional</remarks>
        public string country_of_manufacture
        {
            get { return GetValue(x => x.country_of_manufacture); }
            set { SetValue(x => x.country_of_manufacture, value); }
        }
        /// <summary>
        /// The Apply MAP option. Defines whether the price in the catalog in the frontend is substituted with a Click for price link
        /// </summary>
        /// <remarks>optional</remarks>
        public ManufacturerPriceEnablement? msrp_enabled
        {
            get { return GetValue(x => x.msrp_enabled); }
            set { SetValue(x => x.msrp_enabled, value); }
        }
        /// <summary>
        /// Defines how the price will be displayed in the frontend. Can have the following values: In Cart, Before Order Confirmation, and On Gesture
        /// </summary>
        /// <remarks>optional</remarks>
        public PriceTypeDisplay? msrp_display_actual_price_type
        {
            get { return GetValue(x => x.msrp_display_actual_price_type); }
            set { SetValue(x => x.msrp_display_actual_price_type, value); }
        }
        /// <summary>
        /// Defines whether the gift message is available for the product
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? gift_message_available
        {
            get { return GetValue(x => x.gift_message_available); }
            set { SetValue(x => x.gift_message_available, value); }
        }
        /// <summary>
        /// Product price
        /// </summary>
        /// <remarks>required</remarks>
        [JsonConverter(typeof(DoubleConverter))]
        public double price
        {
            get { return GetValue(x => x.price); }
            set { SetValue(x => x.price, value); }
        }
        /// <summary>
        /// Product special price
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DoubleConverter))]
        public double? special_price
        {
            get { return GetValue(x => x.special_price); }
            set { SetValue(x => x.special_price, value); }
        }
        /// <summary>
        /// Product weight
        /// </summary>
        /// <remarks>required</remarks>
        [JsonConverter(typeof(DoubleConverter))]
        public double weight
        {
            get { return GetValue(x => x.weight); }
            set { SetValue(x => x.weight, value); }
        }
        /// <summary>
        /// The Manufacturer's Suggested Retail Price option. The price that a manufacturer suggests to sell the product at
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DoubleConverter))]
        public double? msrp
        {
            get { return GetValue(x => x.msrp); }
            set { SetValue(x => x.msrp, value); }
        }
        /// <summary>
        /// Product status. Can have the following values: 1- Enabled, 2 - Disabled.
        /// </summary>
        /// <remarks>required</remarks>
        [JsonConverter(typeof(EnumConverter))]
        public ProductStatus status
        {
            get { return GetValue(x => x.status); }
            set { SetValue(x => x.status, value); }
        }
        /// <summary>
        /// Product visibility. Can have the following values: 1 - Not Visible Individually, 2 - Catalog, 3 - Search, 4 - Catalog, Search.
        /// </summary>
        /// <remarks>required</remarks>
        [JsonConverter(typeof(EnumConverter))]
        public ProductVisibility visibility
        {
            get { return GetValue(x => x.visibility); }
            set { SetValue(x => x.visibility, value); }
        }
        /// <summary>
        /// Defines whether the product can be purchased with the help of the Google Checkout payment service. Can have the following values: Yes and No
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(BoolConverter))]
        public bool? enable_googlecheckout
        {
            get { return GetValue(x => x.enable_googlecheckout); }
            set { SetValue(x => x.enable_googlecheckout, value); }
        }
        /// <summary>
        /// Product tax class. Can have the following values: 0 - None, 2 - taxable Goods, 4 - Shipping, etc., depending on created tax classes.
        /// </summary>
        /// <remarks>required</remarks>
        public int? tax_class_id
        {
            get { return GetValue(x => x.tax_class_id); }
            set { SetValue(x => x.tax_class_id, value); }
        }
        /// <summary>
        /// Product description.
        /// </summary>
        /// <remarks>required</remarks>
        public string description
        {
            get { return GetValue(x => x.description); }
            set { SetValue(x => x.description, value); }
        }
        /// <summary>
        /// Product short description.
        /// </summary>
        /// <remarks>optional</remarks>
        public string short_description
        {
            get { return GetValue(x => x.short_description); }
            set { SetValue(x => x.short_description, value); }
        }
        /// <summary>
        /// Product meta keywords
        /// </summary>
        /// <remarks>optional</remarks>
        public string meta_keyword
        {
            get { return GetValue(x => x.meta_keyword); }
            set { SetValue(x => x.meta_keyword, value); }
        }
        /// <summary>
        /// An XML block to alter the page layout
        /// </summary>
        /// <remarks>optional</remarks>
        public string custom_layout_update
        {
            get { return GetValue(x => x.custom_layout_update); }
            set { SetValue(x => x.custom_layout_update, value); }
        }
        /// <summary>
        /// Date starting from which the special price will be applied to the product
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? special_from_date
        {
            get { return GetValue(x => x.special_from_date); }
            set { SetValue(x => x.special_from_date, value); }
        }
        /// <summary>
        /// Date till which the special price will be applied to the product
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? special_to_date
        {
            get { return GetValue(x => x.special_to_date); }
            set { SetValue(x => x.special_to_date, value); }
        }
        /// <summary>
        /// Date starting from which the product is promoted as a new product
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? news_from_date
        {
            get { return GetValue(x => x.news_from_date); }
            set { SetValue(x => x.news_from_date, value); }
        }
        /// <summary>
        /// Date till which the product is promoted as a new product
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? news_to_date
        {
            get { return GetValue(x => x.news_to_date); }
            set { SetValue(x => x.news_to_date, value); }
        }
        /// <summary>
        /// Date starting from which the custom design will be applied to the product page
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? custom_design_from
        {
            get { return GetValue(x => x.custom_design_from); }
            set { SetValue(x => x.custom_design_from, value); }
        }
        /// <summary>
        /// Date till which the custom design will be applied to the product page
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? custom_design_to
        {
            get { return GetValue(x => x.custom_design_to); }
            set { SetValue(x => x.custom_design_to, value); }
        }
        /// <summary>
        /// Product group price
        /// </summary>
        /// <remarks>optional</remarks>
        public List<GroupPrice> group_price
        {
            get { return GetValue(x => x.group_price); }
            set { SetValue(x => x.group_price, value); }
        }
        /// <summary>
        /// Product tier price
        /// </summary>
        /// <remarks>optional</remarks>
        public List<TierPrice> tier_price
        {
            get { return GetValue(x => x.tier_price); }
            set { SetValue(x => x.tier_price, value); }
        }
        /// <summary>
        /// Product inventory data
        /// </summary>
        /// <remarks>optional</remarks>
        [JsonConverter(typeof(StockDataConverter))]
        public StockData stock_data
        {
            get { return GetValue(x => x.stock_data); }
            set { SetValue(x => x.stock_data, value); }
        }
        /// <summary>
        /// A dictionary of all specified attributes
        /// </summary>
        public Dictionary<string, string> Attributes
        {
            get { return GetValue(x => x.Attributes); }
            set { SetValue(x => x.Attributes, value); }
        }

    }
}