using System;

namespace WisolSMTLineApp.Model
{
    public class WorkPlaceStatus
    {
        public string StatusCode { get; set; }
        public int StatusCounter { get; set; }
        public string StatusMsg { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public bool Finished { set; get; }
    }


    public enum WorkingStatus { Normal, Order, Stop }
    public enum WorkingMode { Auto, Manual }

}
