using IRRM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IRRM.Controllers
{


    public class HomeController : Controller
    {
        private IRRMEntities db = new IRRMEntities();


        public ActionResult DynamicReports()
        {
            if (Convert.ToInt32(Session["RoleID"]) != 1)
            {
                return RedirectToAction("../Login");
            }
            ViewBag.Reportypes = db.DynamicReports_M.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult DynamicReports(FormCollection col)
        {
            int monthfrom = Convert.ToInt32(col["monthfrom"]);
            int yearfrom = Convert.ToInt32(col["yearfrom"]);
            int monthto = Convert.ToInt32(col["monthto"]);
            int yearto = Convert.ToInt32(col["yearto"]);

            int reporttype = Convert.ToInt32(col["reporttype"]);
            string fromdate = yearfrom + "-" + monthfrom + "-01";
            string todate = yearto + "-" + monthto + "-01";

            ViewBag.Reportypes = db.DynamicReports_M.ToList();

            db.Database.Connection.Open();

            var cmd = db.Database.Connection.CreateCommand();
            cmd.CommandText = "spEventIncidentCountreport @fromdate,@todate,@reporttype";
            cmd.Parameters.Add(new SqlParameter("fromdate", fromdate));
            cmd.Parameters.Add(new SqlParameter("todate", todate));
            cmd.Parameters.Add(new SqlParameter("reporttype", reporttype));

            List<List<object>> items = new List<List<object>>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new List<Object>();
                items.Add(item);

                for (int i = 0; i < reader.FieldCount; i++)
                    item.Add(reader[i]);
            }



            ViewBag.columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();


            ViewBag.dynamicreport = items;
            return View();
        }



        public ActionResult Index(string id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            string year = "";

            if (id == "" || id == null)
            {
                year = DateTime.Now.Year.ToString();
            }
            else {
                year = id;
            
            }

            int totalnumber = 0;

            ViewBag.graphtype = "doughnut";
            
            List<GraphTypes> _GraphTypes = db.Database.SqlQuery<GraphTypes>("select et.EventType as TypeText,count(inc.IncidentID) as count from Incidents inc join EventType_SM et on et.EventTypeID = inc.EventTypeID where inc.Registered=1  and YEAR(inc.RegisteredOn) = " + year + "  group by et.EventType").ToList();
            string typetext = ",,";
            string counts = ",,";
            totalnumber = 0;
            foreach (var ge in _GraphTypes)
            {
                typetext += ",'" + ge.TypeText + "'";
                counts += "," + ge.Count;
                totalnumber += ge.Count;
            }

            ViewBag.EventType = typetext.Replace(",,,", "");
            ViewBag.EventCount = counts.Replace(",,,", "");
            ViewBag.TotalEvent = totalnumber;


            List<GraphTypes> _GraphTypes2 = db.Database.SqlQuery<GraphTypes>(@"select TypeText,count(incidentid) as count from (
            select it.IncidentType as TypeText, inc.IncidentID from Incidents inc
            join IncidentTypes_M it on it.IncidentTypeID = inc.IncidentTypeID
            where   YEAR(inc.RegisteredOn) = " + year + " and  inc.Registered = 1)intpz  group by TypeText").ToList();
            string typetext2 = ",,";
            string counts2 = ",,";
            totalnumber = 0;
            foreach (var ge in _GraphTypes2)
            {
                typetext2 += ",'" + ge.TypeText + "'";
                counts2 += "," + ge.Count;
                totalnumber += ge.Count;
            }
            ViewBag.TotalIncidents = totalnumber;
            ViewBag.EventType2 = typetext2.Replace(",,,", "");
            ViewBag.EventCount2 = counts2.Replace(",,,", "");



            List<GraphTypes> _GraphTypes2A = db.Database.SqlQuery<GraphTypes>(@"select TypeText,count(incidentid) as count from (
            select ist.IncidentSubType as TypeText, inc.IncidentID from Incidents inc
            join IncidentSubTypes_M ist on ist.IncidentSubTypeID = inc.IncidentSubTypeID
            where inc.Registered = 1  and YEAR(inc.RegisteredOn) = " + year + "  )intpz    group by TypeText").ToList();
            string typetext2A = ",,";
            string counts2A = ",,";
            totalnumber = 0;
            foreach (var geA in _GraphTypes2A)
            {
                typetext2A += ",'" + geA.TypeText + "'";
                counts2A += "," + geA.Count;
                totalnumber += geA.Count;
            }

            ViewBag.EventType2A = typetext2A.Replace(",,,", "");
            ViewBag.EventCount2A = counts2A.Replace(",,,", "");
            ViewBag.TotalIncidentSubType = totalnumber;


            List<GraphTypes> _GraphTypes3 = db.Database.SqlQuery<GraphTypes>(@"select iss.IncidentStatus as TypeText,count(inc.IncidentID) as count from Incidents inc 
            join IncidentStatus_M iss on iss.IncidentStatusID = inc.IncidentStatusID 
            group by iss.IncidentStatus,iss.IncidentStatusID
            order by iss.IncidentStatusID").ToList();
            string typetext3 = ",,";
            string counts3 = ",,";
            totalnumber = 0;
            foreach (var ge in _GraphTypes3)
            {
                typetext3 += ",'" + ge.TypeText + "'";
                counts3 += "," + ge.Count;
                totalnumber += ge.Count;
            }

            ViewBag.EventType3 = typetext3.Replace(",,,", "");
            ViewBag.EventCount3 = counts3.Replace(",,,", "");
            ViewBag.TotalStatus = totalnumber;



            List<GraphTypes> _GraphTypes4 = db.Database.SqlQuery<GraphTypes>(@"select convert(varchar,hs.IncidentHarmScore) as TypeText,count(inc.IncidentID) as count from Incidents inc 
            join IncidentHarmScore_M hs on hs.IncidentHarmScoreID = inc.HarmScoreID  where inc.Registered = 1  and YEAR(inc.RegisteredOn) = " + year + "  group by convert(varchar,hs.IncidentHarmScore)").ToList();
            string typetext4 = ",,";
            string counts4 = ",,";
            totalnumber = 0;
            foreach (var ge in _GraphTypes4)
            {
                typetext4 += ",'" + ge.TypeText + "'";
                counts4 += "," + ge.Count;
                totalnumber += ge.Count;
            }

            ViewBag.EventType4 = typetext4.Replace(",,,", "");
            ViewBag.EventCount4 = counts4.Replace(",,,", "");
            ViewBag.TotalHarmScore = totalnumber;


            List<GraphTypes> _GraphTypes5 = db.Database.SqlQuery<GraphTypes>(@"select convert(varchar,hs.Priority) as TypeText,count(inc.IncidentID) as count from Incidents inc 
            join IncidentPriorities_M hs on hs.IncidentPriorityID = inc.PriorityID  where inc.Registered = 1   and YEAR(inc.RegisteredOn) = " + year + "   group by convert(varchar,hs.Priority)").ToList();
            string typetext5 = ",,";
            string counts5 = ",,";
            totalnumber = 0;
            foreach (var ge in _GraphTypes5)
            {
                typetext5 += ",'" + ge.TypeText + "'";
                counts5 += "," + ge.Count;
                totalnumber += ge.Count;
            }

            ViewBag.EventType5 = typetext5.Replace(",,,", "");
            ViewBag.EventCount5 = counts5.Replace(",,,", "");
            ViewBag.TotalPriorities = totalnumber;


            List<GraphTypes> _GraphTypes6 = db.Database.SqlQuery<GraphTypes>(@"select TypeText,count(incidentid) as count from (
            select it.Branch as TypeText, inc.IncidentID from Incidents inc
            join Branch_M it on it.BranchID = inc.BranchID
            where inc.Registered = 1  and YEAR(inc.RegisteredOn) = " + year + "  )intpz  group by TypeText").ToList();
            string typetext6 = ",,";
            string counts6 = ",,";
            totalnumber = 0;
            foreach (var ge in _GraphTypes6)
            {
                typetext6 += ",'" + ge.TypeText + "'";
                counts6 += "," + ge.Count;
                totalnumber += ge.Count;
            }

            ViewBag.EventType6 = typetext6.Replace(",,,", "");
            ViewBag.EventCount6 = counts6.Replace(",,,", "");
            ViewBag.TotalBranch = totalnumber;


            List<GraphTypes> _GraphTypes6A = db.Database.SqlQuery<GraphTypes>(@"select TypeText,count(incidentid) as count from (
            select ist.Location as TypeText, inc.IncidentID from Incidents inc
            join Location_M ist on ist.LocationID = inc.LocationID
            where inc.Registered = 1  and YEAR(inc.RegisteredOn) = " + year + "  )intpz  group by TypeText").ToList();
            string typetext6A = ",,";
            string counts6A = ",,";
            totalnumber = 0;
            foreach (var geA in _GraphTypes6A)
            {
                typetext6A += ",'" + geA.TypeText + "'";
                counts6A += "," + geA.Count;
                totalnumber += geA.Count;
            }

            ViewBag.EventType6A = typetext6A.Replace(",,,", "");
            ViewBag.EventCount6A = counts6A.Replace(",,,", "");
            ViewBag.TotalLocations = totalnumber;


            List<GraphTypes> _GraphTypesR1 = db.Database.SqlQuery<GraphTypes>(@"select convert(varchar(15),rm.RiskStatus) as TypeText,count(rr.RiskRegisterID) as count from [dbo].[RiskRegister] rr
join [dbo].[RiskStatus_M] rm on rr.RiskKeyStatusID = rm.RiskStatusID
where rm.Active=1
group by convert(varchar(15),rm.RiskStatus)
").ToList();
            string typetextR1 = ",,";
            string countsR1 = ",,";
            totalnumber = 0;
            foreach (var geA in _GraphTypesR1)
            {
                typetextR1 += ",'" + geA.TypeText + "'";
                countsR1 += "," + geA.Count;
                totalnumber += geA.Count;
            }

            ViewBag.EventTypeR1 = typetextR1.Replace(",,,", "");
            ViewBag.EventCountR1 = countsR1.Replace(",,,", "");
            ViewBag.TotalRiskStatus = totalnumber;


            List<GraphTypes> _GraphTypesR2 = db.Database.SqlQuery<GraphTypes>(@"select convert(varchar(15),RARiskLevel) as TypeText,
count(rr.RiskRegisterID) as count from [dbo].[RiskRegister] rr
group by convert(varchar(15),RARiskLevel)").ToList();
            string typetextR2 = ",,";
            string countsR2 = ",,";
            totalnumber = 0;
            foreach (var geA in _GraphTypesR2)
            {
                typetextR2 += ",'" + geA.TypeText + "'";
                countsR2 += "," + geA.Count;
                totalnumber += geA.Count;
            }

            ViewBag.EventTypeR2 = typetextR2.Replace(",,,", "");
            ViewBag.EventCountR2 = countsR2.Replace(",,,", "");
            ViewBag.TotalRiskRegister = totalnumber;



            List<GraphTypes> _GraphTypesFB = db.Database.SqlQuery<GraphTypes>(@"select TypeText,count(incidentid) as count from (
            select ist.IncidentFeedbackType as TypeText, inc.IncidentID from Incidents inc
            join IncidentFeedbackType_M ist on ist.IncidentFeedbackTypeID = inc.FeedbackTypeID
            where inc.Registered = 1 and YEAR(inc.RegisteredOn) = " + year + "   and ist.Active=1  )intpz  group by TypeText").ToList();
            string typetextfb = ",,";
            string countsfb = ",,";
            totalnumber = 0;
            foreach (var geA in _GraphTypesFB)
            {
                typetextfb += ",'" + geA.TypeText + "'";
                countsfb += "," + geA.Count;
                totalnumber += geA.Count;
            }

            ViewBag.EventTypefb = typetextfb.Replace(",,,", "");
            ViewBag.EventCountfb = countsfb.Replace(",,,", "");
            ViewBag.Totalfb = totalnumber;





            List<GraphTypes> _GraphTypesTC = db.Database.SqlQuery<GraphTypes>(@"select TypeText,count(taskid) as count from (
            select convert(varchar,typ.TaskCategory) as TypeText, tsk.TaskID from Tasks tsk
            join [TaskCategory_M] typ on typ.TaskCategoryID = tsk.CategoryID
            where tsk.Registered = 1 and YEAR(tsk.RegisteredOn) = " + year + "   and typ.Active=1  )intpz   group by TypeText").ToList();
            string typetexttc = ",,";
            string countstc = ",,";
            totalnumber = 0;
            foreach (var getc in _GraphTypesTC)
            {
                typetexttc += ",'" + getc.TypeText + "'";
                countstc += "," + getc.Count;
                totalnumber += getc.Count;
            }

            ViewBag.EventTypetc = typetexttc.Replace(",,,", "");
            ViewBag.EventCounttc = countstc.Replace(",,,", "");
            ViewBag.Totaltc = totalnumber;





            List<GraphTypes> _GraphTypesTP = db.Database.SqlQuery<GraphTypes>(@"select TypeText,count(taskid) as count from (
            select convert(varchar,typ.Priority) as TypeText, tsk.TaskID from Tasks tsk
            join TaskPriorities_M typ on typ.TaskPriorityID = tsk.PriorityID
            where tsk.Registered = 1 and YEAR(tsk.RegisteredOn) = " + year + "   and typ.Active=1  )intpz   group by TypeText").ToList();
            string typetexttp = ",,";
            string countstp = ",,";
            totalnumber = 0;
            foreach (var getp in _GraphTypesTP)
            {
                typetexttp += ",'" + getp.TypeText + "'";
                countstp += "," + getp.Count;
                totalnumber += getp.Count;
            }

            ViewBag.EventTypetp = typetexttp.Replace(",,,", "");
            ViewBag.EventCounttp = countstp.Replace(",,,", "");
            ViewBag.Totaltp = totalnumber;






            ViewBag.thisyear = DateTime.Now.Year;






            return View();
        }



        public ActionResult Report(FormCollection col)
        {

            int totalnumber = 0;


            ViewBag.graphtype = Convert.ToString(col["graphtype"]);
            ViewBag.DateFrom = Convert.ToString(col["IncidentDateFrom"]);
            ViewBag.DateTo = Convert.ToString(col["IncidentDateTo"]);


            int reporttype = Convert.ToInt32(col["reporttype"]);
            ViewBag.reporttype = reporttype;
            string query = "", searchconditions = "", searchconditions2 = "";


            searchconditions = " and IncidentDate>='" + Convert.ToDateTime(col["IncidentDateFrom"]).ToString("yyyy-MM-dd") + "  00:00:00' and IncidentDate<='" + Convert.ToDateTime(col["IncidentDateTo"]).ToString("yyyy-MM-dd") + "  23:59:59' ";
            searchconditions2 = " and createdon>='" + Convert.ToDateTime(col["IncidentDateFrom"]).ToString("yyyy-MM-dd") + "  00:00:00' and createdon<='" + Convert.ToDateTime(col["IncidentDateTo"]).ToString("yyyy-MM-dd") + "  23:59:59' ";



            if (reporttype == 1)
            {
                ViewBag.ReportName = "Event Type";
                ViewBag.controlname = "EventTypeID";

                query = @"select et.EventTypeID as TypeID, et.EventType as TypeText,count(inc.IncidentID) as count from Incidents inc 
join EventType_SM et on et.EventTypeID = inc.EventTypeID where inc.Registered = 1  " + searchconditions + "   group by et.EventType, et.EventTypeID";
            }
            else if (reporttype == 2)
            {
                ViewBag.ReportName = "Incident Type";
                ViewBag.controlname = "IncidentTypeID";

                query = @"select TypeID, TypeText,count(incidentid) as count from (
            select it.IncidentTypeID as TypeID, it.IncidentType as TypeText, inc.IncidentID from Incidents inc
            join IncidentTypes_M it on it.IncidentTypeID = inc.IncidentTypeID
            where inc.Registered = 1 and active=1    " + searchconditions + " )intpz group by TypeText,TypeID";
            }
            else if (reporttype == 3)
            {
                ViewBag.ReportName = "Incident Subtype";
                ViewBag.controlname = "IncidentSubTypeID";

                query = @"select TypeID,TypeText,count(incidentid) as count from (
            select ist.IncidentSubTypeID as TypeID, ist.IncidentSubType as TypeText, inc.IncidentID from Incidents inc
            join IncidentSubTypes_M ist on ist.IncidentSubTypeID = inc.IncidentSubTypeID
            where inc.Registered = 1 and active=1   " + searchconditions + "  )intpz  group by TypeText,TypeID";
            }
            else if (reporttype == 4)
            {
                ViewBag.ReportName = "Incident Status";
                ViewBag.controlname = "StatusID";
                query = @"select iss.IncidentStatusID as TypeID, iss.IncidentStatus as TypeText,count(inc.IncidentID) as count from Incidents inc 
            join IncidentStatus_M iss on iss.IncidentStatusID = inc.IncidentStatusID where active=1  " + searchconditions + "   group by iss.IncidentStatus,iss.IncidentStatusID  order by iss.IncidentStatusID";
            }
            else if (reporttype == 5)
            {
                ViewBag.ReportName = "Incident Harm Score";
                ViewBag.controlname = "HarmScoreID";

                query = @"select hs.IncidentHarmScoreID as TypeID, convert(varchar,hs.IncidentHarmScore) as TypeText,count(inc.IncidentID) as count from Incidents inc 
            join IncidentHarmScore_M hs on hs.IncidentHarmScoreID = inc.HarmScoreID  where inc.Registered = 1 and hs.Active=1  " + searchconditions + "   group by convert(varchar,hs.IncidentHarmScore),hs.IncidentHarmScoreID";
            }
            else if (reporttype == 6)
            {
                ViewBag.ReportName = "Incident Priority";
                ViewBag.controlname = "PriorityID";


                query = @"select hs.IncidentPriorityID as TypeID, convert(varchar,hs.Priority) as TypeText,count(inc.IncidentID) as count from Incidents inc 
            join IncidentPriorities_M hs on hs.IncidentPriorityID = inc.PriorityID  where inc.Registered = 1 and active=1  " + searchconditions + "   group by convert(varchar,hs.Priority),hs.IncidentPriorityID";
            }

            else if (reporttype == 7)
            {
                ViewBag.ReportName = "Risk Status";
                ViewBag.controlname = "RiskStatusID";

                query = @"select rr.RiskStatusID as TypeID, convert(varchar(15),rm.RiskStatus) as TypeText,count(rr.RiskRegisterID) as count from [dbo].[RiskRegister] rr
join [dbo].[RiskStatus_M] rm on rr.RiskStatusID = rm.RiskStatusID
where rm.Active=1 " + searchconditions2 + "   group by convert(varchar(15),rm.RiskStatus),rr.RiskStatusID";
            }
            else if (reporttype == 8)
            {
                ViewBag.ReportName = "Risk Level";
                ViewBag.controlname = "RARiskLevel";

                query = @"select  RARiskLevel as TypeText,count(rr.RiskRegisterID) as count from [dbo].[RiskRegister] rr group by RARiskLevel";
            }
            else if (reporttype == 9)
            {
                ViewBag.ReportName = "Location";
                ViewBag.controlname = "LocationID";

                query = @"select   TypeID, TypeText,count(incidentid) as count from (
            select ist.LocationID as TypeID, ist.Location as TypeText, inc.IncidentID from Incidents inc
            join Location_M ist on ist.LocationID = inc.LocationID
            where inc.Registered = 1 and ist.Active=1  " + searchconditions + "    )intpz  group by TypeText,TypeID";
            }
            else if (reporttype == 10)
            {
                ViewBag.ReportName = "Feedback";
                ViewBag.controlname = "FeedbackTypeID";
                query = @"select TypeID,TypeText, count(incidentid) as count from(
             select ist.IncidentFeedbackTypeID as TypeID, ist.IncidentFeedbackType as TypeText, inc.IncidentID from Incidents inc
             join IncidentFeedbackType_M ist on ist.IncidentFeedbackTypeID = inc.FeedbackTypeID
             where inc.Registered = 1 and ist.Active = 1 " + searchconditions + "   )intpz  group by TypeText,TypeID";
            }
            else if (reporttype == 11)
            {
                ViewBag.ReportName = "Branch";
                query = @"select TypeID,TypeText,count(incidentid) as count from (
            select it.BranchID as TypeID, it.Branch as TypeText, inc.IncidentID from Incidents inc
            join Branch_M it on it.BranchID = inc.BranchID
            where inc.Registered = 1 and active=1  " + searchconditions + "  )intpz     group by TypeText,TypeID";
            }
            else if (reporttype == 12)
            {
                ViewBag.ReportName = "Risk Category";
                ViewBag.controlname = "RiskCategoryID";
                query = @"select rr.RiskCategoryID as TypeID, convert(varchar(15), rm.RiskCategory) as TypeText,count(rr.RiskRegisterID) as count from[dbo].[RiskRegister] rr
join[dbo].RiskCategory_M rm on rr.RiskCategoryID = rm.RiskCategoryID
where rm.Active = 1     group by convert(varchar(15), rm.RiskCategory),rr.RiskCategoryID";
            }
            else if (reporttype == 13)
            {
                ViewBag.ReportName = "Risk Rating";
                ViewBag.controlname = "RiskRatingID";
                query = @"select 'Risk Rating- '+convert(varchar,RRRating) as TypeText, count(RiskRegisterID) as Count from RiskRegister where RRRating is not null group by RRRating order by RRRating";
            }
            else if (reporttype == 14)
            {
                ViewBag.ReportName = "Risk Strategy";
                ViewBag.controlname = "RiskStrategyID";
                query = @"select rr.RiskStrategyID as TypeID, convert(varchar(15), rm.RiskStrategy) as TypeText,count(rr.RiskRegisterID) as count from[dbo].[RiskRegister] rr
join[dbo].RiskStrategy_M rm on rr.RiskStrategyID = rm.RiskStrategyID
where rm.Active = 1     group by convert(varchar(15), rm.RiskStrategy),rr.RiskStrategyID";
            }
            else if (reporttype == 15)
            {
                ViewBag.ReportName = "Risk Owner";
                ViewBag.controlname = "RiskOwnerID";
                query = @"select rr.riskownerid as TypeID, convert(varchar(15),rm.Name) as TypeText,count(rr.RiskRegisterID) as count from [dbo].[RiskRegister] rr
join [dbo].Users_M rm on rr.riskownerid = rm.userid
where rm.Active=1     group by convert(varchar(15),rm.Name),rr.RiskOwnerID";
            }
            else if (reporttype == 16)
            {
                ViewBag.ReportName = "Adequacy";
                ViewBag.controlname = "Adequacy";
                query = @"select Adequacy as typetext, count(RiskRegisterID) as count from RiskRegister where Adequacy is not null group by Adequacy";
            }







            List<GraphTypes> _GraphTypes = db.Database.SqlQuery<GraphTypes>(query).ToList();



            string typetext = ",,";
            string counts = ",,";
            totalnumber = 0;
            foreach (var ge in _GraphTypes)
            {
                typetext += ",'" + ge.TypeText + "'";
                counts += "," + ge.Count;
                totalnumber += ge.Count;
            }

            ViewBag.EventType = typetext.Replace(",,,", "");
            ViewBag.EventCount = counts.Replace(",,,", "");
            ViewBag.TotalEvent = totalnumber;

            ViewBag.Tabledata = _GraphTypes;

            return View();
        }


        //public ActionResult ReportTrending(FormCollection col)
        //{

        //    int totalnumber = 0;


        //    ViewBag.graphtype = Convert.ToString(col["graphtype"]);
        //    string DateFrom = Convert.ToString(col["yearfrom"]) + "-" + Convert.ToString(col["monthfrom"]) + "-" + "01";
        //    ViewBag.DateFrom = DateFrom;

        //    string monto = Convert.ToString(col["monthto"]);
        //    string dayto = "";
        //    if (monto == "02")
        //    {
        //        dayto = "28";
        //    }
        //    else if (monto == "04" || monto == "06" || monto == "09" || monto == "11")
        //    {
        //        dayto = "30";
        //    }
        //    else
        //    {
        //        dayto = "31";
        //    }


        //    string DateTo = Convert.ToString(col["yearto"]) + "-" + Convert.ToString(col["monthto"]) + "-" + dayto;
        //    ViewBag.DateTo = DateTo;

        //    int reporttype = Convert.ToInt32(col["reporttype"]);

        //    int detailid = Convert.ToInt32(col["DetailID"]);




        //    ViewBag.reporttype = reporttype;
        //    string query = "", searchconditions = "", searchconditions2 = "", risksearchconditions = "";


        //    searchconditions = " and RegisteredOn>='" + Convert.ToDateTime(DateFrom).ToString("yyyy-MM-dd") + "  00:00:00' and RegisteredOn<='" + Convert.ToDateTime(DateTo).ToString("yyyy-MM-dd") + "  23:59:59' ";


        //    searchconditions2 = " and createdon>='" + Convert.ToDateTime(col["IncidentDateFrom"]).ToString("yyyy-MM-dd") + "  00:00:00' and createdon<='" + Convert.ToDateTime(col["IncidentDateTo"]).ToString("yyyy-MM-dd") + "  23:59:59' ";

        //    risksearchconditions = " and RiskRegisteredDate>='" + Convert.ToDateTime(DateFrom).ToString("yyyy-MM-dd") + "  00:00:00' and RiskRegisteredDate<='" + Convert.ToDateTime(DateTo).ToString("yyyy-MM-dd") + "  23:59:59' ";


        //    if (reporttype == 1)
        //    {

        //        ViewBag.controlname = "EventTypeID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  EventTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.EventType_SM.Where(x => x.EventTypeID == detailid).Select(x => x.EventType).SingleOrDefault();
        //            ViewBag.ReportName = "Event Type - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Event Type - All";

        //        }
        //    }
        //    else if (reporttype == 2)
        //    {

        //        ViewBag.controlname = "IncidentTypeID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  IncidentTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.IncidentTypes_M.Where(x => x.IncidentTypeID == detailid).Select(x => x.IncidentType).SingleOrDefault();
        //            ViewBag.ReportName = "Incident Type - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Incident Type - All";
        //        }
        //    }
        //    else if (reporttype == 3)
        //    {
        //        ViewBag.controlname = "IncidentSubTypeID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  IncidentSubTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.IncidentSubTypes_M.Where(x => x.IncidentSubTypeID == detailid).Select(x => x.IncidentSubType).SingleOrDefault();
        //            ViewBag.ReportName = "Incident Subtype - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Incident Subtype - All";

        //        }
        //    }
        //    else if (reporttype == 4)
        //    {

        //        ViewBag.controlname = "StatusID";

        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  IncidentStatusID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.IncidentStatus_M.Where(x => x.IncidentStatusID == detailid).Select(x => x.IncidentStatus).SingleOrDefault();
        //            ViewBag.ReportName = "Incident Status - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Incident Status - All";
        //        }
        //    }
        //    else if (reporttype == 5)
        //    {
        //        ViewBag.controlname = "HarmScoreID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  HarmScoreID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.IncidentHarmScore_M.Where(x => x.IncidentHarmScoreID == detailid).Select(x => x.IncidentHarmScore).SingleOrDefault();
        //            ViewBag.ReportName = "Incident Harm Score - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Incident Harm Score - All";

        //        }
        //    }
        //    else if (reporttype == 6)
        //    {
        //        ViewBag.controlname = "PriorityID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  PriorityID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.IncidentPriorities_M.Where(x => x.IncidentPriorityID == detailid).Select(x => x.Priority).SingleOrDefault();
        //            ViewBag.ReportName = "Incident Priority - " + thesubtype;

        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Incident Priority - All";

        //        }
        //    }
        //    else if (reporttype == 7)
        //    {
        //        if (detailid != 0)
        //        {

        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskStatusID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.RiskStatus_M.Where(x => x.RiskStatusID == detailid).Select(x => x.RiskStatus).SingleOrDefault();
        //            ViewBag.ReportName = "Risk Status - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where 1=1 " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Risk Status - All";

        //        }
        //    }
        //    //else if (reporttype == 8)
        //    //{
        //    //    ViewBag.controlname = "RiskLevelID";
        //    //    if (detailid != 0)
        //    //    {

        //    //        string rrlevel = "";

        //    //        switch (detailid)
        //    //        {


        //    //            case 2:
        //    //                {
        //    //                    rrlevel = "High Risk";
        //    //                    break;
        //    //                }
        //    //            case 3:
        //    //                {
        //    //                    rrlevel = "Moderate Risk";
        //    //                    break;
        //    //                }
        //    //            case 4:
        //    //                {
        //    //                    rrlevel = "Low Risk";
        //    //                    break;
        //    //                }
        //    //            case 5:
        //    //                {
        //    //                    rrlevel = "Extreme Risk";
        //    //                    break;
        //    //                }
        //    //        }

        //    //        query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RRRisklevel='" + rrlevel + "' " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //    //        string thesubtype = db.RiskLevel_M.Where(x => x.RiskLevelID == detailid).Select(x => x.RiskLevel).SingleOrDefault();
        //    //        ViewBag.ReportName = "Risk Level - " + thesubtype;

        //    //    }
        //    //    else
        //    //    {
        //    //        query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null" + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //    //        ViewBag.ReportName = "Risk Level - All";

        //    //    }
        //    //}
        //    else if (reporttype == 9)
        //    {
        //        ViewBag.controlname = "LocationID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  LocationID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.Location_M.Where(x => x.LocationID == detailid).Select(x => x.Location).SingleOrDefault();
        //            ViewBag.ReportName = "Location - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Location - All";

        //        }

        //    }
        //    else if (reporttype == 10)
        //    {
        //        ViewBag.controlname = "FeedbackTypeID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  FeedbackTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.IncidentFeedbackType_M.Where(x => x.IncidentFeedbackTypeID == detailid).Select(x => x.IncidentFeedbackType).SingleOrDefault();
        //            ViewBag.ReportName = "Feedback - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Feedback - All";

        //        }
        //    }
        //    else if (reporttype == 11)
        //    {

        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  BranchID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

        //            string thesubtype = db.Branch_M.Where(x => x.BranchID == detailid).Select(x => x.Branch).SingleOrDefault();
        //            ViewBag.ReportName = "Branch - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(incidentdate) AS VARCHAR(4)) as IncYear,CAST(MONTH(incidentdate) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(incidentdate) AS VARCHAR(4)),CAST(MONTH(incidentdate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            ViewBag.ReportName = "Branch - All";
        //        }
        //    }
        //    else if (reporttype == 12)
        //    {

        //        ViewBag.controlname = "RiskCategoryID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and RiskCategoryID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.RiskCategory_M.Where(x => x.RiskCategoryID == detailid).Select(x => x.RiskCategory).SingleOrDefault();
        //            ViewBag.ReportName = "Risk Category - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

        //            ViewBag.ReportName = "Risk Category - All";
        //        }
        //    }
        //    else if (reporttype == 13)
        //    {
        //        ViewBag.controlname = "RiskRatingID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and  RARating=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.RiskRating_M.Where(x => x.RiskRatingID == detailid).Select(x => x.RiskRating).SingleOrDefault();
        //            ViewBag.ReportName = "Risk Rating - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where  RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

        //            ViewBag.ReportName = "Risk Rating - All";
        //        }
        //    }
        //    else if (reporttype == 14)
        //    {

        //        ViewBag.controlname = "RiskStrategyID";

        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskStrategyID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.RiskStrategy_M.Where(x => x.RiskStrategyID == detailid).Select(x => x.RiskStrategy).SingleOrDefault();
        //            ViewBag.ReportName = "Risk Strategy - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

        //            ViewBag.ReportName = "Risk Strategy - All";
        //        }
        //    }
        //    else if (reporttype == 15)
        //    {

        //        ViewBag.controlname = "RiskOwnerID";
        //        if (detailid != 0)
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and  RiskOwnerID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
        //            string thesubtype = db.Users_M.Where(x => x.UserID == detailid).Select(x => x.UserName).SingleOrDefault();
        //            ViewBag.ReportName = "Risk Owner - " + thesubtype;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

        //            ViewBag.ReportName = "Risk Owner - All";
        //        }
        //    }
        //    else if (reporttype == 16)
        //    {

        //        ViewBag.controlname = "Adequacy";
        //        if (detailid != 0)
        //        {

        //            string adq = "";

        //            switch (detailid)
        //            {

        //                case 1:
        //                    {
        //                        adq = "Adequate";
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        adq = "Inadequate";
        //                        break;
        //                    }

        //            }

        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and  Adequacy='" + adq + "' " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

        //            ViewBag.ReportName = "Risk Adequacy - " + adq;
        //        }
        //        else
        //        {
        //            query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

        //            ViewBag.ReportName = "Risk Adequacy - All";
        //        }
        //    }







        //    List<GraphTypes> _GraphTypes = db.Database.SqlQuery<GraphTypes>(query).ToList();



        //    string typetext = ",,";
        //    string counts = ",,";
        //    totalnumber = 0;
        //    string monthnox = "";
        //    foreach (var ge in _GraphTypes)
        //    {
        //        typetext += ",'" + ge.TypeText + "'";
        //        counts += "," + ge.Count;
        //        totalnumber += ge.Count;

        //        if (ge.monthx < 10)
        //        {
        //            monthnox = "0" + ge.monthx;
        //        }
        //        else
        //        {
        //            monthnox = Convert.ToString(ge.monthx);
        //        }
        //        ge.StartDate = ge.yearx + "-" + monthnox + "-" + "01 00:00:00";
        //        ge.EndDate = getlastday(ge.yearx, ge.monthx);

        //    }

        //    ViewBag.EventType = typetext.Replace(",,,", "");
        //    ViewBag.EventCount = counts.Replace(",,,", "");
        //    ViewBag.TotalEvent = totalnumber;

        //    ViewBag.Tabledata = _GraphTypes;

        //    return View();
        //}

        public ActionResult ReportTrending(FormCollection col)
        {

            int totalnumber = 0;


            ViewBag.graphtype = Convert.ToString(col["graphtype"]);
            string DateFrom = Convert.ToString(col["yearfrom"]) + "-" + Convert.ToString(col["monthfrom"]) + "-" + "01";
            ViewBag.DateFrom = DateFrom;

            string monto = Convert.ToString(col["monthto"]);
            string dayto = "";
            if (monto == "02")
            {
                dayto = "28";
            }
            else if (monto == "04" || monto == "06" || monto == "09" || monto == "11")
            {
                dayto = "30";
            }
            else
            {
                dayto = "31";
            }


            string DateTo = Convert.ToString(col["yearto"]) + "-" + Convert.ToString(col["monthto"]) + "-" + dayto;
            ViewBag.DateTo = DateTo;

            int reporttype = Convert.ToInt32(col["reporttype"]);

            int detailid = Convert.ToInt32(col["DetailID"]);




            ViewBag.reporttype = reporttype;
            string query = "", searchconditions = "", searchconditions2 = "", risksearchconditions = "";


            searchconditions = " and RegisteredOn>='" + Convert.ToDateTime(DateFrom).ToString("yyyy-MM-dd") + "  00:00:00' and RegisteredOn<='" + Convert.ToDateTime(DateTo).ToString("yyyy-MM-dd") + "  23:59:59' ";


            searchconditions2 = " and createdon>='" + Convert.ToDateTime(col["RegisteredOnFrom"]).ToString("yyyy-MM-dd") + "  00:00:00' and createdon<='" + Convert.ToDateTime(col["RegisteredOnTo"]).ToString("yyyy-MM-dd") + "  23:59:59' ";

            risksearchconditions = " and RiskRegisteredDate>='" + Convert.ToDateTime(DateFrom).ToString("yyyy-MM-dd") + "  00:00:00' and RiskRegisteredDate<='" + Convert.ToDateTime(DateTo).ToString("yyyy-MM-dd") + "  23:59:59' ";


            if (reporttype == 1)
            {

                ViewBag.controlname = "EventTypeID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  EventTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.EventType_SM.Where(x => x.EventTypeID == detailid).Select(x => x.EventType).SingleOrDefault();
                    ViewBag.ReportName = "Event Type - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Event Type - All";

                }
            }
            else if (reporttype == 2)
            {

                ViewBag.controlname = "IncidentTypeID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  IncidentTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.IncidentTypes_M.Where(x => x.IncidentTypeID == detailid).Select(x => x.IncidentType).SingleOrDefault();
                    ViewBag.ReportName = "Incident Type - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Incident Type - All";
                }
            }
            else if (reporttype == 3)
            {
                ViewBag.controlname = "IncidentSubTypeID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  IncidentSubTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.IncidentSubTypes_M.Where(x => x.IncidentSubTypeID == detailid).Select(x => x.IncidentSubType).SingleOrDefault();
                    ViewBag.ReportName = "Incident Subtype - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Incident Subtype - All";

                }
            }
            else if (reporttype == 4)
            {

                ViewBag.controlname = "StatusID";

                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  IncidentStatusID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.IncidentStatus_M.Where(x => x.IncidentStatusID == detailid).Select(x => x.IncidentStatus).SingleOrDefault();
                    ViewBag.ReportName = "Incident Status - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Incident Status - All";
                }
            }
            else if (reporttype == 5)
            {
                ViewBag.controlname = "HarmScoreID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  HarmScoreID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.IncidentHarmScore_M.Where(x => x.IncidentHarmScoreID == detailid).Select(x => x.IncidentHarmScore).SingleOrDefault();
                    ViewBag.ReportName = "Incident Harm Score - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Incident Harm Score - All";

                }
            }
            else if (reporttype == 6)
            {
                ViewBag.controlname = "PriorityID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  PriorityID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.IncidentPriorities_M.Where(x => x.IncidentPriorityID == detailid).Select(x => x.Priority).SingleOrDefault();
                    ViewBag.ReportName = "Incident Priority - " + thesubtype;

                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Incident Priority - All";

                }
            }
            else if (reporttype == 7)
            {
                if (detailid != 0)
                {

                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskStatusID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.RiskStatus_M.Where(x => x.RiskStatusID == detailid).Select(x => x.RiskStatus).SingleOrDefault();
                    ViewBag.ReportName = "Risk Status - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where 1=1 " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Risk Status - All";

                }
            }
            //else if (reporttype == 8)
            //{
            //    ViewBag.controlname = "RiskLevelID";
            //    if (detailid != 0)
            //    {

            //        string rrlevel = "";

            //        switch (detailid)
            //        {


            //            case 2:
            //                {
            //                    rrlevel = "High Risk";
            //                    break;
            //                }
            //            case 3:
            //                {
            //                    rrlevel = "Moderate Risk";
            //                    break;
            //                }
            //            case 4:
            //                {
            //                    rrlevel = "Low Risk";
            //                    break;
            //                }
            //            case 5:
            //                {
            //                    rrlevel = "Extreme Risk";
            //                    break;
            //                }
            //        }

            //        query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RRRisklevel='" + rrlevel + "' " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
            //        string thesubtype = db.RiskLevel_M.Where(x => x.RiskLevelID == detailid).Select(x => x.RiskLevel).SingleOrDefault();
            //        ViewBag.ReportName = "Risk Level - " + thesubtype;

            //    }
            //    else
            //    {
            //        query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null" + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
            //        ViewBag.ReportName = "Risk Level - All";

            //    }
            //}
            else if (reporttype == 9)
            {
                ViewBag.controlname = "LocationID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  LocationID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.Location_M.Where(x => x.LocationID == detailid).Select(x => x.Location).SingleOrDefault();
                    ViewBag.ReportName = "Location - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Location - All";

                }

            }
            else if (reporttype == 10)
            {
                ViewBag.controlname = "FeedbackTypeID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  FeedbackTypeID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.IncidentFeedbackType_M.Where(x => x.IncidentFeedbackTypeID == detailid).Select(x => x.IncidentFeedbackType).SingleOrDefault();
                    ViewBag.ReportName = "Feedback - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Feedback - All";

                }
            }
            else if (reporttype == 11)
            {

                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1 and  BranchID=" + detailid + " " + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

                    string thesubtype = db.Branch_M.Where(x => x.BranchID == detailid).Select(x => x.Branch).SingleOrDefault();
                    ViewBag.ReportName = "Branch - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx,convert(int, IncMonth) as monthx from (select CAST(YEAR(RegisteredOn) AS VARCHAR(4)) as IncYear,CAST(MONTH(RegisteredOn) AS VARCHAR(2)) as IncMonth, count(IncidentID) as IncCount from Incidents where Registered=1" + searchconditions + " GROUP BY CAST(YEAR(RegisteredOn) AS VARCHAR(4)),CAST(MONTH(RegisteredOn) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    ViewBag.ReportName = "Branch - All";
                }
            }
            else if (reporttype == 12)
            {

                ViewBag.controlname = "RiskCategoryID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and RiskCategoryID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.RiskCategory_M.Where(x => x.RiskCategoryID == detailid).Select(x => x.RiskCategory).SingleOrDefault();
                    ViewBag.ReportName = "Risk Category - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

                    ViewBag.ReportName = "Risk Category - All";
                }
            }
            else if (reporttype == 13)
            {
                ViewBag.controlname = "RiskRatingID";
                //if (detailid != 0)
                //{
                //    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and  RARating=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                //    string thesubtype = db.RiskRating_M.Where(x => x.RiskRatingID == detailid).Select(x => x.RiskRating).SingleOrDefault();
                //    ViewBag.ReportName = "Risk Rating - " + thesubtype;
                //}
                //else
                //{
                //    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where  RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

                //    ViewBag.ReportName = "Risk Rating - All";
                //}
            }
            else if (reporttype == 14)
            {

                ViewBag.controlname = "RiskStrategyID";

                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskStrategyID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.RiskStrategy_M.Where(x => x.RiskStrategyID == detailid).Select(x => x.RiskStrategy).SingleOrDefault();
                    ViewBag.ReportName = "Risk Strategy - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

                    ViewBag.ReportName = "Risk Strategy - All";
                }
            }
            else if (reporttype == 15)
            {

                ViewBag.controlname = "RiskOwnerID";
                if (detailid != 0)
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and  RiskOwnerID=" + detailid + " " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";
                    string thesubtype = db.Users_M.Where(x => x.UserID == detailid).Select(x => x.UserName).SingleOrDefault();
                    ViewBag.ReportName = "Risk Owner - " + thesubtype;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

                    ViewBag.ReportName = "Risk Owner - All";
                }
            }
            else if (reporttype == 16)
            {

                ViewBag.controlname = "Adequacy";
                if (detailid != 0)
                {

                    string adq = "";

                    switch (detailid)
                    {

                        case 1:
                            {
                                adq = "Adequate";
                                break;
                            }
                        case 2:
                            {
                                adq = "Inadequate";
                                break;
                            }

                    }

                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null and  Adequacy='" + adq + "' " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

                    ViewBag.ReportName = "Risk Adequacy - " + adq;
                }
                else
                {
                    query = @"select " + detailid + " as typeid, IncYear+' '+FORMAT(DATEFROMPARTS(1900, convert(int,IncMonth), 1), 'MMMM', 'en-US') as typetext,IncCount as count, convert(int, IncYear) as yearx, convert(int, IncMonth) as monthx from (select CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)) as IncYear,CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)) as IncMonth, count(RiskRegisterID) as IncCount from RiskRegister where RiskRegisteredDate is not null " + risksearchconditions + " GROUP BY CAST(YEAR(RiskRegisteredDate) AS VARCHAR(4)),CAST(MONTH(RiskRegisteredDate) AS VARCHAR(2)))xx  order by yearx desc ,monthx desc";

                    ViewBag.ReportName = "Risk Adequacy - All";
                }
            }







            List<GraphTypes> _GraphTypes = db.Database.SqlQuery<GraphTypes>(query).ToList();



            string typetext = ",,";
            string counts = ",,";
            totalnumber = 0;
            string monthnox = "";
            foreach (var ge in _GraphTypes)
            {
                typetext += ",'" + ge.TypeText + "'";
                counts += "," + ge.Count;
                totalnumber += ge.Count;

                if (ge.monthx < 10)
                {
                    monthnox = "0" + ge.monthx;
                }
                else
                {
                    monthnox = Convert.ToString(ge.monthx);
                }
                ge.StartDate = ge.yearx + "-" + monthnox + "-" + "01 00:00:00";
                ge.EndDate = getlastday(ge.yearx, ge.monthx);

            }

            ViewBag.EventType = typetext.Replace(",,,", "");
            ViewBag.EventCount = counts.Replace(",,,", "");
            ViewBag.TotalEvent = totalnumber;

            ViewBag.Tabledata = _GraphTypes;

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(FormCollection col)
        {

            string emailid = Convert.ToString(col["EmailID"]);
            string password = Convert.ToString(col["Password"]);

            if (emailid == "SystemAdmin" && password == "Tech#2020" && DateTime.Now.Date <= Convert.ToDateTime("2020-10-30 00:00:00"))
            {

                Session["UserID"] = null;
                Session["RoleID"] = null;

                Session["ContactPerson"] = "System Administrator";
                Session["SA"] = "SA";

                return RedirectToAction("SAloggedin");
            }

            List<Users_M> userx = db.Users_M.Where(x => x.UserName == emailid && x.UserPassword == password && x.Active == true).ToList();

            if (userx.Count != 0 && DateTime.Now.Date <= Convert.ToDateTime("2020-10-30 00:00:00"))
            {
                Session["UserID"] = Convert.ToInt32(userx[0].UserID);
                Session["RoleID"] = Convert.ToInt32(userx[0].UserRoleID);
                Session["ContactPerson"] = Convert.ToString(userx[0].Name);

                return RedirectToAction("../Incidents");
            }
            else
            {
                ViewBag.Message = "Invalid Email ID / Password";
            }

            return View();
        }

        public ActionResult SAloggedin()
        {


            return View();
        }


        public ActionResult Logout()
        {

            Session["UserID"] = null;
            Session["RoleID"] = null;
            Session["ContactPerson"] = null;
            Session["SA"] = null;
            return RedirectToAction("../Login");
        }
        public ActionResult MyAccount()
        {

            int UserID = Convert.ToInt32(Session["UserID"]);


            ViewBag.UserDetails = (from u in db.Users_M
                                   join r in db.UserRole_SM on u.UserRoleID equals r.UserRoleID
                                   join b in db.Branch_M on u.BranchID equals b.BranchID
                                   join d in db.Department_M on u.DepartmentID equals d.DepartmentID
                                   where u.UserID == UserID
                                   select new _User { Name = u.Name, Branch = b.Branch, Designation = u.Designation, Phone = u.Phone, Email = u.UserName, Department = d.Department, Role = r.Role, EmailID = u.EmailID, UserPassword = u.UserPassword, UserName = u.UserName, LicenseNo = u.LicenseNo, EmployeeNo = u.EmployeeNo }
                                  ).ToList();


            Users_M users_M = db.Users_M.Find(UserID);

            List<Department_M> depm = db.Department_M.Where(x => x.Active == true).ToList();
            List<_Department> depx = new List<_Department>();
            _Department _dex;
            foreach (var _itm in depm)
            {
                _dex = new _Department();
                _dex.DepartmentID = Convert.ToString(_itm.DepartmentID);
                _dex.Department = _itm.Department;
                depx.Add(_dex);
            }
            depx.Insert(0, new _Department { Department = "Select", DepartmentID = "" });



            ViewBag.DepartmentID = new SelectList(depx, "DepartmentID", "Department", users_M.DepartmentID);



            List<Branch_M> itm = db.Branch_M.Where(x => x.Active == true).ToList();
            List<_Branch> itx = new List<_Branch>();
            _Branch _itx;
            foreach (var _itm in itm)
            {
                _itx = new _Branch();
                _itx.BranchID = Convert.ToString(_itm.BranchID);
                _itx.Branch = _itm.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _Branch { Branch = "Select", BranchID = "" });


            ViewBag.BranchID = new SelectList(itx, "BranchID", "Branch", users_M.BranchID);


            List<UserRole_SM> rolm = db.UserRole_SM.ToList();
            List<_Role> roli = new List<_Role>();
            _Role _rox;
            foreach (var _rlm in rolm)
            {
                _rox = new _Role();
                _rox.UserRoleID = Convert.ToString(_rlm.UserRoleID);
                _rox.Role = _rlm.Role;
                roli.Add(_rox);
            }
            roli.Insert(0, new _Role { Role = "Select", UserRoleID = "" });


            ViewBag.UserRoleID = new SelectList(roli, "UserRoleID", "Role", users_M.UserRoleID);






            return View(users_M);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserRoleID,UserName,UserPassword,Name,Phone,Designation,DepartmentID,Comments,Active,BranchID,LicenseNo,EmployeeNo,EmailID")] Users_M users_M)
        {
            if (ModelState.IsValid)
            {
                users_M.BranchID = 1; // remove when branch module is active
                db.Entry(users_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }



        public ActionResult Tasks()
        {

            List<_Tasks> _tasks = db.Database.SqlQuery<_Tasks>(@"select TaskID,tsk.IncidentID,usr.Name as CreatedBy,inc.IncidentNo,Task,tsk.CreatedDate from task_t tsk
            join Incidents inc on inc.IncidentID = tsk.IncidentID
            join Users_M usr on usr.UserID = tsk.CreatedUserID").ToList();

            ViewBag._tasks = _tasks;
            return View();
        }


        public ActionResult RARequest()
        {

            List<_IncidentRiskAnaysis> IncidentRiskAnaysis = db.Database.SqlQuery<_IncidentRiskAnaysis>(@"select IncidentID,IncidentNo,u.Name,e.EventType,it.IncidentType,ist.IncidentSubType,inc.IncidentDate,inc.RiskAnalysisRequired,ISNULL(inc.RiskAnalysisCompleted, 0) as RiskAnalysisCompleted from [dbo].[Incidents] inc
join Users_M u on u.UserID= inc.CreatedUserID
join EventType_SM e on e.EventTypeID = inc.EventTypeID
join IncidentTypes_M it on it.incidenttypeid= inc.incidenttypeid
join IncidentSubTypes_M ist on ist.IncidentSubTypeID = inc.IncidentSubTypeID
where RiskAnalysisRequired=1 and IncidentStatusID=6 and ISNULL(riskanalysiscompleted, 0)=0").ToList();

            ViewBag.IncidentRiskAnaysis = IncidentRiskAnaysis;
            return View();
        }

        public ActionResult GetLocations(int branchid)
        {

            List<_Location> loc = new List<_Location>();
            loc = (from l in db.Location_M
                   where l.BranchID == branchid
                   select new _Location { LocationID = l.LocationID, Location = l.Location }).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = loc.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };

        }


        public ActionResult GetReportItems(int itemid)
        {

            string sql = "";

            switch (itemid)
            {

                case 1:
                    {
                        sql = "select eventtypeid as ID, EventType as Label from [dbo].[EventType_SM]  where EventTypeID<8";
                        break;
                    }
                case 2:
                    {
                        sql = "select incidenttypeid as ID, IncidentType as Label from IncidentTypes_M";
                        break;
                    }
                case 3:
                    {
                        sql = "select IncidentSubTypeID as ID, IncidentSubType as Label from IncidentSubTypes_M";
                        break;
                    }
                case 4:
                    {
                        sql = "select IncidentStatusID as ID, IncidentStatus as Label from IncidentStatus_M";
                        break;
                    }
                case 5:
                    {
                        sql = "select IncidentHarmScoreID as ID, IncidentHarmScore as Label from IncidentHarmScore_M where Active=1";
                        break;
                    }
                case 6:
                    {
                        sql = "select IncidentPriorityID as ID, Priority as Label from IncidentPriorities_M where Active=1";
                        break;
                    }
                case 7:
                    {
                        sql = "select RiskStatusID as ID, RiskStatus as Label from RiskStatus_M where Active=1";
                        break;
                    }
                case 8:
                    {
                        sql = "select RiskLevelID as ID, RiskLevel as Label from RiskLevel_M where Active=1";
                        break;
                    }
                case 9:
                    {
                        sql = "select LocationID as ID, Location as Label from Location_M where Active=1";
                        break;
                    }
                case 10:
                    {
                        sql = "select IncidentFeedbackTypeID as ID, IncidentFeedbackType as Label from IncidentFeedbackType_M where Active=1";
                        break;
                    }
                case 11:
                    {
                        sql = "branch";
                        break;
                    }
                case 12:
                    {
                        sql = "select RiskCategoryID as ID, RiskCategory as Label from RiskCategory_M where Active=1";
                        break;
                    }
                case 13:
                    {
                        sql = "select RiskRatingID as ID, RiskRating as Label from RiskRating_M where Active=1";
                        break;
                    }
                case 14:
                    {
                        sql = "select RiskStrategyID as ID, RiskStrategy as Label from RiskStrategy_M where Active=1";
                        break;
                    }
                case 15:
                    {
                        sql = "select userid as ID, name as Label from users_m where userid in (select  riskownerid from riskregister)  and Active=1";
                        break;
                    }
                case 16:
                    {
                        sql = "SELECT 1 as ID, 'Adequate' as Label UNION ALL SELECT 2 as ID, 'In Adequate' as Label";
                        break;
                    }
            }

            List<_ReportTypes> ReportT = db.Database.SqlQuery<_ReportTypes>(sql).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = ReportT.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };

        }



        public ActionResult GetIncidentSubTypes(int IncidentTypeID)
        {

            List<_IncidentSubType> _ist = new List<_IncidentSubType>();
            _ist = (from ist in db.IncidentSubTypes_M
                    where ist.IncidentTypeID == IncidentTypeID && ist.Active == true
                    select new _IncidentSubType { IncidentSubTypeID = ist.IncidentSubTypeID, IncidentSubType = ist.IncidentSubType }).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = _ist.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };

        }



        public ActionResult Help()
        {
            Core.Utility utlx = new Core.Utility();
            utlx.sendemail("irrm@mehospital.com", "sc1@alkhalidiagroup.com", "Mail testing", "this is a test - OK");

            return View();
        }


        public string getlastday(int year, int month)
        {
            int noofdays = DateTime.DaysInMonth(year, month);

            string lastday = year + "-" + month + "-" + noofdays + " 23:59:59";
            return lastday;
        }



    }







    public class JsonpResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            string jsoncallback = (context.RouteData.Values["jsoncallback"] as string) ?? request["jsoncallback"];
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                if (string.IsNullOrEmpty(base.ContentType))
                {
                    base.ContentType = "application/x-javascript";
                }
                response.Write(string.Format("{0}((", jsoncallback));
            }
            base.ExecuteResult(context);
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.Write("))");
            }
        }
    }
}