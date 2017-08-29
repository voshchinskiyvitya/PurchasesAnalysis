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

        public IList<AnalysisResult<TKey, TValue>> Analyse<TKey, TValue>(//TGroupKey
            IList<Expression<Func<Purchase, bool>>> filters, 
            //Expression<Func<Purchase, TGroupKey>> groupClause,
            //Expression<Func<IQueryable<AnalysisResult<TKey, TValue>>, Func<TValue, TValue>, AnalysisResult<TKey, TValue>>> aggregateFunction, 
            Expression<Func<Purchase, AnalysisResult<TKey, TValue>>> select)
        {
            using (var context = _contextProvider.GetContext())
            {
                //IList<AnalysisResult<TKey, TValue>> result;
                var items = context.Purchases.AsQueryable();
                filters.Aggregate(items, AddFilter);

                //if (groupClause != null)
                //{
                //    result = AddGrouping(items, groupClause)
                //        .Select(i => i.AsQueryable().Select(select).Select(r => new AnalysisGroupingResult<TKey, TValue, TGroupKey>(r, i.Key))).ToList();
                //    return result;
                //}

                var result = items.Select(select);
                return result.ToList();
            }
        }

        public IQueryable<Purchase> AddFilter(IQueryable<Purchase> items, Expression<Func<Purchase, bool>> filter)
        {
            return items.Where(filter);
        }

        public IQueryable<IGrouping<TGroupKey, Purchase>> AddGrouping<TGroupKey>(IQueryable<Purchase> items, Expression<Func<Purchase, TGroupKey>> groupClause)
        {
            return items.GroupBy(groupClause);
        }
    }
}
