using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Database_Layer;

namespace WebApplication1.DataAccess
{
    public class DAUser : IUser
    {
        public Response getAllUsers()
        {
            Response res = new Response();
            List<GetUserModel> UserList = new List<GetUserModel>();

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

                        GetUserModel user = new GetUserModel
                        {
                            U_UserID = reader["UserID"].ToString(),
                            U_UserName = reader["UserName"].ToString(),
                            U_PasswordHash = reader["PasswordHash"].ToString(),
                            U_RoleID = reader["RoleID"].ToString()
                        };


                        UserList.Add(user);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = UserList;
            return res;


        }
    }
           





















    }
