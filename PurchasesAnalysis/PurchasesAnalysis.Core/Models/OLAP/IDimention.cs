using System.Collections.Generic;

namespace PurchasesAnalysis.Core.Models.OLAP
{
    public interface IDimention
    {
        IList<ICriteria> Criteries { get; }
    }
}
