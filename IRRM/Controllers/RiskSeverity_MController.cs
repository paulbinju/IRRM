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
    public class RiskSeverity_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskSeverity_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var riskSeverity_M = db.RiskSeverity_M.Include(r => r.RiskDescriptor_M).Include(r => r.RiskScore_M);
            return View(riskSeverity_M.ToList());
        }

        // GET: RiskSeverity_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskSeverity_M riskSeverity_M = db.RiskSeverity_M.Find(id);
            if (riskSeverity_M == null)
            {
                return HttpNotFound();
            }
            return View(riskSeverity_M);
        }

        // GET: RiskSeverity_M/Create
        public ActionResult Create()
        {

            List<RiskDescriptor_M> itm = db.RiskDescriptor_M.Where(x => x.Active == true).ToList();
            List<_RiskDescriptorID> itx = new List<_RiskDescriptorID>();
            _RiskDescriptorID _itx;
            foreach (var _itm in itm)
            {
                _itx = new _RiskDescriptorID();
                _itx.RiskDescriptorID = Convert.ToString(_itm.RiskDescriptorID);
                _itx.RiskDescriptor = _itm.RiskDescriptor;
                itx.Add(_itx);
            }
            itx.Insert(0, new _RiskDescriptorID { RiskDescriptor = "Select", RiskDescriptorID = "" });

            ViewBag.RiskDescriptorID = new SelectList(itx, "RiskDescriptorID", "RiskDescriptor");




            List<RiskScore_M> itm2 = db.RiskScore_M.Where(x => x.Active == true).ToList();
            List<_RiskScoreID> itx2 = new List<_RiskScoreID>();
            _RiskScoreID _itx2;
            foreach (var _itm in itm2)
            {
                _itx2 = new _RiskScoreID();
                _itx2.RiskScoreID = Convert.ToString(_itm.RiskScoreID);
                _itx2.RiskScore = _itm.RiskScore;
                itx2.Add(_itx2);
            }
            itx2.Insert(0, new _RiskScoreID { RiskScore = "Select", RiskScoreID = "" });

            ViewBag.RiskScoreID = new SelectList(itx2, "RiskScoreID", "RiskScore");
            return View();
        }

        // POST: RiskSeverity_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskSeverityID,RiskScoreID,RiskDescriptorID,RiskSeverity,Comments,Active")] RiskSeverity_M riskSeverity_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskSeverity_M.Add(riskSeverity_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RiskDescriptorID = new SelectList(db.RiskDescriptor_M, "RiskDescriptorID", "RiskDescriptor", riskSeverity_M.RiskDescriptorID);
            ViewBag.RiskScoreID = new SelectList(db.RiskScore_M, "RiskScoreID", "RiskScore", riskSeverity_M.RiskScoreID);
            return View(riskSeverity_M);
        }

        // GET: RiskSeverity_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskSeverity_M riskSeverity_M = db.RiskSeverity_M.Find(id);
            if (riskSeverity_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.RiskDescriptorID = new SelectList(db.RiskDescriptor_M, "RiskDescriptorID", "RiskDescriptor", riskSeverity_M.RiskDescriptorID);
            ViewBag.RiskScoreID = new SelectList(db.RiskScore_M, "RiskScoreID", "RiskScore", riskSeverity_M.RiskScoreID);
            return View(riskSeverity_M);
        }

        // POST: RiskSeverity_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskSeverityID,RiskScoreID,RiskDescriptorID,RiskSeverity,Comments,Active")] RiskSeverity_M riskSeverity_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskSeverity_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RiskDescriptorID = new SelectList(db.RiskDescriptor_M, "RiskDescriptorID", "RiskDescriptor", riskSeverity_M.RiskDescriptorID);
            ViewBag.RiskScoreID = new SelectList(db.RiskScore_M, "RiskScoreID", "RiskScore", riskSeverity_M.RiskScoreID);
            return View(riskSeverity_M);
        }

        // GET: RiskSeverity_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskSeverity_M riskSeverity_M = db.RiskSeverity_M.Find(id);
            if (riskSeverity_M == null)
            {
                return HttpNotFound();
            }
            return View(riskSeverity_M);
        }

        // POST: RiskSeverity_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskSeverity_M riskSeverity_M = db.RiskSeverity_M.Find(id);
            db.RiskSeverity_M.Remove(riskSeverity_M);
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
