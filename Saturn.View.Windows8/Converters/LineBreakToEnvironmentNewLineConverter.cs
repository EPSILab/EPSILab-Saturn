using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class LineBreakToEnvironmentNewLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                return string.Format(value.ToString(), Environment.NewLine);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
