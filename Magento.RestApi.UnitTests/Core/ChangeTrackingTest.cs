using Magento.RestApi.Core;

namespace Magento.RestApi.UnitTests.Core
{
    public class ChangeTrackingTest : IChangeTracking
    {
        private bool _hasChanged;

        public void SetHasChanged(bool hasChanged)
        {
            _hasChanged = hasChanged;
        }

        public bool HasChanged()
        {
            return _hasChanged;
        }

        public void StartTracking()
        {
            
        }
    }
}
