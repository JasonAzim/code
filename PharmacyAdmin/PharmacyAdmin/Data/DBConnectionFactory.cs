using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace PharmacyAdmin.Data
{
    public class DBConnectionFactory 
    {
        private IDbConnection _connection;
        private readonly IOptions<DBConfiguration> _configs;

        public DBConnectionFactory(IOptions<DBConfiguration> Configs)
        {
            _configs = Configs;
        }

        public IDbConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_configs.Value.DbConnectionString);
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
