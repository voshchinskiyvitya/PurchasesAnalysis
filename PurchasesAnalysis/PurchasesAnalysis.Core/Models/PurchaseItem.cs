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
        public Product Product { get; set; }

        /// <summary>
        /// Type of purchased items.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Purchase date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
