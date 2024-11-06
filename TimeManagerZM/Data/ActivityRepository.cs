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

        public async Task AddActivity(MyActivity activity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Activity (ActivityName, StartTime, EndTime, ActivityTypeId, UserId) " +
                               "VALUES (@ActivityName, @StartTime, @EndTime, @ActivityTypeId, @UserId)";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@EndTime", activity.EndTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@ActivityTypeId", activity.ActivityTypeId);
                command.Parameters.AddWithValue("@UserId", activity.UserId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<MyActivity>> GetAllActivitiesByUserId(int userId)
        {
            List<MyActivity> activities = new List<MyActivity>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Activity WHERE UserId = @userId";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@userId", userId);

                using (var reader = await command.ExecuteReaderAsync())
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

        public async Task<MyActivity> GetActivityById(int id)
        {
            MyActivity activity = null;

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Activity WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
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

        public async Task UpdateActivity(MyActivity activity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Activity SET ActivityName = @ActivityName, StartTime = @StartTime, " +
                               "EndTime = @EndTime, ActivityTypeId = @ActivityTypeId, UserId = @UserId WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                command.Parameters.AddWithValue("@StartTime", activity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@EndTime", activity.EndTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@ActivityTypeId", activity.ActivityTypeId);
                command.Parameters.AddWithValue("@UserId", activity.UserId);
                command.Parameters.AddWithValue("@Id", activity.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteActivity(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Activity WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
