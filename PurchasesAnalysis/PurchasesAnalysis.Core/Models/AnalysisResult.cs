namespace PurchasesAnalysis.Core.Models
{
    public class AnalysisResult<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
}
