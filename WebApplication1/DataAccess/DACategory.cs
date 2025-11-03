using biZTrack.Static;
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
    public class DACategory : ICategory
    {



        public Response GetAllCategories()
        {
            Response res = new Response();
            List<GetCategoryModal> CategoryList = new List<GetCategoryModal>();

            // SQL query to fetch only active categories
            string Query = @"SELECT 
                        CategoryID, 
                        CategoryName 
                     FROM 
                        Category
                     WHERE 
                        Status = 'A'";   // <-- Only active categories

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {
                        // Creating a Category object to hold the data
                        GetCategoryModal category = new GetCategoryModal
                        {
                            C_CategoryID = reader["CategoryID"].ToString(),
                            C_CategoryName = reader["CategoryName"].ToString()
                        };

                        // Adding the category to the list
                        CategoryList.Add(category);
                    }
                }
            }

            res.StatusCode = 200;
            res.ResultSet = CategoryList;
            return res;
        }




        public Response AddCategoriesDetails(GetCategoryModal addCategory)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                // Constructing the SQL Insert Query with CategoryName and default Status = 'A'
                string Query = "INSERT INTO Category " +
                               "(CategoryName, Status) " +  // Include Status column
                               "VALUES('" + addCategory.C_CategoryName + "', 'A')"; // Default Status = 'A'

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query)) // Assuming AddEditDel executes the query
                    {
                        res.StatusCode = 200;
                        res.Result = "Category Added Successfully!!";
                    }
                    else
                    {
                        res.StatusCode = 400;
                        res.Result = "Failed to Add Category!!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Error occurred while adding category!!";
            }
            return res;
        }





        public Response PutCategoriesDetails(GetCategoryModal updateCategory)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                // Constructing the SQL Update Query to update the Category Name
                string updateQuery = @"UPDATE Category
                               SET CategoryName = '" + updateCategory.C_CategoryName + @"'
                               WHERE CategoryID = '" + updateCategory.C_CategoryID + @"'";

                // Using the DBconnect object to execute the query
                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))  // Assuming AddEditDel executes the query
                    {
                        res.StatusCode = 200;
                        res.Result = "Category updated successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed to update category!";
            }
            return res;
        }

        public Response DeleteCategoriesDetails(GetCategoryModal addCategory)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE Category
                                SET Status = 'I'
                                WHERE CategoryID = '" + addCategory.C_CategoryID + @"'";

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!! Category Inactivated.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed!! Inactivation of category failed.";
            }
            return res;
        }
























    }
}