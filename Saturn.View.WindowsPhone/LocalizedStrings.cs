using SolarSystem.Saturn.View.WindowsPhone.Resources;

namespace SolarSystem.Saturn.View.WindowsPhone
{
    /// <summary>
    /// A helper to let pages reach the ressources files
    /// </summary>
    public class LocalizedStrings
    {
        private static readonly AppResources _appResources = new AppResources();

        public static AppResources AppResources
        {
            get { return _appResources; }
        }
    }
}