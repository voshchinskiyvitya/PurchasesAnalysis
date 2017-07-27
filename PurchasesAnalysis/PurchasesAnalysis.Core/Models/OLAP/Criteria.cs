using System;

namespace PurchasesAnalysis.Core.Models.OLAP
{
    public class Criteria<T>: ICriteria
    {
        private readonly string _columnName;
        private T _value;

        public Criteria(string columnName)
        {
            _columnName = columnName;
        }

        public object Value
        {
            get { return _value; }
            set
            {
                if (value is T)
                {
                    _value = (T) value;
                }
                else
                {
                    throw new ArgumentException("Value type should be " + typeof (T));
                }
            }
        }
    }
}
