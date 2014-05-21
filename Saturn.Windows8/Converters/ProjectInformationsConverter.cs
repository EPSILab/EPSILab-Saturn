using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// Converter used for the Project details page.
    /// Display show informations in terms of the parameter
    /// </summary>
    public sealed class ProjectInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Project && parameter is string)
            {
                Project projet = value as Project;

                IDictionary<string, string> informations = new Dictionary<string, string>
                {
                    { "Progress", string.Format(FormatsRsxAccessor.GetString("Project_Progress"), projet.Progression) },
                    { "Location", string.Format(FormatsRsxAccessor.GetString("Project_Location"), projet.Campus.Place) }
                };

                return informations[parameter.ToString()];
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}