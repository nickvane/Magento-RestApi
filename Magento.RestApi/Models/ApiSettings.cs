namespace Magento.RestApi.Models
{
    /// <summary>
    /// structure for magento api Settings
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// key acquired from oAuth flow
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// secret acquired from oAuth flow
        /// </summary>
        public string AccessSecret { get; set; }
        /// <summary>
        /// key from user in Magento
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// secret from user in Magento
        /// </summary>
        public string ConsumerSecret { get; set; }
        /// <summary>
        /// Magento user password for admin authentication
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Magento user name for admin authentication
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Magento base URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// item id used for healthcheck
        /// </summary>
        public int HealthcheckItemId { get; set; }
        
    }
}
