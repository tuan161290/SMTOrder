using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisolSMTLineApp.Model
{
    public class FluxOrder
    {
        public int FluxOrderID { get; set; }
        public int LineInfoID { get; set; }
        public LineInfo LineInfo { get; set; }
        public int PlanInfoID { get; set; }
        public string LotID { get; set; }
        public FLuxOrderStatus FLuxOrderStatus { get; set; }
        public DateTime DefrostTimeStamp { get; set; }
        public DateTime MixingTimeStamp { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime SendToLineTimeStamp { get; set; }
        public DateTime LineInput { get; set; }
        public bool IsFinished { get; set; }

        public TimeSpan Duration { get; set; }
        public TimeSpan TotalDuration { get; set; }
    }

    public enum FLuxOrderStatus
    {
        WAITING, DEFROST, DEFROSTING, MIX, MIXING, MIXED, NO_ORDER,
        OK,
        READY,
    }
}
