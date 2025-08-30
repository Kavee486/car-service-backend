using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VehicleController : Controller
    {

        private readonly IVehicle _vehicle;

        //DATest DATest = new DATest();

        public VehicleController(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        [HttpGet]
        public ActionResult getAllVehicles()
        {
            var result = _vehicle.getAllVehicles();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetVehicleByCustomerID(string CustomerID)
        {
            var result = _vehicle.GetVehicleByCustomerID(CustomerID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetVehicleByVehicleID(string VehicleID)
        {
            var result = _vehicle.GetVehicleByVehicleID(VehicleID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddVehicalDetails(GetVehicleModal addVehicle)
        {
            var result = _vehicle.AddVehicalDetails(addVehicle);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult PutVehicalDetails(GetVehicleModal addVehicle)
        {
            var result = _vehicle.PutVehicalDetails(addVehicle);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult DeleteVehicalDetails(GetVehicleModal addVehicle)
        {
            var result = _vehicle.DeleteVehicalDetails(addVehicle);
            return Json(result, JsonRequestBehavior.AllowGet);

        }








        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }
    }
}