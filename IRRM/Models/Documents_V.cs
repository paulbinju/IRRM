//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IRRM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Documents_V
    {
        public int DocumentID { get; set; }
        public string Title { get; set; }
        public string DocCategory { get; set; }
        public string DocSubCategory { get; set; }
        public string Stakeholder { get; set; }
        public string ReferenceNo { get; set; }
        public string FileAttached { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidUpto { get; set; }
        public string CurrentStatus { get; set; }
        public string Department { get; set; }
        public string DocumentNo { get; set; }
        public Nullable<System.DateTime> RegisteredOn { get; set; }
        public string DocumentManager { get; set; }
        public Nullable<int> DocCategoryID { get; set; }
        public Nullable<int> DocSubCategoryID { get; set; }
    }
}
