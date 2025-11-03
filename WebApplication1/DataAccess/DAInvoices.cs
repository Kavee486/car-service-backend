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
                // SQL Insert query for adding invoice details
                string Query = "INSERT INTO Invoices " +
                               "(InvoiceID, " +
                               "BookingID, " +
                               "TotalAmount, " +
                               "InvoiceDate, " +
                               "PaymentStatus, " +
                               "JobCardID) " + // Assuming JobCardID is part of the invoice
                               "VALUES('" + addInvoice.I_InvoiceID + "'," +
                                       "'" + addInvoice.J_BookingID + "'," +
                                       "'" + addInvoice.I_TotalAmount + "'," +
                                       "'" + addInvoice.I_InvoiceDate + "'," +
                                       "'" + addInvoice.I_PaymentStatus + "'," +
                                       "'" + addInvoice.JobCardID + "')"; // Add JobCardID to relate with JobCards table

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query)) // Call AddEditDel method for inserting the data
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!!";
                    }
                    else
                    {
                        res.StatusCode = 500;
                        res.Result = "Failed to insert invoice details!!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "An error occurred while adding invoice details!!";
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

        public Response GetAllInvoicesWithJobCards()
        {
            Response res = new Response();
            List<GetInvoicesModal> InvoicesList = new List<GetInvoicesModal>();

            // Updated Query to JOIN the Invoices and JobCards tables
            string Query = "SELECT " +
                            "Invoices.InvoiceID, " +
                            "JobCards.BookingID, " +
                            "JobCards.JobCardStatus, " +
                            "Invoices.TotalAmount, " +
                            "Invoices.InvoiceDate, " +
                            "Invoices.PaymentStatus " +
                            "FROM " +
                            "Invoices " +
                            "INNER JOIN JobCards ON Invoices.JobCardID = JobCards.JobCardID"; // JOIN condition

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {
                        GetInvoicesModal Invoice = new GetInvoicesModal
                        {
                            I_InvoiceID = reader["InvoiceID"].ToString(),
                            J_BookingID = reader["BookingID"].ToString(),
                            J_JobCardStatus = reader["JobCardStatus"].ToString(),
                            I_TotalAmount = reader["TotalAmount"].ToString(),
                            I_InvoiceDate = reader["InvoiceDate"].ToString(),
                            I_PaymentStatus = reader["PaymentStatus"].ToString()
                        };

                        InvoicesList.Add(Invoice);
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

        public Response UpdateInvoiceDetails(GetInvoicesModal updateInvoice)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                // Construct the UPDATE query
                string updateQuery = @"UPDATE Invoices
                               SET PaymentStatus = '" + updateInvoice.I_PaymentStatus + @"',
                                   InvoiceDate = '" + updateInvoice.I_InvoiceDate + @"',
                                   TotalAmount = '" + updateInvoice.I_TotalAmount + @"'
                               WHERE InvoiceID = '" + updateInvoice.I_InvoiceID + @"'";  // Update condition on InvoiceID

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))  // Assuming AddEditDel is responsible for executing the query
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!!";  // Return success response
                    }
                    else
                    {
                        res.StatusCode = 500;
                        res.Result = "Failed to update invoice details.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);  // Log errors
                res.StatusCode = 500;
                res.Result = "Failed!!";
            }
            return res;
        }
    }
}