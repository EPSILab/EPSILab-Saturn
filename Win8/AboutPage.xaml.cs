using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class AboutPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        #region Controls's events handlers

        private async void URL_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var textBox = (FrameworkElement) sender;

            string url = textBox.Tag.ToString();

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var uri = new Uri(url);
                await Launcher.LaunchUriAsync(uri);
            }
        }

        private async void Email_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var textBox = (FrameworkElement) sender;

            string url = "mailto:" + textBox.Tag +
                         "?subject=Demande de support pour l'application EPSILab pour Windows 8";

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var uri = new Uri(url);
                await Launcher.LaunchUriAsync(uri);
            }
        }

        private async void SocialButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (FrameworkElement) sender;

            string tag = button.Tag.ToString();

            if (!string.IsNullOrWhiteSpace(tag) && Uri.IsWellFormedUriString(tag, UriKind.Absolute))
            {
                string url = button.Tag.ToString();
                var uri = new Uri(url, UriKind.Absolute);
                await Launcher.LaunchUriAsync(uri);
            }
        }

        #endregion
    }
}