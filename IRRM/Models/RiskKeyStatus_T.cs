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
    
    public partial class RiskKeyStatus_T
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RiskKeyStatus_T()
        {
            this.RiskRegisters = new HashSet<RiskRegister>();
        }
    
        public int RiskKeyStatusID { get; set; }
        public string RiskKeyStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RiskRegister> RiskRegisters { get; set; }
    }
}
