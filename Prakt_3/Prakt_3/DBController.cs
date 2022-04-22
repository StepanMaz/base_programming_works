using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Prakt_3
{
    static class DBController
    {
        private static SqlConnection sqlConnection;
        static DBController()
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=AccountSystem;Integrated Security=True");
            sqlConnection.Open();
        }

        public static void ChangeStatus(int id, bool status)
        {
            SqlCommand command = new SqlCommand($"UPDATE [dbo].[User] SET Active = {(status ? 1 : 0)} WHERE Id = {id}", sqlConnection);
            command.ExecuteNonQuery();
        }

        public static bool VerifyUser(string login, string password)
        {
            return (bool)new SqlCommand($"SELECT dbo.ValidateUser('{login}', '{password}')", sqlConnection).ExecuteScalar();
        }

        public static DataTable GetAllUsers()
        {
            SqlCommand command = new SqlCommand("SELECT Id, Login, Name, SecondName, Active FROM [dbo].[User]", sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            return table;
        }

        public static (string, string) GetUserInfo(string login)
        {
            SqlCommand command = new SqlCommand($"SELECT Name, SecondName FROM [dbo].[User] WHERE Login = '{login}'", sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            if (table.Rows.Count == 0)
                return ("", "");
            return ((string)table.Rows[0].ItemArray[0], (string)table.Rows[0].ItemArray[1]);
        }

        public static void ChangePassword(string login, string password)
        {
            new SqlCommand($"EXECUTE dbo.ChangePassword '{login}', '{password}'", sqlConnection).ExecuteNonQuery();
        }

        public static void ChangeInfo(string login, string Name, string SecondName)
        {
            new SqlCommand($"EXECUTE dbo.ChangeInfo '{login}', N'{Name}', N'{SecondName}'", sqlConnection).ExecuteNonQuery();
        }

        public static bool AddUser(string login, string password, string name, string secondName)
        {
            try
            {
                new SqlCommand($"EXECUTE dbo.AddUser '{login}', '{password}', '{name}', '{secondName}'", sqlConnection).ExecuteNonQuery();
                return true;
            }
            catch
            {
                System.Windows.MessageBox.Show("Користувач з таким ім'ям вже існує");
                return false;
            }
        }
    }
}
