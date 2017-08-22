using System;
using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.ContextProvider
{
    public class DbContextProvider: IDbContextProvider
    {
        public PurchasesEntities GetContext()
        {
            return new PurchasesEntities();
        }
    }
}
