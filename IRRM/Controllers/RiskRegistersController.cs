using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IRRM.Models;

namespace IRRM.Controllers
{
    public class RiskRegistersController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskRegisters
        public ActionResult Index()
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            //var riskreg = (from rr in db.RiskRegisters
            //               join rc in db.RiskCategory_M on rr.RiskCategoryID equals rc.RiskCategoryID
            //               join rcst in db.RiskCost_M on rr.RiskCostID equals rcst.RiskCostID
            //               join rd in db.RiskDescriptor_M on rr.RiskDescriptorID equals rd.RiskDescriptorID
            //               join rs in db.RiskStatus_M on rr.RiskStatusID equals rs.RiskStatusID
            //               join rt in db.RiskStrategy_M on rr.RiskStrategyID equals rt.RiskStrategyID
            //               join usr in db.Users_M on rr.RiskOwnerID equals usr.UserID
            //               join usr2 in db.Users_M on rr.RiskAssessedByID equals usr2.UserID
            //               join ks in db.RiskKeyStatus_T on rr.RiskKeyStatusID equals ks.RiskKeyStatusID
            //               where rr.RiskKeyStatusID <= 2
            //               orderby rr.RiskRegisterID descending
            //               select new _RiskRegister
            //               {
            //                   RiskRegisterID = rr.RiskRegisterID,
            //                   RiskRegisterNo = rr.RiskRegisterNo,
            //                   Proactive = rr.Proactive,
            //                   ReferenceNo = rr.ReferenceNo,
            //                   RiskStatus = rs.RiskStatus,
            //                   RiskDescriptor = rd.RiskDescriptor,
            //                   Adequacy = rr.Adequacy,
            //                   RARating = rr.RARating,
            //                   RARiskLevel = rr.RARiskLevel,
            //                   RRRating = rr.RRRating,
            //                   RRRiskLevel = rr.RRRiskLevel,
            //                   RiskCategory = rc.RiskCategory,
            //                   RiskCost = rcst.RiskCost,
            //                   RiskKeyStatus = ks.RiskKeyStatus,
            //                   RiskStrategy = rt.RiskStrategy,
            //                   RiskOwner = usr.Name,
            //                   RiskRegisteredDate = rr.RiskRegisteredDate,
            //                   RiskAssessedBy = usr2.Name
            //               }
            //               ).ToList();
            //ViewBag.riskreg = riskreg;

           

