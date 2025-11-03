using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IUser
    {
        Response getAllUsers();
        Response AddUserDetails(GetUserModal addUser);
        Response GetUserByMobileNo(string MobileNo);
        Response getUsersByRole3();
        Response getUsersByRole2();
        Response UpdateUserDetails(GetUserModal updateUser);
        Response DeactivateUser(GetUserModal updateUser);





    }
}
