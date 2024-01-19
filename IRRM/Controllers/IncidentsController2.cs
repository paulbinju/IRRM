using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IRRM.Models;

namespace IRRM.Controllers
{
    public class IncidentsController2 : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Incidents
        public ActionResult Index()
        {
            var incidents = db.Incidents.Include(i => i.IncidentFeedbackType_M).Include(i => i.IncidentPeopleInvolved_M).Include(i => i.IncidentSubTypes_M).Include(i => i.IncidentTypes_M).Include(i => i.Location_M).Include(i => i.Users_M);
            return View(incidents.ToList());
        }

        // GET: Incidents/Details/5
        public ActionResult Details(int? id)
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

        // GET: Incidents/Create
        public ActionResult Create()
        {
            ViewBag.IncidentFeedbackTypeID = new SelectList(db.IncidentFeedbackType_M, "IncidentFeedbackTypeID", "IncidentFeedbackType");
            ViewBag.IncidentPeopleInvolvedID = new SelectList(db.IncidentPeopleInvolved_M, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved");
            ViewBag.IncidentSubTypeID = new SelectList(db.IncidentSubTypes_M, "IncidentSubTypeID", "IncidentSubType");
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType");
            ViewBag.LocationID = new SelectList(db.Location_M, "LocationID", "Location");
            ViewBag.CreatedUserID = new SelectList(db.Users_M, "UserID", "UserName");
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentNo,CreatedUserID,IncidentCreatedDate,IncidentPeopleInvolvedID,Summary,Details,LocationID,IncidentTypeID,IncidentSubTypeID,Confidential,InvestigationComments,InvestigatorReport,IncidentFeedbackTypeID,FeedbackComments,Registered,RegisteredOn")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                incident.IncidentGUID = Guid.NewGuid();
                incident.IncidentCreatedDate = DateTime.Now;
                db.Incidents.Add(incident);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentFeedbackTypeID = new SelectList(db.IncidentFeedbackType_M, "IncidentFeedbackTypeID", "IncidentFeedbackType", incident.IncidentFeedbackTypeID);
            ViewBag.IncidentPeopleInvolvedID = new SelectList(db.IncidentPeopleInvolved_M, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved", incident.IncidentPeopleInvolvedID);
            ViewBag.IncidentSubTypeID = new SelectList(db.IncidentSubTypes_M, "IncidentSubTypeID", "IncidentSubType", incident.IncidentSubTypeID);
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incident.IncidentTypeID);
            ViewBag.LocationID = new SelectList(db.Location_M, "LocationID", "Location", incident.LocationID);
            ViewBag.CreatedUserID = new SelectList(db.Users_M, "UserID", "UserName", incident.CreatedUserID);
            return View(incident);
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
            ViewBag.IncidentFeedbackTypeID = new SelectList(db.IncidentFeedbackType_M, "IncidentFeedbackTypeID", "IncidentFeedbackType", incident.IncidentFeedbackTypeID);
            ViewBag.IncidentPeopleInvolvedID = new SelectList(db.IncidentPeopleInvolved_M, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved", incident.IncidentPeopleInvolvedID);
            ViewBag.IncidentSubTypeID = new SelectList(db.IncidentSubTypes_M, "IncidentSubTypeID", "IncidentSubType", incident.IncidentSubTypeID);
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incident.IncidentTypeID);
            ViewBag.LocationID = new SelectList(db.Location_M, "LocationID", "Location", incident.LocationID);
            ViewBag.CreatedUserID = new SelectList(db.Users_M, "UserID", "UserName", incident.CreatedUserID);
            return View(incident);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentID,IncidentNo,CreatedUserID,IncidentCreatedDate,IncidentPeopleInvolvedID,Summary,Details,LocationID,IncidentTypeID,IncidentSubTypeID,Confidential,InvestigationComments,InvestigatorReport,IncidentFeedbackTypeID,FeedbackComments,IncidentGUID,Registered,RegisteredOn")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incident).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentFeedbackTypeID = new SelectList(db.IncidentFeedbackType_M, "IncidentFeedbackTypeID", "IncidentFeedbackType", incident.IncidentFeedbackTypeID);
            ViewBag.IncidentPeopleInvolvedID = new SelectList(db.IncidentPeopleInvolved_M, "IncidentPeopleInvolvedID", "IncidentPeopleInvolved", incident.IncidentPeopleInvolvedID);
            ViewBag.IncidentSubTypeID = new SelectList(db.IncidentSubTypes_M, "IncidentSubTypeID", "IncidentSubType", incident.IncidentSubTypeID);
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incident.IncidentTypeID);
            ViewBag.LocationID = new SelectList(db.Location_M, "LocationID", "Location", incident.LocationID);
            ViewBag.CreatedUserID = new SelectList(db.Users_M, "UserID", "UserName", incident.CreatedUserID);
            return View(incident);
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


        [HttpGet]
        public ActionResult Register(int incidentid) {

            decimal irno = db.Database.SqlQuery<decimal>("insert into IncidentRegistery_T (createddate) values(getdate());select @@identity;").SingleOrDefault();

            db.Database.ExecuteSqlCommand("Update Incidents set registered=1,registeredon=getdate(),IncidentNo=" + irno + " where incidentid=" + incidentid);
            return Content("OK");
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
