using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisolSMTLineApp.ViewModel;

namespace WisolSMTLineApp.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public List<PlanInfo> PlanInfos { get; set; }
        public bool IsActive { get; set; }
    }
}
