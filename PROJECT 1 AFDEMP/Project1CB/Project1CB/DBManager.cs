using System;
using System.Data.SqlClient;

namespace Project1CB
{
    class DBManager
    {
        public string ConnectionString;
        public SqlConnection SqlConnection;

        public DBManager()
        {
            ConnectionString = "Server=MAKKO\\SQLEXPRESS;Database = Project1DB;Integrated Security=SSPI;";
            SqlConnection = new SqlConnection(ConnectionString);
        }

        public bool CheckUser(string username)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE Username = '{username}';", SqlConnection);

            bool result;
            if (command.ExecuteScalar() == null)
                result = false;
            else
                result = true;

            SqlConnection.Close();

            return result;
        }

        public User CheckUser(string username, string password)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}';", SqlConnection);

            var result = command.ExecuteReaderAsync().GetAwaiter().GetResult();

            User user = null;
            if (result.Read())
                user = new User(username, (UserType) result.GetValue(5));

            SqlConnection.Close();

            return user;
        }

        //INSERT
        public void InsertUser(string username, string name, string surname, string password, int privileges)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"INSERT INTO Users (Username, Name, Surname, Password, Type) VALUES ('{username}', '{name}', '{surname}', '{password}', {privileges});", SqlConnection);
            command.ExecuteNonQuery();

            SqlConnection.Close();
        }

        public void InsertMessage(string sender, string receiver, string data)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"INSERT INTO Messages (Date, Sender, Receiver, Data) VALUES ('{DateTime.Now}', '{sender}', '{receiver}', '{data}');", SqlConnection);
            command.ExecuteNonQuery();

            SqlConnection.Close();
        }

        //VIEW
        public void ViewUsers()
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Users;", SqlConnection);

            var result = command.ExecuteReaderAsync().GetAwaiter().GetResult();

            Console.WriteLine("Username, Name, Surname, Privileges");
            while (result.Read())
                Console.WriteLine(result.GetValue(1) + " " + result.GetValue(2) + " " + result.GetValue(3) + " " + (UserType) result.GetValue(5));

            SqlConnection.Close();
        }

        public void ViewData(string user1, string user2)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"SELECT * FROM Messages WHERE (Sender = '{user1}' AND Receiver = '{user2}') OR (Sender = '{user2}' AND Receiver = '{user1}');", SqlConnection);

            var result = command.ExecuteReaderAsync().GetAwaiter().GetResult();

            Console.WriteLine("MessageID, Sender, Receiver, Message, Date");
            while (result.Read())
                Console.WriteLine(result.GetValue(0) + ") " + result.GetValue(2)+ " " + result.GetValue(3) + " " + result.GetValue(4) + " " + ((DateTime) result.GetValue(1)).ToString("dd/MM/yyyy"));

            SqlConnection.Close();
        }
        
        //UPDATE
        public void UpdateMessage(string messageID, string data)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"UPDATE Messages SET Data = '{data}' WHERE MessageID = {messageID};", SqlConnection);
            command.ExecuteNonQuery();

            SqlConnection.Close();
        }

        public void UpdateUser(string username, string password, int privileges)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"UPDATE Users SET Password = '{password}', Type = {privileges} WHERE Username = '{username}';", SqlConnection);
            command.ExecuteNonQuery();

            SqlConnection.Close();
        }

        //DELETE
        public void DeleteUser(string username)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand($"DELETE FROM Users WHERE Username = '{username}';", SqlConnection);
            command.ExecuteNonQuery();

            command = new SqlCommand($"DELETE FROM Messages WHERE Sender = '{username}' OR Receiver = '{username}';", SqlConnection);
            command.ExecuteNonQuery();

            SqlConnection.Close();
        }

        public void DeleteMessage(string messageID)
        {
            SqlConnection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Messages WHERE MessageID =  @MessageID", SqlConnection);
            command.Parameters.AddWithValue("MessageID", messageID);
            command.ExecuteNonQuery();

            SqlConnection.Close();
        }
    }
}