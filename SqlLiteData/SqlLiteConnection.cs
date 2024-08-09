using Microsoft.Maui.Controls;
using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Reflection;

namespace EconomizzeHybrid.SqlLiteData
{
    public class SqlLiteConnection : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _dbConnection;
        private readonly string _assemblyDirectory;
        private readonly string _dbPath;
        private readonly string _relativePath;

        public SqlLiteConnection()
        {
            _assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _relativePath = "SqlLiteData\\UserInfo.db";
            _dbPath = Path.Combine(_assemblyDirectory, _relativePath);
            _connectionString = $"Data Source={_dbPath}";
            _dbConnection = new SqliteConnection(_connectionString);
        }

        public IDbConnection GetConnection()
        {
            return _dbConnection;
        }
    }
}
