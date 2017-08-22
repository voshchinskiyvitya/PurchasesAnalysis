using System.Collections.Generic;
using System.Linq;
using DBConnector.RequestExecuter;
using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Repositories
{
    public class PurchasesRepository: IPurchasesRepository
    {
        private readonly IRequestExecuter _requestExecuter;

        public PurchasesRepository(IRequestExecuter requestExecuter)
        {
            _requestExecuter = requestExecuter;
        }

        public void Save(PurchaseItem purchase)
        {
            using (var context = new PurchasesEntities())
            {
                context.Create_Purchase(purchase.Date, purchase.Product.Name, purchase.Type.Name, purchase.Price, purchase.Quantity);
            }
        }

        public PurchaseItem Get(int id)
        {
            using (var context = new PurchasesEntities())
            {
                var purchase = context.Purchases.FirstOrDefault(p => p.ID == id);

                if (purchase == null)
                    return null;

                return new PurchaseItem
                {
                    Id = purchase.ID,
                    Type = new TypeItem
                    {
                        Id = purchase.Type1.ID,
                        Name = purchase.Type1.Name
                    },
                    Product = new ProductItem
                    {
                        Id = purchase.Product1.ID,
                        Name = purchase.Product1.Name
                    },
                    Date = purchase.Date1.Date1
                };
            }
        }

        public IList<PurchaseItem> GetAll()
        {
            using (var context = new PurchasesEntities())
            {
                var purchases = context.Purchases;

                return purchases?.Select(purchase => new PurchaseItem
                {
                    Id = purchase.ID,
                    Type = new TypeItem
                    {
                        Id = purchase.Type1.ID,
                        Name = purchase.Type1.Name
                    },
                    Product = new ProductItem
                    {
                        Id = purchase.Product1.ID,
                        Name = purchase.Product1.Name
                    },
                    Date = purchase.Date1.Date1,
                    Price = purchase.Price,
                    Quantity = purchase.Quantity
                }).ToList();
            }
        }
    }
}
