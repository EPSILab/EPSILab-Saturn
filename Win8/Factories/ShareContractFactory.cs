using SolarSystem.Saturn.ViewModel.Objects;
using Windows.ApplicationModel.DataTransfer;

namespace SolarSystem.Saturn.Win8.Factories
{
    class ShareContractFactory
    {
        private readonly ShareableWin8Object _shareableObject;

        public ShareContractFactory(ShareableWin8Object shareableObject)
        {
            _shareableObject = shareableObject;
        }

        public void Create(DataRequestedEventArgs args)
        {
            DataPackage dataPackage = args.Request.Data;
            dataPackage.Properties.ApplicationName = "EPSILab";
            dataPackage.Properties.Title = _shareableObject.Title;
            dataPackage.Properties.Description = _shareableObject.Message;
            dataPackage.SetText(_shareableObject.Message);
            dataPackage.SetHtmlFormat(_shareableObject.HTMLText);
        }
    }
}