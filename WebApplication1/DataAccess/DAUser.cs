using biZTrack.Static;
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
        public Response AddUserDetails(GetUserModal addUser)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO Users " +
                                          "(UserName," +
                                           "RoleID," +
                                           "MobileNo," +
                                           "Status," +
                                           "Email) " +
                               "VALUES('" + addUser.UserName + "'," +
                                       "'" + addUser.RoleID + "'," +
                                       "'" + addUser.MobileNo + "'," +
                                       "'A'," +
                                       "'" + addUser.Email + "')";



                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query))
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed!!";
            }
            return res;
        }
        




        public Response getAllUsers()
        {
            Response res = new Response();
            List<GetUserModal> UserList = new List<GetUserModal>();

            string Query = "SELECT " +
                                "UserID, " +
                                "UserName," +
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
