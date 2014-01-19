using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using Windows.ApplicationModel.DataTransfer;

namespace EPSILab.SolarSystem.Saturn.Windows8.Factories
{
    /// <summary>
    /// Build share contracts when the user wants to share an element in another app
    /// </summary>
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
        /// Prepare the informations which are going to be shared
        /// </summary>
        /// <param name="args">Share event arguments</param>
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