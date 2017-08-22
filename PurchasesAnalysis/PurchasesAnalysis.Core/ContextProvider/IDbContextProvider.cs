using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.ContextProvider
{
    public interface IDbContextProvider
    {
        PurchasesEntities GetContext();
    }
}
