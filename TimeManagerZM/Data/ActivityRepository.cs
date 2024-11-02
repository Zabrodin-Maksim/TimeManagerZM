using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using TimeManagerZM.Model;

namespace TimeManagerZM.Data
{
    public class ActivityRepository
    {
        private string connectionString;

        public ActivityRepository()
        {
            string databasePath = @"C:\\Users\\zabro\\source\\repos\\TimeManagerZM\\TimeManagerZM\\Data\\timemanager.db";

            connectionString = $"Data Source={databasePath};Version=3;";
        }

        // Метод для добавления новой активности
        public void AddActivity(MyActivity activity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Activity (ActivityName, StartTime, EndTime, ActivityTypeId, UserId) " +
                               "VALUES (@ActivityName, @StartTime, @EndTime, @ActivityTypeId, @UserId)";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@EndTime", activity.EndTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@ActivityTypeId", activity.ActivityTypeId);
                command.Parameters.AddWithValue("@UserId", activity.UserId);

                command.ExecuteNonQuery();
            }
        }

        // Метод для получения всех активностей
        public List<MyActivity> GetAllActivities()
        {
            List<MyActivity> activities = new List<MyActivity>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Activity";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        activities.Add(new MyActivity
                        {
                            Id = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            StartTime = DateTime.Parse(reader.GetString(2)),
                            EndTime = reader.IsDBNull(3) ? (DateTime?)null : DateTime.Parse(reader.GetString(3)),
                            ActivityTypeId = reader.GetInt32(4),
                            UserId = reader.GetInt32(5)
                        });
                    }
                }
            }

            return activities;
        }

        // Метод для получения активности по её Id
        public MyActivity GetActivityById(int id)
        {
            MyActivity activity = null;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Activity WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        activity = new MyActivity
                        {
                            Id = reader.GetInt32(0),
                            ActivityName = reader.GetString(1),
                            StartTime = DateTime.Parse(reader.GetString(2)),
                            EndTime = reader.IsDBNull(3) ? (DateTime?)null : DateTime.Parse(reader.GetString(3)),
                            ActivityTypeId = reader.GetInt32(4),
                            UserId = reader.GetInt32(5)
                        };
                    }
                }
            }

            return activity;
        }

        // Метод для обновления активности
        public void UpdateActivity(MyActivity activity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Activity SET ActivityName = @ActivityName, StartTime = @StartTime, " +
                               "EndTime = @EndTime, ActivityTypeId = @ActivityTypeId, UserId = @UserId WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@EndTime", activity.EndTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@ActivityTypeId", activity.ActivityTypeId);
                command.Parameters.AddWithValue("@UserId", activity.UserId);
                command.Parameters.AddWithValue("@Id", activity.Id);

                command.ExecuteNonQuery();
            }
        }

        // Метод для удаления активности
        public void DeleteActivity(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Activity WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
