using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WiSolSMTRepo.Model
{
    public class Order : INotifyPropertyChanged
    {
        public int OrderID { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ConfirmedTime { get; set; }
        public bool IsConfirmed { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int LineInfoID { get; set; }
        public LineInfo LineInfo { get; set; }
        public int StopDuration { get; set; }
        public int PlanInfoID { get; set; }
        public PlanInfo PlanInfo { get; set; }

        OrderStatus _OrderStatus;
        public OrderStatus OrderStatus
        {
            get { return _OrderStatus; }
            set
            {
                if (_OrderStatus != value)
                {
                    _OrderStatus = value;
                    NotifyPropertyChanged(nameof(OrderStatus));
                }
            }
        }

        OrderShortageReason _Reason;
        public OrderShortageReason Reason
        {
            get { return _Reason; }
            set
            {
                if (_Reason != value)
                {
                    _Reason = value;
                    NotifyPropertyChanged(nameof(Reason));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string proName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proName));
        }
    }

    public enum OrderStatus { OK, WAITING, SHORTAGE }
    public enum OrderShortageReason { OK, WAITING, MGZ_Shortage, PCB_Shortage, JIG_Shortage, PLM_Waiting }
}
