using System.Collections.Generic;

namespace PurchasesAnalysis.Core.Models.OLAP.Dimentions
{
    public class Dimention: IDimention
    {
        private readonly string _tableName;

        public Dimention(string tableName, IList<ICriteria> criteries)
        {
            _tableName = tableName;
            Criteries = criteries;
        }

        public IList<ICriteria> Criteries { get; }
    }
}
