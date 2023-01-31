using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TaskOrganizerAndro.Helpers
{
    public class DateTodayColorConverter : IValueConverter
    {
        OSAppTheme currentTheme = Application.Current.RequestedTheme;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var dateToday = DateTime.Today;
            if (date == dateToday)
            {
                return Color.Blue;
            }
            else
            {
                if (currentTheme == OSAppTheme.Dark)
                {
                    return Color.Gray;
                }
                else
                {
                    return Color.Transparent;
                }             
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
