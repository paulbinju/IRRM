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
    public class RiskProbability_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskProbability_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var riskProbability_M = db.RiskProbability_M.Include(r => r.RiskFrequencyScore_M);
            return View(riskProbability_M.ToList());
        }

        // GET: RiskProbability_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskProbability_M riskProbability_M = db.RiskProbability_M.Find(id);
            if (riskProbability_M == null)
            {
                return HttpNotFound();
            }
            return View(riskProbability_M);
        }

        // GET: RiskProbability_M/Create
        public ActionResult Create()
        {
            List<RiskFrequencyScore_M> itm2 = db.RiskFrequencyScore_M.ToList();
            List<_RiskFrequencyScore> itx2 = new List<_RiskFrequencyScore>();
            _RiskFrequencyScore _itx2;
            foreach (var _itm in itm2)
            {
                _itx2 = new _RiskFrequencyScore();
                _itx2.RiskFrequencyScoreID = Convert.ToString(_itm.RiskFrequencyScoreID);
                _itx2.RiskFrequencyScore = _itm.RiskFrequencyScore;
                itx2.Add(_itx2);
            }
            itx2.Insert(0, new _RiskFrequencyScore { RiskFrequencyScore = "Select", RiskFrequencyScoreID = "" });

            ViewBag.RiskFrequencyScoreID = new SelectList(itx2, "RiskFrequencyScoreID", "RiskFrequencyScore");
            return View();
        }

        // POST: RiskProbability_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskProbabilityID,RiskFrequencyScoreID,RiskProbability,Comments,Active")] RiskProbability_M riskProbability_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskProbability_M.Add(riskProbability_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RiskFrequencyScoreID = new SelectList(db.RiskFrequencyScore_M, "RiskFrequencyScoreID", "RiskFrequencyScore", riskProbability_M.RiskFrequencyScoreID);
            return View(riskProbability_M);
        }

        // GET: RiskProbability_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskProbability_M riskProbability_M = db.RiskProbability_M.Find(id);
            if (riskProbability_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.RiskFrequencyScoreID = new SelectList(db.RiskFrequencyScore_M, "RiskFrequencyScoreID", "RiskFrequencyScore", riskProbability_M.RiskFrequencyScoreID);
            return View(riskProbability_M);
        }

        // POST: RiskProbability_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskProbabilityID,RiskFrequencyScoreID,RiskProbability,Comments,Active")] RiskProbability_M riskProbability_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskProbability_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RiskFrequencyScoreID = new SelectList(db.RiskFrequencyScore_M, "RiskFrequencyScoreID", "RiskFrequencyScore", riskProbability_M.RiskFrequencyScoreID);
            return View(riskProbability_M);
        }

        // GET: RiskProbability_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskProbability_M riskProbability_M = db.RiskProbability_M.Find(id);
            if (riskProbability_M == null)
            {
                return HttpNotFound();
            }
            return View(riskProbability_M);
        }

        // POST: RiskProbability_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskProbability_M riskProbability_M = db.RiskProbability_M.Find(id);
            db.RiskProbability_M.Remove(riskProbability_M);
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
