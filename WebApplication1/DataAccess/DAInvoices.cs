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
    }
}