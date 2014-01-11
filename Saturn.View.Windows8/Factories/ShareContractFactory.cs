using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.Win8.Resources;
using Windows.ApplicationModel.DataTransfer;

namespace SolarSystem.Saturn.Win8.Factories
{
    class ShareContractFactory
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shareableObject">Element being shared</param>
        public ShareContractFactory(ShareableWin8Object shareableObject)
        {
            _shareableObject = shareableObject;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Element being shared
        /// </summary>
        private readonly ShareableWin8Object _shareableObject;

        #endregion

        #region Methods

        /// <summary>
        /// Prepare the share informations and display the UI
        /// </summary>
        /// <param name="args"></param>
        public void DisplayShareUI(DataRequestedEventArgs args)
        {
            DataPackage dataPackage = args.Request.Data;

            dataPackage.Properties.ApplicationName = ResourcesRsxAccessor.GetString("AppName");
            dataPackage.Properties.Title = _shareableObject.Title;
            dataPackage.Properties.Description = _shareableObject.Message;

            dataPackage.SetText(_shareableObject.Message);
            dataPackage.SetHtmlFormat(_shareableObject.HTMLText);
        }

        #endregion
    }
}