using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _User;

        //DATest DATest = new DATest();

        public UserController(IUser user)
        {
            _User = user;
        }

        // GET: Test

   



    }
}








 

