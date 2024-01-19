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
    public class IncidentHarmGroup_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentHarmGroup_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.IncidentHarmGroup_M.ToList());
        }

        // GET: IncidentHarmGroup_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentHarmGroup_M incidentHarmGroup_M = db.IncidentHarmGroup_M.Find(id);
            if (incidentHarmGroup_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentHarmGroup_M);
        }

        // GET: IncidentHarmGroup_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentHarmGroup_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentHarmGroupID,IncidentHarmGroup,Comments,Active")] IncidentHarmGroup_M incidentHarmGroup_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentHarmGroup_M.Add(incidentHarmGroup_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentHarmGroup_M);
        }

        // GET: IncidentHarmGroup_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentHarmGroup_M incidentHarmGroup_M = db.IncidentHarmGroup_M.Find(id);
            if (incidentHarmGroup_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentHarmGroup_M);
        }

        // POST: IncidentHarmGroup_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentHarmGroupID,IncidentHarmGroup,Comments,Active")] IncidentHarmGroup_M incidentHarmGroup_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentHarmGroup_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentHarmGroup_M);
        }

        // GET: IncidentHarmGroup_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentHarmGroup_M incidentHarmGroup_M = db.IncidentHarmGroup_M.Find(id);
            if (incidentHarmGroup_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentHarmGroup_M);
        }

        // POST: IncidentHarmGroup_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentHarmGroup_M incidentHarmGroup_M = db.IncidentHarmGroup_M.Find(id);
            db.IncidentHarmGroup_M.Remove(incidentHarmGroup_M);
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
