using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Services
{
    public interface IAnalysisService
    {
        IList<AnalysisResult<TKey, TValue>> Analyse<TKey, TValue, TGroupKey>(
            IList<Expression<Func<Purchase, bool>>> filters,
            Expression<Func<Purchase, TGroupKey>> groupClause,
            Expression<Func<IGrouping<TGroupKey, Purchase>, AnalysisResult<TKey, TValue>>> aggregateFunction,
            Expression<Func<Purchase, AnalysisResult<TKey, TValue>>> select);

        IQueryable<Purchase> AddFilter(IQueryable<Purchase> items, Expression<Func<Purchase, bool>> filters);

        IQueryable<IGrouping<TGroupKey, Purchase>> AddGrouping<TGroupKey>(IQueryable<Purchase> items, Expression<Func<Purchase, TGroupKey>> groupClause);
    }
}
