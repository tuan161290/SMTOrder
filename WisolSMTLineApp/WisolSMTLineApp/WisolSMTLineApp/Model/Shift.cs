using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisolSMTLineApp.Model
{
    public class Shift
    {
        public int FactoryID { set; get; }
        public int ID { set; get; }
        public string Name { set; get; }
        public int StartTime { set; get; }
        public int StopTime { set; get; }
        public bool Disabled { set; get; }
    }
}
