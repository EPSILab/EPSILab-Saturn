using Windows.ApplicationModel.Resources;

namespace SolarSystem.Saturn.Win8.Resources
{
    static class MessagesRsxAccessor
    {
        private static readonly ResourceLoader _resourceLoader;

        static MessagesRsxAccessor()
        {
            _resourceLoader = new ResourceLoader("Messages");
        }

        public static string GetString(string resource)
        {
            return _resourceLoader.GetString(resource);
        }
    }
}