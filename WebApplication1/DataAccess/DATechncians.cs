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
    public class DATechncians : ITechnicians
    {
        public Response DeleteTechnicianDetails(GetTechniciansModal technician)
        {
            Response res = new Response();
            DBconnect dbConnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE Technicians
                                SET Status = 'I'
                                WHERE TechnicianID = '" + technician.TechnicianID + @"'";

                using (var dbConnectInstance = new DBconnect())

                {
                    if (dbConnectInstance.AddEditDel(updateQuery))
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!! Technician status updated to inactive.";
                    }
                    else
                    {
                        res.StatusCode = 400;
                        res.Result = "Failed to update technician status.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Error occurred while updating technician status.";
            }
            return res;
        }

        public Response getAllTechnicians()
        {
            Response res = new Response();
            List<GetTechniciansModal> TechnicianList = new List<GetTechniciansModal>();

            string Query = "SELECT " +
                                "TechnicianID, " +
                                "FullName, " +
                                "Phone, " +
                                "Email, " +
                                "Address, " +
                                "NIC, " +
                                "Status " +
                            "FROM " +
                                "Technicians";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {
                        // Create a new Technician object to hold the data
                        GetTechniciansModal Technician = new GetTechniciansModal
                        {
                            TechnicianID = reader["TechnicianID"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            NIC = reader["NIC"].ToString(),
                            Status = reader["Status"].ToString()
                        };

                        // Add the technician to the list
                        TechnicianList.Add(Technician);
                    }
                }
            }

            // Setting response details
            res.StatusCode = 200;
            res.ResultSet = TechnicianList;

            return res;
        }

        public Response UpdateTechnicianDetails(GetTechniciansModal updateTechnician)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                // Constructing the SQL Update Query to update the technician details
                string updateQuery = @"UPDATE Technicians
                               SET FullName = '" + updateTechnician.FullName + @"', 
                                   Phone = '" + updateTechnician.Phone + @"', 
                                   Email = '" + updateTechnician.Email + @"', 
                                   Address = '" + updateTechnician.Address + @"', 
                                   NIC = '" + updateTechnician.NIC + @"', 
                                   Status = '" + updateTechnician.Status + @"'
                               WHERE TechnicianID = '" + updateTechnician.TechnicianID + @"'";

                // Using the DBconnect object to execute the query
                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))  // Assuming AddEditDel executes the query
                    {
                        res.StatusCode = 200;
                        res.Result = "Technician details updated successfully!";
                    }
                    else
                    {
                        res.StatusCode = 400;
                        res.Result = "Failed to update technician details!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "An error occurred while updating technician details!";
            }
            return res;
        }




    }
}