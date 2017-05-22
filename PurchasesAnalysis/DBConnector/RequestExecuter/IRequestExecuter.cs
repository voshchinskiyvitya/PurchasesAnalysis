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
    }
}
