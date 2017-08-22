using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Services
{
    public static class ModelConverter
    {
        public static ProductItem ToDto(this Product product)
        {
            return new ProductItem
            {
                Id = product.ID,
                Name = product.Name
            };
        }

        public static TypeItem ToDto(this Type type)
        {
            return new TypeItem
            {
                Id = type.ID,
                Name = type.Name
            };
        }

        public static PurchaseItem ToDto(this Purchase purchase)
        {
            return new PurchaseItem
            {
                Id = purchase.ID,
                Product = purchase.Product1.ToDto(),
                Type = purchase.Type1.ToDto(),
                Date = purchase.Date1.Date1,
                Price = purchase.Price,
                Quantity = purchase.Quantity
            };
        }
    }
}
