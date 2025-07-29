using biZTrack.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DACustomer: ICustomer
    {

        public Response GetAllCustomers()

        {
            Response res = new Response();
            List<GetCustomerModal> CustomerList = new List<GetCustomerModal>();

            string Query = "SELECT " +
                                "CustomerID, " +
                                "UserID," +
                                "FullName, " +
                                "Phone," +
                                "Email, " +
                                "Address " +

                            "FROM " +
                                "Customers ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetCustomerModal customer = new GetCustomerModal
                        {
                            C_CustomerID = reader["CustomerID"].ToString(),
                            C_UserID = reader["UserID"].ToString(),
                            C_FullName = reader["FullName"].ToString(),
                            C_Phone = reader["Phone"].ToString(),
                            C_Email = reader["Email"].ToString(),
                            C_Address = reader["Address"].ToString()
                        };


                        CustomerList.Add(customer);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = CustomerList;
            return res;


        }

        public Response GetCustomerByCustomerID(string CustomerID)
        {


            Response res = new Response();
            List<GetCustomerModal> CustomerList = new List<GetCustomerModal>();

            string Query = "SELECT " +
                                "CustomerID, " +
                                "UserID," +
                                "FullName, " +
                                "Phone," +
                                "Email, " +
                                "Address " +
                            "FROM " +
                                     "Customers " +
                            "WHERE " +
                                     "CustomerID = '" + CustomerID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetCustomerModal customer = new GetCustomerModal
                        {
                            C_CustomerID = reader["CustomerID"].ToString(),
                            C_UserID = reader["UserID"].ToString(),
                            C_FullName = reader["FullName"].ToString(),
                            C_Phone = reader["Phone"].ToString(),
                            C_Email = reader["Email"].ToString(),
                            C_Address = reader["Address"].ToString()
                        };
                        CustomerList.Add(customer);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = CustomerList;
            return res;
        }

        public Response AddCustomerDetails(GetCustomerModal addCustomer)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO Customers " +
                                          "(UserID," +
                                           "FullName," +
                                           "Phone," +
                                           "Email," +
                                           "Address) " +
                               "VALUES('"  + addCustomer.C_UserID + "'," +
                                       "'" + addCustomer.C_FullName + "'," +
                                       "'" + addCustomer.C_Phone + "'," +
                                       "'" + addCustomer.C_Email + "'," +
                                       "'" + addCustomer.C_Address + "')";



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
    }
}