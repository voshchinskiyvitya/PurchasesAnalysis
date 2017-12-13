using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Services
{
    public interface IAnalysisService
    {
        IList<AnalysisResult> Analyse(
            IList<Expression<Func<Purchase, bool>>> filters,
            Constants.Dimentions argument, Constants.Facts value, Constants.Aggregation aggregation
            );
    }
}
