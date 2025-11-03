using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class StatusController : Controller
    {

        private readonly IStatus _Status;

        //DATest DATest = new DATest();

        public StatusController(IStatus Status)
        {
            _Status = Status;
        }

        [HttpGet]
        public ActionResult GetAllStatus()
        {
            var result = _Status.GetAllStatus();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}