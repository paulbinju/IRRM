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
    public class IncidentSubTypes_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentSubTypes_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var incidentSubTypes_M = db.IncidentSubTypes_M.OrderBy(x => x.IncidentTypes_M.IncidentType).ThenBy(x => x.IncidentSubType);
            return View(incidentSubTypes_M.ToList());
        }

        // GET: IncidentSubTypes_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubTypes_M incidentSubTypes_M = db.IncidentSubTypes_M.Find(id);
            if (incidentSubTypes_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentSubTypes_M);
        }

        // GET: IncidentSubTypes_M/Create
        public ActionResult Create()
        {

            List<IncidentTypes_M> itm = db.IncidentTypes_M.Where(x => x.Active == true).ToList();

            List<_IncidentType> itx = new List<_IncidentType>();
            _IncidentType _itx;

            foreach (var _itm in itm) {
                _itx = new _IncidentType();

                _itx.IncidentTypeID = Convert.ToString(_itm.IncidentTypeID);
                _itx.IncidentType = _itm.IncidentType;
                itx.Add(_itx);
            }
            itx.Insert(0, new _IncidentType { IncidentType = "Select", IncidentTypeID = "" });

            ViewBag.IncidentTypeID = new SelectList(itx, "IncidentTypeID", "IncidentType");


            List<IncidentPriorities_M> lipm = db.IncidentPriorities_M.Where(x => x.Active == true).ToList();
            List<_IncidentPriority> ipx = new List<_IncidentPriority>();
            _IncidentPriority _ipx;
            foreach (var _lipm in lipm)
            {
                _ipx = new _IncidentPriority();
                _ipx.IncidentPriorityID = Convert.ToString(_lipm.IncidentPriorityID);
                _ipx.Priority = _lipm.Priority;
                ipx.Add(_ipx);
            }
            ipx.Insert(0, new _IncidentPriority { Priority = "Select", IncidentPriorityID = "" });
            ViewBag.IncidentPriorityID = new SelectList(ipx, "IncidentPriorityID", "Priority");
            return View();
        }

        // POST: IncidentSubTypes_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentSubTypeID,IncidentTypeID,IncidentSubType,Active,RiskAssessment,Comments,IncidentPriorityID")] IncidentSubTypes_M incidentSubTypes_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentSubTypes_M.Add(incidentSubTypes_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incidentSubTypes_M.IncidentTypeID);
            ViewBag.IncidentPriorityID = new SelectList(db.IncidentPriorities_M.Where(x => x.Active == true).ToList(), "IncidentPriorityID", "Priority", incidentSubTypes_M.IncidentPriorityID);
            return View(incidentSubTypes_M);
        }

        // GET: IncidentSubTypes_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubTypes_M incidentSubTypes_M = db.IncidentSubTypes_M.Find(id);
            if (incidentSubTypes_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incidentSubTypes_M.IncidentTypeID);
            ViewBag.IncidentPriorityID = new SelectList(db.IncidentPriorities_M.Where(x => x.Active == true).ToList(), "IncidentPriorityID", "Priority", incidentSubTypes_M.IncidentPriorityID);
            return View(incidentSubTypes_M);
        }

        // POST: IncidentSubTypes_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentSubTypeID,IncidentTypeID,IncidentSubType,Active,RiskAssessment,Comments,IncidentPriorityID")] IncidentSubTypes_M incidentSubTypes_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentSubTypes_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentTypeID = new SelectList(db.IncidentTypes_M, "IncidentTypeID", "IncidentType", incidentSubTypes_M.IncidentTypeID);
            ViewBag.IncidentPriorityID = new SelectList(db.IncidentPriorities_M.Where(x => x.Active == true).ToList(), "IncidentPriorityID", "Priority", incidentSubTypes_M.IncidentPriorityID);
            return View(incidentSubTypes_M);
        }

        // GET: IncidentSubTypes_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubTypes_M incidentSubTypes_M = db.IncidentSubTypes_M.Find(id);
            if (incidentSubTypes_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentSubTypes_M);
        }

        // POST: IncidentSubTypes_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentSubTypes_M incidentSubTypes_M = db.IncidentSubTypes_M.Find(id);
            db.IncidentSubTypes_M.Remove(incidentSubTypes_M);
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
