using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// A converter used by the Member details page.
    /// Display informations below the passed parameter
    /// </summary>
    public sealed class MemberInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Member && parameter is string)
            {
                Member member = value as Member;

                IDictionary<string, string> informations = new Dictionary<string, string>
                {
                    { "From", string.Format(FormatsRsxAccessor.GetString("Member_From"), member.CityFrom) },
                    { "CampusInfo", string.Format(FormatsRsxAccessor.GetString("Member_CampusInfo"), member.Promotion.GraduationYear, member.Campus.Place) },
                    { "Name", string.Format(FormatsRsxAccessor.GetString("Member_Name"), member.FirstName, member.LastName) }
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