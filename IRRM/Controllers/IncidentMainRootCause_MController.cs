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
    public class IncidentMainRootCause_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentMainRootCause_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.IncidentMainRootCause_M.ToList());
        }

        // GET: IncidentMainRootCause_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentMainRootCause_M incidentMainRootCause_M = db.IncidentMainRootCause_M.Find(id);
            if (incidentMainRootCause_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentMainRootCause_M);
        }

        // GET: IncidentMainRootCause_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentMainRootCause_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentMainRootCauseID,IncidentMainRootCause,Active,Comments")] IncidentMainRootCause_M incidentMainRootCause_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentMainRootCause_M.Add(incidentMainRootCause_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentMainRootCause_M);
        }

        // GET: IncidentMainRootCause_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentMainRootCause_M incidentMainRootCause_M = db.IncidentMainRootCause_M.Find(id);
            if (incidentMainRootCause_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentMainRootCause_M);
        }

        // POST: IncidentMainRootCause_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentMainRootCauseID,IncidentMainRootCause,Active,Comments")] IncidentMainRootCause_M incidentMainRootCause_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentMainRootCause_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentMainRootCause_M);
        }

        // GET: IncidentMainRootCause_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentMainRootCause_M incidentMainRootCause_M = db.IncidentMainRootCause_M.Find(id);
            if (incidentMainRootCause_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentMainRootCause_M);
        }

        // POST: IncidentMainRootCause_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentMainRootCause_M incidentMainRootCause_M = db.IncidentMainRootCause_M.Find(id);
            db.IncidentMainRootCause_M.Remove(incidentMainRootCause_M);
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
