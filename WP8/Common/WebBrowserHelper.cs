using System.Windows;
using Microsoft.Phone.Controls;

namespace SolarSystem.Saturn.WP8.Common
{
    public class WebBrowserHelper : DependencyObject
    {
        public static readonly DependencyProperty Html =
            DependencyProperty.RegisterAttached(
                "Html",
                typeof (string),
                typeof (WebBrowserHelper),
                new PropertyMetadata(OnHtmlPropertyChanged));

        private static void OnHtmlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                string html = "<style>body { background-color: #EEE; font-size: 14px; }</style>";

                if (e.NewValue != null)
                {
                    html += e.NewValue.ToString();
                }

                var wb = (WebBrowser) d;
                wb.NavigateToString(html);
            }
        }

        public static void SetHtml(DependencyObject obj, string html)
        {
            obj.SetValue(Html, html);
        }

        public static string GetHtml(DependencyObject obj)
        {
            return (string) obj.GetValue(Html);
        }
    }
}