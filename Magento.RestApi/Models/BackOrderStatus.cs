namespace Magento.RestApi.Models
{
    public enum BackOrderStatus
    {
        NoBackorders = 0,
        AllowQtyBelow0 = 1,
        AllowQtyBelow0AndNotifyCustomer = 2
    }
}
