using System.Collections.Generic;
using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Repositories
{
    public interface IPurchasesRepository
    {
        void Save(PurchaseItem purchase);

        PurchaseItem Get(int id);

        IList<PurchaseItem> GetAll();
    }
}
