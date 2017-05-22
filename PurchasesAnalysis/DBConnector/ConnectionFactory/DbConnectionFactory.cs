using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBConnector.ConnectionFactory
{
    /// <summary>
    /// Factory for db connections.
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Gets opened db connection.
        /// </summary>
        /// <returns>DB connection.</returns>
        public IDbConnection GetOpenedDbConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}
