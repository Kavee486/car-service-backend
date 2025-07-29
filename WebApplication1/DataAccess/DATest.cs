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
    public class DATest : ITest
    {
        public Response TestUser()
        {
            Response res = new Response();
            List<GetUserModal> userList = new List<GetUserModal>();

            string Query = "SELECT " +
                                "User_id, " +
                                "type," +
                                "phone_no, " +
                                "Email, " +
                                "user_name, " +
                                "company_id, " +
                                "(select company_name from assets_company_master where id = company_id) company_name " +
                            "FROM " +
                                "assets_user_master ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetUserModal user = new GetUserModal();


                        //user.UserId = reader["User_id"].ToString();
                        //user.Type = reader["type"].ToString();
                        //user.PhoneNo = reader["phone_no"].ToString();
                        //ser.CompanyId = reader["company_id"].ToString();

                        userList.Add(user);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = userList;
            return res;
        }


    }
}