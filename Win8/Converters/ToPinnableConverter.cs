﻿using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class ToPinnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ResourceLoader resourceLoader = new ResourceLoader();
            string applicationName = resourceLoader.GetString("APPLICATION_NAME");

            PinnableObject pinnableObject = null;

            if (value is VisualGenericItem)
            {
                VisualGenericItem item = value as VisualGenericItem;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-{1}-{2}", applicationName, item.Type, item.Id),
                    Title = item.Title,
                    Image = item.Image,
                    Content = item.Title
                };
            }
            else if (value is News)
            {
                News news = value as News;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-News-{1}", applicationName, news.Code_News),
                    Title = news.Titre,
                    Image = news.Image,
                    Content = news.Titre
                };
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-Conference-{1}", applicationName, conference.Code_Conference),
                    Title = conference.Nom,
                    Image = conference.Image,
                    Content = conference.Nom
                };
            }
            else if (value is Salon)
            {
                Salon salon = value as Salon;

                pinnableObject = new PinnableObject
                {
                    Id = string.Format("{0}-Salon-{1}", applicationName, salon.Code_Salon),
                    Title = salon.Nom,
                    Image = salon.Image,
                    Content = salon.Nom
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