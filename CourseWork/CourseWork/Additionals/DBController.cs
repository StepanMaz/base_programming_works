using System.Data;
using System.Data.SqlClient;

namespace CourseWork
{
    static class DBController
    {
        private static SqlConnection sqlConnection;
        static DBController()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TouristAgency;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public static DataTable GetTable(string command)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(command, sqlConnection));
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            return table;
        }

        public static T GetObject<T>(string command)
        {
            return (T)new SqlCommand(command, sqlConnection).ExecuteScalar();
        }
    }
}
