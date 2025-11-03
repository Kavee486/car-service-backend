using System;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication1.Database_Layer
{
    internal class DBconnect : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DBconnect()
        {
            //_connectionString = string.Format("Data Source=ito-test020; " +
            //  "Initial Catalog=NOC-OPS;" +
            //  "User id=sa;" +
            //  "Password=admin@123; MultipleActiveResultSets=true;Max Pool Size=600;");


            // _connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=Car_DB; Integrated Security=True; MultipleActiveResultSets=True;";
            // _connectionString = "Data Source=DESKTOP-M4RH8HC\\SQLEXPRESS; Initial Catalog=Car_DB; Integrated Security=True; MultipleActiveResultSets=True;";
            _connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=car; Integrated Security=True; MultipleActiveResultSets=True;";
        }

        public SqlConnection GetOpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        //public SqlDataReader ReadTable(string readStr, SqlParameter sqlParameter)
        //{
        //    SqlConnection connection = GetOpenConnection();
        //    var command = new SqlCommand(readStr, connection);
        //    return command.ExecuteReader(CommandBehavior.CloseConnection);
        //}
        public SqlDataReader ReadTable(string readStr)
        {
            SqlConnection connection = GetOpenConnection();
            var command = new SqlCommand(readStr, connection);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public bool AddEditDel(string AddEditDelStr)
        {
            SqlConnection connection = GetOpenConnection();
            var command = new SqlCommand(AddEditDelStr, connection);
            int affectedRows = command.ExecuteNonQuery();
            connection.Close(); // Close the connection after executing the query
            return affectedRows > 0;
        }


        // <<< Add this method to support fetching a single value >>>
        public object ExecuteScalar(string query)
        {
            object result = null;
            using (SqlConnection connection = GetOpenConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    result = command.ExecuteScalar();
                }
            }
            return result;
        }






        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        //internal void ExecuteQuery(string query)
        //{
        //    throw new NotImplementedException();
        //}

        //internal SqlDataReader ReadTable(string query)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
