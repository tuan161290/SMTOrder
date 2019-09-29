using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiSolSMTRepo.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public List<PlanInfo> PlanInfos { get; set; }
    }
}
