using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisolSMTLineApp.Model
{
    public class LineInfo
    {
        public int LineInfoID { get; set; }
        public string Name { get; set; }
        public bool Is_active { get; set; }
        public List<PlanInfo> PlanInfos { get; set; }
        public PlanInfo CurrentPlan { get; set; }
    }
}
