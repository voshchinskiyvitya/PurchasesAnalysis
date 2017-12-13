using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Reflection;
using PurchasesAnalysis.Core.ContextProvider;
using PurchasesAnalysis.Core.Extentions;
using PurchasesAnalysis.Core.Models;
using Type = System.Type;

namespace PurchasesAnalysis.Core.Services
{
    public class AnalysisService: IAnalysisService
    {
        private readonly IDbContextProvider _contextProvider;

        public AnalysisService(IDbContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public IList<AnalysisResult> Analyse(
            IList<Expression<Func<Purchase, bool>>> filters,
            Constants.Dimentions argument, Constants.Facts value, Constants.Aggregation aggregation
            )
        {
            using (var context = _contextProvider.GetContext())
            {
                var items = context.Purchases.AsQueryable();
                foreach (var f in filters)
                {
                    items = AddFilter(items, f);
                }

                var argProperty = argument == Constants.Dimentions.Date ? "Date1" : "Name";

                var result = items.Select($"new(it.{argument.GetDescription()}.{argProperty}, it as Purchase)");
                result = result.GroupBy(argProperty, "Purchase");
                result = result.Select($"new(Key, {aggregation.GetDescription()}({value.GetDescription()}) as Value)");

                var list = new List<AnalysisResult>();
                foreach (var item in result)
                {
                    var dyn = (dynamic) item;
                    list.Add(new AnalysisResult
                    {
                        Key = dyn.Key,
                        Value = dyn.Value
                    });
                }

                return list;
            }
        } 

        private IQueryable<Purchase> AddFilter(IQueryable<Purchase> items, Expression<Func<Purchase, bool>> filter)
        {
            return items.Where(filter);
        }
    }
}
