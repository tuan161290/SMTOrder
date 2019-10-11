using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiSolSMTRepo.Model
{
    public class FluxOrder
    {
        public int FluxOrderID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime StartDefrostTime { get; set; }
        public DateTime MixedFluxTime { get; set; }
        public int LineInfoID { get; set; }
        public LineInfo LineInfo { get; set; }
        public FLuxStatus FLuxStatus { get; set; }
        public bool IsComplete { get; set; }

    }

    public enum FLuxStatus { DEFROST_CONFIRM, DEFROSTING, MIXFLUX_CONFIRM, MIXING_FLUX }
}
