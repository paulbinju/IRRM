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
    
    public partial class DocumentSubCategory_M
    {
        public int DocSubCategoryID { get; set; }
        public Nullable<int> DocCategoryID { get; set; }
        public string DocSubCategory { get; set; }
        public string Remarks { get; set; }
        public bool Active { get; set; }
    
        public virtual DocumentCategory_M DocumentCategory_M { get; set; }
    }
}
