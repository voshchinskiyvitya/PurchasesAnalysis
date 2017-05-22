using System.Data;

namespace DBConnector.ConnectionFactory
{
    /// <summary>
    /// Factory for db connections.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Gets opened db connection.
        /// </summary>
        /// <returns>DB connection.</returns>
        IDbConnection GetOpenedDbConnection();
    }
}
