using Microsoft.Phone.Tasks;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.Helpers.Tasks;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.Resources;
using System;
using System.Windows;
using System.Windows.Input;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8
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
        /// Raised when the user clicks on a Url
        /// </summary>
        /// <param name="sender">Textbox containing the Url</param>
        /// <param name="e">Event arguments</param>
        private void Url_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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