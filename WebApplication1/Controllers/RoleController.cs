using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class RoleController : Controller
    {


        private readonly IRole _Role;


        public RoleController(IRole Role)
        {
            _Role = Role;
        }

        [HttpGet]
        public ActionResult GetAllRoles()
        {
            var result = _Role.GetAllRoles();
            return Json(result, JsonRequestBehavior.AllowGet);
        }




    }
}