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
    public class IncidentStatus_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentStatus_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }

            return View(db.IncidentStatus_M.ToList());
        }

        // GET: IncidentStatus_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentStatus_M incidentStatus_M = db.IncidentStatus_M.Find(id);
            if (incidentStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentStatus_M);
        }

        // GET: IncidentStatus_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentStatus_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentStatusID,IncidentStatus,Active,Comments")] IncidentStatus_M incidentStatus_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentStatus_M.Add(incidentStatus_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentStatus_M);
        }

        // GET: IncidentStatus_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentStatus_M incidentStatus_M = db.IncidentStatus_M.Find(id);
            if (incidentStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentStatus_M);
        }

        // POST: IncidentStatus_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentStatusID,IncidentStatus,Active,Comments")] IncidentStatus_M incidentStatus_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentStatus_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentStatus_M);
        }

        // GET: IncidentStatus_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentStatus_M incidentStatus_M = db.IncidentStatus_M.Find(id);
            if (incidentStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentStatus_M);
        }

        // POST: IncidentStatus_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentStatus_M incidentStatus_M = db.IncidentStatus_M.Find(id);
            db.IncidentStatus_M.Remove(incidentStatus_M);
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
