using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Model;

namespace TimeManagerZM.Data
{
    public class ActivityTypeRepository
    {
        private string connectionString;

        public ActivityTypeRepository()
        {
            string databasePath = @"C:\\Users\\zabro\\source\\repos\\TimeManagerZM\\TimeManagerZM\\Data\\timemanager.db";
            connectionString = $"Data Source={databasePath};Version=3;";
        }

        // Метод для добавления нового типа активности
        public void AddActivityType(ActivityType activityType)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO ActivityTypes (TypeName, UserId) VALUES (@TypeName, @UserId)";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@TypeName", activityType.TypeName);
                command.Parameters.AddWithValue("@UserId", activityType.UserId);

                command.ExecuteNonQuery();
            }
        }

        // Метод для получения всех типов активности
        public List<ActivityType> GetAllActivityTypes()
        {
            List<ActivityType> activityTypes = new List<ActivityType>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ActivityTypes";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        activityTypes.Add(new ActivityType
                        {
                            Id = reader.GetInt32(0),
                            TypeName = reader.GetString(1),
                            UserId = reader.GetInt32(2)
                        });
                    }
                }
            }

            return activityTypes;
        }

        // Метод для получения типа активности по его Id
        public ActivityType GetActivityTypeById(int id)
        {
            ActivityType activityType = null;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ActivityTypes WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        activityType = new ActivityType
                        {
                            Id = reader.GetInt32(0),
                            TypeName = reader.GetString(1),
                            UserId = reader.GetInt32(2)
                        };
                    }
                }
            }

            return activityType;
        }

        // Метод для обновления типа активности
        public void UpdateActivityType(ActivityType activityType)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE ActivityTypes SET TypeName = @TypeName, UserId = @UserId WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@TypeName", activityType.TypeName);
                command.Parameters.AddWithValue("@UserId", activityType.UserId);
                command.Parameters.AddWithValue("@Id", activityType.Id);

                command.ExecuteNonQuery();
            }
        }

        // Метод для удаления типа активности
        public void DeleteActivityType(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM ActivityTypes WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
