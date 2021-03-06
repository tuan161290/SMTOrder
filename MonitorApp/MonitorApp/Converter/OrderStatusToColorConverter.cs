﻿using MonitorApp.Model;
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

    public class FluxOrderStatusToColorConverter : IValueConverter
    {
        SolidColorBrush LightGreen = new SolidColorBrush(Color.FromArgb(0x60, 0x00, 0xFF, 0x00));
        SolidColorBrush LightRed = new SolidColorBrush(Color.FromArgb(0x60, 0xFF, 0x00, 0x00));
        SolidColorBrush LightOrange = new SolidColorBrush(Color.FromArgb(0x60, 0xFF, 0xA5, 0x00));//#FFFFA500.
        SolidColorBrush DarkGray = new SolidColorBrush(Colors.DarkGray);//#FFFFA500.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var FluxOrder = (FluxOrder)value;
            if ((App.Now - FluxOrder.CreatedTime).TotalHours < 5)
            {
                return DarkGray;
            }

            var Status = FluxOrder.FLuxOrderStatus;
            if (Status == FLuxOrderStatus.NO_ORDER)
                return DarkGray;
            if (parameter.ToString() == "DEFROST")
            {
                if (Status == FLuxOrderStatus.DEFROSTING)
                {
                    return LightOrange;
                }
                else if (Status == FLuxOrderStatus.DEFROST || Status == FLuxOrderStatus.WAITING)
                {
                    return LightRed;
                }
                return DarkGray;
            }
            else if (parameter.ToString() == "READY")
            {
                if (Status == FLuxOrderStatus.READY)
                {
                    return LightGreen;
                }
                return LightRed;
            }
            return DarkGray;
            //if ((Status == FLuxOrderStatus.DEFROSTING || Status == FLuxOrderStatus.WAITING) || parameter == "DEFR"
            //{
            //    return LightOrange;
            //}
            //else if (Status == FLuxOrderStatus.DEFROST || Status == FLuxOrderStatus.READY)
            //{
            //    return LightRed;
            //}
            //else if (Status == FLuxOrderStatus.NO_ORDER)
            //    return DarkGray;
            //return LightGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
