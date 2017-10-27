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
                Product = purchase.Product.ToDto(),
                Type = purchase.Type.ToDto(),
                Date = purchase.Date.Date1,
                Price = purchase.Price,
                Quantity = purchase.Quantity
            };
        }
    }
}
