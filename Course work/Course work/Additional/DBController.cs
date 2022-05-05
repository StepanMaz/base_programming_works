using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Additional
{
    public static class DBController
    {
        public static SqlConnection sqlConnection;
        static DBController()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TouristAgency;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public static DataTable GetTable(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            return table;
        }

        public static object GetObject(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            return sqlCommand.ExecuteScalar();
        }
    }
}
