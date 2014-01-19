using System;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Objects
{
    /// <summary>
    /// A generic object to share informations on Windows Phone
    /// </summary>
    public class ShareableObject
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public Uri Uri { get; set; }
    }
}
