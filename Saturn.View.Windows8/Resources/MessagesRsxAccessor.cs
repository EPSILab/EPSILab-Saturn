using Windows.ApplicationModel.Resources;

namespace SolarSystem.Saturn.Win8.Resources
{
    /// <summary>
    /// Access to the messages resources file
    /// </summary>
    static class MessagesRsxAccessor
    {
        #region Static constructor

        /// <summary>
        /// Static constructor
        /// </summary>
        static MessagesRsxAccessor()
        {
            const string filename = "Messages";
            ResourceLoader = new ResourceLoader(filename);
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Resource file loaded
        /// </summary>
        private static readonly ResourceLoader ResourceLoader;

        #endregion

        #region Properties

        /// <summary>
        /// Get a string from the ressource file
        /// </summary>
        /// <param name="resource">Resource key</param>
        /// <returns>Resource value</returns>
        public static string GetString(string resource)
        {
            return ResourceLoader.GetString(resource);
        }

        #endregion
    }
}