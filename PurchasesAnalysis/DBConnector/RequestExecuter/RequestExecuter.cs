using System.Data;
using System.Data.SqlClient;
using DBConnector.ConnectionFactory;

namespace DBConnector.RequestExecuter
{
    /// <summary>
    /// Represents sql executer.
    /// </summary>
    public class RequestExecuter : IRequestExecuter
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RequestExecuter(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Executes simple sql select.
        /// </summary>
        /// <param name="selectStatement">Select statement.</param>
        /// <returns>DataTable result.</returns>
        public DataTable ExecuteSelect(string selectStatement)
        {
            var selectResult = new DataTable();
            using (var connection = _connectionFactory.GetOpenedDbConnection())
            {
                var adapter = new SqlDataAdapter(selectStatement, (SqlConnection) connection);
                adapter.Fill(selectResult);
            }

            return selectResult;
        }
    }
}
