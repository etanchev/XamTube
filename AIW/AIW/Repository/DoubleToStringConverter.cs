using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AIW
{
    class DoubleToStringConverter : IValueConverter
    {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (string)value ;
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (double)value;
            }
        
    }
}
