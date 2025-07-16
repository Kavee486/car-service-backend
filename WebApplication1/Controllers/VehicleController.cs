using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;

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



        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }
    }
}