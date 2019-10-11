using MonitorApp.Model;
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
        public List<FluxOrder> FluxOrders { get; set; }
        public PlanInfo CurrentPlan { get; set; }
        public Product CurrentProduct { get; set; }
        public FluxOrder CurrentFluxOrder { get; set; }
        public List<Order> Orders { get; set; }
    }
}
