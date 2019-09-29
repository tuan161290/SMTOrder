using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiSolSMTRepo.Model
{
    public class LineInfo
    {
        public int LineInfoID { get; set; }
        public string Name { get; set; }
        public bool Is_active { get; set; }
        public List<PlanInfo> PlanInfos { get; set; }
        public PlanInfo CurrentPlan { get; set; } = new PlanInfo();
        public Product CurrentProduct { get; set; }
        public List<Order> Orders { get; set; }
    }
}
