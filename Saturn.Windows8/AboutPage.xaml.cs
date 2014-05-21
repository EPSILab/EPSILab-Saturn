using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// About page
    /// </summary>
    public sealed partial class AboutPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Controls Events

        /// <summary>
        /// Raised when the user clicks on a hyperlink
        /// </summary>
        /// <param name="sender">Textbox pointed</param>
        /// <param name="e">Event arguments</param>
        private async void Url_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var textBox = (FrameworkElement)sender;

            string url = textBox.Tag.ToString();

            // Check if Url is valid
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var uri = new Uri(url);
                await Launcher.LaunchUriAsync(uri);
            }
        }

        /// <summary>
        /// Raised when the user clicks on a email address
        /// </summary>
        /// <param name="sender">Textbox pointed</param>
        /// <param name="e">Event arguments</param>
        private async void Email_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var textBox = (FrameworkElement)sender;

            string url = string.Format("mailto:{0}?subject={1}", textBox.Tag, MessagesRsxAccessor.GetString("EmailSubject"));

            // Check if Url is valid
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var uri = new Uri(url);
                await Launcher.LaunchUriAsync(uri);
            }
        }

        #endregion
    }
}