using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IRRM.Models
{
    public class DataModel
    {
    }


    public class DocumentSubCat {

        public int DocSubCategoryID { get; set; }
        public string DocSubCategory { get; set; }
    }
    public class GraphTypes {
        public int TypeID { get; set; }
        public string TypeText { get; set; }
        public int Count { get; set; }
        public int yearx { get; set; }
        public int monthx { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }

    public class _IncidentRiskAnaysis
    {
        public int IncidentID { get; set; }
        public string IncidentNo { get; set; }
        public string Name { get; set; }
        public string EventType { get; set; }
        public string IncidentType { get; set; }
        public string IncidentSubType { get; set; }
        public DateTime IncidentDate { get; set; }
        public bool RiskAnalysisRequired { get; set; }
        public bool RiskAnalysisCompleted { get; set; }
    }


    public class _IncidentDetails {

        public int IncidentID { get; set; }
        public string Outcome { get; set; }
        public string InvestigatorReport { get; set; }
        public string RemedialAction { get; set; }
        public string RootCauseAnalysis { get; set; }
        public string InvestigationConclusion { get; set; }

    }

    public class _ReportTypes {
        public int ID { get; set; }
        public string Label { get; set; }
    }


    public class _Location
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
    }

    public class _IncidentSubType
    {
        public int IncidentSubTypeID { get; set; }
        public string IncidentSubType { get; set; }
    }

    public class _TaskSubCategory
    {
        public int TaskSubCategoryID { get; set; }
        public string SubCategory { get; set; }
    }

    public class _IncidentHarmScore
    {
        public string IncidentHarmScoreID { get; set; }
        public string IncidentHarmScore { get; set; }
    }


    public class _IncidentPriorities
    {
        public string IncidentPriorityID { get; set; }
        public string Priority { get; set; }
    }

    public class _IncidentPeopleInvolved
    {
        public string IncidentPeopleInvolvedID { get; set; }
        public string IncidentPeopleInvolved { get; set; }
    }
    public class _IncidentRelation
    {
        public string IncidentRelationID { get; set; }
        public string IncidentRelation { get; set; }
    }

    public class _Incident_People
    {
        public string IncidentPeopleID { get; set; }
        public string Name { get; set; }
    }

    public class _IncidentHarmGroup
    {
        public string IncidentHarmGroupID { get; set; }
        public string IncidentHarmGroup { get; set; }
    }


    public class _IncidentMainRootCauseQP {
        public int IncidentSubRootCauseID { get; set; }
        public int MyProperty { get; set; }
        public int AnswerType { get; set; }
        public int? AnswerChoiceID { get; set; }
        public int IncidentMainRootCauseID { get; set; }
        public string IncidentMainRootCause { get; set; }
        public string IncidentSubRootCause { get; set; }
        public string ChoiceAnswer { get; set; }
    }

    public class _IncidentMainOutcomeQP
    {
        public int IncidentSubOutcomeID { get; set; }
        public int MyProperty { get; set; }
        public int AnswerType { get; set; }
        public int? AnswerChoiceID { get; set; }
        public string IncidentMainOutcome { get; set; }
        public int IncidentMainOutcomeID { get; set; }
        public string IncidentSubOutcome { get; set; }
        public string ChoiceAnswer { get; set; }
    }

    public class _IncidentRootCauseAns
    {
        public int SubRootCauseAnswerID { get; set; }
        public int IncidentMainRootCauseID { get; set; }
        public int IncidentSubRootCauseID { get; set; }
        public int AnswerType { get; set; }
        public string IncidentMainRootCause { get; set; }
        public string IncidentSubRootCause { get; set; }
        public string Answer { get; set; }
    }


    public class _IncidentOutcomeAns
    {
        public int SubOutcomeAnswerID { get; set; }
        public int IncidentMainOutcomeID { get; set; }
        public int IncidentSubOutcomeID { get; set; }
        public int AnswerType { get; set; }
        public string IncidentMainOutcome { get; set; }
        public string IncidentSubOutcome { get; set; }
        public string Answer { get; set; }
    }


    public class _Tasks {
        public int TaskID { get; set; }
        public int? IncidentID { get; set; }
        public int? CreatedUserID { get; set; }
        public string CreatedBy { get; set; }
        public string IncidentNo { get; set; }
        public string Task { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? RiskAnaysisCreated { get; set; }
    }

 

    public class _User
    {
        public int UserID { get; set; }
        public string UserID2 { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }

        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public int BranchID { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string LicenseNo { get; set; }
        public string EmployeeNo { get; set; }

    }


    public class _RiskKeyStaus
    {
        public int RiskKeyStatusDateID { get; set; }
        public int RiskRegisterID { get; set; }
        public int RiskKeyStatusID { get; set; }
        public DateTime? StatusDate { get; set; }
        public string RiskKeyStatus { get; set; }
    }



    public class _IncWitness
    {
        public int IncidentWitnessID { get; set; }
        public int? IncidentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? BranchID { get; set; }
        public string Branch { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }



    }


    public class _Incidents {
        public long rownumber { get; set; }
        public int IncidentID { get; set; }
        public int? relationuserid { get; set; }
        public int CreatedUserID { get; set; }
        public string IncCreatedBy { get; set; }
        public int UserRoleID { get; set; }
        public string IncidentNo { get; set; }
        public int IncidentTypeID { get; set; }
        public string IncidentType { get; set; }
        public int IncidentSubTypeID { get; set; }
        public string IncidentSubType { get; set; }
        public int BranchID { get; set; }
        public string Branch { get; set; }
        public int LocationID { get; set; }
        public string Location { get; set; }
        public DateTime? IncidentDate { get; set; }
        public DateTime? IncidentCreatedDate { get; set; }
        public DateTime? RegisteredOn { get; set; }
        public DateTime? InvestigationDueDate { get; set; }
        public int? IncidentStatusID { get; set; }
        public string IncidentStatus { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public string Name { get; set; }
        public int? incidentrelationid { get; set; }
        public bool CreatorCanComment { get; set; }
    }


    public class _RiskRegister {
        public int RiskRegisterID { get; set; }
        public int RRNo { get; set; }
        public string RiskRegisterNo { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Proactive { get; set; }
        public int IncidentID { get; set; }
        public int RiskCategoryID { get; set; }
        public string RiskCategory { get; set; }
        public int RiskCostID { get; set; }
        public string RiskCost { get; set; }

        public int RiskDescriptorID { get; set; }
        public string RiskDescriptor { get; set; }

        public int RiskStatusID { get; set; }
        public string RiskStatus { get; set; }
        public int RiskStrategyID { get; set; }
        public string RiskStrategy { get; set; }
        public string Controlsinplace { get; set; }
        public string Adequacy { get; set; }
        public int? RARating { get; set; }
        public string RARiskLevel { get; set; }
        public int? RRRating { get; set; }
        public string RRRiskLevel { get; set; }
        public string FutureStrategies { get; set; }
        public string PotentialRiskFactors { get; set; }
        public string Comments { get; set; }
        public string Description { get; set; }
        //public int RiskAssessedByID { get; set; }
        //public string RiskAssessedBy { get; set; }

        public int RiskOwnerID { get; set; }
        public string RiskOwner { get; set; }

        public string TaskNo { get; set; }
        public int RiskKeyStatusID { get; set; }
        public string RiskKeyStatus { get; set; }

        public string ReferenceNo { get; set; }
        public DateTime? RiskRegisteredDate { get; set; }
        public string Location { get; set; }


    }


    public class _Recommendations {
        public int RecommendationID { get; set; }
        public string Recommendation { get; set; }
    }

    public class _IncidentDocument
    {
        public int IncidentDocumentID { get; set; }
        public int? IncidentID { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public Guid? DocGUID { get; set; }
        public string ActualFileName { get; set; }
    }


    public class _TaskDocument
    {
        public int TaskDocumentID { get; set; }
        public int? TaskID { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public Guid? DocGUID { get; set; }
        public string ActualFileName { get; set; }
    }


    public class _IncPeople
    {
        public int IncidentPeopleID { get; set; }
        public int IncidentID { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public int IncidentPeopleInvolvedID { get; set; }
        public string IncidentPeopleInvolved { get; set; }
        public int? IncidentRelationID { get; set; }
        public string IncidentRelation { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Designation { get; set; }
        public string LicenseNo { get; set; }
        public string IdentityNo { get; set; }
        public string RegNo { get; set; }
        public string CVRNo { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPatientInjured { get; set; }
        
    }

    public class _RiskLevel {
        public string RARiskLevel { get; set; }
    }

    public class _IncPatients
    {
        public int PatientID { get; set; }
        public int? IncidentID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PatientIdn { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }

    }


    public class _IncidentInterviews
    {
        public int IncidentInterviewID { get; set; }
        public int IncidentID { get; set; }
        public string Interviewer { get; set; }
        public string Interviewee { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime InterviewDate { get; set; }
        public DateTime CreatedOn { get; set; }
    }


    public class _IncidentPriority {
        public string IncidentPriorityID { get; set; }
        public string Priority { get; set; }
    }

    public class _IncidentType {
        public string IncidentType { get; set; }
        public string IncidentTypeID { get; set; }
    }

    public class _IncidentMainRootCause
    {
        public string IncidentMainRootCause { get; set; }
        public string IncidentMainRootCauseID { get; set; }
    }





    public class _RiskFrequencyScore
    {
        public string RiskFrequencyScoreID { get; set; }
        public string RiskFrequencyScore { get; set; }
    }
    public class _RiskScoreID
    {
        public string RiskScoreID { get; set; }
        public string RiskScore { get; set; }
    }

    public class _RiskDescriptorID
    {
        public string RiskDescriptorID { get; set; }
        public string RiskDescriptor { get; set; }
    }

    public class _Branch
    {
        public string BranchID { get; set; }
        public string Branch { get; set; }
    }

    public class _Role
    {
        public string UserRoleID { get; set; }
        public string Role { get; set; }
    }


 

    public class _Department
    {
        public string DepartmentID { get; set; }
        public string Department { get; set; }
    }


    public class _IncidentComments
    {
        public int CommentID { get; set; }
        public int? IncidentID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public DateTime? CommentDateTime { get; set; }
        public string Comment { get; set; }
    }
}