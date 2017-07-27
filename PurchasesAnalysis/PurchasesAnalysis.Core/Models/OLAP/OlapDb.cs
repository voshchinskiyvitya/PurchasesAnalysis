using System.Collections.Generic;

namespace PurchasesAnalysis.Core.Models.OLAP
{
    public static class OlapDb
    {
        public static FactsTable FactsTable { get; private set; }

        public static IList<IDimention> Dimentions { get; private set; }

        public static void RegisterDbStructure(FactsTable factsTable, IList<IDimention> dimentions)
        {
            FactsTable = factsTable;
            Dimentions = dimentions;
        }
    }
}
