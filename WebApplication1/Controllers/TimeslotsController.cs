using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TimeslotController : Controller
    {
        private readonly ITimeslots _timeslot;

        // Temporary fallback constructor
        public TimeslotController()
        {
            _timeslot = new DATimeslots();
        }

        [HttpGet]
        public ActionResult GetAllTimeslots()
        {
            var result = _timeslot.GetAllTimeslots();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddTimeslot(GetTimeslotModal addTimeslot)
        {
            var result = _timeslot.AddTimeslot(addTimeslot);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteTimeslot(GetTimeslotModal deleteTimeslot)
        {
            var result = _timeslot.DeleteTimeslot(deleteTimeslot);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}