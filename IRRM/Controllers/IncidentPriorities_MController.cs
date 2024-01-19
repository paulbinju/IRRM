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
    public class IncidentPriorities_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentPriorities_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.IncidentPriorities_M.ToList());
        }

        // GET: IncidentPriorities_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentPriorities_M incidentPriorities_M = db.IncidentPriorities_M.Find(id);
            if (incidentPriorities_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentPriorities_M);
        }

        // GET: IncidentPriorities_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentPriorities_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentPriorityID,Priority,InvestigationDays,Comments,Active")] IncidentPriorities_M incidentPriorities_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentPriorities_M.Add(incidentPriorities_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentPriorities_M);
        }

        // GET: IncidentPriorities_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentPriorities_M incidentPriorities_M = db.IncidentPriorities_M.Find(id);
            if (incidentPriorities_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentPriorities_M);
        }

        // POST: IncidentPriorities_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentPriorityID,Priority,InvestigationDays,Comments,Active")] IncidentPriorities_M incidentPriorities_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentPriorities_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentPriorities_M);
        }

        // GET: IncidentPriorities_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentPriorities_M incidentPriorities_M = db.IncidentPriorities_M.Find(id);
            if (incidentPriorities_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentPriorities_M);
        }

        // POST: IncidentPriorities_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentPriorities_M incidentPriorities_M = db.IncidentPriorities_M.Find(id);
            db.IncidentPriorities_M.Remove(incidentPriorities_M);
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
