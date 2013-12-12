using SolarSystem.Saturn.WP8.Resources;

namespace SolarSystem.Saturn.WP8
{
    public class LocalizedStrings
    {
        private static readonly AppResources _appResources = new AppResources();

        public AppResources AppResources
        {
            get { return _appResources; }
        }
    }
}