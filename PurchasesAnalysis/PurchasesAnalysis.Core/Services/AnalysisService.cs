using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PurchasesAnalysis.Core.ContextProvider;
using PurchasesAnalysis.Core.Models;

namespace PurchasesAnalysis.Core.Services
{
    public class AnalysisService: IAnalysisService
    {
        private readonly IDbContextProvider _contextProvider;

        public AnalysisService(IDbContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public IList<AnalysisResult<TKey, TValue>> Analyse<TKey, TValue>(
            IList<Expression<Func<Purchase, bool>>> filters, 
            Expression<Func<Purchase, AnalysisResult<TKey, TValue>>> select,
            Func<IGrouping<TKey, AnalysisResult<TKey, TValue>>, AnalysisResult<TKey, TValue>> aggregateFunction
            )
        {
            using (var context = _contextProvider.GetContext())
            {
                var items = context.Purchases.AsQueryable();
                foreach (var f in filters)
                {
                    items = AddFilter(items, f);
                }


                IList<AnalysisResult<TKey, TValue>> result = items.Select(select).ToList();
                result = AddAggregation(result, aggregateFunction);

                return result.ToList();
            }
        }

        public IQueryable<Purchase> AddFilter(IQueryable<Purchase> items, Expression<Func<Purchase, bool>> filter)
        {
            return items.Where(filter);
        }

        public IList<AnalysisResult<TKey, TValue>> AddAggregation<TKey, TValue>(IList<AnalysisResult<TKey, TValue>> items, Func<IGrouping<TKey, AnalysisResult<TKey, TValue>>, AnalysisResult<TKey, TValue>> aggregateFunction)
        {
            return items
                .GroupBy(i => i.Key)
                .Select(aggregateFunction)
                .ToList();
        }

        public IQueryable<IGrouping<TGroupKey, Purchase>> AddGrouping<TGroupKey>(IQueryable<Purchase> items, Expression<Func<Purchase, TGroupKey>> groupClause)
        {
            return items.GroupBy(groupClause);
        }
    }
}
