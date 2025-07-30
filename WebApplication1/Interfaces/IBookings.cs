using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IBookings
    {
        Response GetAllBookings();
        Response GetBookingsByCustomerID(string CustomerID);
        Response GetBookingsByVehicleID(string VehicleID);

    }
}
