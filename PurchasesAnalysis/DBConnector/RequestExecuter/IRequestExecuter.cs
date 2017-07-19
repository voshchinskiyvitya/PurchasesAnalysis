using System.Collections.Generic;
using System.Data;

namespace DBConnector.RequestExecuter
{
    /// <summary>
    /// Represents sql executer.
    /// </summary>
    public interface IRequestExecuter
    {
        /// <summary>
        /// Executes simple sql select.
        /// </summary>
        /// <param name="selectStatement">Select statement.</param>
        /// <returns>DataTable result.</returns>
        DataTable ExecuteSelect(string selectStatement);

        /// <summary>
        /// Executes stored procedure.
        /// </summary>
        /// <param name="procedureName">Name of stored procedure.</param>
        /// <param name="parameters">Procedure parameters.</param>
        void ExecuteProcedure(string procedureName, IDictionary<string, object> parameters);
    }
}
