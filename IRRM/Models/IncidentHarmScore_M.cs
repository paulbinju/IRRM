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
    
    public partial class IncidentHarmScore_M
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IncidentHarmScore_M()
        {
            this.Incidents = new HashSet<Incident>();
        }
    
        public int IncidentHarmScoreID { get; set; }
        public Nullable<int> IncidentHarmGroupID { get; set; }
        public string IncidentHarmScoreCode { get; set; }
        public string IncidentHarmScore { get; set; }
        public string Comments { get; set; }
        public bool Active { get; set; }
    
        public virtual IncidentHarmGroup_M IncidentHarmGroup_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}