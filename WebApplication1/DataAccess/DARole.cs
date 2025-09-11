using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DARole : IRole
    {
        public Response GetAllRoles()
        {

            Response res = new Response();
            List<GetRoleModal> RoleList = new List<GetRoleModal>();

            string Query = "SELECT " +
                                "RoleID, " +
                                "RoleName " +
                            "FROM " +
                                "Roles " +
                            "WHERE " +
                                "Status = 'A' ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetRoleModal Role = new GetRoleModal
                        {
                            R_RoleID = reader["RoleID"].ToString(),
                            R_RoleName = reader["RoleName"].ToString()
                        };


                        RoleList.Add(Role);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = RoleList;
            return res;

        }
    }
}