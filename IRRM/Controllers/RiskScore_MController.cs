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
    public class RiskScore_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskScore_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskScore_M.ToList());
        }

        // GET: RiskScore_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskScore_M riskScore_M = db.RiskScore_M.Find(id);
            if (riskScore_M == null)
            {
                return HttpNotFound();
            }
            return View(riskScore_M);
        }

        // GET: RiskScore_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskScore_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskScoreID,RiskScore,Comments,Active")] RiskScore_M riskScore_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskScore_M.Add(riskScore_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskScore_M);
        }

        // GET: RiskScore_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskScore_M riskScore_M = db.RiskScore_M.Find(id);
            if (riskScore_M == null)
            {
                return HttpNotFound();
            }
            return View(riskScore_M);
        }

        // POST: RiskScore_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskScoreID,RiskScore,Comments,Active")] RiskScore_M riskScore_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskScore_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskScore_M);
        }

        // GET: RiskScore_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskScore_M riskScore_M = db.RiskScore_M.Find(id);
            if (riskScore_M == null)
            {
                return HttpNotFound();
            }
            return View(riskScore_M);
        }

        // POST: RiskScore_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskScore_M riskScore_M = db.RiskScore_M.Find(id);
            db.RiskScore_M.Remove(riskScore_M);
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
