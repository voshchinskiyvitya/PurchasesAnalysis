using System;

namespace PurchasesAnalysis.Core.Models
{
    public class PurchaseItem
    {
        /// <summary>
        /// Unique Id for purchase item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Purchased item.
        /// </summary>
        public ProductItem Product { get; set; }

        /// <summary>
        /// Type of purchased items.
        /// </summary>
        public TypeItem Type { get; set; }

        /// <summary>
        /// Purchase date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Purchase items price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Purchase items quantity.
        /// </summary>
        public int Quantity { get; set; }
    }
}
