using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
//using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _User;

        //DATest DATest = new DATest();

        public UserController(IUser User)
        {
            _User = User;
        }

        [HttpGet]
        public ActionResult getAllUsers()
        {
            var result = _User.getAllUsers();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddUserDetails(GetUserModal addUser)
        {
            var result = _User.AddUserDetails(addUser);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetUserByMobileNo(string MobileNo)
        {
            var result = _User.GetUserByMobileNo(MobileNo);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult getUsersByRole3()
        {
            var result = _User.getUsersByRole3();  // Call the method from the service layer or business logic
            return Json(result, JsonRequestBehavior.AllowGet);  // Return the result as a JSON response
        }



        [HttpGet]
        public ActionResult getUsersByRole2()
        {
            var result = _User.getUsersByRole2();  // Call the method from the service layer or business logic
            return Json(result, JsonRequestBehavior.AllowGet);  // Return the result as a JSON response
        }



        [HttpPost]
        public ActionResult UpdateUserDetails(GetUserModal updateUser)
        {
            var result = _User.UpdateUserDetails(updateUser);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult DeactivateUser(GetUserModal updateUser)
        {
            var result = _User.DeactivateUser(updateUser);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



    }
}

