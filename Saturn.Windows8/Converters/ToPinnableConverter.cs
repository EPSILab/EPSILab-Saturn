using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// A converter which transforms a Model entity to a generic pinnable object
    /// </summary>
    public sealed class ToPinnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string applicationName = ResourcesRsxAccessor.GetString("AppName");

            PinnableObject pinnableObject = null;

            if (value is VisualGenericItem)
            {
                VisualGenericItem item = value as VisualGenericItem;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-{1}-{2}", applicationName, item.Type, item.Id),
                    Title = item.Title,
                    ImageUrl = item.ImageUrl,
                    Content = item.Title
                };
            }
            else if (value is News)
            {
                News news = value as News;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-News-{1}", applicationName, news.Id),
                    Title = news.Title,
                    ImageUrl = news.ImageUrl,
                    Content = news.Title
                };
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-Conference-{1}", applicationName, conference.Id),
                    Title = conference.Name,
                    ImageUrl = conference.ImageUrl,
                    Content = conference.Name
                };
            }
            else if (value is Show)
            {
                Show salon = value as Show;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-Show-{1}", applicationName, salon.Id),
                    Title = salon.Name,
                    ImageUrl = salon.ImageUrl,
                    Content = salon.Name
                };
            }

            return pinnableObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}