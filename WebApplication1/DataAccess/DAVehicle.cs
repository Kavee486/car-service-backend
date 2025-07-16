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
    public class DAVehicle : IVehicle
    {
        public Response getAllVehicles()
        {
            Response res = new Response();
            List<GetVehicleModal> VehicleList = new List<GetVehicleModal>();

            string Query = "SELECT " +
                                "id, " +
                                "color," +
                                "license_plate, " +
                                "make, " +
                                "model, " +
                                "user_id, " +
                                "type, " +
                                "vehicle_no " +
                            "FROM " +
                                "vehicles ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetVehicleModal Vehicle = new GetVehicleModal();


                        Vehicle.V_ID = reader["id"].ToString();
                        Vehicle.V_Color = reader["color"].ToString();
                        Vehicle.V_LicensePlate = reader["license_plate"].ToString();
                        Vehicle.V_Make = reader["make"].ToString();
                        Vehicle.V_Model = reader["model"].ToString();
                        Vehicle.V_UserID = reader["user_id"].ToString();
                        Vehicle.V_Type = reader["type"].ToString();
                        Vehicle.V_No = reader["vehicle_no"].ToString();

                        VehicleList.Add(Vehicle);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = VehicleList;
            return res;
        }
    }
}