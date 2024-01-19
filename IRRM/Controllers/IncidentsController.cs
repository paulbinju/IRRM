using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IRRM.Models;
using System.Text;
using System.IO;

namespace IRRM.Controllers
{
    public class IncidentsController : Controller
    {
        private IRRMEntities db = new IRRMEntities();
        private Utility utl = new Utility();
        // GET: Incidents
        public ActionResult Index()
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);




            if (Convert.ToInt32(Session["RoleID"]) == 1)
            { // admin

                ViewBag.Incidents = db.Database.SqlQuery<_Incidents>(@"select distinct row_number() over(order by inc.IncidentID) as rownumber,inc.IncidentID,inc.InvestigationDueDate,inc.CreatedUserID,incCreated.Name as IncCreatedBy,incCreated.UserRoleID,IncidentNo,inc.IncidentTypeID,intype.IncidentType,
inc.IncidentSubTypeID,instype.IncidentSubType,inc.CreatorCanComment,
inc.BranchID,b.Branch,inc.LocationID,l.Location,IncidentDate,RegisteredOn,IncidentCreatedDate,inc.IncidentStatusID,inst.IncidentStatus,inpu.Name as AssignedTo,ip.Name as AssignedBy from  [dbo].[Incidents] inc
left join (
select incidentid,ipx.userid,incidentrelationid,asgnby.name from [Incident-People_T] ipx
left join Users_M asgnby on asgnby.UserID = ipx.CreateUserID
where IncidentRelationID=1
)ip on ip.IncidentID= inc.IncidentID
left join Users_M inpu on inpu.UserID = ip.UserID
left join Users_M incCreated on incCreated.UserID = inc.CreatedUserID
left join IncidentTypes_M intype on intype.IncidentTypeID = inc.IncidentTypeID
left join IncidentSubTypes_M instype on instype.IncidentSubTypeID = inc.IncidentSubTypeID
left join Branch_M b on b.BranchID = inc.BranchID
left join Location_M l on l.LocationID = inc.LocationID
left join IncidentStatus_M inst on inst.IncidentStatusID= inc.IncidentStatusID where inc.EventTypeID is not null and inc.IncidentStatusID is not null  and inc.IncidentStatusID<>6  order by inc.IncidentID desc").ToList();


            }
            else
            {
               List<_Incidents> inci  = db.Database.SqlQuery<_Incidents>(@"select distinct row_number() over(order by inc.IncidentID) as rownumber,inc.IncidentID,ip2.incidentrelationid,ip2.UserID as relationuserid, inc.InvestigationDueDate,inc.CreatedUserID,incCreated.Name as IncCreatedBy,incCreated.UserRoleID,IncidentNo,inc.IncidentTypeID,intype.IncidentType,inc.IncidentSubTypeID,instype.IncidentSubType,inc.CreatorCanComment,
inc.BranchID,b.Branch,inc.LocationID,l.Location,IncidentDate,RegisteredOn,IncidentCreatedDate,inc.IncidentStatusID,inst.IncidentStatus,inpu.Name as AssignedTo,ip.Name as AssignedBy from  [dbo].[Incidents] inc
left join (
select incidentid,ipx.userid,asgnby.name from [Incident-People_T] ipx
left join Users_M asgnby on asgnby.UserID = ipx.CreateUserID
where IncidentRelationID=1
)ip on ip.IncidentID= inc.IncidentID
left join (
select  ipx.userid,ipx.IncidentID,incidentrelationid  from [Incident-People_T] ipx
where IncidentRelationID<=2 or IncidentRelationID=7
)ip2 on ip2.IncidentID= inc.IncidentID
left join Users_M inpu on inpu.UserID = ip.UserID
left join Users_M incCreated on incCreated.UserID = inc.CreatedUserID
left join IncidentTypes_M intype on intype.IncidentTypeID = inc.IncidentTypeID
left join IncidentSubTypes_M instype on instype.IncidentSubTypeID = inc.IncidentSubTypeID
left join Branch_M b on b.BranchID = inc.BranchID
left join Location_M l on l.LocationID = inc.LocationID
left join IncidentStatus_M inst on inst.IncidentStatusID= inc.IncidentStatusID
where inc.Registered=1  and ((inc.CreatedUserID=" + UserID + ") or ip2.UserID=" + UserID + "  )   and inc.EventTypeID is not null  order by inc.IncidentID desc").ToList();


                List<_Incidents> distindidentids = (from incis in inci
                                                    group incis by incis.IncidentID into incigroup
                                                    select new _Incidents
                                                    {
                                                        rownumber = incigroup.Min(x => x.rownumber),
                                                    }).ToList();
                List<long> idsonly = distindidentids.Select(x => x.rownumber).ToList();

                ViewBag.Incidents = (from incidentsx in inci
                                    where idsonly.Contains(incidentsx.rownumber)
                                    select incidentsx).ToList();
            }
            //where inc.Registered=1  and ((inc.CreatedUserID=" + UserID + " and (CreatorCanComment=1 or inc.IncidentStatusID=6)) or ip2.UserID=" + UserID + "  )   and inc.EventTypeID is not null  order by inc.IncidentID desc").ToList();


            

            return View();
        }




        [HttpPost]
        public ActionResult Search(FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);


            string searchconditions = "";

            if (Convert.ToInt32(col["EventTypeID"]) != 0) {
                searchconditions += " and EventTypeID=" + Convert.ToInt32(col["EventTypeID"]) + " ";
            }
            if (Convert.ToString(col["IncidentDateFrom"]) != "")
            {
                searchconditions += " and RegisteredOn>='" + Convert.ToDateTime(col["IncidentDateFrom"]).ToString("yyyy-MM-dd") + " 00:00:00' ";
            }
            if (Convert.ToString(col["IncidentDateTo"]) != "")
            {
                searchconditions += " and RegisteredOn<='" + Convert.ToDateTime(col["IncidentDateTo"]).ToString("yyyy-MM-dd") + " 23:59:59' ";
            }
            if (Convert.ToInt32(col["IncidentTypeID"]) != 0)
            {
                searchconditions += " and inc.IncidentTypeID=" + Convert.ToInt32(col["IncidentTypeID"]) + " ";
            }
            if (Convert.ToInt32(col["IncidentSubTypeID"]) != 0)
            {
                searchconditions += " and inc.IncidentSubTypeID=" + Convert.ToInt32(col["IncidentSubTypeID"]) + " ";
            }
            if (Convert.ToInt32(col["StatusID"]) != 0)
            {
                searchconditions += " and inc.IncidentStatusID=" + Convert.ToInt32(col["StatusID"]) + " ";
            }
            if (Convert.ToInt32(col["MainRootCauseID"]) != 0)
            {
                searchconditions += " and inc.IncidentID in (select IncidentID from  [dbo].[Incidents_MainRootCause_T] where IncidentMainRootCauseID=" + Convert.ToInt32(col["MainRootCauseID"]) + ") ";
            }
            if (Convert.ToInt32(col["SubRootCauseID"]) != 0)
            {
                searchconditions += " and inc.IncidentID in (select IncidentID from  [dbo].[Incidents_MainRootCause_T] where IncidentMainRootCauseID in (select IncidentMainRootCauseID from   [dbo].[IncidentSubRootCause_M] where IncidentSubRootCauseID=" + Convert.ToInt32(col["SubRootCauseID"]) + ")) ";
            }



            if (Convert.ToInt32(col["IncidentPeopleInvolved"]) != 0)
            {
                searchconditions += " and inc.IncidentId in (select IncidentID from   [dbo].[Incident-People_T] where IncidentPeopleInvolvedID = " + Convert.ToInt32(col["IncidentPeopleInvolved"]) + ") ";
            }

            if (Convert.ToInt32(col["PriorityID"]) != 0)
            {
                searchconditions += " and inc.PriorityID=" + Convert.ToInt32(col["PriorityID"]) + " ";
            }
            if (Convert.ToInt32(col["HarmScoreID"]) != 0)
            {
                searchconditions += " and inc.HarmScoreID=" + Convert.ToInt32(col["HarmScoreID"]) + " ";
            }

            if (Convert.ToInt32(col["FeedbackTypeID"]) != 0)
            {
                searchconditions += " and inc.FeedbackTypeID=" + Convert.ToString(col["FeedbackTypeID"]) + " ";
            }
            if (Convert.ToInt32(col["LocationID"]) != 0)
            {
                searchconditions += " and inc.LocationID=" + Convert.ToString(col["LocationID"]) + " ";
            }




            if (Convert.ToInt32(Session["RoleID"]) == 1)
            { // admin

                string query = @"select distinct row_number() over(order by inc.IncidentID) as rownumber,inc.IncidentID, inc.InvestigationDueDate,inc.CreatedUserID,incCreated.Name as IncCreatedBy,incCreated.UserRoleID,IncidentNo,inc.IncidentTypeID,intype.IncidentType,inc.IncidentSubTypeID,instype.IncidentSubType,inc.CreatorCanComment,
inc.BranchID,b.Branch,inc.LocationID,l.Location,IncidentDate,RegisteredOn,IncidentCreatedDate,inc.IncidentStatusID,inst.IncidentStatus,inpu.Name as AssignedTo,ip.Name as AssignedBy from  [dbo].[Incidents] inc
left join (
select incidentid,ipx.userid,incidentrelationid,asgnby.name from [Incident-People_T] ipx
left join Users_M asgnby on asgnby.UserID = ipx.CreateUserID
where IncidentRelationID=1
)ip on ip.IncidentID= inc.IncidentID
left join Users_M inpu on inpu.UserID = ip.UserID
left join Users_M incCreated on incCreated.UserID = inc.CreatedUserID
left join IncidentTypes_M intype on intype.IncidentTypeID = inc.IncidentTypeID
left join IncidentSubTypes_M instype on instype.IncidentSubTypeID = inc.IncidentSubTypeID
left join Branch_M b on b.BranchID = inc.BranchID
left join Location_M l on l.LocationID = inc.LocationID
left join IncidentStatus_M inst on inst.IncidentStatusID= inc.IncidentStatusID where inc.IncidentStatusID is not null  " + searchconditions + " order by inc.RegisteredOn desc, inc.IncidentNo desc";

                ViewBag.Incidents = db.Database.SqlQuery<_Incidents>(query).ToList();


            }
            else
            {
                string query = @"select distinct row_number() over(order by inc.IncidentID) as rownumber,inc.IncidentID,ip2.incidentrelationid,ip2.UserID as relationuserid, inc.InvestigationDueDate,inc.CreatedUserID,incCreated.Name as IncCreatedBy,incCreated.UserRoleID,IncidentNo,inc.IncidentTypeID,intype.IncidentType,inc.IncidentSubTypeID,instype.IncidentSubType,inc.CreatorCanComment,
inc.BranchID,b.Branch,inc.LocationID,l.Location,IncidentDate,RegisteredOn,IncidentCreatedDate,inc.IncidentStatusID,inst.IncidentStatus,inpu.Name as AssignedTo,ip.Name as AssignedBy from  [dbo].[Incidents] inc
left join (
select incidentid,ipx.userid,incidentrelationid,asgnby.name from [Incident-People_T] ipx
left join Users_M asgnby on asgnby.UserID = ipx.CreateUserID
where IncidentRelationID=1
)ip on ip.IncidentID= inc.IncidentID
left join (
select  ipx.userid,ipx.IncidentID,incidentrelationid  from [Incident-People_T] ipx
where IncidentRelationID<=2 or IncidentRelationID=7
)ip2 on ip2.IncidentID= inc.IncidentID
left join Users_M inpu on inpu.UserID = ip.UserID
left join Users_M incCreated on incCreated.UserID = inc.CreatedUserID
left join IncidentTypes_M intype on intype.IncidentTypeID = inc.IncidentTypeID
left join IncidentSubTypes_M instype on instype.IncidentSubTypeID = inc.IncidentSubTypeID
left join Branch_M b on b.BranchID = inc.BranchID
left join Location_M l on l.LocationID = inc.LocationID
left join IncidentStatus_M inst on inst.IncidentStatusID= inc.IncidentStatusID
where inc.Registered=1  and ((inc.CreatedUserID=" + UserID + ") or ip2.UserID=" + UserID + "  )   " + searchconditions + "   order by inc.RegisteredOn desc";

                var inci = db.Database.SqlQuery<_Incidents>(query).ToList();
                List<_Incidents> distindidentids = (from incis in inci
                                                    group incis by incis.IncidentID into incigroup
                                                    select new _Incidents
                                                    {
                                                        rownumber = incigroup.Min(x => x.rownumber),
                                                    }).ToList();
                List<long> idsonly = distindidentids.Select(x => x.rownumber).ToList();

                ViewBag.Incidents = (from incidentsx in inci
                                    where idsonly.Contains(incidentsx.rownumber)
                                    orderby incidentsx.RegisteredOn descending
                                     select incidentsx).ToList();

            }

            return View();
        }

