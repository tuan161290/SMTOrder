using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisolSMTLineApp.Model
{
    public class ProductionDtl
    {
        public ProductionDtl()
        {

        }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "id")]
        public int ID { set; get; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "working_date")]
        public string Working_Date { set; get; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "factory_id")]
        public int Factory_ID { set; get; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "line_id")]
        public int Line_ID { set; get; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "shift_id")]
        public int Shift_ID { set; get; } = 1;
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "product_id")]
        public int Product_ID { set; get; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "duaration")]
        public int Duaration { get; set; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        ////---------------------------------------------------------------------
        [JsonProperty(PropertyName = "finished")]
        public bool Finished { set; get; }
    }
    //WorkingDate : params.date,
    //  FactoryID : 1,
    //  LineID : params.lineId,
    //  ShiftID : params.shiftId, 
    //  Amount : params.amount,
    //  Finished : false
}