            string qry = "";
            if (Convert.ToInt32(Session["RoleID"]) == 1)
            { // admin
                qry = @"select *,usr.Name as RiskOwner from riskregister rr
join RiskCategory_M rc on rr.RiskCategoryID = rc.RiskCategoryID
join  RiskCost_M rcst on rr.RiskCostID = rcst.RiskCostID
join  RiskDescriptor_M rd on rr.RiskDescriptorID = rd.RiskDescriptorID
join Location_M  rs on rr.RiskStatusID = rs.LocationID
join  RiskStrategy_M rt on rr.RiskStrategyID = rt.RiskStrategyID
join  Users_M usr on rr.RiskOwnerID = usr.UserID
join  RiskKeyStatus_T ks on rr.RiskKeyStatusID = ks.RiskKeyStatusID where 1=1  order by rr.riskregisterid desc";
            }
            else
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                qry = @"select *,usr.Name as RiskOwner  from riskregister rr
join RiskCategory_M rc on rr.RiskCategoryID = rc.RiskCategoryID
join  RiskCost_M rcst on rr.RiskCostID = rcst.RiskCostID
join  RiskDescriptor_M rd on rr.RiskDescriptorID = rd.RiskDescriptorID
join Location_M  rs on rr.RiskStatusID = rs.LocationID
join  RiskStrategy_M rt on rr.RiskStrategyID = rt.RiskStrategyID
join  Users_M usr on rr.RiskOwnerID = usr.UserID
join  RiskKeyStatus_T ks on rr.RiskKeyStatusID = ks.RiskKeyStatusID where 1=1 and (rr.RiskOwnerID=" + userid + " or rr.RiskAssessedByID = " + userid + " )   order by rr.riskregisterid desc";

            }



            var riskreg = db.Database.SqlQuery<_RiskRegister>(qry).ToList();

            ViewBag.riskreg = riskreg;


            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

     

            string searchconditions = "";

            if (Convert.ToInt32(Session["RoleID"]) != 1)
            { // not admin
                int userid = Convert.ToInt32(Session["UserID"]);
                searchconditions += " and (rr.RiskOwnerID=" + userid + " or rr.RiskAssessedByID = " + userid + " )"; 

            }


            if (Convert.ToString(col["RRDateFrom"]) != "")
            {
                searchconditions += " and rr.RiskRegisteredDate>='" + Convert.ToDateTime(col["RRDateFrom"]).ToString("yyyy-MM-dd") + " 00:00:00' ";
            }
            if (Convert.ToString(col["RRDateTo"]) != "")
            {
                searchconditions += " and rr.RiskRegisteredDate<='" + Convert.ToDateTime(col["RRDateTo"]).ToString("yyyy-MM-dd") + " 23:59:59' ";
            }
            if (Convert.ToInt32(col["Proactive"]) != -1)
            {
                searchconditions += " and rr.Proactive=" + Convert.ToInt32(col["Proactive"]) + " ";
            }
            if (Convert.ToInt32(col["RiskCategoryID"]) != 0)
            {
                searchconditions += " and rr.RiskCategoryID=" + Convert.ToInt32(col["RiskCategoryID"]) + " ";
            }


            if (Convert.ToInt32(col["RiskCostID"]) != 0)
            {
                searchconditions += " and rr.RiskCostID=" + Convert.ToInt32(col["RiskCostID"]) + " ";
            }

            if (Convert.ToInt32(col["RiskDescriptorID"]) != 0)
            {
                searchconditions += " and rr.RiskDescriptorID=" + Convert.ToInt32(col["RiskDescriptorID"]) + " ";
            }

            if (Convert.ToInt32(col["RiskStatusID"]) != 0)
            {
                searchconditions += " and rr.RiskStatusID=" + Convert.ToInt32(col["RiskStatusID"]) + " ";
            }


            if (Convert.ToInt32(col["RiskStrategyID"]) != 0)
            {
                searchconditions += " and rr.RiskStrategyID=" + Convert.ToInt32(col["RiskStrategyID"]) + " ";
            }


            if (Convert.ToInt32(col["RARating"]) != 0)
            {
                searchconditions += " and rr.RARating=" + Convert.ToInt32(col["RARating"]) + " ";
            }


            if (Convert.ToString(col["RARiskLevel"]) != "0")
            {
                searchconditions += " and rr.RARiskLevel='" + Convert.ToString(col["RARiskLevel"]) + "' ";
            }

            if (Convert.ToString(col["Adequacy"]) != "-1")
            {
                searchconditions += " and rr.Adequacy='" + Convert.ToString(col["Adequacy"]) + "' ";
            }

            if (Convert.ToInt32(col["RiskOwnerID"]) != 0)
            {
                searchconditions += " and rr.RiskOwnerID=" + Convert.ToInt32(col["RiskOwnerID"]) + " ";
            }


            if (Convert.ToInt32(col["RiskAssessedByID"]) != 0)
            {
                searchconditions += " and rr.RiskAssessedByID=" + Convert.ToInt32(col["RiskAssessedByID"]) + " ";
            }


            if (Convert.ToInt32(col["RiskKeyStatusID"]) != 0)
            {
                searchconditions += " and rr.RiskKeyStatusID=" + Convert.ToInt32(col["RiskKeyStatusID"]) + " ";
            }



            string qry = @"select *,usr.Name as RiskOwner,usr2.Name as RiskAssessedBy from riskregister rr
