using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasesAnalysis.Core.Models.OLAP
{
    public class FactsTable
    {
        private readonly string _tableName;

        public FactsTable(string tableName, IList<IFact> facts)
        {
            _tableName = tableName;
            Facts = facts;
        }

        public IList<IFact> Facts { get; }
    }
}
