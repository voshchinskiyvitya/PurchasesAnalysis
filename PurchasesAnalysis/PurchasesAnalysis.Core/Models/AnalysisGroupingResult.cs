namespace PurchasesAnalysis.Core.Models
{
    public class AnalysisGroupingResult<TKey, TValue, TGrouping> : AnalysisResult<TKey, TValue>
    {
        public AnalysisGroupingResult(AnalysisResult<TKey, TValue> analysisResult, TGrouping grouping)
        {
            Key = analysisResult.Key;
            Value = analysisResult.Value;
            Grouping = grouping;
        }

        public TGrouping Grouping { get; set; }
    }
}