        public ActionResult SearchPartial() {


            ViewBag.EventTypeID = db.EventType_SM.Where(x => x.EventTypeID < 8).ToList();
            ViewBag.IncidentTypeID = db.IncidentTypes_M.ToList();
            ViewBag.IncidentRootCause = db.IncidentMainRootCause_M.ToList();
            ViewBag.IncidentSubRootCause = db.IncidentSubRootCause_M.ToList();
            ViewBag.IncidentPeopleInvolved = db.IncidentPeopleInvolved_M.ToList();
            ViewBag.IncidentPriority = db.IncidentPriorities_M.ToList();
            ViewBag.IncidentHarmScore = db.IncidentHarmScore_M.ToList();
     


            ViewBag.FeedbackTypeID = db.IncidentFeedbackType_M.ToList();

            ViewBag.StatusID = db.IncidentStatus_M.Where(x => x.IncidentStatusID > 1).ToList();

            return PartialView();
        }



        // GET: Incidents/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int UserID = Convert.ToInt32(Session["UserID"]);
            var incident = db.Incidents.Include(i => i.EventType_SM).Include(i => i.IncidentPriorities_M).Where(x => x.IncidentID == id).SingleOrDefault();
            ViewBag.EventTypeID = db.EventType_SM.ToList();
            int roleflag = -1;
            int IncidentRelationID = db.Database.SqlQuery<int>("select top 1 IncidentRelationID from [dbo].[IncidentRelation_M] where IncidentRelationID in (select IncidentRelationID from [Incident-People_T] where IncidentID=" + id + " and UserID=" + Convert.ToString(Session["UserID"]) + ")").SingleOrDefault();
            if (Convert.ToString(Session["RoleID"]) == "1")
            {
                roleflag = 0;// administrator
                ViewBag.StatusID = db.IncidentStatus_M.Where(x => x.IncidentStatusID > 5).ToList();
                if (incident.IncidentStatusID == 3 && IncidentRelationID == 1)
                { // investigator opening this incident for the first time
                    utl.setStatus(incident.IncidentID, 4, Convert.ToInt32(Session["UserID"]));
                    return RedirectToAction("../Incidents/Details/" + incident.IncidentID);
                }
            }
            else
            {
                if (IncidentRelationID == 1)
                {
                    roleflag = 1; // Investigator  
                    if (incident.IncidentStatusID == 3)
                    { // investigator opening this incident for the first time
                        utl.setStatus(incident.IncidentID, 4, Convert.ToInt32(Session["UserID"]));
                        return RedirectToAction("../Incidents/Details/" + incident.IncidentID);
                    }
                }
                else if (IncidentRelationID == 2)
                {
                    roleflag = 2; //additional investigator
                }
                else if (IncidentRelationID == 7)
                {
                    roleflag = 7; //reviewer
                }
                if (incident.CreatedUserID == UserID)
                {
                    roleflag = 3; // created user
                }
                ViewBag.StatusID = db.IncidentStatus_M.Where(x => x.IncidentStatusID == 0).ToList();// only admin can set status
            }
            ViewBag.roleflag = roleflag;
            ViewBag.IncidentTypeID = db.IncidentTypes_M.Where(x => x.Active == true).ToList();
            ViewBag.BranchID = db.Branch_M.Where(x => x.Active == true).ToList();
            ViewBag.DocumentType = db.DocumentType_SM.ToList();
            ViewBag.IncidentID = id;


            if (roleflag == 0)
            {

                ViewBag.Status = (from inst in db.Incident_Status_T
                                  join ins in db.IncidentStatus_M on inst.IncidentStatusID equals ins.IncidentStatusID
                                  join usr in db.Users_M on inst.CreatedUserID equals usr.UserID
                                  where inst.IncidentID == id
                                  select new _Incidents { IncidentStatus = ins.IncidentStatus, Name = usr.Name, RegisteredOn = inst.CreatedOn }
                                                                ).ToList();
            }
            else
            {

                ViewBag.Status = (from inst in db.Incident_Status_T
                                  join ins in db.IncidentStatus_M on inst.IncidentStatusID equals ins.IncidentStatusID
                                  join usr in db.Users_M on inst.CreatedUserID equals usr.UserID
                                  where inst.IncidentID == id && inst.IncidentStatusID != 1
                                  select new _Incidents { IncidentStatus = ins.IncidentStatus, Name = usr.Name, RegisteredOn = inst.CreatedOn }
                                                                ).ToList();

            }


            ViewBag.FeedbackType = db.IncidentFeedbackType_M.Where(x => x.Active == true).ToList();
            List<IncidentPriorities_M> ipinAlldatap = db.IncidentPriorities_M.ToList();
            List<_IncidentPriorities> ipinNewlistp = new List<_IncidentPriorities>();
            _IncidentPriorities _ipinnewItemp;
            foreach (var ipin in ipinAlldatap)
            {
                _ipinnewItemp = new _IncidentPriorities();
                _ipinnewItemp.IncidentPriorityID = ipin.IncidentPriorityID.ToString();
                _ipinnewItemp.Priority = ipin.Priority;
                ipinNewlistp.Add(_ipinnewItemp);
            }
            ipinNewlistp.Insert(0, new _IncidentPriorities { IncidentPriorityID = "", Priority = "Select" });
            ViewBag.IncidentPriorities = ipinNewlistp.ToList();
            List<IncidentHarmScore_M> ipinAlldatahs = db.IncidentHarmScore_M.ToList();
            List<_IncidentHarmScore> ipinNewlisths = new List<_IncidentHarmScore>();
            _IncidentHarmScore _ipinnewItemhs;
            foreach (var ipinx in ipinAlldatahs)
            {
                _ipinnewItemhs = new _IncidentHarmScore();
                _ipinnewItemhs.IncidentHarmScoreID = ipinx.IncidentHarmScoreID.ToString();
                _ipinnewItemhs.IncidentHarmScore = ipinx.IncidentHarmScore;
                ipinNewlisths.Add(_ipinnewItemhs);
            }
            ipinNewlisths.Insert(0, new _IncidentHarmScore { IncidentHarmScoreID = "", IncidentHarmScore = "Select" });
            ViewBag.IncidentHarmScoreID = ipinNewlisths.ToList();


