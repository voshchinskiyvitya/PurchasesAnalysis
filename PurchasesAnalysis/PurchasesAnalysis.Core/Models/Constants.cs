using System.Collections.Generic;

namespace PurchasesAnalysis.Core.Models
{
    public class Constants
    {
        public class Filters
        {
            public const string Type = "Type";
            public const string Product = "Product";
            public const string Date = "Date";

            public static readonly IList<string> All = new List<string> { Product, Type, Date };
        }
    }
}
