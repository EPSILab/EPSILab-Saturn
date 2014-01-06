using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks;
using SolarSystem.Saturn.View.WindowsPhone.Resources;

namespace SolarSystem.Saturn.View.WindowsPhone
{
    public partial class AProposPage
    {
        public AProposPage()
        {
            InitializeComponent();
        }

        private void URL_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement textBox = (FrameworkElement)sender;
            string url = textBox.Tag.ToString();

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                WebBrowserTaskHelper.OpenBrowser(new Uri(url));
            }
        }

        private void Email_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EmailComposeTask task = new EmailComposeTask
                {
                    To = AppResources.LBL_EMAIL_SUPPORT,
                    Subject = "Demande de support pour l'application EPSILab pour Windows Phone"
                };

            task.Show();
        }
    }
}