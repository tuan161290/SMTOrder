using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApp.ViewModel
{
    public class LineViewModel : INotifyPropertyChanged
    {
        public int LineInfoID { get; set; }
        public string Name { get; set; }
        private string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    NotifyPropertyChanged(nameof(ProductName));
                }
            }
        }
        int _Order;
        public int Order
        {
            get { return _Order; }
            set
            {
                if (_Order != value)
                {
                    _Order = value;
                    NotifyPropertyChanged(nameof(Order));
                }
            }
        }

        int _Elapse;
        public int Elapse
        {
            get { return _Elapse; }
            set
            {
                if (_Elapse != value)
                {
                    _Elapse = value;
                    NotifyPropertyChanged(nameof(Elapse));
                }
            }
        }

        int _Remain;
        public int Remain
        {
            get { return _Remain; }
            set
            {
                if (_Remain != value)
                {
                    _Remain = value;
                    NotifyPropertyChanged(nameof(Remain));
                }
            }
        }

        private WorkingStatus _WorkingStatus;
        public WorkingStatus WorkingStatus
        {
            get { return _WorkingStatus; }
            set
            {
                if (_WorkingStatus != value)
                {
                    _WorkingStatus = value;
                    NotifyPropertyChanged(nameof(WorkingStatus));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged(string proName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proName));
        }
    }

    public enum WorkingStatus { Normal, Order, STOP, NO_PRODUCTION }

}
