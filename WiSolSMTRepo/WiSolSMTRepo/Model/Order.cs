using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiSolSMTRepo.Model
{
    public class Order
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
        public OrderStatus OrderStatus { get; set; }
        public OrderShortageReason Reason { get; set; }
    }

    public enum OrderStatus { OK, WAITTING, SHORTAGE }
    public enum OrderShortageReason { OK, WAITING, MGZ_Shortage, PCB_Shortage, JIG_Shortage, PLM_Waiting }
}