join RiskCategory_M rc on rr.RiskCategoryID = rc.RiskCategoryID
join  RiskCost_M rcst on rr.RiskCostID = rcst.RiskCostID
join  RiskDescriptor_M rd on rr.RiskDescriptorID = rd.RiskDescriptorID
join  Location_M rs on rr.RiskStatusID = rs.LocationID
join  RiskStrategy_M rt on rr.RiskStrategyID = rt.RiskStrategyID
join  Users_M usr on rr.RiskOwnerID = usr.UserID
left join  Users_M usr2 on rr.RiskAssessedByID = usr2.UserID
join  RiskKeyStatus_T ks on rr.RiskKeyStatusID = ks.RiskKeyStatusID where 1=1 " + searchconditions + " order by rr.riskregisterid desc";

            var riskreg = db.Database.SqlQuery<_RiskRegister>(qry).ToList();

            ViewBag.riskreg = riskreg;
            return View();
        }

        // GET: RiskRegisters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskRegister riskRegister = db.RiskRegisters.Find(id);
            if (riskRegister == null)
            {
                return HttpNotFound();
            }
            return View(riskRegister);
        }

        // GET: RiskRegisters/Create
        public ActionResult Create(int? IncidentID = 0)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);

            ViewBag.IncidentID = IncidentID;
            ViewBag.IncidentNo = db.Incidents.Where(x => x.IncidentID == IncidentID).Select(x => x.IncidentNo).SingleOrDefault();
            ViewBag.RiskCategoryID = db.RiskCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskCostID = db.RiskCost_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskStatusID = db.RiskStatus_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskStrategyID = db.RiskStrategy_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskDescriptorID = db.RiskDescriptor_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskProbability = db.RiskProbability_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskSeverity = db.RiskSeverity_M.Where(x => x.Active == true).ToList();

            ViewBag.Location = db.Location_M.Where(x => x.Active == true).ToList();

            List<Users_M> itm = db.Users_M.Where(x => x.Active == true).ToList();
            List<_User> itx = new List<_User>();
            _User _itx;

            foreach (var _itm in itm)
            {
                _itx = new _User();

                _itx.UserID2 = Convert.ToString(_itm.UserID);
                _itx.Name = _itm.Name + "-" + _itm.Designation + "-" + _itm.Department_M.Department + "-" + _itm.Branch_M.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _User { Name = "Select", UserID2 = "" });

            ViewBag.UserID = new SelectList(itx, "UserID2", "Name");

            ViewBag.Users = itx.ToList();

            return View();
        }

        // POST: RiskRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskRegisterID,RRNo,RiskRegisterNo,CreatedUserID,CreatedOn,Proactive,IncidentID,ReferenceNo,RiskCategoryID,RiskCostID,RiskDescriptorID,RiskStatusID,RiskStrategyID,Description,Controlsinplace,Adequacy,RARating,RARiskLevel,RRRating,RRRiskLevel,RARiskProbabilityID,RRRiskProbabilityID,RASeverityID,RRSeverityID,FutureStrategies,PotentialRiskFactors,Comments,RiskAssessedByID,RiskOwnerID,TaskNo,Description")] RiskRegister riskRegister)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int UserID = Convert.ToInt32(Session["UserID"]);

            int? rrno = rrno = db.RiskRegisters.Max(x => x.RRNo);

            if (rrno != null)
            {
                rrno++;
            }
            else
            {
                rrno = 1;
            }

            if (riskRegister.IncidentID == 0)
            {
                riskRegister.Proactive = true;
            }
            else
            {
                db.Database.ExecuteSqlCommand("Update Incidents set RiskAnalysisCompleted=1 where incidentid=" + riskRegister.IncidentID);
            }


            Utility utl = new Utility();
            string newregno = utl.Getregistryno("RR");


            string[] rrnoonly = newregno.Split('-');


            riskRegister.RRNo = Convert.ToInt32(rrnoonly[2]);
            riskRegister.RiskRegisterNo = newregno;
            riskRegister.CreatedUserID = UserID;
            riskRegister.CreatedOn = DateTime.UtcNow.AddHours(3);
            riskRegister.RiskRegisteredDate = DateTime.UtcNow.AddHours(3);
            riskRegister.RiskKeyStatusID = 1;
            db.RiskRegisters.Add(riskRegister);
            db.SaveChanges();

            RiskKeyStatusDates_T rkd = new RiskKeyStatusDates_T();
            rkd.RiskRegisterID = riskRegister.RiskRegisterID;
            rkd.RiskKeyStatusID = 1;
            rkd.StatusDate = DateTime.UtcNow.AddHours(3);
            db.RiskKeyStatusDates_T.Add(rkd);
            db.SaveChanges();

            int? riskownerid = riskRegister.RiskOwnerID;
            if (riskownerid != null) {

                string email = db.Users_M.Where(x=>x.UserID== riskownerid).Select(x=>x.EmailID).SingleOrDefault();

                Core.Utility utlc = new Core.Utility();
                string emailbody = @"Dear Sir / Madam,<br><p>You are added as the Risk Owner for the Risk Register No." + riskRegister.RiskRegisterNo + " </p><p>Please login to see the details.</p>";

                try
                {
                    utlc.sendemail("irrm@mehospital.com", email, "Risk Register", emailbody);
                }
                catch { }

            }


            return RedirectToAction("Index");

        }

        // GET: RiskRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RiskRegister riskRegister = db.RiskRegisters.Find(id);
            if (riskRegister == null)
            {
                return HttpNotFound();
            }

            ViewBag.Location = db.Location_M.Where(x => x.Active == true).ToList();

            ViewBag.RiskSeverity = db.RiskSeverity_M.Where(x => x.Active == true).ToList();

            ViewBag.RiskCategoryID = db.RiskCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskCostID = db.RiskCost_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskStatusID = db.RiskStatus_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskStrategyID = db.RiskStrategy_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskDescriptorID = db.RiskDescriptor_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskProbability = db.RiskProbability_M.Where(x => x.Active == true).ToList();
            ViewBag.RiskKeyDates = (from rk in db.RiskKeyStatusDates_T
                                    join rs in db.RiskKeyStatus_T on rk.RiskKeyStatusID equals rs.RiskKeyStatusID
                                    where rk.RiskRegisterID == id
                                    select new _RiskKeyStaus { StatusDate = rk.StatusDate, RiskKeyStatus = rs.RiskKeyStatus, RiskKeyStatusDateID = rk.RiskKeyStatusDateID }
                                    ).ToList();

            List<Users_M> itm = db.Users_M.Where(x => x.Active == true).ToList();
            List<_User> itx = new List<_User>();
            _User _itx;

            string ReviewDate = db.Database.SqlQuery<string>("select convert(varchar, DATEADD(day,ReviewTimeScale,GETDATE()),111) as reviewdate  from RiskLevel_M where RiskLevel like '%'+(select RARiskLevel from RiskRegister where RiskRegisterID=" + id + ")+'%'").SingleOrDefault();

            ViewBag.ReviewDate = ReviewDate.Replace("/", "-");

            foreach (var _itm in itm)
            {
                _itx = new _User();

                _itx.UserID2 = Convert.ToString(_itm.UserID);
                _itx.Name = _itm.Name + "-" + _itm.Designation + "-" + _itm.Department_M.Department + "-" + _itm.Branch_M.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _User { Name = "Select", UserID2 = "" });

            ViewBag.UserID = new SelectList(itx, "UserID2", "Name");

            ViewBag.Users = itx.ToList();

            ViewBag.Assessors = db.RiskAssessorUser_V.Where(x=>x.RiskID == id).ToList();
            return View(riskRegister);
        }


        [HttpPost]
        public ActionResult ReviewStatus(FormCollection col)
        {
            RiskKeyStatusDates_T rkd = new RiskKeyStatusDates_T();
            int rrid = Convert.ToInt32(col["RiskRegisterID"]);
            rkd.RiskRegisterID = rrid;
            rkd.RiskKeyStatusID = 3;
            rkd.StatusDate = Convert.ToDateTime(col["ReviewDate"]);
            db.RiskKeyStatusDates_T.Add(rkd);
            db.SaveChanges();

            RiskRegister rr = db.RiskRegisters.Find(rrid);
            rr.RiskKeyStatusID = 3;
            db.Entry(rr).Property(x => x.RiskKeyStatusID).IsModified = true;
            db.SaveChanges();


            int newrrnumber = db.RiskRegisters.Where(x => x.RRNo == rr.RRNo && x.RiskRegisteredDate == rr.RiskRegisteredDate).Count();

            // new record
            rr.ReferenceNo = rr.RiskRegisterNo;
            rr.RiskRegisterNo = "RR-" + Convert.ToDateTime(rr.RiskRegisteredDate).ToString("MM") + Convert.ToDateTime(rr.RiskRegisteredDate).ToString("yy") + "-" + rr.RRNo + "-" + newrrnumber;
            rr.CreatedOn = DateTime.UtcNow.AddHours(3);
            rr.RiskKeyStatusID = 2;
            db.RiskRegisters.Add(rr);
            db.SaveChanges();


            rkd.RiskRegisterID = rr.RiskRegisterID;
            rkd.RiskKeyStatusID = 2;
            rkd.StatusDate = Convert.ToDateTime(col["ReviewDate"]);
            db.RiskKeyStatusDates_T.Add(rkd);
            db.SaveChanges();



            return View();
        }

        [HttpPost]
        public ActionResult CloseStatus(FormCollection col)
        {
            int rrid = Convert.ToInt32(col["RiskRegisterID"]);
            RiskRegister rr = db.RiskRegisters.Find(rrid);
            rr.RiskKeyStatusID = 4;
            db.Entry(rr).Property(x => x.RiskKeyStatusID).IsModified = true;
            db.SaveChanges();


            RiskKeyStatusDates_T rkd = new RiskKeyStatusDates_T();
            rkd.RiskRegisterID = rrid;
            rkd.RiskKeyStatusID = 4;
            rkd.StatusDate = DateTime.UtcNow.AddHours(3);
            db.RiskKeyStatusDates_T.Add(rkd);
            db.SaveChanges();

            return View();
        }




        public ActionResult RiskSearchPartial()
        {
            ViewBag.RiskCategoryID = db.RiskCategory_M.ToList();
            ViewBag.RiskCostID = db.RiskCost_M.ToList();
            ViewBag.RiskDescriptorID = db.RiskDescriptor_M.ToList();
            ViewBag.RiskStatusID = db.RiskStatus_M.ToList();
            ViewBag.RiskStrategyID = db.RiskStrategy_M.ToList();

            List<_RiskLevel> RARiskLevel = (from rr in db.RiskRegisters
                                            select new _RiskLevel { RARiskLevel = rr.RARiskLevel }
                                             ).Distinct().ToList();
                //db.RiskRegisters.Select(x => x.RARiskLevel).Distinct().ToList();

            ViewBag.RARiskLevel = RARiskLevel;
            ViewBag.Users = db.Users_M.ToList();
            ViewBag.RiskKeyStatusID = db.RiskKeyStatus_T.ToList();
            ViewBag.Location = db.Location_M.ToList();

            return PartialView();
        }



        // POST: RiskRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskRegisterID,RRNo,RiskRegisterNo,CreatedUserID,CreatedOn,Proactive,IncidentID,IncidentNo,RiskCategoryID,RiskCostID,RiskDescriptorID,RiskStatusID,RiskStrategyID,Description,Controlsinplace,Adequacy,RARating,RARiskLevel,RRRating,RRRiskLevel,RARiskProbabilityID,RASeverityID,RRRiskProbabilityID,RRSeverityID,FutureStrategies,PotentialRiskFactors,Comments,RiskAssessedByID,RiskOwnerID,TaskNo,Description, RiskKeyStatusID,ReferenceNo,RiskRegisteredDate")] RiskRegister riskRegister)
        {

            db.Entry(riskRegister).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit", "RiskRegisters", riskRegister.RiskRegisterID);
        }


        public ActionResult Report(int id)
        {

            RiskRegister riskRegister = db.RiskRegisters.Find(id);
            ViewBag.Location = db.Location_M.Where(x => x.LocationID == riskRegister.RiskStatusID).Select(x => x.Location).SingleOrDefault();

            ViewBag.Assessors = db.RiskAssessorUser_V.Where(x => x.RiskID == id).ToList();

            int? ownerid = db.RiskRegisters.Where(x => x.RiskRegisterID == id).Select(x => x.RiskOwnerID).SingleOrDefault();
            Users_M usrx = db.Users_M.Where(x => x.UserID == ownerid).SingleOrDefault();

            ViewBag.RiskOwner = usrx.Name + " - " + usrx.Designation;


            ViewBag.RASeverity = db.RiskSeverity_M.Where(x => x.RiskSeverityID == riskRegister.RASeverityID).Select(x => x.RiskSeverity).SingleOrDefault();
            ViewBag.RRSeverity = db.RiskSeverity_M.Where(x => x.RiskSeverityID == riskRegister.RRSeverityID).Select(x => x.RiskSeverity).SingleOrDefault();


            return View(riskRegister);
        }


        public ActionResult GetRiskLevel(string level)
        {
            var rklevel = db.RiskLevel_M.Where(x => x.RiskLevel.Contains(level)).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = rklevel.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        // GET: RiskRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskRegister riskRegister = db.RiskRegisters.Find(id);
            if (riskRegister == null)
            {
                return HttpNotFound();
            }
            return View(riskRegister);
        }

        // POST: RiskRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskRegister riskRegister = db.RiskRegisters.Find(id);
            db.RiskRegisters.Remove(riskRegister);
            db.SaveChanges();
            return RedirectToAction("Index");
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
