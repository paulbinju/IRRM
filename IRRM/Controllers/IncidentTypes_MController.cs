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
    public class IncidentTypes_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentTypes_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var incidentTypes_M = db.IncidentTypes_M.Include(i => i.IncidentPriorities_M);
            return View(incidentTypes_M.OrderBy(x => x.IncidentType).ToList());
        }

        // GET: IncidentTypes_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentTypes_M incidentTypes_M = db.IncidentTypes_M.Find(id);
            if (incidentTypes_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentTypes_M);
        }

        // GET: IncidentTypes_M/Create
        public ActionResult Create()
        {

            List<IncidentPriorities_M> lipm = db.IncidentPriorities_M.Where(x => x.Active == true).ToList();

            List<_IncidentPriority> ipx = new List<_IncidentPriority>();

            _IncidentPriority _ipx;
            foreach (var _lipm in lipm) {

                _ipx = new _IncidentPriority();
                _ipx.IncidentPriorityID = Convert.ToString(_lipm.IncidentPriorityID);
                _ipx.Priority = _lipm.Priority;
                ipx.Add(_ipx);
            }
            ipx.Insert(0, new _IncidentPriority { Priority = "Select", IncidentPriorityID = "" });

            ViewBag.IncidentPriorityID = new SelectList(ipx, "IncidentPriorityID", "Priority");
            return View();
        }

        // POST: IncidentTypes_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentTypeID,IncidentPriorityID,Active,Comments,IncidentType")] IncidentTypes_M incidentTypes_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentTypes_M.Add(incidentTypes_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentPriorityID = new SelectList(db.IncidentPriorities_M, "IncidentPriorityID", "Priority", incidentTypes_M.IncidentPriorityID);
            return View(incidentTypes_M);
        }

        // GET: IncidentTypes_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentTypes_M incidentTypes_M = db.IncidentTypes_M.Find(id);
            if (incidentTypes_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentPriorityID = new SelectList(db.IncidentPriorities_M.Where(x => x.Active == true).ToList(), "IncidentPriorityID", "Priority", incidentTypes_M.IncidentPriorityID);
            return View(incidentTypes_M);
        }

        // POST: IncidentTypes_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentTypeID,IncidentPriorityID,Active,Comments,IncidentType")] IncidentTypes_M incidentTypes_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentTypes_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentPriorityID = new SelectList(db.IncidentPriorities_M, "IncidentPriorityID", "Priority", incidentTypes_M.IncidentPriorityID);
            return View(incidentTypes_M);
        }

        // GET: IncidentTypes_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentTypes_M incidentTypes_M = db.IncidentTypes_M.Find(id);
            if (incidentTypes_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentTypes_M);
        }

        // POST: IncidentTypes_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentTypes_M incidentTypes_M = db.IncidentTypes_M.Find(id);
            db.IncidentTypes_M.Remove(incidentTypes_M);
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
