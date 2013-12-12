using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SolarSystem.Saturn.Win8.Helpers
{
    internal class WebViewHelper
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html", typeof (string), typeof (WebViewHelper), new PropertyMetadata(string.Empty, OnHtmlChanged));

        public static string GetHtml(DependencyObject dependencyObject)
        {
            return (string) dependencyObject.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = d as WebView;
            if (browser == null)
                return;

            string html = e.NewValue == null ? string.Empty : e.NewValue.ToString();
            html = WrapHtml(html, browser.ActualWidth);
            browser.NavigateToString(html);
        }

        public static string WrapHtml(string htmlSubString, double viewportWidth)
        {
            var html = new StringBuilder();
            html.Append("<html>");
            html.Append(HtmlHeader(viewportWidth));
            html.Append("<body onload=\"attachNotifications()\">");
            html.Append(htmlSubString);
            html.Append("</body>");
            html.Append("</html>");
            return html.ToString();
        }

        public static string HtmlHeader(double viewportWidth)
        {
            var head = new StringBuilder();

            head.Append("<head>");
            head.Append(string.Format(
                "<meta name=\"viewport\" value=\"width={0}\" user-scalable=\"no\" />",
                viewportWidth));
            head.Append("<style>");
            head.Append("html { -ms-text-size-adjust:200% }");
            head.Append(string.Format(
                "body {{background:{0};color:{1};font-family:'Segoe UI';font-size:14pt;margin:0;padding:0 }}",
                "#F8F8F8",
                "#000000"));
            head.Append("a {{color:#5B9AFF}}");
            head.Append("</style>");
            head.Append("<script type=\"text/javascript\">function attachNotifications() { for (var i = 0; i < document.links.length; i++) { document.links[i].onclick = function() { window.external.notify('LaunchLink:' + this.href); return false; } } }</script>");
            head.Append("</head>");


            return head.ToString();
        }
    }
}