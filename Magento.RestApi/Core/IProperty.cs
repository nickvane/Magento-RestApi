namespace Magento.RestApi.Core
{
    public interface IProperty
    {
        bool HasChanged();
        void SetValueAsInitial();
    }
}
