using System.Collections.Generic;

namespace PurchasesAnalysis.Core.Models
{
    public class Constants
    {
        public class Dimentions
        {
            public const string Type = "Type";
            public const string Product = "Product";
            public const string Date = "Date";

            public static readonly IList<string> All = new List<string> { Product, Type, Date };
        }

        public class Facts
        {
            public const string Price = "Price";
            public const string Quantity = "Quantity";

            public static readonly IList<string> All = new List<string> { Price, Quantity };
        }

        public class Aggregation
        {
            public const string Sum = "Sum";
            public const string Average = "Average";
            public const string Max = "Max";
            public const string Min = "Min";

            public static readonly IList<string> All = new List<string> { Sum, Average, Max, Min};
        }
    }
}
