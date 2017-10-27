using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Services
{
    public interface IAnalysisService
    {
        //IList<AnalysisResult<TKey, TValue>> Analyse<TKey, TValue>(
        //    IList<Expression<Func<Purchase, bool>>> filters,
        //    Expression<Func<Purchase, AnalysisResult<TKey, TValue>>> select,
        //    Func<IGrouping<TKey, AnalysisResult<TKey, TValue>>, AnalysisResult<TKey, TValue>> aggregateFunction);

        //IQueryable<Purchase> AddFilter(IQueryable<Purchase> items, Expression<Func<Purchase, bool>> filters);

        //IList<AnalysisResult<TKey, TValue>> AddAggregation<TKey, TValue>(
        //    IList<AnalysisResult<TKey, TValue>> items,
        //    Func<IGrouping<TKey, AnalysisResult<TKey, TValue>>, AnalysisResult<TKey, TValue>> aggregateFunction);

        //IQueryable<IGrouping<TGroupKey, Purchase>> AddGrouping<TGroupKey>(IQueryable<Purchase> items, Expression<Func<Purchase, TGroupKey>> groupClause);
    }
}
