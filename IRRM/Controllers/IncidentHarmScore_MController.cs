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
    public class IncidentHarmScore_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentHarmScore_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var incidentHarmScore_M = db.IncidentHarmScore_M.Include(i => i.IncidentHarmGroup_M);
            return View(incidentHarmScore_M.ToList());
        }

        // GET: IncidentHarmScore_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentHarmScore_M incidentHarmScore_M = db.IncidentHarmScore_M.Find(id);
            if (incidentHarmScore_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentHarmScore_M);
        }

        // GET: IncidentHarmScore_M/Create
        public ActionResult Create()
        {

            List<IncidentHarmGroup_M> ipinAlldata = db.IncidentHarmGroup_M.Where(x => x.Active == true).ToList();
            List<_IncidentHarmGroup> ipinNewlist = new List<_IncidentHarmGroup>();
            _IncidentHarmGroup _ipinnewItem;

            foreach (var ipin in ipinAlldata)
            {

                _ipinnewItem = new _IncidentHarmGroup();
                _ipinnewItem.IncidentHarmGroupID = ipin.IncidentHarmGroupID.ToString();
                _ipinnewItem.IncidentHarmGroup = ipin.IncidentHarmGroup;
                ipinNewlist.Add(_ipinnewItem);
            }
            ipinNewlist.Insert(0, new _IncidentHarmGroup { IncidentHarmGroupID = "", IncidentHarmGroup = "Select" });


            ViewBag.IncidentHarmGroupID = new SelectList(ipinNewlist, "IncidentHarmGroupID", "IncidentHarmGroup");
            return View();
        }

        // POST: IncidentHarmScore_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentHarmScoreID,IncidentHarmGroupID,IncidentHarmScoreCode,IncidentHarmScore,Comments,Active")] IncidentHarmScore_M incidentHarmScore_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentHarmScore_M.Add(incidentHarmScore_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentHarmGroupID = new SelectList(db.IncidentHarmGroup_M, "IncidentHarmGroupID", "IncidentHarmGroup", incidentHarmScore_M.IncidentHarmGroupID);
            return View(incidentHarmScore_M);
        }

        // GET: IncidentHarmScore_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentHarmScore_M incidentHarmScore_M = db.IncidentHarmScore_M.Find(id);
            if (incidentHarmScore_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentHarmGroupID = new SelectList(db.IncidentHarmGroup_M, "IncidentHarmGroupID", "IncidentHarmGroup", incidentHarmScore_M.IncidentHarmGroupID);
            return View(incidentHarmScore_M);
        }

        // POST: IncidentHarmScore_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentHarmScoreID,IncidentHarmGroupID,IncidentHarmScoreCode,IncidentHarmScore,Comments,Active")] IncidentHarmScore_M incidentHarmScore_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentHarmScore_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentHarmGroupID = new SelectList(db.IncidentHarmGroup_M, "IncidentHarmGroupID", "IncidentHarmGroup", incidentHarmScore_M.IncidentHarmGroupID);
            return View(incidentHarmScore_M);
        }

        // GET: IncidentHarmScore_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentHarmScore_M incidentHarmScore_M = db.IncidentHarmScore_M.Find(id);
            if (incidentHarmScore_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentHarmScore_M);
        }

        // POST: IncidentHarmScore_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentHarmScore_M incidentHarmScore_M = db.IncidentHarmScore_M.Find(id);
            db.IncidentHarmScore_M.Remove(incidentHarmScore_M);
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
