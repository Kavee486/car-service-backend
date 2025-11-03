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
    public class LoginController : Controller
    {
        private readonly ILogin _loginRepository;

        public LoginController(ILogin login)
        {
            _loginRepository = login;
        }

        // GET: Test
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult Login(string contact)
        {
            var result = _loginRepository.Login(contact);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Verify OTP
        [HttpPost]
        public ActionResult VerifyOtp(OtpVerificationRequest otpRequest)
        {
            // Call the VerifyOtp method from DALogin
            var result = _loginRepository.VerifyOtp(otpRequest.Contact, otpRequest.OtpCode);
            // Return the result as JSON to the client
            return Json(result, JsonRequestBehavior.AllowGet);
        }




    }
}









