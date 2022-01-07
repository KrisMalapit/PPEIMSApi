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
    
    public partial class Requests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Requests()
        {
            this.RequestDetails = new HashSet<RequestDetails>();
        }
    
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ReferenceNo { get; set; }
        public string CreatedBy { get; set; }
        public string DocumentStatus { get; set; }
        public string Status { get; set; }
        public int CreatedByUserId { get; set; }
        public System.DateTime DepartmentApprovedDate { get; set; }
        public int DepartmentHeadId { get; set; }
        public System.DateTime SafetyApprovedDate { get; set; }
        public int SafetyId { get; set; }
        public System.DateTime WarehouseApprovedDate { get; set; }
        public int WarehousemanId { get; set; }
        public System.DateTime DateSubmitted { get; set; }
        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
        public System.DateTime ApprovedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestDetails> RequestDetails { get; set; }
    }
}
