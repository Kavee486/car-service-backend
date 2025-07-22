using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointment _Appointment;

        //DATest DATest = new DATest();

        public AppointmentController(IAppointment Appointment)
        {
            _Appointment = Appointment;
        }

        [HttpGet]
        public ActionResult GetAllAppointments()
        {
            var result = _Appointment.GetAllAppointments();
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetAppointmentsById(string id)
        {
            var result = _Appointment.GetAppointmentsById(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetAvailableTimeSlots(string date, string serviceId)
        {
            var result = _Appointment.GetAvailableTimeSlots(date, serviceId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }
    }
}