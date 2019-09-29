using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiSolSMTRepo.Model
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
}
