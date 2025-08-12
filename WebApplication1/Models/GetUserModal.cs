using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetUserModal
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string RoleID { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

    }
}