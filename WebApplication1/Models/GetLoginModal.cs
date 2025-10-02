using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public object Result { get; set; }
       
        public string RedirectUrl { get; set; }  // New field for redirection
    }

    public class OtpResponse : ApiResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int OtpCode { get; set; }
        public int RoleID { get;  set; }
    }



    public class GetLoginModel
    {

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
    }



    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class OtpVerificationRequest
    {
        public string Contact { get; set; }
        public int OtpCode { get; set; }
    }





}