            ViewBag.IncidentMainRootCause = db.IncidentMainRootCause_M.Where(x => x.Active == true).ToList();
            ViewBag.IncidentMainOutcome = db.IncidentMainOutcome_M.Where(x => x.Active == true).ToList();

            //if (incident.PriorityID != null)
            //{
            //    ViewBag.InvestigationDueDate = Convert.ToDateTime(incident.RegisteredOn).AddDays(Convert.ToDouble(incident.IncidentPriorities_M.InvestigationDays));
            //}

            List<_IncidentRootCauseAns> _IncidentRootCauseAnwser = db.Database.SqlQuery<_IncidentRootCauseAns>(@" select SubRootCauseAnswerID,insrc.IncidentMainRootCauseID,AnswerType,inmrc.IncidentMainRootCause,IncidentSubRootCause,insrcans.IncidentSubRootCauseID,Answer from [IncidentSubRootCauseAnswer_T] insrcans
 join IncidentSubRootCause_M insrc on insrcans.incidentsubrootcauseid=insrc.incidentsubrootcauseid
 join IncidentMainRootCause_M inmrc on insrc.IncidentMainRootCauseID = inmrc.IncidentMainRootCauseID  where IncidentID = " + id + " ").ToList();
            ViewBag._IncidentRootCauseAnwser = _IncidentRootCauseAnwser;

            List<_IncidentOutcomeAns> _IncidentOutcomeAnwser = db.Database.SqlQuery<_IncidentOutcomeAns>(@" select SubOutcomeAnswerID,insrc.IncidentMainOutcomeID,AnswerType,inmrc.IncidentMainOutcome,IncidentSubOutcome,insrcans.IncidentSubOutcomeID,Answer from [IncidentSubOutcomeAnswer_T] insrcans
 join IncidentSubOutcome_M insrc on insrcans.incidentsubOutcomeid=insrc.incidentsubOutcomeid
 join IncidentMainOutcome_M inmrc on insrc.IncidentMainOutcomeID = inmrc.IncidentMainOutcomeID  where IncidentID = " + id + " ").ToList();
            ViewBag._IncidentOutcomeAnwser = _IncidentOutcomeAnwser;



            string qur1 = @"select src.IncidentSubRootCauseID,src.IncidentMainRootCauseID,mrc.IncidentMainRootCause,src.IncidentSubRootCause,AnswerType,AnswerChoiceID,ChoiceAnswer from IncidentSubRootCause_M  src
 inner join IncidentMainRootCause_M mrc on mrc.IncidentMainRootCauseID = src.IncidentMainRootCauseID
 left join IncidentSubRootCauseAnswerChoice_T ac on ac.IncidentSubRootCauseID = src.IncidentSubRootCauseID
 where src.IncidentMainRootCauseID in ( select IncidentMainRootCauseID from [dbo].[Incidents_MainRootCause_T] where IncidentID=" + id + " and answered=0) and mrc.active=1";

            List<_IncidentMainRootCauseQP> imrcqp = db.Database.SqlQuery<_IncidentMainRootCauseQP>(qur1).ToList();
            ViewBag.imrcqp = imrcqp;

            List<_IncidentMainOutcomeQP> imoqp = db.Database.SqlQuery<_IncidentMainOutcomeQP>(@"select src.IncidentSubOutcomeID,src.IncidentMainOutcomeID,mrc.IncidentMainOutcome,src.IncidentSubOutcome,AnswerType,AnswerChoiceID,ChoiceAnswer from IncidentSubOutcome_M  src
 inner join IncidentMainOutcome_M mrc on mrc.IncidentMainOutcomeID = src.IncidentMainOutcomeID
 left join IncidentSubOutcomeAnswerChoice_T ac on ac.IncidentSubOutcomeID = src.IncidentSubOutcomeID
 where src.IncidentMainOutcomeID in ( select IncidentMainOutcomeID from [dbo].[Incidents_MainOutcome_T] where IncidentID=" + id + " and answered=0) and mrc.active=1").ToList();

            ViewBag.imoqp = imoqp;


            if(incident.IncidentStatusID < 5 && incident.InvestigationDueDate <= DateTime.UtcNow.AddHours(3))
            {
                ViewBag.OverDue = "Yes";
                ViewBag.OverDueDays = Convert.ToInt32((DateTime.UtcNow.AddHours(3) - Convert.ToDateTime(incident.InvestigationDueDate)).TotalDays);
            }




            ViewBag.Comments = (from c in db.Incident_Comments
                                join u in db.Users_M on c.UserID equals u.UserID
                                where c.IncidentID == id
                                orderby c.CommentID descending
                                select new _IncidentComments { CommentID = c.CommentID, UserID = u.UserID, IncidentID = c.IncidentID, Comment = c.Comment, CommentDateTime = c.CommentDateTime, Name = u.Name, Designation = u.Designation }
                                ).ToList();


            ViewBag.HistoryIncidents = db.IncidentHistories.Where(x => x.IncidentID == id).ToList();


            return View(incident);
        }

        public ActionResult HistoryReport(int historyid) {

            ViewBag.HistoryIncidents = db.IncidentHistories.Where(x => x.ID == historyid).ToList();

            return View();
        }



        public ActionResult Report(int id)
        {
           // if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int UserID = Convert.ToInt32(Session["UserID"]);
            var incident = db.Incidents.Include(i => i.EventType_SM).Include(i => i.IncidentPriorities_M).Where(x => x.IncidentID == id).SingleOrDefault();


            List<_IncPatients> IncPatients = (from incp in db.Incident_People_T
                                              where incp.IncidentID == id && incp.IncidentPeopleInvolvedID == 1
                                              select new _IncPatients { IncidentID = incp.IncidentID, Name = incp.Name, Phone = incp.Mobile, DOB = incp.DateOfBirth, Gender = incp.Gender, PatientIdn = incp.IdentityNo }
                                            ).ToList();

            ViewBag.IncPatients = IncPatients;

            ViewBag.IncidentPeople = IncidentPeople(id);
            ViewBag.IncidentInterviews = IncidentInterviews(id);

            ViewBag.Status = (from inst in db.Incident_Status_T
                              join ins in db.IncidentStatus_M on inst.IncidentStatusID equals ins.IncidentStatusID
                              join usr in db.Users_M on inst.CreatedUserID equals usr.UserID
                              where inst.IncidentID == id
                              select new _Incidents { IncidentStatus = ins.IncidentStatus, Name = usr.Name, RegisteredOn = inst.CreatedOn, IncidentStatusID = inst.IncidentStatusID }
                                                    ).ToList();


            List<_IncidentRootCauseAns> _IncidentRootCauseAnwser = db.Database.SqlQuery<_IncidentRootCauseAns>(@" select SubRootCauseAnswerID,insrc.IncidentMainRootCauseID,AnswerType,inmrc.IncidentMainRootCause,IncidentSubRootCause,insrcans.IncidentSubRootCauseID,Answer from [IncidentSubRootCauseAnswer_T] insrcans
 join IncidentSubRootCause_M insrc on insrcans.incidentsubrootcauseid=insrc.incidentsubrootcauseid
 join IncidentMainRootCause_M inmrc on insrc.IncidentMainRootCauseID = inmrc.IncidentMainRootCauseID where IncidentID = " + id + " ").ToList();
            ViewBag._IncidentRootCauseAnwser = _IncidentRootCauseAnwser;

            List<_IncidentOutcomeAns> _IncidentOutcomeAnwser = db.Database.SqlQuery<_IncidentOutcomeAns>(@" select SubOutcomeAnswerID,insrc.IncidentMainOutcomeID,AnswerType,inmrc.IncidentMainOutcome,IncidentSubOutcome,insrcans.IncidentSubOutcomeID,Answer from [IncidentSubOutcomeAnswer_T] insrcans
 join IncidentSubOutcome_M insrc on insrcans.incidentsubOutcomeid=insrc.incidentsubOutcomeid
 join IncidentMainOutcome_M inmrc on insrc.IncidentMainOutcomeID = inmrc.IncidentMainOutcomeID  where IncidentID = " + id + " ").ToList();
            ViewBag._IncidentOutcomeAnwser = _IncidentOutcomeAnwser;

            List <Incident_Recommendation_T> IncidentRecommendations = db.Incident_Recommendation_T.Where(x => x.IncidentID == id).ToList();
            ViewBag.IncidentRecommendations = IncidentRecommendations;

            return View(incident);
        }



        public ActionResult ReportDraft(int id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int UserID = Convert.ToInt32(Session["UserID"]);
            var incident = db.Incidents.Include(i => i.EventType_SM).Include(i => i.IncidentPriorities_M).Where(x => x.IncidentID == id).SingleOrDefault();


            List<_IncPatients> IncPatients = (from incp in db.Incident_People_T
                                              where incp.IncidentID == id && incp.IncidentPeopleInvolvedID == 1
                                              select new _IncPatients { IncidentID = incp.IncidentID, Name = incp.Name, Phone = incp.Mobile, DOB = incp.DateOfBirth, Gender = incp.Gender, PatientIdn = incp.IdentityNo }
                                            ).ToList();

            ViewBag.IncPatients = IncPatients;

            ViewBag.IncidentPeople = IncidentPeople(id);
            ViewBag.IncidentInterviews = IncidentInterviews(id);

            ViewBag.Status = (from inst in db.Incident_Status_T
                              join ins in db.IncidentStatus_M on inst.IncidentStatusID equals ins.IncidentStatusID
                              join usr in db.Users_M on inst.CreatedUserID equals usr.UserID
                              where inst.IncidentID == id
                              select new _Incidents { IncidentStatus = ins.IncidentStatus, Name = usr.Name, RegisteredOn = inst.CreatedOn, IncidentStatusID = inst.IncidentStatusID }
                                                    ).ToList();


            List<_IncidentRootCauseAns> _IncidentRootCauseAnwser = db.Database.SqlQuery<_IncidentRootCauseAns>(@" select SubRootCauseAnswerID,insrc.IncidentMainRootCauseID,AnswerType,inmrc.IncidentMainRootCause,IncidentSubRootCause,insrcans.IncidentSubRootCauseID,Answer from [IncidentSubRootCauseAnswer_T] insrcans
 join IncidentSubRootCause_M insrc on insrcans.incidentsubrootcauseid=insrc.incidentsubrootcauseid
 join IncidentMainRootCause_M inmrc on insrc.IncidentMainRootCauseID = inmrc.IncidentMainRootCauseID where IncidentID = " + id + " ").ToList();
            ViewBag._IncidentRootCauseAnwser = _IncidentRootCauseAnwser;

            List<_IncidentOutcomeAns> _IncidentOutcomeAnwser = db.Database.SqlQuery<_IncidentOutcomeAns>(@" select SubOutcomeAnswerID,insrc.IncidentMainOutcomeID,AnswerType,inmrc.IncidentMainOutcome,IncidentSubOutcome,insrcans.IncidentSubOutcomeID,Answer from [IncidentSubOutcomeAnswer_T] insrcans
 join IncidentSubOutcome_M insrc on insrcans.incidentsubOutcomeid=insrc.incidentsubOutcomeid
 join IncidentMainOutcome_M inmrc on insrc.IncidentMainOutcomeID = inmrc.IncidentMainOutcomeID  where IncidentID = " + id + " ").ToList();
            ViewBag._IncidentOutcomeAnwser = _IncidentOutcomeAnwser;

            List<Incident_Recommendation_T> IncidentRecommendations = db.Incident_Recommendation_T.Where(x => x.IncidentID == id).ToList();
            ViewBag.IncidentRecommendations = IncidentRecommendations;

            return View(incident);
        }




        public ActionResult Comment(FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);

            Incident_Comments incCom = new Incident_Comments();
            incCom.IncidentID = Convert.ToInt32(col["IncidentID"]);
            incCom.UserID = UserID;
            incCom.CommentDateTime = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));
            incCom.Comment = Convert.ToString(col["Comment"]);
            db.Incident_Comments.Add(incCom);
            db.SaveChanges();
            return RedirectToAction("../Incidents/Details/" + incCom.IncidentID);
        }

        // GET: Incidents/Create
        public ActionResult Create()
        {

            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            Session["IncidentID"] = null;

            ViewBag.EventTypeID = db.EventType_SM.Where(x => x.EventTypeID < 8).ToList();
            ViewBag.BranchID = db.Branch_M.ToList();

            ViewBag.IncidentTypeID = db.IncidentTypes_M.ToList();

            ViewBag.DocumentType = db.DocumentType_SM.ToList();

            int userid = Convert.ToInt32(Session["UserID"]);

            List<_User> _user = (from x in db.Users_M
                                 join b in db.Branch_M on x.BranchID equals b.BranchID
                                 where x.UserID == userid
                                 select new _User { UserID = x.UserID, Branch = b.Branch, Name = x.Name, Email = x.UserName, Designation = x.Designation, Phone = x.Phone, BranchID = b.BranchID }
                               ).ToList();



            ViewBag.Userx = _user;


            List<IncidentPeopleInvolved_M> ipinAlldata = db.IncidentPeopleInvolved_M.Where(x => x.Active == true && x.IncidentPeopleInvolvedID>1).ToList();
            List<_IncidentPeopleInvolved> ipinNewlist = new List<_IncidentPeopleInvolved>();
            _IncidentPeopleInvolved _ipinnewItem;

            foreach (var ipin in ipinAlldata)
            {

                _ipinnewItem = new _IncidentPeopleInvolved();
                _ipinnewItem.IncidentPeopleInvolvedID = ipin.IncidentPeopleInvolvedID.ToString();
                _ipinnewItem.IncidentPeopleInvolved = ipin.IncidentPeopleInvolved;
                ipinNewlist.Add(_ipinnewItem);
            }
            ipinNewlist.Insert(0, new _IncidentPeopleInvolved { IncidentPeopleInvolvedID = "", IncidentPeopleInvolved = "Select" });

            ViewBag.IncidentPeopleInvolvedID = new SelectList(ipinNewlist, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            if (ModelState.IsValid)
            {

                Incident incident = new Incident();
                incident.EventTypeID = Convert.ToInt32(col["EventTypeID"]);
                incident.CreatedUserID = Convert.ToInt32(Session["UserID"]);
                incident.OnBehalfReporting = Convert.ToBoolean(col["OnBehalfReporting"]);
                incident.ReporterName = Convert.ToString(col["ReporterName"]);
                incident.ReporterEmail = Convert.ToString(col["ReporterEmail"]);
                incident.ReporterPhone = Convert.ToString(col["ReporterPhone"]);
                incident.ReporterDesignation = Convert.ToString(col["ReporterDesignation"]);
                incident.ReporterBranch = Convert.ToString(col["ReporterBranch"]);
                incident.IncidentDate = Convert.ToDateTime(col["IncidentDate"]);
                incident.IncidentCreatedDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));
                incident.Description = Convert.ToString(col["Description"]);
                incident.ActionTaken = Convert.ToString(col["ActionTaken"]);
                incident.BranchID = Convert.ToInt32(col["BranchID"]);
                incident.LocationID = Convert.ToInt32(col["LocationID"]);



                if (col["IncidentTypeID"] != "" && col["IncidentTypeID"] != null) {
                    incident.IncidentTypeID = Convert.ToInt32(col["IncidentTypeID"]);
                }
                else
                {
                    incident.IncidentTypeID = 0;
                }

                if (col["IncidentSubTypeID"] != "" && col["IncidentSubTypeID"] != null)
                {
                    incident.IncidentSubTypeID = Convert.ToInt32(col["IncidentSubTypeID"]);
                    incident.PriorityID = db.IncidentSubTypes_M.Where(x => x.IncidentSubTypeID == incident.IncidentSubTypeID).Select(x => x.IncidentPriorityID).SingleOrDefault();
                }
                else
                {
                    incident.IncidentSubTypeID = 0;
                }

                incident.Confidential = Convert.ToBoolean(col["Confidential"]);
                incident.IncidentGUID = Guid.NewGuid();
                incident.IncidentStatusID = 1;

                if (Session["IncidentID"] == null)
                {
                    db.Incidents.Add(incident);
                    db.SaveChanges();
                }
                else {
                    incident.IncidentID = Convert.ToInt32(Session["IncidentID"]);
                    db.Entry(incident).State = EntityState.Modified;
                    db.SaveChanges();
                }
                utl.setStatus(incident.IncidentID, 1, Convert.ToInt32(Session["UserID"]));


                Core.Utility mailutl = new Core.Utility();

                string mailbody = "Dear Administrator,<p>New Incident has been triggered, please check the system.</p><p>Incident No: " + incident.IncidentNo + " </p>";

                List<Users_M> admins = db.Users_M.Where(x => x.UserRoleID == 1).ToList();

                foreach (var e in admins)
                {

                    if (e.EmailID != null)
                    {
                        try
                        {
                            mailutl.sendemail("irrm@mehospital.com", e.EmailID, "Incident Triggered " + incident.IncidentNo, mailbody);
                        }
                        catch { }
                    }
                }


                Session["IncidentID"] = null;

                return RedirectToAction("../Incidents/Success");
            }

            return View();
        }


        public ActionResult CloseIncident(int IncidentID)
        {
            int CreatedUserID = Convert.ToInt32(Session["UserID"]);

            int? curstatusid = db.Incidents.Where(x => x.IncidentID == IncidentID).Select(x => x.IncidentStatusID).SingleOrDefault();

            if (curstatusid == 5 || curstatusid == 7) // if completed then closed or reopened
            {
                utl.setStatus(IncidentID, 6, CreatedUserID);
            }
            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }



        public ActionResult SetStatusInvestigationCompleted(int IncidentID)
        {
            int CreatedUserID = Convert.ToInt32(Session["UserID"]);


            int? curstatusid = db.Incidents.Where(x => x.IncidentID == IncidentID).Select(x => x.IncidentStatusID).SingleOrDefault();


            if (curstatusid == 4) // inprogress
            {
                utl.setStatus(IncidentID, 5, CreatedUserID); // completed
            }
            
            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, Outcome = inc.Outcome }
                                          ).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incx.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult Success() {


            return View();
        }

        [HttpPost]
        public ActionResult UpdateRootCauseAnalysis(FormCollection col)
        {

            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            int IncidentMainRootCauseID = Convert.ToInt32(col["IncidentMainRootCauseID"]);
            Incidents_MainRootCause_T im = new Incidents_MainRootCause_T();
            im.IncidentID = IncidentID;
            im.IncidentMainRootCauseID = IncidentMainRootCauseID;
            im.Answered = false;
            im.CreatedDate = DateTime.UtcNow.AddHours(3);
            db.Incidents_MainRootCause_T.Add(im);
            db.SaveChanges();

            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, Outcome = inc.Outcome }
                                       ).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incx.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        [HttpPost]
        public ActionResult UpdateOutcome(FormCollection col)
        {

            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            int IncidentMainOutcomeID = Convert.ToInt32(col["IncidentMainOutcomeID"]);
            Incidents_MainOutcome_T imx = new Incidents_MainOutcome_T();
            imx.IncidentID = IncidentID;
            imx.IncidentMainOutcomeID = IncidentMainOutcomeID;
            imx.Answered = false;
            imx.CreatedDate = DateTime.UtcNow.AddHours(3);
            db.Incidents_MainOutcome_T.Add(imx);
            db.SaveChanges();


            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, Outcome = inc.Outcome }
                                    ).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incx.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        //[HttpPost]
        //public ActionResult AddPatient(FormCollection col)
        //{
        //    if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
        //    int IncidentID = 0;

        //    if (col["IncidentID"] == null)
        //    {

        //        if (Session["IncidentID"] == null)
        //        {
        //            Incident inc = new Incident();
        //            inc.IncidentCreatedDate = DateTime.UtcNow;
        //            db.Incidents.Add(inc);
        //            db.SaveChanges();
        //            IncidentID = Convert.ToInt32(inc.IncidentID);
        //            Session["IncidentID"] = IncidentID;
        //        }
        //        else
        //        {
        //            IncidentID = Convert.ToInt32(Session["IncidentID"]);
        //        }
        //    }
        //    else
        //    {
        //        IncidentID = Convert.ToInt32(col["IncidentID"]);
        //    }


        //    Incident_Patient_T incp = new Incident_Patient_T();
        //    incp.IncidentID = IncidentID;
        //    incp.Name = Convert.ToString(col["patientname"]);
        //    incp.Phone = Convert.ToString(col["patientphone"]);
        //    incp.PatientIdn = Convert.ToString(col["patientidn"]);
        //    incp.Gender = Convert.ToString(col["patientgender"]);
        //    incp.DOB = Convert.ToDateTime(Convert.ToDateTime(col["patientdob"]).ToString("yyyy/MM/dd 00:00:00"));
        //    db.Incident_Patient_T.Add(incp);
        //    db.SaveChanges();


        //    List<_IncPatients> IncPatients = (from incpx in db.Incident_Patient_T
        //                                      where incpx.IncidentID == IncidentID
        //                                      select new _IncPatients { IncidentID = incpx.IncidentID, Name = incpx.Name, Phone = incpx.Phone, PatientIdn = incpx.PatientIdn, DOB = incpx.DOB, Gender = incpx.Gender }
        //                           ).ToList();

        //    return new JsonpResult
        //    {
        //        ContentEncoding = Encoding.UTF8,
        //        Data = IncPatients.ToList(),
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //    };
        //}

        public ActionResult ViewPatients(int IncidentID)
        {
            List<_IncPatients> IncPatients = (from incp in db.Incident_People_T
                                              where incp.IncidentID == IncidentID
                                              select new _IncPatients { IncidentID = incp.IncidentID, Name = incp.Name, Phone = incp.Mobile, DOB = incp.DateOfBirth, Gender = incp.Gender, PatientIdn = incp.IdentityNo }
                                        ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = IncPatients.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        public ActionResult ViewRootCauseAnalysis(int IncidentID)
        {

            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, RootCauseAnalysis = inc.RootCauseAnalysis }
                                          ).ToList();


            List<_IncidentDetails> LstInvRpt = new List<_IncidentDetails>();
            _IncidentDetails Invrpt = new _IncidentDetails();
            foreach (var rpt in incx)
            {
                Invrpt.IncidentID = rpt.IncidentID;
                if (rpt.RootCauseAnalysis != null)
                {
                    Invrpt.RootCauseAnalysis = rpt.RootCauseAnalysis.Replace("\n", "<br>");
                }
                else
                {
                    Invrpt.RootCauseAnalysis = rpt.RootCauseAnalysis;
                }
                LstInvRpt.Add(Invrpt);
            }

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = LstInvRpt.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        public ActionResult ViewOutcome(int IncidentID)
        {

            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, Outcome = inc.Outcome }
                                          ).ToList();



            List<_IncidentDetails> LstInvRpt = new List<_IncidentDetails>();
            _IncidentDetails Invrpt = new _IncidentDetails();
            foreach (var rpt in incx)
            {
                Invrpt.IncidentID = rpt.IncidentID;
                if (rpt.Outcome != null)
                {
                    Invrpt.Outcome = rpt.Outcome.Replace("\n", "<br>");
                }
                else {
                    Invrpt.Outcome = rpt.Outcome;

                }
                LstInvRpt.Add(Invrpt);
            }

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = LstInvRpt.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        [HttpPost]
        public ActionResult UpdateOutcomeMain(FormCollection col)
        {

            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            string outcome = Convert.ToString(col["Outcomex"]).Replace("'", "");

            db.Database.ExecuteSqlCommand("Update Incidents set Outcome='" + outcome + "' where IncidentID=" + IncidentID);
            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, RemedialAction = inc.RemedialAction }
                                          ).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incx.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        [HttpPost]
        public ActionResult UpdateRemedialAction(FormCollection col)
        {

            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            string ra = Convert.ToString(col["RemedialAction"]);

            Incident incd = db.Incidents.Find(IncidentID);

            incd.RemedialAction = Convert.ToString(col["RemedialAction"]);
            db.Entry(incd).Property(x => x.RemedialAction).IsModified = true;
            db.SaveChanges();
 
            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, RemedialAction = inc.RemedialAction }
                                          ).ToList();


            return RedirectToAction("../Incidents/Details/" + IncidentID);
        }


        public ActionResult ViewRemedialAction(int IncidentID)
        {

            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, RemedialAction = inc.RemedialAction }
                                          ).ToList();


            List<_IncidentDetails> LstInvRpt = new List<_IncidentDetails>();

            _IncidentDetails Invrpt = new _IncidentDetails();

            foreach (var rpt in incx)
            {

                Invrpt.IncidentID = rpt.IncidentID;
                if (rpt.RemedialAction != null)
                {
                    Invrpt.RemedialAction = rpt.RemedialAction.Replace("\n", "<br>");
                }
                else
                {
                    Invrpt.RemedialAction = rpt.RemedialAction;
                }
                LstInvRpt.Add(Invrpt);
            }

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = LstInvRpt.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        [HttpPost]
        public ActionResult UpdateInvestigatorReport(FormCollection col)
        {

            int IncidentID = Convert.ToInt32(col["IncidentID"]);

            string InvestigatorReport = Convert.ToString(col["InvestigatorReport"]).Replace("'", "");
            string InvestigationConclusion = Convert.ToString(col["InvestigationConclusion"]).Replace("'", "");

            db.Database.ExecuteSqlCommand("Update Incidents set InvestigatorReport='" + InvestigatorReport + "', InvestigationConclusion='" + InvestigationConclusion + "' where IncidentID=" + IncidentID);
            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, InvestigatorReport = inc.InvestigatorReport }
                                          ).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incx.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult ViewInvestigatorReport(int IncidentID)
        {

            List<_IncidentDetails> incx = (from inc in db.Incidents
                                           where inc.IncidentID == IncidentID
                                           select new _IncidentDetails { IncidentID = inc.IncidentID, InvestigatorReport = inc.InvestigatorReport, InvestigationConclusion = inc.InvestigationConclusion }
                                          ).ToList();


            List<_IncidentDetails> LstInvRpt = new List<_IncidentDetails>();

            _IncidentDetails Invrpt = new _IncidentDetails();

            foreach (var rpt in incx) {

                Invrpt.IncidentID = rpt.IncidentID;
                if (rpt.InvestigatorReport != null)
                {
                    Invrpt.InvestigatorReport = rpt.InvestigatorReport.Replace("\n", "<br>");
                }
                else {
                    Invrpt.InvestigatorReport = rpt.InvestigatorReport;
                }
                if (rpt.InvestigationConclusion != null)
                {
                    Invrpt.InvestigationConclusion = rpt.InvestigationConclusion.Replace("\n", "<br>");
                }
                else
                {
                    Invrpt.InvestigationConclusion = rpt.InvestigationConclusion;
                }



                LstInvRpt.Add(Invrpt);
            }

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = LstInvRpt.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        [HttpPost]
        public ActionResult AddRecommendation(FormCollection col)
        {
            int UserID = Convert.ToInt32(Session["UserID"]);

            Incident_Recommendation_T increc = new Incident_Recommendation_T();
            increc.IncidentID = Convert.ToInt32(col["IncidentID"]);
            increc.Recommendation = Convert.ToString(col["Recommendations"]);
            increc.CreatedUserID = UserID;
            db.Incident_Recommendation_T.Add(increc);
            db.SaveChanges();
            
            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult DeleteRecommendation(int IncidentID, int RecommendationID)
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            if (Convert.ToString(Session["RoleID"]) == "1") // admin
            {
                db.Database.ExecuteSqlCommand("delete from [Incident-Recommendation_T] where incidentid=" + IncidentID + " and RecommendationID=" + RecommendationID);
            }
            else { 
                db.Database.ExecuteSqlCommand("delete from [Incident-Recommendation_T] where incidentid=" + IncidentID + " and createduserid=" + UserID + " and RecommendationID=" + RecommendationID);

            }
            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult GetRecommendations(int IncidentID)
        {
            int UserID = Convert.ToInt32(Session["UserID"]);

            List<_Recommendations> recmd = (from incr in db.Incident_Recommendation_T
                                            where incr.IncidentID == IncidentID
                                            select new _Recommendations { Recommendation = incr.Recommendation, RecommendationID = incr.RecommendationID }
                                            ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = recmd.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }



        [HttpPost]
        public ActionResult AddAttachment(FormCollection col, HttpPostedFileBase UploadMedia)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int IncidentID = 0;

            if (col["IncidentID"] == null)
            {

                if (Session["IncidentID"] == null)
                {
                    Incident inc = new Incident();
                    inc.IncidentCreatedDate = DateTime.UtcNow;
                    db.Incidents.Add(inc);
                    db.SaveChanges();
                    IncidentID = Convert.ToInt32(inc.IncidentID);
                    Session["IncidentID"] = IncidentID;
                }
                else
                {
                    IncidentID = Convert.ToInt32(Session["IncidentID"]);
                }
            }
            else
            {
                IncidentID = Convert.ToInt32(col["IncidentID"]);
            }

            if (UploadMedia != null)
            {

                var id = new IncidentDocument_T()
                {
                    IncidentID = IncidentID,
                    DocumentTypeID = Convert.ToInt32(col["DocumentTypeID"]),
                    Description = Convert.ToString(col["Description"]),
                    FileName = UploadMedia.FileName,
                    DocGUID = Guid.NewGuid(),
                };

                db.IncidentDocument_T.Add(id);
                db.SaveChanges();

                string extension = Path.GetExtension(UploadMedia.FileName);
                extension = extension.TrimStart('.');

                string folderpath = Server.MapPath("~/Documents/Incidents/" + id.IncidentID + "/");
                DirectoryInfo dir = new DirectoryInfo(folderpath);
                if (!dir.Exists)
                {
                    dir.Create();
                }


                UploadMedia.SaveAs(folderpath + "/" + Convert.ToString(id.DocGUID) + "." + extension);
            }



            List<_IncidentDocument> iv = new List<_IncidentDocument>();
            iv = (from d in db.IncidentDocument_T
                  join t in db.DocumentType_SM on d.DocumentTypeID equals t.DocumentTypeID
                  where d.IncidentID == IncidentID
                  select new _IncidentDocument { IncidentDocumentID = d.IncidentDocumentID, DocumentType = t.DocumentType, FileName = d.FileName, Description = d.Description, IncidentID = d.IncidentID }).Take(1).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = iv.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult ViewAttachment(int IncidentID)
        {

         

            List<_IncidentDocument> iv = new List<_IncidentDocument>();
            iv = (from d in db.IncidentDocument_T
                  join t in db.DocumentType_SM on d.DocumentTypeID equals t.DocumentTypeID
                  where d.IncidentID == IncidentID
                  select new _IncidentDocument { IncidentDocumentID = d.IncidentDocumentID, DocumentType = t.DocumentType, FileName = d.FileName, Description = d.Description, IncidentID = d.IncidentID, DocGUID= d.DocGUID }).ToList();

            List<_IncidentDocument> dox = new List<_IncidentDocument>();

            foreach (var doc in iv)
            {

                _IncidentDocument docer = new _IncidentDocument();
                docer.IncidentDocumentID = doc.IncidentDocumentID;
                docer.IncidentID = doc.IncidentID;
                docer.DocumentType = doc.DocumentType;
                docer.Description = doc.Description;
                docer.FileName = doc.FileName;

                string[] exten = doc.FileName.Split('.');

                docer.ActualFileName = doc.DocGUID + "." + exten[1];

                dox.Add(docer);
            }

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = dox.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

 


        public ActionResult DeleteAttachment(int IncidentDocumentID, int IncidentID)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            db.Database.ExecuteSqlCommand("Delete from IncidentDocument_T where IncidentID=" + IncidentID + " and IncidentDocumentID=" + IncidentDocumentID);

            return Content("OK");
        }




        //[HttpPost]
        //public ActionResult AddWitness(FormCollection col)
        //{
        //    if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

        //    int IncidentID = 0;

        //    if (col["IncidentID"] == null)
        //    {

        //        if (Session["IncidentID"] == null)
        //        {
        //            Incident inc = new Incident();
        //            inc.IncidentCreatedDate = DateTime.UtcNow;
        //            db.Incidents.Add(inc);
        //            db.SaveChanges();
        //            IncidentID = Convert.ToInt32(inc.IncidentID);
        //            Session["IncidentID"] = IncidentID;
        //        }
        //        else
        //        {
        //            IncidentID = Convert.ToInt32(Session["IncidentID"]);
        //        }
        //    }
        //    else
        //    {
        //        IncidentID = Convert.ToInt32(col["IncidentID"]);
        //    }




        //    List<_IncWitness> iv = new List<_IncWitness>();
        //    iv = (from v in db.IncidentWitness_T
        //          join b in db.Branch_M on v.BranchID equals b.BranchID
        //          where v.IncidentID == IncidentID
        //          select new _IncWitness { Name = v.Name, Email = v.Email, Designation = v.Designation, Phone = v.Phone, BranchID = v.BranchID, IncidentWitnessID = v.IncidentWitnessID, Branch = b.Branch, IncidentID = v.IncidentID }).Take(1).ToList();


        //    return new JsonpResult
        //    {
        //        ContentEncoding = Encoding.UTF8,
        //        Data = iv.ToList(),
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //    };
        //}

        //public ActionResult ViewWitness(int IncidentID)
        //{


        //    List<_IncWitness> iv = new List<_IncWitness>();
        //    iv = (from v in db.IncidentWitness_T
        //          join b in db.Branch_M on v.BranchID equals b.BranchID
        //          where v.IncidentID == IncidentID
        //          select new _IncWitness { Name = v.Name, Email = v.Email, Designation = v.Designation, Phone = v.Phone, BranchID = v.BranchID, IncidentWitnessID = v.IncidentWitnessID, Branch = b.Branch, IncidentID = v.IncidentID }).ToList();


        //    return new JsonpResult
        //    {
        //        ContentEncoding = Encoding.UTF8,
        //        Data = iv.ToList(),
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //    };
        //}

        public ActionResult DeleteWitness(int IncidentWitnessID, int IncidentID)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            db.Database.ExecuteSqlCommand("Delete from IncidentWitness_T where IncidentID=" + IncidentID + " and IncidentWitnessID=" + IncidentWitnessID);

            return Content("OK");
        }



        public ActionResult ViewPeople(int IncidentID)
        {
            List<_IncPeople> ipn2 = IncidentPeople(IncidentID);
            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = ipn2.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult DeletePeople(int IncidentPeopleID, int IncidentID)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int incdrelationid = (int)db.Incident_People_T.Where(x => x.IncidentPeopleID == IncidentPeopleID).Select(x => x.IncidentRelationID).SingleOrDefault();
            db.Database.ExecuteSqlCommand("Delete from [Incident-People_T] where IncidentID=" + IncidentID + " and IncidentPeopleID=" + IncidentPeopleID);
                       

            if (incdrelationid == 1)
            {// lead investigator 
                db.Database.ExecuteSqlCommand("Update Incidents set IncidentStatusID=2 where incidentid=" + IncidentID);
            }


            return Content("OK");
        }


        public List<_IncPeople> IncidentPeople(int IncidentID) {

            List<_IncPeople> ipn = db.Database.SqlQuery<_IncPeople>(@"select distinct ip.IncidentPeopleID,ip.IsPatientInjured,ip.Gender,ip.DateOfbirth,ip.IdentityNo,ip.RegNo,ip.CVRNo,u.LicenseNo,ip.IncidentRelationID,ip.IdentityNo,ip.UserID
,IncidentID, ip.Name, ip.Mobile,ip.UserID, u.Name as UserName,u.Phone as UserMobile,IncidentRelation, IncidentPeopleInvolved, u.Designation as Designation,u.LicenseNo as LicenseNo
from [dbo].[Incident-People_T] ip
left join Users_M u on ip.UserID=u.UserID
left join IncidentPeopleInvolved_M pi on ip.IncidentPeopleInvolvedID= pi.IncidentPeopleInvolvedID
left join IncidentRelation_M ir on ip.IncidentRelationID = ir.IncidentRelationID where ip.incidentid=" + IncidentID).ToList();

            List<_IncPeople> ipn2 = new List<_IncPeople>();

            _IncPeople incp2;

            foreach (var item in ipn)
            {
                incp2 = new _IncPeople();
                incp2.UserID = item.UserID;
                incp2.IncidentID = item.IncidentID;
                incp2.IncidentPeopleInvolved = item.IncidentPeopleInvolved;
                incp2.IncidentRelation = item.IncidentRelation;
                incp2.IncidentRelationID = item.IncidentRelationID;
                
                
                incp2.CreatedDate = item.CreatedDate;
                incp2.Designation = item.Designation;
                if (item.UserID != null)
                {
                    incp2.Name = item.UserName;
                    incp2.Mobile = item.UserMobile;
                    incp2.IdentityNo = item.LicenseNo;
                }
                else
                {
                    incp2.Name = item.Name;
                    incp2.Mobile = item.Mobile;
                    incp2.IdentityNo = item.IdentityNo;
                    incp2.RegNo = item.RegNo;
                    incp2.CVRNo = item.CVRNo;
                }
                incp2.DateOfBirth = item.DateOfBirth;
                incp2.Gender = item.Gender;
                incp2.IsPatientInjured = item.IsPatientInjured;
                incp2.IncidentPeopleID = item.IncidentPeopleID;
                ipn2.Add(incp2);
            }


            return ipn2;
        }


        public List<_IncidentInterviews> IncidentInterviews(int IncidentID) {

            List<_IncidentInterviews> ipn = db.Database.SqlQuery<_IncidentInterviews>(@"select inte.IncidentInterviewID,inte.IncidentID,interviewer.Name as Interviewer, interviewee.Name as Interviewee ,intervieweeuser.Name, 
 inte.Details,inte.InterviewDate, inte.CreatedOn   from [dbo].[Incident-Interview_T] inte
left join Users_M interviewer on inte.InterviewerID = interviewer.UserID
left join [Incident-People_T] interviewee on inte.IncidentPeopleID = interviewee.IncidentPeopleID
left join Users_M intervieweeuser on interviewee.UserID = intervieweeuser.UserID
where inte.IncidentID=" + IncidentID).ToList();

            List<_IncidentInterviews> Interview = new List<_IncidentInterviews>();
            _IncidentInterviews intvw;

            foreach (var intr in ipn)
            {
                intvw = new _IncidentInterviews();
                intvw.IncidentInterviewID = intr.IncidentInterviewID;
                intvw.IncidentID = intr.IncidentID;
                intvw.InterviewDate = intr.InterviewDate;
                intvw.Details = intr.Details;
                intvw.Interviewer = intr.Interviewer;

                if (intr.Interviewee == null)
                {
                    intvw.Interviewee = intr.Name;
                }
                else
                {
                    intvw.Interviewee = intr.Interviewee;
                }
                intvw.CreatedOn = intr.CreatedOn;
                Interview.Add(intvw);
            }

            return Interview;
        }

        public ActionResult ViewInterviews(int IncidentID)
        {

            List<_IncidentInterviews> Interview = IncidentInterviews(IncidentID);


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = Interview.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult DeleteInterviews(int IncidentInterviewID, int IncidentID)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            db.Database.ExecuteSqlCommand("Delete from [Incident-Interview_T] where IncidentID=" + IncidentID + " and IncidentInterviewID=" + IncidentInterviewID);

            return Content("OK");
        }


        


        // GET: Incidents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident incident = db.Incidents.Find(id);
            if (incident == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTypeID = new SelectList(db.EventType_SM, "EventTypeID", "Prefix", incident.EventTypeID);
            ViewBag.IncidentFeedbackTypeID = new SelectList(db.IncidentFeedbackType_M, "IncidentFeedbackTypeID", "IncidentFeedbackType", incident.IncidentFeedbackTypeID);
            ViewBag.IncidentStatusID = new SelectList(db.IncidentStatus_M, "IncidentStatusID", "IncidentStatus", incident.IncidentStatusID);
            ViewBag.IncidentSubTypeID = new SelectList(db.IncidentSubTypes_M, "IncidentSubTypeID", "IncidentSubType", incident.IncidentSubTypeID);
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incident.IncidentTypeID);
            ViewBag.LocationID = new SelectList(db.Location_M, "LocationID", "Location", incident.LocationID);
            ViewBag.CreatedUserID = new SelectList(db.Users_M, "UserID", "UserName", incident.CreatedUserID);
            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", incident.BranchID);
            return View(incident);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentID,EventTypeID,EventSubTypeID,IncidentNo,CreatedUserID,OnBehalfReporting,ReporterName,ReporterEmail,ReporterDesignation,ReporterBranch,ReporterPhone,IncidentDate,Description,ActionTaken,BranchID,LocationID,IncidentTypeID,IncidentSubTypeID,Confidential,InvestigationComments,InvestigatorReport,IncidentFeedbackTypeID,FeedbackComments,IncidentGUID,Registered,RegisteredOn,IncidentStatusID,IncidentCreatedDate")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incident).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeID = new SelectList(db.EventType_SM, "EventTypeID", "Prefix", incident.EventTypeID);
            ViewBag.IncidentFeedbackTypeID = new SelectList(db.IncidentFeedbackType_M, "IncidentFeedbackTypeID", "IncidentFeedbackType", incident.IncidentFeedbackTypeID);
            ViewBag.IncidentStatusID = new SelectList(db.IncidentStatus_M, "IncidentStatusID", "IncidentStatus", incident.IncidentStatusID);
            ViewBag.IncidentSubTypeID = new SelectList(db.IncidentSubTypes_M, "IncidentSubTypeID", "IncidentSubType", incident.IncidentSubTypeID);
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incident.IncidentTypeID);
            ViewBag.LocationID = new SelectList(db.Location_M, "LocationID", "Location", incident.LocationID);
            ViewBag.CreatedUserID = new SelectList(db.Users_M, "UserID", "UserName", incident.CreatedUserID);
            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", incident.BranchID);
            return View(incident);
        }

        public ActionResult Update(FormCollection col)
        {
            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            Incident incident = db.Incidents.FirstOrDefault(x => x.IncidentID == IncidentID);

            if (col["BranchID"] != null)
            {
                incident.BranchID = Convert.ToInt32(col["BranchID"]);
            }
            else
            {
                incident.BranchID = 0;
            }

            if (col["LocationID"] != null)
            {
                incident.LocationID = Convert.ToInt32(col["LocationID"]);
            }
            if (col["IncidentTypeID"] != "")
            {
                incident.IncidentTypeID = Convert.ToInt32(col["IncidentTypeID"]);
            }
            if (col["IncidentSubTypeID"] != "")
            {
                incident.IncidentSubTypeID = Convert.ToInt32(col["IncidentSubTypeID"]);
            }
            
            if(col["IncidentDate"] != null)
            {
                incident.IncidentDate = Convert.ToDateTime(col["IncidentDate"]);
            }

            if (col["PriorityID"] != null && col["PriorityID"] != "" && incident.RegisteredOn != null)
            {
                int priorityid = Convert.ToInt32(col["PriorityID"]);
                incident.PriorityID = priorityid;
                double investdays = (double)db.IncidentPriorities_M.Where(x => x.IncidentPriorityID == priorityid).Select(x => x.InvestigationDays).SingleOrDefault();
                incident.InvestigationDueDate = Convert.ToDateTime(incident.RegisteredOn).AddDays(investdays);
            }
            else {

                incident.InvestigationDueDate = null;
            }

            if (col["Description"] != null)
            {
                incident.Description = Convert.ToString(col["Description"]);
            }

            if (col["ActionTaken"] != null)
            {
                incident.ActionTaken = Convert.ToString(col["ActionTaken"]);
            }

            if (col["HarmScoreID"] != "")
            {
                incident.HarmScoreID = Convert.ToInt32(col["HarmScoreID"]);
            }

            incident.EventTypeID = Convert.ToInt32(col["EventTypeID"]); 

            incident.CreatorCanComment = Convert.ToBoolean(col["CreatorCanComment"]);
            incident.RiskAnalysisRequired = Convert.ToBoolean(col["RiskAnalysisRequired"]);

            
            db.Entry(incident).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("../Incidents/Details/" + incident.IncidentID);
        }

        [HttpPost]
        public ActionResult StatusUpdate(FormCollection col)
        {
            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            int IncidentStatusID = Convert.ToInt32(col["IncidentStatusID"]);
            utl.setStatus(IncidentID, IncidentStatusID, Convert.ToInt32(Session["UserID"]));
            db.Database.ExecuteSqlCommand("Update Incidents set ReOpened=1,ReOpenRemarks='- " + col["ReOpenRemarks"] + "'+ '\n\n'+ convert(nvarchar(max),ISNULL(ReOpenRemarks,''))  where IncidentID=" + IncidentID);


            string strPathAndQuery = HttpContext.Request.Url.PathAndQuery;
            string strUrl = HttpContext.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            string pagepath = strUrl + "Incidents/Report/" + IncidentID;
            string pagecontent = Get_HTML(pagepath);

            IncidentHistory inhs = new IncidentHistory();
            inhs.IncidentID = IncidentID;
            inhs.HistoryDate = DateTime.UtcNow.AddHours(3);
            inhs.PageContent = pagecontent;
            db.IncidentHistories.Add(inhs);
            db.SaveChanges();


            return RedirectToAction("../Incidents/Details/" + IncidentID);
        }
        



        // GET: Incidents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident incident = db.Incidents.Find(id);
            if (incident == null)
            {
                return HttpNotFound();
            }
            return View(incident);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incident incident = db.Incidents.Find(id);
            db.Incidents.Remove(incident);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Register(int incidentid, string prefix)
        {
            Utility utl = new Utility();
            string newregno = utl.Getregistryno(prefix);
            String CreatedOn = Convert.ToDateTime(DateTime.UtcNow.AddHours(3)).ToString("yyyy/MM/dd HH:mm:ss");
            db.Database.ExecuteSqlCommand("Update Incidents set registered=1,registeredon='" + CreatedOn + "',IncidentNo='" + newregno + "' where incidentid=" + incidentid);
            utl.setStatus(incidentid, 2, Convert.ToInt32(Session["UserID"]));
            return Content("OK");
        }


        [HttpGet]
        public ActionResult Invalid(int incidentid)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            db.Database.ExecuteSqlCommand("delete from [Incident-People_T] where incidentid="+ incidentid + ";delete from  Incidents where incidentid=" + incidentid);
            return Content("OK");
        }

        [HttpPost]
        public ActionResult Feedback(FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            int FeedbackType = Convert.ToInt32(col["FeedbackType"]);
            string Feedback = Convert.ToString(col["Feedback"]);
            db.Database.ExecuteSqlCommand("update Incidents set FeedbackTypeID=" + FeedbackType + ",Feedback='" + Feedback + "'  where incidentid=" + IncidentID);
            return RedirectToAction("../Incidents/Details/" + IncidentID);
        }

        [HttpPost]
        public ActionResult AddIncidentTask(FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int IncidentID = Convert.ToInt32(col["IncidentID"]);

            int taskid = db.Task_T.Where(x => x.IncidentID == IncidentID).Select(x => x.TaskID).SingleOrDefault();

            if (taskid == 0)
            {
                Task_T tsk = new Task_T();
                tsk.IncidentID = IncidentID;
                tsk.CreatedUserID = Convert.ToInt32(Session["UserID"]);
                tsk.CreatedDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));
                tsk.Task = Convert.ToString(col["Task"]);
                db.Task_T.Add(tsk);
                db.SaveChanges();
            }
            else {
                Task_T tsk = new Task_T();
                tsk.TaskID = taskid;
                tsk.IncidentID = IncidentID;
                tsk.CreatedUserID = Convert.ToInt32(Session["UserID"]);
                tsk.CreatedDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));
                tsk.Task = Convert.ToString(col["Task"]);
                db.Entry(tsk).State = EntityState.Modified;
                db.SaveChanges();
            }

            List<_Tasks> incd = (from ts in db.Task_T
                                 where ts.IncidentID == IncidentID
                                 select new _Tasks { IncidentID = ts.IncidentID, Task = ts.Task, TaskID = ts.TaskID, CreatedDate = ts.CreatedDate, CreatedUserID = ts.CreatedUserID }
                               ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incd.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        public ActionResult GetTasks(int IncidentID) {

            List<_Tasks> incd = (from ts in db.Tasks
                                 where ts.ReferenceID == IncidentID && ts.Registered == true
                                 select new _Tasks { Task = ts.TaskNo, TaskID = ts.TaskID }
                                 ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incd.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

      
        public ActionResult DetachMRCTemplate(int IncidentMainRootCauseID, int IncidentID)
        {
            db.Database.ExecuteSqlCommand("Delete from [Incidents_MainRootCause_T] where incidentid=" + IncidentID + " and IncidentMainRootCauseID=" + IncidentMainRootCauseID);

            List<_Tasks> incd = (from ts in db.Task_T
                                 where ts.IncidentID == IncidentID
                                 select new _Tasks { IncidentID = ts.IncidentID, Task = ts.Task, TaskID = ts.TaskID, CreatedDate = ts.CreatedDate, CreatedUserID = ts.CreatedUserID }
                                 ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incd.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult DetachOutcomeTemplate(int IncidentMainOutcomeID, int IncidentID)
        {
            db.Database.ExecuteSqlCommand("Delete from [Incidents_MainOutcome_T] where incidentid=" + IncidentID + " and IncidentMainOutcomeID=" + IncidentMainOutcomeID);

            List<_Tasks> incd = (from ts in db.Task_T
                                 where ts.IncidentID == IncidentID
                                 select new _Tasks { IncidentID = ts.IncidentID, Task = ts.Task, TaskID = ts.TaskID, CreatedDate = ts.CreatedDate, CreatedUserID = ts.CreatedUserID }
                                 ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incd.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

    
        public ActionResult DeleteMRCAns(int IncidentMainRootCauseID, int IncidentID)
        {
            db.Database.ExecuteSqlCommand(@"Update [Incidents_MainRootCause_T] set Answered=0 where IncidentID=" + IncidentID + " and IncidentMainRootCauseID="+ IncidentMainRootCauseID + ";Delete from [IncidentSubRootCauseAnswer_T] where IncidentID= " + IncidentID + " and IncidentSubRootCauseID in (select IncidentSubRootCauseID from IncidentSubRootCause_M where IncidentMainRootCauseID=" + IncidentMainRootCauseID + ")");

            List<_Tasks> incd = (from ts in db.Task_T
                                 where ts.IncidentID == IncidentID
                                 select new _Tasks { IncidentID = ts.IncidentID, Task = ts.Task, TaskID = ts.TaskID, CreatedDate = ts.CreatedDate, CreatedUserID = ts.CreatedUserID }
                                 ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incd.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult DeleteMOCAns(int IncidentMainOutcomeID, int IncidentID)
        {
            db.Database.ExecuteSqlCommand(@"Update [Incidents_MainOutcome_T] set Answered=0 where IncidentID=" + IncidentID + " and IncidentMainOutcomeID="+ IncidentMainOutcomeID + ";Delete from [IncidentSubOutcomeAnswer_T] where IncidentID= " + IncidentID + " and IncidentSubOutcomeID in (select IncidentSubOutcomeID from IncidentSubOutcome_M where IncidentMainOutcomeID=" + IncidentMainOutcomeID + ")");

            List<_Tasks> incd = (from ts in db.Task_T
                                 where ts.IncidentID == IncidentID
                                 select new _Tasks { IncidentID = ts.IncidentID, Task = ts.Task, TaskID = ts.TaskID, CreatedDate = ts.CreatedDate, CreatedUserID = ts.CreatedUserID }
                                 ).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = incd.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        

        [HttpPost]
        public ActionResult MRCtemplatePost(FormCollection col)
        {

            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            string IncidentSubRootCauseID = Convert.ToString(col["IncidentSubRootCauseID"]);
            string IncidentMainRootCauseID = Convert.ToString(col["IncidentMainRootCauseID"]);

            string[] AryIncidentSubRootCauseID = IncidentSubRootCauseID.Split(',');
            IncidentSubRootCauseAnswer_T _IncidentSubRootCauseAnswer_T;
            foreach (var x in AryIncidentSubRootCauseID)
            {
                _IncidentSubRootCauseAnswer_T = new IncidentSubRootCauseAnswer_T();
                _IncidentSubRootCauseAnswer_T.IncidentSubRootCauseID = Convert.ToInt32(x);
                _IncidentSubRootCauseAnswer_T.IncidentID = IncidentID;

                if (Convert.ToString(col["AnswerType-" + Convert.ToInt32(x)]) == "1")
                {
                    _IncidentSubRootCauseAnswer_T.Answer = Convert.ToString(col["ta-" + Convert.ToInt32(x)]);
                }
                else
                {
                    _IncidentSubRootCauseAnswer_T.Answer = Convert.ToString(col["chk-" + Convert.ToInt32(x)]);
                }
                _IncidentSubRootCauseAnswer_T.AnswerTypeID = Convert.ToInt32(Convert.ToString(col["AnswerType-" + Convert.ToInt32(x)]));

                db.IncidentSubRootCauseAnswer_T.Add(_IncidentSubRootCauseAnswer_T);
                db.SaveChanges();
            }

            db.Database.ExecuteSqlCommand("update [Incidents_MainRootCause_T] set answered=1 where IncidentID=" + IncidentID + " and IncidentMainRootCauseID=" + IncidentMainRootCauseID);

            return RedirectToAction("../Incidents/Details/" + IncidentID);
        }


        [HttpPost]
        public ActionResult MOutcometemplatePost(FormCollection col)
        {
            int IncidentID = Convert.ToInt32(col["IncidentID"]);
            string IncidentSubOutcomeID = Convert.ToString(col["IncidentSubOutcomeID"]);
            string IncidentMainOutcomeID = Convert.ToString(col["IncidentMainOutcomeID"]);
            string[] AryIncidentSubOutcomeID = IncidentSubOutcomeID.Split(',');
            IncidentSubOutcomeAnswer_T _IncidentSubOutcomeAnswer_T;
            foreach (var x in AryIncidentSubOutcomeID)
            {
                _IncidentSubOutcomeAnswer_T = new IncidentSubOutcomeAnswer_T();
                _IncidentSubOutcomeAnswer_T.IncidentSubOutcomeID = Convert.ToInt32(x);
                _IncidentSubOutcomeAnswer_T.IncidentID = IncidentID;

                if (Convert.ToString(col["AnswerType-" + Convert.ToInt32(x)]) == "1")
                {
                    _IncidentSubOutcomeAnswer_T.Answer = Convert.ToString(col["ta-" + Convert.ToInt32(x)]);
                }
                else
                {
                    _IncidentSubOutcomeAnswer_T.Answer = Convert.ToString(col["chk-" + Convert.ToInt32(x)]);
                }
                _IncidentSubOutcomeAnswer_T.AnswerTypeID = Convert.ToInt32(Convert.ToString(col["AnswerType-" + Convert.ToInt32(x)]));

                db.IncidentSubOutcomeAnswer_T.Add(_IncidentSubOutcomeAnswer_T);
                db.SaveChanges();
            }
            db.Database.ExecuteSqlCommand("update [Incidents_MainOutcome_T] set answered=1 where IncidentID=" + IncidentID + " and IncidentMainOutcomeID=" + IncidentMainOutcomeID);
            return RedirectToAction("../Incidents/Details/" + IncidentID);
        }


        public static string Get_HTML(string Url)
        {
            

            System.Net.WebResponse Result = null;
            string Page_Source_Code;

            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            Result = req.GetResponse();
            System.IO.Stream RStream = Result.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(RStream);
            new System.IO.StreamReader(RStream);
            Page_Source_Code = sr.ReadToEnd();
            sr.Dispose();
            try
            { }
            catch
            {
                // error while reading the url: the url dosen’t exist, connection problem...
                Page_Source_Code = "";
            }
            finally
            {
                if (Result != null) Result.Close();
            }
            return Page_Source_Code;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
