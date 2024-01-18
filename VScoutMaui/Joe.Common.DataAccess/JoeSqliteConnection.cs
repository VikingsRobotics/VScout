using Microsoft.Data.Sqlite;
using System.Data;

namespace Joe.Common.DataAccess
{
    public class JoeSqliteConnection : IDisposable
    {
        private bool disposedValue;
        private readonly SqliteConnection _sqliteConnection;

        public JoeSqliteConnection(string connectionString)
        {
            _sqliteConnection = new SqliteConnection(connectionString);
        }

        public void Open()
        {
            _sqliteConnection.Open();
        }

        public async Task<object?> ExecuteScalarAsync(string sql)
        {
            SqliteCommand command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;

            return await command.ExecuteScalarAsync();
        }

        public async Task<object?> ExecuteScalarAsync(string sql, JoeSqlParameterCollection parameterCollection)
        {
            SqliteCommand command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            AddParameters(parameterCollection, command);

            return await command.ExecuteScalarAsync();
        }

        public async Task ExecuteNonQueryAsync(string sql)
        {
            SqliteCommand command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync();
        }

        public async Task ExecuteNonQueryAsync(string sql, JoeSqlParameterCollection parameterCollection)
        {
            SqliteCommand command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            AddParameters(parameterCollection, command);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<DataTable> GetDataTableAsync(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException($"'{nameof(sql)}' cannot be null or whitespace.", nameof(sql));
            }

            SqliteCommand command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;

            DataTable dataTable = new DataTable();

            using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                dataTable.Load(reader);
            }

            return dataTable;
        }

        public async Task<DataTable> GetDataTableAsync(string sql, JoeSqlParameterCollection parameterCollection)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException($"'{nameof(sql)}' cannot be null or whitespace.", nameof(sql));
            }

            SqliteCommand command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            AddParameters(parameterCollection, command);

            DataTable dataTable = new DataTable();

            using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                dataTable.Load(reader);
            }

            return dataTable;
        }

        private static void AddParameters(JoeSqlParameterCollection parameterCollection, SqliteCommand command)
        {
            foreach (JoeSqlParameter p in parameterCollection.Items)
            {
                SqliteParameter param = command.CreateParameter();
                param.ParameterName = p.Name;
                param.Value = p.Value;
                command.Parameters.Add(param);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _sqliteConnection.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}