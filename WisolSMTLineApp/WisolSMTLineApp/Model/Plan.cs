using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisolSMTLineApp.ViewModel;

namespace WisolSMTLineApp.Model
{
    public class PlanInfo
    {
        public int PlanInfoID { get; set; }
        public int Order { get; set; }
        public int Elapsed { get; set; }
        public int Remain { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime FinishedTime { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int LineInfoID { get; set; }
        public LineInfo Line { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class PlanViewObject : BaseViewModel
    {
        int _Ordered;
        public int Ordered
        {
            get { return _Ordered; }
            set
            {
                if (_Ordered != value)
                {
                    _Ordered = value;
                    OnPropertyChanged(nameof(Ordered));
                }
            }
        }

        int _Elapsed;
        public int Elapsed
        {
            get { return _Elapsed; }
            set
            {
                if (_Elapsed != value)
                {
                    _Elapsed = value;
                    OnPropertyChanged(nameof(Elapsed));
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
                    OnPropertyChanged(nameof(Remain));
                }
            }
        }

        private FluxOrder _FluxOrder;
        public FluxOrder FluxOrder
        {
            get { return _FluxOrder; }
            set
            {
                if (_FluxOrder != value)
                {
                    _FluxOrder = value;
                    OnPropertyChanged(nameof(FluxOrder));
                }
            }
        }
    }
}

