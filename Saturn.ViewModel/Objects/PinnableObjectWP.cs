using System;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Objects
{
    /// <summary>
    /// A generic object to pin on Windows Phone start screen
    /// </summary>
    public class PinnableObjectWP : PinnableObject
    {
        public string BackTitle { get; set; }
        public Uri NavigationPage { get; set; }
    }
}