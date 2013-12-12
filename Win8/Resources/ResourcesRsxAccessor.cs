using Windows.ApplicationModel.Resources;

namespace SolarSystem.Saturn.Win8.Resources
{
    static class ResourcesRsxAccessor
    {
        private static readonly ResourceLoader _resourceLoader;

        static ResourcesRsxAccessor()
        {
            _resourceLoader = new ResourceLoader("Resources");
        }

        public static string GetString(string resource)
        {
            return _resourceLoader.GetString(resource);
        }
    }
}