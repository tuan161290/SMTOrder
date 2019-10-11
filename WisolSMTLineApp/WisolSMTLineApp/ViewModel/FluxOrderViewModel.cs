using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisolSMTLineApp.Model;

namespace WisolSMTLineApp.ViewModel
{
    public class FluxOrderViewModel : BaseViewModel
    {
        public FluxOrder FluxOrder { get; set; }
        public TimeSpan DefrostDuration { get; set; }
        public TimeSpan MixingDuration { get; set; }
    }
}
