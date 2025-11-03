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
    public class TechniciansController : Controller
    {
        private readonly ITechnicians _technicians;

        public TechniciansController(ITechnicians technicians)
        {
            _technicians = technicians;
        }


        [HttpGet]
        public ActionResult getAllTechnicians()
        {
            var technicians = _technicians.getAllTechnicians();
            return Json(technicians, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateTechnicianDetails(GetTechniciansModal updateTechnician)
        {
            var result = _technicians.UpdateTechnicianDetails(updateTechnician);
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public ActionResult DeleteTechnicianDetails(GetTechniciansModal updateTechnician)
        {
            var result = _technicians.DeleteTechnicianDetails(updateTechnician);
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        // GET: Technicians
        public ActionResult Index()
        {
            return View();
        }
    }
}