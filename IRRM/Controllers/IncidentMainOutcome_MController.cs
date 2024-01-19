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
    public class IncidentMainOutcome_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentMainOutcome_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.IncidentMainOutcome_M.ToList());
        }

        // GET: IncidentMainOutcome_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentMainOutcome_M incidentMainOutcome_M = db.IncidentMainOutcome_M.Find(id);
            if (incidentMainOutcome_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentMainOutcome_M);
        }

        // GET: IncidentMainOutcome_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentMainOutcome_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentMainOutcomeID,IncidentMainOutcome,Active,Comments")] IncidentMainOutcome_M incidentMainOutcome_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentMainOutcome_M.Add(incidentMainOutcome_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentMainOutcome_M);
        }

        // GET: IncidentMainOutcome_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentMainOutcome_M incidentMainOutcome_M = db.IncidentMainOutcome_M.Find(id);
            if (incidentMainOutcome_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentMainOutcome_M);
        }

        // POST: IncidentMainOutcome_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentMainOutcomeID,IncidentMainOutcome,Active,Comments")] IncidentMainOutcome_M incidentMainOutcome_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentMainOutcome_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentMainOutcome_M);
        }

        // GET: IncidentMainOutcome_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentMainOutcome_M incidentMainOutcome_M = db.IncidentMainOutcome_M.Find(id);
            if (incidentMainOutcome_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentMainOutcome_M);
        }

        // POST: IncidentMainOutcome_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentMainOutcome_M incidentMainOutcome_M = db.IncidentMainOutcome_M.Find(id);
            db.IncidentMainOutcome_M.Remove(incidentMainOutcome_M);
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
