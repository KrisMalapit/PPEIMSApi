//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPEIMSApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemDetails
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string LineNo { get; set; }
        public string Remarks { get; set; }
    
        public virtual Items Items { get; set; }
    }
}