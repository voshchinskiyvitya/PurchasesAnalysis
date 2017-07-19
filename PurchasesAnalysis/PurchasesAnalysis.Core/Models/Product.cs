namespace PurchasesAnalysis.Core.Models
{
    public class Product
    {
        /// <summary>
        /// Product unique Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity of purchased items.
        /// </summary>
        public int Quantity { get; set; }
    }
}
