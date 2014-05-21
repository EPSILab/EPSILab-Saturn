using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// A converter which transforms a Model entity to a shareable generic object
    /// </summary>
    public sealed class ToShareableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ShareableWin8Object shareableObject = null;
            string websiteFormat = FormatsRsxAccessor.GetString("Website");

            if (value is News)
            {
                News news = value as News;

                shareableObject = new ShareableWin8Object
                {
                    Title = news.Title,
                    Message = string.Format(FormatsRsxAccessor.GetString("News"), news.Title, news.DateTime, news.Member.FirstName, news.Member.LastName),
                    Uri = new Uri(string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_News"), news.Id, news.Url))
                };

                shareableObject.HTMLText = string.Format(FormatsRsxAccessor.GetString("News_HTML"),
                                                         news.Member.FirstName, news.Member.LastName, news.DateTime,
                                                         news.ImageUrl, news.Text, shareableObject.Uri);
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                shareableObject = new ShareableWin8Object
                {
                    Title = conference.Name,
                    Message = string.Format(FormatsRsxAccessor.GetString("Conference"), conference.Name, conference.Start_DateTime, conference.End_DateTime, conference.Place),
                    Uri = new Uri(string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Conferences"), conference.Id, conference.Url))
                };

                shareableObject.HTMLText = string.Format(FormatsRsxAccessor.GetString("Conference_HTML"),
                                                         conference.Start_DateTime, conference.End_DateTime,
                                                         conference.Place,
                                                         conference.Campus.Place, conference.ImageUrl,
                                                         conference.Description, shareableObject.Uri);
            }
            else if (value is Show)
            {
                Show salon = value as Show;

                shareableObject = new ShareableWin8Object
                {
                    Title = salon.Name,
                    Message = string.Format(FormatsRsxAccessor.GetString("Show"), salon.Name, salon.Start_DateTime, salon.End_DateTime, salon.Place),
                    Uri = new Uri(string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Shows"), salon.Id, salon.Url))
                };

                shareableObject.HTMLText = string.Format(FormatsRsxAccessor.GetString("Show_HTML"),
                                                         salon.Start_DateTime, salon.End_DateTime, salon.Place,
                                                         salon.Place,
                                                         salon.ImageUrl, salon.Description, shareableObject.Uri);
            }

            return shareableObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}