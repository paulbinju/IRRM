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
    public class IncidentFeedbackType_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentFeedbackType_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.IncidentFeedbackType_M.ToList());
        }

        // GET: IncidentFeedbackType_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentFeedbackType_M incidentFeedbackType_M = db.IncidentFeedbackType_M.Find(id);
            if (incidentFeedbackType_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentFeedbackType_M);
        }

        // GET: IncidentFeedbackType_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentFeedbackType_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentFeedbackTypeID,IncidentFeedbackType,Comments,Active")] IncidentFeedbackType_M incidentFeedbackType_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentFeedbackType_M.Add(incidentFeedbackType_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentFeedbackType_M);
        }

        // GET: IncidentFeedbackType_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentFeedbackType_M incidentFeedbackType_M = db.IncidentFeedbackType_M.Find(id);
            if (incidentFeedbackType_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentFeedbackType_M);
        }

        // POST: IncidentFeedbackType_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentFeedbackTypeID,IncidentFeedbackType,Comments,Active")] IncidentFeedbackType_M incidentFeedbackType_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentFeedbackType_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentFeedbackType_M);
        }

        // GET: IncidentFeedbackType_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentFeedbackType_M incidentFeedbackType_M = db.IncidentFeedbackType_M.Find(id);
            if (incidentFeedbackType_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentFeedbackType_M);
        }

        // POST: IncidentFeedbackType_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentFeedbackType_M incidentFeedbackType_M = db.IncidentFeedbackType_M.Find(id);
            db.IncidentFeedbackType_M.Remove(incidentFeedbackType_M);
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
