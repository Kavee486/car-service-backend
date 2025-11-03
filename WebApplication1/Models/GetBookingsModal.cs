using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetBookingsModal
    {
        public string B_BookingID { get; set; }
        public string B_CustomerID { get; set; }
        public string B_VehicleID { get; set; }
        public string B_BookingDate { get; set; }
        public string B_PreferredDate { get; set; }
        public string B_BookingStatus { get; set; }
        public string B_Status { get; set; }
        public string B_timeslotID { get; set; }
        public string B_ServiceID { get; set; }

        // ✅ New property for multiple services
        public List<int> B_ServiceIDs { get; set; }
        //public string B_StartingTime { get; set; }
        //public string B_EndingTime { get; set; }
        public string B_CustomerName { get; set; }
        public string B_CustomerPhone { get; set; }
        public string B_ServiceName { get; set; }
        public string B_StartTime { get; set; }
        public string B_EndTime { get; set; }
    }

}