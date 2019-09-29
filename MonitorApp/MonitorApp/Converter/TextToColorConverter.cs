using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WiSolSMTRepo.Model;

namespace MonitorApp.Converter
{
    public class TextToColorConverter : IValueConverter
    {
        SolidColorBrush Orange = new SolidColorBrush(Color.FromArgb(0x50, 0xff, 0xa5, 0x00));//
        SolidColorBrush Green = new SolidColorBrush(Color.FromArgb(0x50, 0x84, 0xfd, 0x7f));//#84FD7F
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if ((OrderStatus)value == OrderStatus.OK)
            //{
            //    if (parameter.ToString() == "ok")
            //    {
            //        return new SolidColorBrush(Colors.LimeGreen);
            //    }
            //    else
            //    {
            //        return new SolidColorBrush(Colors.Orange);
            //    }
            //}
            string para = parameter.ToString();
            var Status = (OrderShortageReason)value;
            if (para == "border")
            {
                if (Status == OrderShortageReason.WAITING)
                {
                    return new SolidColorBrush(Color.FromArgb(0x50, 0xFF, 0x00, 0x00));
                }
                else if (Status == OrderShortageReason.OK)
                {
                    return new SolidColorBrush(Color.FromArgb(0x50, 0x00, 0xFF, 0x00));
                }
                else
                {
                    return Orange;
                }
            }
            else if (para == "ok" && Status == OrderShortageReason.OK)
            {
                return new SolidColorBrush(Colors.LimeGreen);
            }
            else if ((para == "jig-shortage" && Status == OrderShortageReason.JIG_Shortage))
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if ((para == "pcb-shortage" && Status == OrderShortageReason.PCB_Shortage))
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if ((para == "magazin-shortage" && Status == OrderShortageReason.MGZ_Shortage))
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if ((para == "plasma-waiting" && Status == OrderShortageReason.PLM_Waiting))
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x70, 0xbb, 0xfd));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
