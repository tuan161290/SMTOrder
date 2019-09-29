using MonitorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MonitorApp.Converter
{
    public class OrderStatusToColorConverter : IValueConverter
    {
        SolidColorBrush LightGreen = new SolidColorBrush(Color.FromArgb(0x50, 0x00, 0xFF, 0x00));
        SolidColorBrush LightRed = new SolidColorBrush(Color.FromArgb(0x50, 0xFF, 0x00, 0x00));
        SolidColorBrush LightOrange = new SolidColorBrush(Color.FromArgb(0x50, 0xFF, 0xA5, 0x00));//#FFFFA500.
        SolidColorBrush DarkGray = new SolidColorBrush(Colors.DarkGray);//#FFFFA500.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Status = (WorkingStatus)value;
            if (Status == WorkingStatus.Normal)
                return LightGreen;
            else if (Status == WorkingStatus.STOP)
            {
                return LightRed;
            }
            else if (Status == WorkingStatus.Order)
            {
                return LightOrange;
            }
            else
            {
                return DarkGray;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
