using System;

namespace WebApplication1.Models
{
    public class GetTimeslotModal
    {
        public string T_TimeslotID { get; set; }
        public string T_Date { get; set; }
        public string T_StartTime { get; set; }
        public string T_EndTime { get; set; }
        public string T_MaxCustomers { get; set; }
        public string T_Status { get; set; }
    }
}
