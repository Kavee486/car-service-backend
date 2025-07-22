using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IAppointment
    {
        Response GetAllAppointments();
        Response GetAppointmentsById(string id);
        Response GetAvailableTimeSlots(string date, string serviceId);

    }
}
