//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PurchasesAnalysis.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        public int ID { get; set; }
        public int TypeId { get; set; }
        public int ProductId { get; set; }
        public int DateId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    
        public virtual Date Date { get; set; }
        public virtual Product Product { get; set; }
        public virtual Type Type { get; set; }
    }
}
