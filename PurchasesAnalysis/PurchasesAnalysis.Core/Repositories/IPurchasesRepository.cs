using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Repositories
{
    public interface IPurchasesRepository
    {
        void Save(PurchaseItem purchase);
    }
}
