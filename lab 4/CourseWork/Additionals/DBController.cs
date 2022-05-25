using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

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

        public static bool Update(string table, Dictionary<string, object> values, string condition)
        {
            //try
            //{
                string command = $"UPDATE [dbo].[{table}] SET " +
                    string.Join(", ", values.Select((elem, iter) => $"{elem.Key} = @{iter}"))
                    + $" WHERE {condition}";// value = @0, value = @1, 
                var sqlcommand = new SqlCommand(command, sqlConnection);
                for (int i = 0; i < values.Count; i++)
                {
                    sqlcommand.Parameters.AddWithValue($"@{i}", values.ElementAt(i).Value);
                }
                sqlcommand.ExecuteNonQuery();
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static bool Insert(string table, Dictionary<string, object> values)
        {
            //try
            //{
                string command = $"INSERT INTO [dbo].[{table}] ({string.Join(", ", values.Keys)}) VALUES (" +
                    string.Join(", ", Enumerable.Range(0, values.Count).Select(t => $"@{t}")) + ")"; 
                var sqlcommand = new SqlCommand(command, sqlConnection);
                for (int i = 0; i < values.Count; i++)
                {
                    sqlcommand.Parameters.AddWithValue($"@{i}", values.ElementAt(i).Value);
                }
                sqlcommand.ExecuteNonQuery();
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static bool TryDeleteById(string table, string condiition)
        {
            try
            {
                var command = new SqlCommand($"DELETE FROM [dbo].[{table}] WHERE {condiition}", sqlConnection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
