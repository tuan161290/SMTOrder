using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisolSMTLineApp.Model
{

    //{"status":"success","data":[{"working_date":"2019-01-18","factory_id":1,"shift_id":1,"line_id":1,"product_id":1,"ordered_qty":0,"good_prod_qty":0,"remain_qty":0,"is_active":false,"created_at":"2019-08-23T13:42:50.622Z","updated_at":"2019-08-23T13:42:50.622Z"}]}
    public class ProductionPlan
    {
        [JsonProperty(PropertyName = "working_date")]
        public string Working_Date { set; get; }

        [JsonProperty(PropertyName = "factory_id")]
        public int Factory_ID { set; get; }

        [JsonProperty(PropertyName = "line_id")]
        public int Line_ID { set; get; }

        [JsonProperty(PropertyName = "shift_id")]
        public int Shift_ID { set; get; } = 1;

        [JsonProperty(PropertyName = "product_id")]
        public int Product_ID { set; get; }

        //[JsonProperty(PropertyName = "name")]
        //public string Name { set; get; }

        [JsonProperty(PropertyName = "ordered_qty")]
        public int Ordered_Qty { set; get; }

        [JsonProperty(PropertyName = "good_prod_qty")]
        public int Good_Prod_Qty { set; get; }

        [JsonProperty(PropertyName = "remain_qty")]
        public int Remain_Qty { set; get; }

        //[JsonProperty(PropertyName = "ngprod_qty")]
        //public int NGProdQty { set; get; }
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }


        [JsonProperty(PropertyName = "is_active")]
        public bool Is_Active { set; get; }

        [JsonProperty(PropertyName = "created_at")]
        public string Created_at { set; get; }

        [JsonProperty(PropertyName = "updated_at")]
        public string Updated_at { set; get; }
    }
}
