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
    
    public partial class DynamicControlValue
    {
        public int ControlValueID { get; set; }
        public Nullable<int> ControlTypeID { get; set; }
        public Nullable<int> ControlID { get; set; }
        public string ControlValue { get; set; }
    
        public virtual DynamicControl DynamicControl { get; set; }
        public virtual DynamicControlType DynamicControlType { get; set; }
    }
}
