using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TimeManagerZM.Model;
using TimeManagerZM.Services;

namespace TimeManagerZM.Data
{
    public class UserRepository
    {
        private string connectionString;

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public UserRepository()
        {
            string databasePath = @"C:\\Users\\zabro\\source\\repos\\TimeManagerZM\\TimeManagerZM\\Data\\timemanager.db";
            connectionString = $"Data Source={databasePath};Version=3;";
        }

        public void AddUser(User user)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO User (UserName, Password) VALUES (@UserName, @Password)";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                string hashedPassword = _passwordHasher.HashPassword(user.Password);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", hashedPassword);

                command.ExecuteNonQuery();
            }
        }

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

                        if (_passwordHasher.VerifyPassword(password, user.Password))
                        {
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE User SET UserName = @UserName, Password = @Password WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                string hashedPassword = _passwordHasher.HashPassword(user.Password);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                command.Parameters.AddWithValue("@Id", user.Id);

                command.ExecuteNonQuery();
            }
        }

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
