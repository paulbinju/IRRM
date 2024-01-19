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

namespace IRRM.Controllers
{
    public class Incident_People_TController : Controller
    {
        private IRRMEntities db = new IRRMEntities();
        private Utility utl = new Utility();
        // GET: Incident_People_T
        public ActionResult Index()
        {
            var incident_People_T = db.Incident_People_T.Include(i => i.IncidentPeopleInvolved_M).Include(i => i.IncidentRelation_M).Include(i => i.Incident);
            return View(incident_People_T.ToList());
        }

        // GET: Incident_People_T/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_People_T incident_People_T = db.Incident_People_T.Find(id);
            if (incident_People_T == null)
            {
                return HttpNotFound();
            }
            return View(incident_People_T);
        }

        // GET: Incident_People_T/Create
        public ActionResult Create(int IncidentID)
        {
            ViewBag.IncidentID = IncidentID;

           
             

            List<IncidentPeopleInvolved_M> ipinAlldata = db.IncidentPeopleInvolved_M.Where(x => x.Active == true).ToList();
            List<_IncidentPeopleInvolved> ipinNewlist = new List<_IncidentPeopleInvolved>();
            _IncidentPeopleInvolved _ipinnewItem;

            foreach (var ipin in ipinAlldata) {

                _ipinnewItem = new _IncidentPeopleInvolved();
                _ipinnewItem.IncidentPeopleInvolvedID = ipin.IncidentPeopleInvolvedID.ToString();
                _ipinnewItem.IncidentPeopleInvolved = ipin.IncidentPeopleInvolved;
                ipinNewlist.Add(_ipinnewItem);
            }
            ipinNewlist.Insert(0, new _IncidentPeopleInvolved { IncidentPeopleInvolvedID = "", IncidentPeopleInvolved = "Select" });

            // ViewBag.IncidentPeopleInvolvedID = new SelectList(ipinNewlist, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved");

            ViewBag.IncidentPeopleInvolvedID = ipinNewlist.ToList();


         



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

        // POST: Incident_People_T/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentPeopleID,IncidentID,UserID,IncidentPeopleInvolvedID,IncidentRelationID,Name,Mobile,IdentityNo,RegNo,CVRNo,Dateofbirth,Gender,IsPatientInjured")] Incident_People_T incident_People_T)
        {
            incident_People_T.CreateUserID = Convert.ToInt32(Session["UserID"]);
            incident_People_T.CreatedDate = DateTime.UtcNow.AddHours(3);
            db.Incident_People_T.Add(incident_People_T);
            db.SaveChanges();


            if (incident_People_T.IncidentRelationID == 1) {
                utl.setStatus(Convert.ToInt32(incident_People_T.IncidentID), 3, Convert.ToInt32(Session["UserID"]));
            }


            int userid = 0;
            int relationid = 0;
            //SEND EMAIL

            userid = Convert.ToInt32(incident_People_T.UserID);
            relationid = Convert.ToInt32(incident_People_T.IncidentRelationID);
            if (userid != 0)
            {
                IncidentRelation_M irm = db.IncidentRelation_M.Where(x => x.IncidentRelationID == relationid && x.Active == true && x.SendEmail == true).SingleOrDefault();
                if (irm != null)
                {
                    if (irm.EmailHeader != "")
                    {
                        Users_M user = db.Users_M.Where(x => x.UserID == userid).SingleOrDefault();
                        if (user.EmailID != null)
                        {
                            if (user.EmailID != "" && user.EmailID.Contains("@"))
                            {
                                Core.Utility utlx = new Core.Utility();
                                utlx.sendemail("irrm@mehospital.com", user.EmailID, irm.EmailSubject, irm.EmailHeader + ",<br>" + irm.EmailFooter);
                            }
                        }
                    }
                }
            }
            


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }



        public ActionResult GetPeopleRelations(int IncidentID, int IncidentPeopleInvolvedID)
        {
            int roleflag = 0;
            if (Convert.ToString(Session["RoleID"]) == "1")
            {
                roleflag = 0;
            }
            else
            {
                int IncidentRelationID = db.Database.SqlQuery<int>("select top 1 IncidentRelationID from [dbo].[IncidentRelation_M] where IncidentRelationID in (select IncidentRelationID from [Incident-People_T] where IncidentID=" + IncidentID + ")").SingleOrDefault();
                roleflag = IncidentRelationID;
            }



            List<IncidentRelation_M> increlAlldata = new List<IncidentRelation_M>();

            if (roleflag == 0) // administrator
            {

                int? currentstatusid = 0;
                currentstatusid = db.Incidents.Where(x => x.IncidentID == IncidentID).Select(x => x.IncidentStatusID).SingleOrDefault();

                if (currentstatusid >= 3)
                {
                    increlAlldata = db.IncidentRelation_M.Where(x => x.IncidentRelationID > 1 && x.IncidentRelationID != 6).ToList();
                }
                else
                {
                    if (IncidentPeopleInvolvedID != 3) // not staff
                    {
                        increlAlldata = db.IncidentRelation_M.Where(x => x.IncidentRelationID > 2 && x.IncidentRelationID != 6).ToList(); // exclude investgator and addl. investigator
                    }
                    else
                    {
                        increlAlldata = db.IncidentRelation_M.Where(x => x.IncidentRelationID != 6).ToList();
                    }

                }


            }
            else if (roleflag == 1) // investigator
            {
                increlAlldata = db.IncidentRelation_M.Where(x => x.IncidentRelationID > 1 && x.IncidentRelationID < 6).ToList();

                if (IncidentPeopleInvolvedID != 3) // not staff
                {
                    increlAlldata = db.IncidentRelation_M.Where(x => x.IncidentRelationID > 2 && x.IncidentRelationID < 6).ToList(); // exclude investigator and  addl. investigator
                }
                else
                {
                    increlAlldata = db.IncidentRelation_M.Where(x => x.IncidentRelationID > 1 && x.IncidentRelationID < 6).ToList(); //exclude addl. investigator
                }
            }
            else if (roleflag == 2) // add. investigator
            {
                increlAlldata = db.IncidentRelation_M.Where(x => x.IncidentRelationID > 2 && x.IncidentRelationID < 6).ToList();
            }


            List<_IncidentRelation> increlNewlist = new List<_IncidentRelation>();
            _IncidentRelation _increlnewItem;
            foreach (var increl in increlAlldata)
            {
                _increlnewItem = new _IncidentRelation();
                _increlnewItem.IncidentRelationID = increl.IncidentRelationID.ToString();
                _increlnewItem.IncidentRelation = increl.IncidentRelation;
                increlNewlist.Add(_increlnewItem);
            }
            increlNewlist.Insert(0, new _IncidentRelation { IncidentRelationID = "", IncidentRelation = "Select" });
            //  ViewBag.IncidentRelationID = new SelectList(increlNewlist, "IncidentRelationID", "IncidentRelation");


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = increlNewlist.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        [HttpPost]
        public ActionResult CreateRegister(FormCollection col)
        {

            int IncidentID = 0;


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


            Incident_People_T incident_People_T = new Incident_People_T();
            incident_People_T.IncidentPeopleInvolvedID = Convert.ToInt32(col["IncidentPeopleInvolvedID"]);
            incident_People_T.IncidentRelationID = Convert.ToInt32(col["IncidentRelationID"]);
            incident_People_T.Name = Convert.ToString(col["Name"]);
            incident_People_T.Mobile = Convert.ToString(col["Mobile"]);
            incident_People_T.IdentityNo = Convert.ToString(col["PatientID"]);
            incident_People_T.RegNo = Convert.ToString(col["RegNo"]);
            incident_People_T.CVRNo = Convert.ToString(col["CVRNo"]);
            incident_People_T.Gender = Convert.ToString(col["Gender"]);
            if (col["Dateofbirth"] == null || col["Dateofbirth"] == "")
            {
                incident_People_T.DateOfBirth = DateTime.UtcNow.AddHours(3);
            }
            else
            {
                incident_People_T.DateOfBirth = Convert.ToDateTime(col["Dateofbirth"]);
            }
            incident_People_T.IsPatientInjured = Convert.ToBoolean(col["PatientInjured"]);
            incident_People_T.CreatedDate = DateTime.UtcNow.AddHours(3);

            incident_People_T.CreateUserID = Convert.ToInt32(Session["UserID"]);
            incident_People_T.IncidentID = IncidentID;
            db.Incident_People_T.Add(incident_People_T);
            db.SaveChanges();

            List<_Incidents> inctmp = new List<_Incidents>();
            _Incidents _inctmp = new _Incidents();
            _inctmp.IncidentID = IncidentID;
            inctmp.Add(_inctmp);

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = inctmp.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        // GET: Incident_People_T/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_People_T incident_People_T = db.Incident_People_T.Find(id);
            if (incident_People_T == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentPeopleInvolvedID = new SelectList(db.IncidentPeopleInvolved_M, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved", incident_People_T.IncidentPeopleInvolvedID);
            ViewBag.IncidentRelationID = new SelectList(db.IncidentRelation_M, "IncidentRelationID", "IncidentRelation", incident_People_T.IncidentRelationID);
            ViewBag.IncidentID = new SelectList(db.Incidents, "IncidentID", "IncidentNo", incident_People_T.IncidentID);
            ViewBag.UserID = new SelectList(db.Users_M, "UserID", "UserName", incident_People_T.UserID);
            return View(incident_People_T);
        }

        // POST: Incident_People_T/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentPeopleID,IncidentID,UserID,IncidentPeopleInvolvedID,IncidentRelationID,Name,Mobile")] Incident_People_T incident_People_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incident_People_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentPeopleInvolvedID = new SelectList(db.IncidentPeopleInvolved_M, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved", incident_People_T.IncidentPeopleInvolvedID);
            ViewBag.IncidentRelationID = new SelectList(db.IncidentRelation_M, "IncidentRelationID", "IncidentRelation", incident_People_T.IncidentRelationID);
            ViewBag.IncidentID = new SelectList(db.Incidents, "IncidentID", "IncidentNo", incident_People_T.IncidentID);
            ViewBag.UserID = new SelectList(db.Users_M, "UserID", "UserName", incident_People_T.UserID);
            return View(incident_People_T);
        }

        // GET: Incident_People_T/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_People_T incident_People_T = db.Incident_People_T.Find(id);
            if (incident_People_T == null)
            {
                return HttpNotFound();
            }
            return View(incident_People_T);
        }

        // POST: Incident_People_T/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incident_People_T incident_People_T = db.Incident_People_T.Find(id);
            db.Incident_People_T.Remove(incident_People_T);
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
