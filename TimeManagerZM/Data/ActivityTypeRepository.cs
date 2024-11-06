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

        public async Task AddActivityType(ActivityType activityType)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO ActivityTypes (TypeName, ColoActr, UserId) VALUES (@TypeName, @ColorAct, @UserId)";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@TypeName", activityType.TypeName);
                command.Parameters.AddWithValue("@ColorAct", activityType.ColoActr);
                command.Parameters.AddWithValue("@UserId", activityType.UserId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<ActivityType>> GetAllActivityTypesByUserId(int id)
        {
            List<ActivityType> activityTypes = new List<ActivityType>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM ActivityTypes WHERE UserId = @userId";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@userId", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        activityTypes.Add(new ActivityType
                        {
                            Id = reader.GetInt32(0),
                            TypeName = reader.GetString(1),
                            ColoActr = reader.GetString(2),
                            UserId = reader.GetInt32(3)
                        });
                    }
                }
            }

            return activityTypes;
        }

        public async Task UpdateActivityType(ActivityType activityType)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE ActivityTypes SET TypeName = @TypeName, ColoActr = @ColorAct, UserId = @UserId WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@TypeName", activityType.TypeName);
                command.Parameters.AddWithValue("@ColorAct", activityType.ColoActr);
                command.Parameters.AddWithValue("@UserId", activityType.UserId);
                command.Parameters.AddWithValue("@Id", activityType.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteActivityType(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM ActivityTypes WHERE Id = @Id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
