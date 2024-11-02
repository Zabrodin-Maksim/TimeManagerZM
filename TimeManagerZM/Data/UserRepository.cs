using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TimeManagerZM.Model;

namespace TimeManagerZM.Data
{
    public class UserRepository
    {
        private string connectionString;

        public UserRepository()
        {
            string databasePath = @"C:\\Users\\zabro\\source\\repos\\TimeManagerZM\\TimeManagerZM\\Data\\timemanager.db";
            connectionString = $"Data Source={databasePath};Version=3;";
        }

        // Метод для добавления нового пользователя
        public void AddUser(User user)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO User (UserName, Password) VALUES (@UserName, @Password)";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);

                command.ExecuteNonQuery();
            }
        }

        // Метод для получения всех пользователей
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM User";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2)
                        });
                    }
                }
            }

            return users;
        }

        // Метод для получения пользователя по его Id
        public User GetUserById(int id)
        {
            User user = null;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM User WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2)
                        };
                    }
                }
            }

            return user;
        }

        public User GetUserByNameAndPassword(string name, string password)
        {
            User user = null;

            using (var connection = new SQLiteConnection(connectionString)) 
            {
                connection.Open();
                string query = "SELECT * FROM User WHERE UserName = @UserName AND Password = @UserPassword";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", name);
                command.Parameters.AddWithValue("@UserPassword", password);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2)
                        };
                    }
                }
            }
            return user;
        }

        // Метод для обновления данных пользователя
        public void UpdateUser(User user)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE User SET UserName = @UserName, Password = @Password WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Id", user.Id);

                command.ExecuteNonQuery();
            }
        }

        // Метод для удаления пользователя
        public void DeleteUser(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM User WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
