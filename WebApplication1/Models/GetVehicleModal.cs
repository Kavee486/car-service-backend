using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetVehicleModal
    {
        public string V_VehicleID { get; set; }
        public string V_CustomerID { get; set; }
        public string V_PlateNumber { get; set; }
        public string V_Make { get; set; }
        public string V_Model { get; set; }
        public string V_Year { get; set; }
        public string V_VIN { get; set; }
        public string V_CustomerName { get; internal set; }
        //public string V_Status { get; set; }



    }
}