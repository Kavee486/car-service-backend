using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.MiddleWear;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        // Admin Controller
        [AuthenticationMiddleware]
        public class AdminController : Controller
        {
            public ActionResult Dashboard()
            {
                var user = Session["User"] as GetLoginModel;
                ViewBag.UserName = user?.UserName;
                return View();
            }
        }

        // Technician Controller
        [AuthenticationMiddleware]
        public class TechnicianController : Controller
        {
            public ActionResult Dashboard()
            {
                var user = Session["User"] as GetLoginModel;
                ViewBag.UserName = user?.UserName;
                return View();
            }
        }

        // Customer Controller
        [AuthenticationMiddleware]
        public class CustomerController : Controller
        {
            public ActionResult Dashboard()
            {
                var user = Session["User"] as GetLoginModel;
                ViewBag.UserName = user?.UserName;
                return View();
            }
        }

    }
}