using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IRRM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //GetTaskSubCategory



            routes.MapRoute(
                name: "GetTaskSubCategory",
                url: "GetTaskSubCategory/{CategoryID}",
                defaults: new { controller = "Tasks", action = "GetTaskSubCategory", CategoryID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "DynamicReports",
                url: "DynamicReports",
                defaults: new { controller = "Home", action = "DynamicReports"  }
            );

            routes.MapRoute(
                name: "TaskDeletePeople",
                url: "TaskDeletePeople/{TaskPeopleID}",
                defaults: new { controller = "Tasks", action = "DeletePeople", TaskPeopleID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "InvalidTask",
                url: "InvalidTask/{taskid}",
                defaults: new { controller = "Tasks", action = "InvalidTask", taskid = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "RegisterTask",
                url: "RegisterTask/{taskid}/{prefix}",
                defaults: new { controller = "Tasks", action = "RegisterTask", taskid = UrlParameter.Optional, prefix = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "GetRiskLevel",
                url: "GetRiskLevel/{level}",
                defaults: new { controller = "RiskRegisters", action = "GetRiskLevel", level = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "HistoryReport",
                url: "HistoryReport/{historyid}",
                defaults: new { controller = "Incidents", action = "HistoryReport", historyid = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "TaskCreate",
                url: "Tasks/Create/{ReferenceID}/{ReferenceNo}",
                defaults: new { controller = "Tasks", action = "Create", ReferenceID = UrlParameter.Optional, ReferenceNo = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "GetReportItems",
                url: "GetReportItems/{itemid}",
                defaults: new { controller = "Home", action = "GetReportItems", itemid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetPeopleRelations",
                url: "GetPeopleRelations/{IncidentID}/{IncidentPeopleInvolvedID}",
                defaults: new { controller = "Incident_People_T", action = "GetPeopleRelations", IncidentID = UrlParameter.Optional, IncidentPeopleInvolvedID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AddRecommendation",
                url: "AddRecommendation",
                defaults: new { controller = "Incidents", action = "AddRecommendation"}
            );


            routes.MapRoute(
                name: "DeleteRecommendation",
                url: "DeleteRecommendation/{IncidentID}/{RecommendationID}",
                defaults: new { controller = "Incidents", action = "DeleteRecommendation", IncidentID = UrlParameter.Optional, RecommendationID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ReportDraft",
                url: "ReportDraft",
                defaults: new { controller = "Home", action = "ReportDraft" }
            );


            routes.MapRoute(
                 name: "Report",
                 url: "Report",
                 defaults: new { controller = "Home", action = "Report" }
             );

            routes.MapRoute(
                 name: "Help",
                 url: "Help",
                 defaults: new { controller = "Home", action = "Help" }
             );

            routes.MapRoute(
                 name: "Tasks",
                 url: "Tasks",
                 defaults: new { controller = "Tasks", action = "Index" }
             );
            

            routes.MapRoute(
                 name: "RARequest",
                 url: "RARequest",
                 defaults: new { controller = "Home", action = "RARequest" }
             );


            routes.MapRoute(
                 name: "RiskRegistersCreate",
                 url: "RiskRegisters/Create/{IncidentID}",
                 defaults: new { controller = "RiskRegisters", action = "Create", IncidentID = UrlParameter.Optional }
             );

            
            routes.MapRoute(
                name: "AddIncidentTask",
                url: "AddIncidentTask",
                defaults: new { controller = "Incidents", action = "AddIncidentTask"}
            );

         
            routes.MapRoute(
                name: "GetTasks",
                url: "GetTasks/{IncidentID}",
                defaults: new { controller = "Incidents", action = "GetTasks", IncidentID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "CloseIncident",
                url: "CloseIncident/{IncidentID}",
                defaults: new { controller = "Incidents", action = "CloseIncident", IncidentID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetRecommendations",
                url: "GetRecommendations/{IncidentID}",
                defaults: new { controller = "Incidents", action = "GetRecommendations", IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "SetStatusInvestigationCompleted",
                url: "SetStatusInvestigationCompleted/{IncidentID}",
                defaults: new { controller = "Incidents", action = "SetStatusInvestigationCompleted", IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ViewRemedialAction",
                url: "ViewRemedialAction/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewRemedialAction", IncidentID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ViewInvestigatorReport",
                url: "ViewInvestigatorReport/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewInvestigatorReport", IncidentID = UrlParameter.Optional }
            );
    

            routes.MapRoute(
                name: "ViewRootCauseAnalysis",
                url: "ViewRootCauseAnalysis/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewRootCauseAnalysis", IncidentID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ViewOutcome",
                url: "ViewOutcome/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewOutcome", IncidentID = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "UpdateRemedialAction",
                url: "UpdateRemedialAction",
                defaults: new { controller = "Incidents", action = "UpdateRemedialAction" }
            );

            routes.MapRoute(
                name: "UpdateInvestigatorReport",
                url: "UpdateInvestigatorReport",
                defaults: new { controller = "Incidents", action = "UpdateInvestigatorReport" }
            );

  

            routes.MapRoute(
                name: "UpdateRootCauseAnalysis",
                url: "UpdateRootCauseAnalysis",
                defaults: new { controller = "Incidents", action = "UpdateRootCauseAnalysis" }
            );

    

            routes.MapRoute(
                name: "UpdateOutcomeMain",
                url: "UpdateOutcomeMain",
                defaults: new { controller = "Incidents", action = "UpdateOutcomeMain" }
            );

            routes.MapRoute(
                name: "UpdateOutcome",
                url: "UpdateOutcome",
                defaults: new { controller = "Incidents", action = "UpdateOutcome" }
            );

            routes.MapRoute(
                name: "AddAttachment",
                url: "AddAttachment",
                defaults: new { controller = "Incidents", action = "AddAttachment" }
            );


            routes.MapRoute(
                name: "DeleteAttachementTask",
                url: "DeleteAttachementTask/{TaskDocumentID}/{TaskID}",
                defaults: new { controller = "Tasks", action = "DeleteAttachment", TaskDocumentID = UrlParameter.Optional, TaskID = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "DeleteAttachement",
                 url: "DeleteAttachment/{IncidentDocumentID}/{IncidentID}",
                 defaults: new { controller = "Incidents", action = "DeleteAttachment", IncidenDocumentID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
             );


            routes.MapRoute(
                name: "DetachMRCTemplate",
                url: "DetachMRCTemplate/{IncidentMainRootCauseID}/{IncidentID}",
                defaults: new { controller = "Incidents", action = "DetachMRCTemplate", IncidentMainRootCauseID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "DetachOutcomeTemplate",
                url: "DetachOutcomeTemplate/{IncidentMainOutcomeID}/{IncidentID}",
                defaults: new { controller = "Incidents", action = "DetachOutcomeTemplate", IncidentMainOutcomeID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
            );

          

            routes.MapRoute(
                name: "DeleteMRCAns",
                url: "DeleteMRCAns/{IncidentMainRootCauseID}/{IncidentID}",
                defaults: new { controller = "Incidents", action = "DeleteMRCAns", IncidentMainRootCauseID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "DeleteMOCAns",
                url: "DeleteMOCAns/{IncidentMainOutcomeID}/{IncidentID}",
                defaults: new { controller = "Incidents", action = "DeleteMOCAns", IncidentMainOutcomeID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ViewPatients",
                url: "ViewPatients/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewPatients", IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "TaskViewAttachment",
                url: "TaskViewAttachment/{TaskID}",
                defaults: new { controller = "Tasks", action = "ViewAttachment", TaskID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ViewAttachment",
                url: "ViewAttachment/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewAttachment", IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ViewPeople",
                url: "ViewPeople/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewPeople", IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ViewInterviews",
                url: "ViewInterviews/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewInterviews", IncidentID = UrlParameter.Optional }
            );
            

            routes.MapRoute(
                name: "DeleteWitness",
                url: "DeleteWitness/{IncidentWitnessID}/{IncidentID}",
                defaults: new { controller = "Incidents", action = "DeleteWitness", IncidentWitnessID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "DeleteInterviews",
                url: "DeleteInterviews/{IncidentInterviewID}/{IncidentID}",
                defaults: new { controller = "Incidents", action = "DeleteInterviews", IncidentInterviewID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeletePeople",
                url: "DeletePeople/{IncidentPeopleID}/{IncidentID}",
                defaults: new { controller = "Incidents", action = "DeletePeople", IncidentPeopleID = UrlParameter.Optional, IncidentID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ViewWitness",
                url: "ViewWitness/{IncidentID}",
                defaults: new { controller = "Incidents", action = "ViewWitness", IncidentID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ViewChoice",
                url: "ViewChoice/{IncidentSubRootCauseID}",
                defaults: new { controller = "IncidentSubRootCause_M", action = "ViewChoice", IncidentSubRootCauseID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "DeleteChoice",
                url: "DeleteChoice/{IncidentSubRootCauseID}/{AnswerChoiceID}",
                defaults: new { controller = "IncidentSubRootCause_M", action = "DeleteChoice", IncidentSubRootCauseID = UrlParameter.Optional, AnswerChoiceID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ViewChoiceOutcome",
                url: "ViewChoiceOutcome/{IncidentSubOutcomeID}",
                defaults: new { controller = "IncidentSubOutcome_M", action = "ViewChoiceOutcome", IncidentSubOutcomeID = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "DeleteChoiceOutcome",
                url: "DeleteChoiceOutcome/{IncidentSubOutcomeID}/{AnswerChoiceID}",
                defaults: new { controller = "IncidentSubOutcome_M", action = "DeleteChoiceOutcome", IncidentSubOutcomeID = UrlParameter.Optional, AnswerChoiceID = UrlParameter.Optional }
            );




            routes.MapRoute(
                name: "AddWitness",
                url: "AddWitness",
                defaults: new { controller = "Incidents", action = "AddWitness" }
            );




            routes.MapRoute(
                name: "GetIncidentSubTypes",
                url: "GetIncidentSubTypes/{IncidentTypeID}",
                defaults: new { controller = "Home", action = "GetIncidentSubTypes", IncidentTypeID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetLocations",
                url: "GetLocations/{BranchID}",
                defaults: new { controller = "Home", action = "GetLocations", BranchID = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "Login",
            url: "Login",
            defaults: new { controller = "Home", action = "Login", BranchID = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Logout",
            url: "Logout",
            defaults: new { controller = "Home", action = "Logout" }
            );


            routes.MapRoute(
            name: "MyAccount",
            url: "MyAccount",
            defaults: new { controller = "Home", action = "MyAccount" }
            );


            routes.MapRoute(
                name: "IncidentsInvalid",
                url: "Incidents/Invalid/{incidentid}",
                defaults: new { controller = "Incidents", action = "Invalid", incidentid = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "IncidentsRegister",
                url: "Incidents/Register/{incidentid}/{prefix}",
                defaults: new { controller = "Incidents", action = "Register", incidentid = UrlParameter.Optional, prefix = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "Dashboard",
                url: "Dashboard/{graphtype}",
                defaults: new { controller = "Home", action = "Index", graphtype = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
