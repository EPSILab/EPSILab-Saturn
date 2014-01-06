using Microsoft.Phone.Tasks;
using SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks;
using SolarSystem.Saturn.View.WindowsPhone.Resources;
using System;
using System.Windows;
using System.Windows.Input;

namespace SolarSystem.Saturn.View.WindowsPhone
{
    /// <summary>
    /// About page
    /// </summary>
    public partial class AboutPage
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

        #region Controls's events

        /// <summary>
        /// Raised when the user clicks on a URL
        /// </summary>
        /// <param name="sender">Textbox containing the URL</param>
        /// <param name="e">Event arguments</param>
        private void URL_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement textBox = (FrameworkElement)sender;
            string url = textBox.Tag.ToString();

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                WebBrowserTaskHelper.OpenBrowser(new Uri(url));
            }
        }

        /// <summary>
        /// Raised when the user clicks on an email address
        /// </summary>
        /// <param name="sender">Textbox containing the email adresse</param>
        /// <param name="e">Event arguments</param>
        private void Email_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EmailComposeTask task = new EmailComposeTask
                {
                    To = AppResources.LBL_EMAIL_SUPPORT,
                    Subject = AppResources.MSG_SUPPORT
                };

            task.Show();
        }

        #endregion
    }
}