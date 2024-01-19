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
    public class IncidentRelation_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentRelation_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.IncidentRelation_M.ToList());
        }

        // GET: IncidentRelation_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentRelation_M incidentRelation_M = db.IncidentRelation_M.Find(id);
            if (incidentRelation_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentRelation_M);
        }

        // GET: IncidentRelation_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentRelation_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentRelationID,IncidentRelation,Active,Comments,SendEmail,EmailSubject,EmailHeader,EmailFooter")] IncidentRelation_M incidentRelation_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentRelation_M.Add(incidentRelation_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentRelation_M);
        }

        // GET: IncidentRelation_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentRelation_M incidentRelation_M = db.IncidentRelation_M.Find(id);
            if (incidentRelation_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentRelation_M);
        }

        // POST: IncidentRelation_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentRelationID,IncidentRelation,Active,Comments,SendEmail,EmailSubject,EmailHeader,EmailFooter")] IncidentRelation_M incidentRelation_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentRelation_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentRelation_M);
        }

        // GET: IncidentRelation_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentRelation_M incidentRelation_M = db.IncidentRelation_M.Find(id);
            if (incidentRelation_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentRelation_M);
        }

        // POST: IncidentRelation_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentRelation_M incidentRelation_M = db.IncidentRelation_M.Find(id);
            db.IncidentRelation_M.Remove(incidentRelation_M);
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
