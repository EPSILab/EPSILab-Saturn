using EPSILab.SolarSystem.Saturn.WindowsPhone8.Resources;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8
{
    /// <summary>
    /// A helper to let pages reach the ressources files
    /// </summary>
    public class LocalizedStrings
    {
        /// <summary>
        /// Resources
        /// </summary>
        private static readonly AppResources _appResources = new AppResources();

        /// <summary>
        /// Returns the ressources file
        /// </summary>
        public static AppResources AppResources
        {
            get { return _appResources; }
        }
    }
}