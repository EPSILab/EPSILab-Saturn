using System;
using EPSILab.SolarSystem.Saturn.ViewModel.Helpers;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Formatters
{
    /// <summary>
    /// A formatter for date. Display "xxx ago" according to actual date time
    /// </summary>
    public static class DateFormatter
    {
        /// <summary>
        /// Format the date in a more confortable format
        /// </summary>
        /// <param name="date">The date to format</param>
        /// <returns>A string according to the date</returns>
        public static string Format(DateTime date)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;
            const int year = 12 * month;

            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = ts.TotalSeconds;
            double absDelta = Math.Abs(delta);

            int value;
            string unit;

            if (absDelta < 1 * second)
            {
                value = ts.Seconds;
                unit = FormatResourcesHelper.GetString("SECOND");
            }
            else if (Math.Abs(absDelta) < 1 * minute)
            {
                value = ts.Seconds;
                unit = FormatResourcesHelper.GetString("SECONDS");
            }
            else if (absDelta < 2 * minute)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("MINUTE");
            }
            else if (absDelta < 45 * minute)
            {
                value = ts.Minutes;
                unit = FormatResourcesHelper.GetString("MINUTES");
            }
            else if (absDelta < 90 * minute)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("HOUR");
            }
            else if (absDelta < 24 * hour)
            {
                value = ts.Hours;
                unit = FormatResourcesHelper.GetString("HOURS");
            }
            else if (absDelta < 2 * day)
            {
                value = ts.Days;
                unit = FormatResourcesHelper.GetString("DAY");
            }
            else if (absDelta < 7 * day)
            {
                value = ts.Days;
                unit = FormatResourcesHelper.GetString("DAYS");
            }
            else if (absDelta < 13 * day)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("WEEK");
            }
            else if (absDelta < 30 * day)
            {
                value = (int)Math.Floor((double)ts.Days / 7);
                unit = FormatResourcesHelper.GetString("WEEKS");
            }
            else if (absDelta < 2 * month)
            {
                value = 1;
                unit = FormatResourcesHelper.GetString("MONTH");
            }
            else if (absDelta < 12 * month)
            {
                value = (int)Math.Floor((double)ts.Days / 30);
                unit = FormatResourcesHelper.GetString("MONTHS");
            }
            else if (absDelta < 2 * year)
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