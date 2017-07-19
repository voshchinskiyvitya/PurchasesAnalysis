using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnector.Repositories
{
    public interface IPurchasesRepository
    {
        /// <summary>
        /// Saves new purchase to DB.
        /// </summary>
        void Add();
    }
}
