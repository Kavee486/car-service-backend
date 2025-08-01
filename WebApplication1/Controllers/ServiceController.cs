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
    public class ServiceController : Controller
    {
        private readonly IService _service;

        

        public ServiceController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAllServices()
        {
            var result = _service.GetAllServices();
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetServiceByServiceID(string ServiceID)
        {
            var result = _service.GetServiceByServiceID(ServiceID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddServiceDetails(GetServiceModal addService)
        {
            var result = _service.AddServiceDetails(addService);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult PutServiceDetails(GetServiceModal addService)
        {
            var result = _service.PutServiceDetails(addService);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpDelete]
        public ActionResult DeleteServiceDetails(GetServiceModal addService)
        {
            var result = _service.DeleteServiceDetails(addService);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
    }
}