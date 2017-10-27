using System.Collections.Generic;
using System.ComponentModel;
using PurchasesAnalysis.Core.Extentions;

namespace PurchasesAnalysis.Core.Models
{
    public class Constants
    {
        public enum Dimentions
        {
            [Description("Type")]
            Type,
            [Description("Product")]
            Product,
            [Description("Date")]
            Date
        }

        public static readonly IList<string> AllDimentions = new List<string>
        {
            Dimentions.Type.GetDescription(),
            Dimentions.Product.GetDescription(),
            Dimentions.Date.GetDescription()
        };

        public enum Facts
        {
            [Description("Price")]
            Price,
            [Description("Quantity")]
            Quantity
        }

        public static readonly IList<string> AllFacts = new List<string>
        {
            Facts.Price.GetDescription(),
            Facts.Quantity.GetDescription()
        };

        public enum Aggregation
        {
            [Description("Sum")]
            Sum,
            [Description("Average")]
            Average,
            [Description("Max")]
            Max,
            [Description("Min")]
            Min
        }

        public static readonly IList<string> AllAggregation = new List<string>
        {
            Aggregation.Sum.GetDescription(),
            Aggregation.Average.GetDescription(),
            Aggregation.Max.GetDescription(),
            Aggregation.Min.GetDescription()
        };
    }
}
