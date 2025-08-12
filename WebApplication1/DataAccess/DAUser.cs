using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DAUser : IUser
    {


        public Response getAllUsers()
        {
            Response res = new Response();
            List<GetUserModal> UserList = new List<GetUserModal>();

            string Query = "SELECT " +
                                "UserID, " +
                                "UserName," +
                                "PasswordHash, " +
                                "RoleID " +

                            "FROM " +
                                "Users ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetUserModal User = new GetUserModal
                        {
                            UserID = reader["UserID"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            RoleID = reader["RoleID"].ToString()
                        };



                        UserList.Add(User);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = UserList;
            return res;


        }











    }

}
