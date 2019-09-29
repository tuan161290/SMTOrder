using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiSolSMTRepo.Model
{
    public class LineInfo
    {
        public int LineInfoID { get; set; }
        public string Name { get; set; }
        public bool Is_active { get; set; }
        public List<PlanInfo> PlanInfos { get; set; }
        [NotMapped]
        public PlanInfo CurrentPlan { get; set; }
        [NotMapped]
        public Product CurrentProduct { get; set; }
        public List<Order> Orders { get; set; }
    }
}
