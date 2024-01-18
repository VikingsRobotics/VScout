using Joe.Common.DataAccess;

namespace VScoutCentral.Services
{
    public class DatabaseSchemaService : IDatabaseSchemaService
    {
        public void DropDatabase()
        {
            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            File.Delete(databaseFilePath);
        }

        public async Task CreateDatabaseAsync()
        {
            using var connection = GetOpenConnection();
            await connection.ExecuteNonQueryAsync(Properties.Resources.DbCreateScript);
        }

        private JoeSqliteConnection GetOpenConnection()
        {
            var connection = new JoeSqliteConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        private string GetConnectionString()
        {
            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            string connectionString = $"Data Source={databaseFilePath}";

            return connectionString;
        }
    }
}
