using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Login1Controller : Controller
    {
        
        private readonly ILogin _loginRepository;

        public Login1Controller(ILogin login)
        {
            _loginRepository = login;
        }

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string contact)
        {
            var result = _loginRepository.Login(contact);
            return Json(result, JsonRequestBehavior.AllowGet);
        }





    }
}