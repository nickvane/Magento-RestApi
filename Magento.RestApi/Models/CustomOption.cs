using Magento.RestApi.Core;
using Newtonsoft.Json;
using System;

namespace Magento.RestApi.Models
{
    [JsonConverter(typeof(CustomOptionConverter))]
    [Serializable]
    public class CustomOption : ChangeTracking<CustomOption>
    {
        public CustomOption()
        {
            this.StartTracking();
        }

        /// <summary>
        /// 
        /// </summary>
        public string label
        {
            get { return GetValue(x => x.label); }
            set { SetValue(x => x.label, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string value
        {
            get { return GetValue(x => x.value); }
            set { SetValue(x => x.value, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string print_value
        {
            get { return GetValue(x => x.print_value); }
            set { SetValue(x => x.print_value, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string option_type
        {
            get { return GetValue(x => x.option_type); }
            set { SetValue(x => x.option_type, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int option_id
        {
            get { return GetValue(x => x.option_id); }
            set { SetValue(x => x.option_id, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string option_value
        {
            get { return GetValue(x => x.option_value); }
            set { SetValue(x => x.option_value, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool custom_view
        {
            get { return GetValue(x => x.custom_view); }
            set { SetValue(x => x.custom_view, value); }
        }
    }
}