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

        //DATest DATest = new DATest();

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
        public ActionResult GetServiceById(string id)
        {
            var result = _service.GetServiceById(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddServiceDetails(GetServiceModal AddService)
        {
            var result = _service.AddServiceDetails(AddService);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
    }
}