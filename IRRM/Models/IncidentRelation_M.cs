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
    
    public partial class IncidentRelation_M
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IncidentRelation_M()
        {
            this.Incident_People_T = new HashSet<Incident_People_T>();
        }
    
        public int IncidentRelationID { get; set; }
        public string IncidentRelation { get; set; }
        public string Comments { get; set; }
        public bool SendEmail { get; set; }
        public string EmailSubject { get; set; }
        public string EmailHeader { get; set; }
        public string EmailFooter { get; set; }
        public bool Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incident_People_T> Incident_People_T { get; set; }
    }
}
