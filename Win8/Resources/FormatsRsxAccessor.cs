using Windows.ApplicationModel.Resources;

namespace SolarSystem.Saturn.Win8.Resources
{
    static class FormatsRsxAccessor
    {
        private static readonly ResourceLoader _resourceLoader;
        
        static FormatsRsxAccessor()
        {
            _resourceLoader = new ResourceLoader("Formats");
        }

        public static string GetString(string resource)
        {
            return _resourceLoader.GetString(resource);
        }
    }
}