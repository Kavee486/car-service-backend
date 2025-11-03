using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetJobCardsModal
    {
        public string J_JobCardID { get; set; }
        public string J_BookingID { get; set; }
        public string J_CreatedDate { get; set; }
        public string J_Technician { get; set; }
        public string J_JobCardStatus { get; set; }
        public string J_Status { get; set; }
        public string ServiceNames { get; set; }
        public List<string> NewServiceIDs { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }


        // ✅ New: Parts list
        public List<PartDetail> PartsList { get; set; }
        public string J_TechnicianID { get;  set; }
        public string VehiclePlateNumber { get;  set; }
        public string VehicleMake { get;  set; }
        public string VehicleModel { get;  set; }
        public string VehicleYear { get;  set; }
        public string VehicleVIN { get;  set; }
    }

    public class PartDetail
    {
        public int PartID { get; set; }
        public int Quantity { get; set; }
    }
}