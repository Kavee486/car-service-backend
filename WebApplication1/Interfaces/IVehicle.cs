using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IVehicle
    {
        Response AddVehicalDetails(GetVehicleModal addVehicle);
        Response getAllVehicles();
       Response GetVehicleByCustomerID(string CustomerID);
       Response GetVehicleByVehicleID(string VehicleID);
        //put,delete
    }
}
