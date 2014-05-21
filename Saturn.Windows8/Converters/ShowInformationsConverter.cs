using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// Converter used for the Show details page.
    /// Display show informations in terms of the parameter
    /// </summary>
    public sealed class ShowInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Show && parameter is string)
            {
                Show salon = value as Show;

                IDictionary<string, string> informations = new Dictionary<string, string>
                {
                    { "Start_DateTime", string.Format(FormatsRsxAccessor.GetString("Show_Start_DateTime"), salon.Start_DateTime)},
                    { "EndDate", string.Format(FormatsRsxAccessor.GetString("Show_EndDate"), salon.End_DateTime)},
                    { "Location", string.Format(FormatsRsxAccessor.GetString("Show_Location"), salon.Place, salon.Place)},
                };

                return informations[parameter.ToString()];
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}