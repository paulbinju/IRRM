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
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.Task_People_T = new HashSet<Task_People_T>();
        }
    
        public int TaskID { get; set; }
        public bool Registered { get; set; }
        public Nullable<System.DateTime> Registeredon { get; set; }
        public Nullable<int> CreatedUserID { get; set; }
        public Nullable<int> ReferenceID { get; set; }
        public string ReferenceNo { get; set; }
        public string TaskNo { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public Nullable<int> OwnerID { get; set; }
        public string AssignedTo { get; set; }
        public string ProviderName { get; set; }
        public string TaskCost { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> TaskStatusID { get; set; }
        public string OwnerNote { get; set; }
        public string Description { get; set; }
        public string Feedback { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> SubCategoryID { get; set; }
        public Nullable<decimal> Progress { get; set; }
        public string FeedbackType { get; set; }
        public Nullable<System.DateTime> TaskDueDate { get; set; }
    
        public virtual TaskCategory_M TaskCategory_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task_People_T> Task_People_T { get; set; }
        public virtual TaskPriorities_M TaskPriorities_M { get; set; }
    }
}
