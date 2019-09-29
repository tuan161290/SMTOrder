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
        public int ID { set; get; }
        public string  Barcode { get; set; }
        public string Name { set; get; }
        public bool Is_active { set; get; }
        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set; get; }
    }
}
