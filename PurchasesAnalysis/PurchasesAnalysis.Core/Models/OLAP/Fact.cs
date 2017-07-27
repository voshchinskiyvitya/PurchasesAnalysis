using System;

namespace PurchasesAnalysis.Core.Models.OLAP
{
    public class Fact<T>: IFact
    {
        private readonly string _columtName;
        private T _value;

        public Fact(string columtName)
        {
            _columtName = columtName;
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
