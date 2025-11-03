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
        //public Response AddUserDetails(GetUserModal addUser)
        //{
        //    Response res = new Response();
        //    DBconnect DBconnect = new DBconnect();
        //    try
        //    {
        //        string Query = "INSERT INTO Users " +
        //                                  "(UserName," +
        //                                   "RoleID," +
        //                                   "MobileNo," +
        //                                   "Status," +
        //                                   "Email) " +
        //                       "VALUES('" + addUser.UserName + "'," +
        //                               "'" + addUser.RoleID + "'," +
        //                               "'" + addUser.MobileNo + "'," +
        //                               "'A'," +
        //                               "'" + addUser.Email + "')";



        //        using (var dbConnect = new DBconnect())
        //        {
        //            if (dbConnect.AddEditDel(Query))
        //            {
        //                res.StatusCode = 200;
        //                res.Result = "Success!!";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        res.StatusCode = 500;
        //        res.Result = "Failed!!";
        //    }
        //    return res;
        //}
        public Response AddUserDetails(GetUserModal addUser)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                // Insert user details into the Users table
                string query = "INSERT INTO Users " +
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
                    if (dbConnect.AddEditDel(query))
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!!";

                        // ✅ If RoleID = 1 → insert into Customers table
                        if (addUser.RoleID.ToString() == "1")
                        {
                            string customerQuery = "INSERT INTO Customers " +
                                                    "(FullName, Phone, Email, Address, Status) " +
                                                    "VALUES('" + addUser.UserName + "'," +
                                                            "'" + addUser.MobileNo + "'," +
                                                            "'" + addUser.Email + "'," +
                                                            "NULL," +
                                                            "'A')";

                            dbConnect.AddEditDel(customerQuery);
                        }
                        // ✅ If RoleID = 3 → insert into Technicians table
                        else if (addUser.RoleID.ToString() == "3")
                        {
                            // Check if NIC is provided for Technicians
                            if (string.IsNullOrEmpty(addUser.NIC))
                            {
                                res.StatusCode = 400;
                                res.Result = "NIC is required for Technicians.";
                                return res;
                            }

                            string technicianQuery = "INSERT INTO Technicians " +
                                                      "(FullName, Phone, Email, Address, NIC, Status) " +
                                                      "VALUES('" + addUser.UserName + "'," +
                                                              "'" + addUser.MobileNo + "'," +
                                                              "'" + addUser.Email + "'," +
                                                              "NULL," +  // Assuming Address can be NULL
                                                              "'" + addUser.NIC + "'," + // NIC value inserted here
                                                              "'A')";

                            if (dbConnect.AddEditDel(technicianQuery))
                            {
                                res.StatusCode = 200;
                                res.Result = "Technician added successfully!";
                            }
                            else
                            {
                                res.StatusCode = 400;
                                res.Result = "Failed to add technician!";
                            }
                        }
                    }
                    else
                    {
                        res.StatusCode = 400;
                        res.Result = "Failed to add user!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "An error occurred while processing your request.";
            }
            return res;
        }

        public Response DeactivateUser(GetUserModal updateUser)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE Users
                                SET Status = 'I'
                                WHERE UserID = '" + updateUser.UserID + @"'";

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))
                    {
                        res.StatusCode = 200;
                        res.Result = "User deactivated successfully!";
                    }
                    else
                    {
                        res.StatusCode = 400;
                        res.Result = "Failed to deactivate user.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "An error occurred while deactivating the user.";
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


        public Response GetUserByMobileNo(string MobileNo)
        {

            Response res = new Response();
            List<GetUserModal> UserList = new List<GetUserModal>();

            string Query = "SELECT " +
                                "UserID, " +
                                "UserName," +
                                "RoleID," +
                                "MobileNo, " +
                                "Email " +
                            "FROM " +
                                     "Users " +
                            "WHERE " +
                                     "MobileNo = '" + MobileNo + "'";
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
                            RoleID = reader["RoleID"].ToString(),
                            MobileNo = reader["MobileNo"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                        UserList.Add(User);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = UserList;
            return res;
        }

        public Response getUsersByRole2()
        {
            Response res = new Response();
            List<GetUserModal> UserList = new List<GetUserModal>();

            // Updated Query to also fetch the Status
            string Query = "SELECT " +
                            "UserID, " +
                            "UserName, " +
                            "Email, " +
                            "MobileNo, " +
                            "RoleID, " + // Add RoleID column here
                            "Status " +  // Add Status column here
                        "FROM " +
                            "Users " +
                        "WHERE RoleID = 2";  // Ensure this filters users by RoleID 2

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
                            Email = reader["Email"]?.ToString() ?? string.Empty,
                            MobileNo = reader["MobileNo"]?.ToString() ?? string.Empty,
                            RoleID = reader["RoleID"].ToString(),
                            Status = reader["Status"]?.ToString() ?? string.Empty // Fetch the Status here
                        };

                        UserList.Add(User);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = UserList;
            return res;
        }

        public Response getUsersByRole3()
        {
            Response res = new Response();
            List<GetUserModal> UserList = new List<GetUserModal>();

            // Updated Query to also fetch the Status
            string Query = "SELECT " +
                            "UserID, " +
                            "UserName, " +
                            "Email, " +
                            "MobileNo, " +
                            "RoleID, " + // Add RoleID column here
                            "Status " +  // Add Status column here
                        "FROM " +
                            "Users " +
                        "WHERE RoleID = 3";  // Ensure this filters users by RoleID 2

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
                            Email = reader["Email"]?.ToString() ?? string.Empty,
                            MobileNo = reader["MobileNo"]?.ToString() ?? string.Empty,
                            RoleID = reader["RoleID"].ToString(),
                            Status = reader["Status"]?.ToString() ?? string.Empty // Fetch the Status here
                        };

                        UserList.Add(User);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = UserList;
            return res;
        }

        public Response UpdateUserDetails(GetUserModal updateUser)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                // Constructing the SQL Update Query to update the user details
                string updateQuery = @"UPDATE Users
                               SET UserName = '" + updateUser.UserName + @"',
                                   Email = '" + updateUser.Email + @"',
                                   MobileNo = '" + updateUser.MobileNo + @"',
                                   RoleID = '" + updateUser.RoleID + @"'
                               WHERE UserID = '" + updateUser.UserID + @"'";

                // Using the DBconnect object to execute the query
                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))  // Assuming AddEditDel executes the query
                    {
                        res.StatusCode = 200;
                        res.Result = "User details updated successfully!";
                    }
                    else
                    {
                        res.StatusCode = 400;
                        res.Result = "Failed to update user details!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "An error occurred while updating user details!";
            }
            return res;
        }









    }

}
