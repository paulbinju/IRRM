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
    
    public partial class IncidentTypes_M
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IncidentTypes_M()
        {
            this.IncidentSubTypes_M = new HashSet<IncidentSubTypes_M>();
            this.Incidents = new HashSet<Incident>();
        }
    
        public int IncidentTypeID { get; set; }
        public Nullable<int> IncidentPriorityID { get; set; }
        public string IncidentType { get; set; }
        public bool Active { get; set; }
        public string Comments { get; set; }
    
        public virtual IncidentPriorities_M IncidentPriorities_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidentSubTypes_M> IncidentSubTypes_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}