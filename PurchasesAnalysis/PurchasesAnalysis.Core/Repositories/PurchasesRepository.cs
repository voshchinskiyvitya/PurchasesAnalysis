using System.Collections.Generic;
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
            _requestExecuter.ExecuteProcedure("Create_Purchase", new Dictionary<string, object>
            {
                { "@date", purchase.Date },
                { "@name", purchase.Product.Name },
                { "@type", purchase.Type.Name },
                { "@price", purchase.Product.Price }, // TODO: change db column type to decimal.
                { "@quantity", purchase.Product.Quantity },
            });
        }
    }
}
