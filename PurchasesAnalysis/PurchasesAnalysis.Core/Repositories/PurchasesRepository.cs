using System.Collections.Generic;
using System.Linq;
using PurchasesAnalysis.Core.ContextProvider;
using PurchasesAnalysis.Core.Models;
using PurchasesAnalysis.Core.Services;

namespace PurchasesAnalysis.Core.Repositories
{
    public class PurchasesRepository: IPurchasesRepository
    {
        private readonly IDbContextProvider _contextProvider;

        public PurchasesRepository(IDbContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public void Save(PurchaseItem purchase)
        {
            using (var context = _contextProvider.GetContext())
            {
                context.Create_Purchase(purchase.Date, purchase.Product.Name, purchase.Type.Name, purchase.Price, purchase.Quantity);
            }
        }

        public PurchaseItem Get(int id)
        {
            using (var context = _contextProvider.GetContext())
            {
                var purchase = context.Purchases.FirstOrDefault(p => p.ID == id);

                return purchase?.ToDto();
            }
        }

        public IList<PurchaseItem> GetAll()
        {
            using (var context = _contextProvider.GetContext())
            {
                var purchases = context.Purchases;

                return purchases?.ToList().Select(p => p.ToDto()).ToList();
            }
        }
    }
}
