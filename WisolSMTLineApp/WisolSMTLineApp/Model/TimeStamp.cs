using System;

namespace WisolSMTLineApp.Model
{
    public class ShiftPeriod
    {
        public TimeSpan From
        {
            get;
            set;
        }
        public TimeSpan To
        {
            get
            {
                return From.Add(TimeSpan.FromHours(12));
            }
        }
    }
}
