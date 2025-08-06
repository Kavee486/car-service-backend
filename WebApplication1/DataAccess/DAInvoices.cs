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
    public class DAInvoices: IInvoices
    {
        public Response AddInvoicesDetails(GetInvoicesModal addInvoice)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO Invoices " +
                                          "(JobCardID," +
                                           "InvoiceDate," +
                                           "TotalAmount," +
                                           "Status," +
                                           "PaymentStatus) " +
                               "VALUES('" + addInvoice.I_JobCardID + "'," +
                                       "'" + addInvoice.I_InvoiceDate + "'," +
                                       "'" + addInvoice.I_TotalAmount + "'," +
                                       "'A'," +
                                       "'" + addInvoice.I_PaymentStatus + "')";



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

        public Response DeleteInvoicesDetails(GetInvoicesModal addInvoice)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE Invoices
                                        SET Status = 'I'
                                        WHERE InvoiceID = '" + addInvoice.I_InvoiceID + @"'";


                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))
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

        public Response GetAllInvoices()

        {
            Response res = new Response();
            List<GetInvoicesModal> InvoicesList = new List<GetInvoicesModal>();

            string Query = "SELECT " +
                                "InvoiceID, " +
                                "JobCardID," +
                                "InvoiceDate, " +
                                "TotalAmount, " +
                                "PaymentStatus " +
                            "FROM " +
                                "Invoices ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetInvoicesModal Invoices = new GetInvoicesModal
                        {
                            I_InvoiceID = reader["InvoiceID"].ToString(),
                            I_JobCardID = reader["JobCardID"].ToString(),
                            I_InvoiceDate = reader["InvoiceDate"].ToString(),
                            I_TotalAmount = reader["TotalAmount"].ToString(),
                            I_PaymentStatus = reader["PaymentStatus"].ToString()
                        };


                        InvoicesList.Add(Invoices);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = InvoicesList;
            return res;


        }

        public Response GetInvoicesByInvoiceID(string InvoiceID)
        {


            Response res = new Response();
            List<GetInvoicesModal> InvoicesList = new List<GetInvoicesModal>();

            string Query = "SELECT " +
                                "InvoiceID, " +
                                "JobCardID," +
                                "InvoiceDate, " +
                                "TotalAmount, " +
                                "PaymentStatus " +
                            "FROM " +
                                     "Invoices " +
                            "WHERE " +
                                     "InvoiceID = '" + InvoiceID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetInvoicesModal Invoices = new GetInvoicesModal
                        {
                            I_InvoiceID = reader["InvoiceID"].ToString(),
                            I_JobCardID = reader["JobCardID"].ToString(),
                            I_InvoiceDate = reader["InvoiceDate"].ToString(),
                            I_TotalAmount = reader["TotalAmount"].ToString(),
                            I_PaymentStatus = reader["PaymentStatus"].ToString()
                        };
                        InvoicesList.Add(Invoices);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = InvoicesList;
            return res;


        }

        public Response PutInvoicesDetails(GetInvoicesModal addInvoice)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE Invoices
                                        SET PaymentStatus = '" + addInvoice.I_PaymentStatus + @"'
                                        WHERE InvoiceID = '" + addInvoice.I_InvoiceID + @"'";


                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))
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