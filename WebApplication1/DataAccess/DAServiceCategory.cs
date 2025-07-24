using biZTrack.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Xml.Linq;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DAServiceCategory : IServiceCategory
    {
        

        public Response GetAllServiceCategories()
        {
            Response res = new Response();
            List<GetServiceCategoryModal> ServiceCategoryList = new List<GetServiceCategoryModal>();

            string Query = "SELECT " +
                                "id, " +
                                "name " +
                            "FROM " +
                                "service_categories ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetServiceCategoryModal service = new GetServiceCategoryModal
                        {
                            SC_ID = reader["id"].ToString(),
                            SC_Name = reader["name"].ToString(),

                        };


                        ServiceCategoryList.Add(service);
                    }

                }
            }
            res.StatusCode = 200;
            res.ResultSet = ServiceCategoryList;
            return res;


        }

        public Response GetAllServiceCategoriesById(string id)
        {


            Response res = new Response();
            List<GetServiceCategoryModal> ServiceCategoryList = new List<GetServiceCategoryModal>();

            string Query = "SELECT " +
                                    "id, " +
                                    "name " +
                            "FROM " +
                                     "service_categories " +
                            "WHERE " +
                                     "id = '" + id + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetServiceCategoryModal service = new GetServiceCategoryModal
                        {
                            SC_ID = reader["id"].ToString(),
                            SC_Name = reader["name"].ToString()
                        };

                        ServiceCategoryList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = ServiceCategoryList;
            return res;
        }




        public Response AddServiceCategoryDetails(GetServiceCategoryModal addServiceCategory)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO service_categories " +
                               "(" +
                                   "name" +
                               ") " +
                               "VALUES('" + addServiceCategory.SC_Name + "')";


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