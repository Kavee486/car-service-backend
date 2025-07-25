using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetUserModel
    {
        public string U_UserID { get; set; }
        public string U_UserName { get; set; }
        public string U_PasswordHash { get; set; }
        public string U_RoleID { get; set; }

    }
}