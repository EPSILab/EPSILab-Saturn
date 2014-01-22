using Windows.ApplicationModel.Resources;

namespace EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Resources
{
    static class ResourcesAccessor
    {
        private static readonly ResourceLoader Loader;

        static ResourcesAccessor()
        {
            const string name = "EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory/Resources/";
            Loader = new ResourceLoader(name);
        }

        public static string GetString(string resource)
        {
            return Loader.GetString(resource);
        }
    }
}