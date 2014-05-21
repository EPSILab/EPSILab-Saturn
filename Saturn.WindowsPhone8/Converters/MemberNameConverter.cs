using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.Converters
{
    public class MemberNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Member)
            {
                Member member = value as Member;
                return string.Format(CultureInfo.CurrentUICulture, "{0} {1}", member.FirstName, member.LastName);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}