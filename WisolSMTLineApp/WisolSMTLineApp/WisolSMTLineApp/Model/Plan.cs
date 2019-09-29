using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisolSMTLineApp.ViewModel;

namespace WisolSMTLineApp.Model
{
    public class Plan : BaseViewModel
    {
        public string modelName { get; set; }
        int _order;
        public int order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    _order = value;
                    OnPropertyChanged(nameof(order));
                }
            }
        }
        int _elapsed;
        private int _remain;

        public int elapsed
        {
            get { return _elapsed; }
            set
            {
                if (_elapsed != value)
                {
                    _elapsed = value;
                    OnPropertyChanged(nameof(elapsed));
                }
            }
        }

        public int remain
        {
            get
            {
                return _remain;
            }
            set
            {
                if (_remain != value)
                {
                    _remain = value;
                    OnPropertyChanged(nameof(remain));
                }
            }
        }
    }
}
