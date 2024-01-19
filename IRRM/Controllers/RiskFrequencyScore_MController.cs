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
    public class RiskFrequencyScore_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskFrequencyScore_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskFrequencyScore_M.ToList());
        }

        // GET: RiskFrequencyScore_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskFrequencyScore_M riskFrequencyScore_M = db.RiskFrequencyScore_M.Find(id);
            if (riskFrequencyScore_M == null)
            {
                return HttpNotFound();
            }
            return View(riskFrequencyScore_M);
        }

        // GET: RiskFrequencyScore_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskFrequencyScore_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskFrequencyScoreID,RiskFrequencyScore")] RiskFrequencyScore_M riskFrequencyScore_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskFrequencyScore_M.Add(riskFrequencyScore_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskFrequencyScore_M);
        }

        // GET: RiskFrequencyScore_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskFrequencyScore_M riskFrequencyScore_M = db.RiskFrequencyScore_M.Find(id);
            if (riskFrequencyScore_M == null)
            {
                return HttpNotFound();
            }
            return View(riskFrequencyScore_M);
        }

        // POST: RiskFrequencyScore_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskFrequencyScoreID,RiskFrequencyScore")] RiskFrequencyScore_M riskFrequencyScore_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskFrequencyScore_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskFrequencyScore_M);
        }

        // GET: RiskFrequencyScore_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskFrequencyScore_M riskFrequencyScore_M = db.RiskFrequencyScore_M.Find(id);
            if (riskFrequencyScore_M == null)
            {
                return HttpNotFound();
            }
            return View(riskFrequencyScore_M);
        }

        // POST: RiskFrequencyScore_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskFrequencyScore_M riskFrequencyScore_M = db.RiskFrequencyScore_M.Find(id);
            db.RiskFrequencyScore_M.Remove(riskFrequencyScore_M);
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
