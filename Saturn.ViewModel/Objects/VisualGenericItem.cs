namespace SolarSystem.Saturn.ViewModel.Objects
{
    /// <summary>
    /// A cross generic item used to show informations on the main page
    /// </summary>
    public class VisualGenericItem
    {
        /// <summary>
        /// Base type name of the origin object
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Image { get; set; }
    }
}