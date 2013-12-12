using SolarSystem.Saturn.ViewModel.Helpers;
using System;

namespace SolarSystem.Saturn.ViewModel.Formatters
{
    public static class DateFormatter
    {
        public static string Format(DateTime date)
        {
            const int Second = 1;
            const int Minute = 60 * Second;
            const int Hour = 60 * Minute;
            const int Day = 24 * Hour;
            const int Month = 30 * Day;
            const int Year = 12 * Month;

            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = ts.TotalSeconds;
            double absDelta = Math.Abs(delta);

            int value;
            string unit;

            if (absDelta < 1 * Second)
            {
                value = ts.Seconds;
                unit = FormatResourcesHelper.GetString("SECOND");
            }
            else if (Math.Abs(absDelta) < 1 * Minute)
            {
                value = ts.Seconds;
                unit = FormatResourcesHelper.GetString("SECONDS");
            }
            else if (absDelta < 2 * Minute)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("MINUTE");
            }
            else if (absDelta < 45 * Minute)
            {
                value = ts.Minutes;
                unit = FormatResourcesHelper.GetString("MINUTES");
            }
            else if (absDelta < 90 * Minute)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("HOUR");
            }
            else if (absDelta < 24 * Hour)
            {
                value = ts.Hours;
                unit = FormatResourcesHelper.GetString("HOURS");
            }
            else if (absDelta < 2 * Day)
            {
                value = ts.Days;
                unit = FormatResourcesHelper.GetString("DAY");
            }
            else if (absDelta < 7 * Day)
            {
                value = ts.Days;
                unit = FormatResourcesHelper.GetString("DAYS");
            }
            else if (absDelta < 13 * Day)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("WEEK");
            }
            else if (absDelta < 30 * Day)
            {
                value = (int)Math.Floor((double)ts.Days / 7);
                unit = FormatResourcesHelper.GetString("WEEKS");
            }
            else if (absDelta < 2 * Month)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("MONTH");
            }
            else if (absDelta < 12 * Month)
            {
                value = (int)Math.Floor((double)ts.Days / 30);
                unit = FormatResourcesHelper.GetString("MONTHS");
            }
            else if (absDelta < 2 * Year)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("YEAR");
            }
            else
            {
                value = (int)Math.Floor((double)ts.Days / 365);
                unit = FormatResourcesHelper.GetString("YEARS");
            }

            value = Math.Abs(value);

            return string.Format(delta > 0 ? FormatResourcesHelper.GetString("FORMAT_BEFORE") : FormatResourcesHelper.GetString("FORMAT_AFTER"), value, unit);
        }
    }
}