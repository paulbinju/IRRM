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
    
    public partial class RiskSeverity_M
    {
        public int RiskSeverityID { get; set; }
        public Nullable<int> RiskScoreID { get; set; }
        public Nullable<int> RiskDescriptorID { get; set; }
        public string RiskSeverity { get; set; }
        public string Comments { get; set; }
        public bool Active { get; set; }
    
        public virtual RiskDescriptor_M RiskDescriptor_M { get; set; }
        public virtual RiskScore_M RiskScore_M { get; set; }
    }
}
