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
    public class RiskStrategy_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskStrategy_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskStrategy_M.ToList());
        }

        // GET: RiskStrategy_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskStrategy_M riskStrategy_M = db.RiskStrategy_M.Find(id);
            if (riskStrategy_M == null)
            {
                return HttpNotFound();
            }
            return View(riskStrategy_M);
        }

        // GET: RiskStrategy_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskStrategy_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskStrategyID,RiskStrategy,Comments,Active")] RiskStrategy_M riskStrategy_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskStrategy_M.Add(riskStrategy_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskStrategy_M);
        }

        // GET: RiskStrategy_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskStrategy_M riskStrategy_M = db.RiskStrategy_M.Find(id);
            if (riskStrategy_M == null)
            {
                return HttpNotFound();
            }
            return View(riskStrategy_M);
        }

        // POST: RiskStrategy_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskStrategyID,RiskStrategy,Comments,Active")] RiskStrategy_M riskStrategy_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskStrategy_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskStrategy_M);
        }

        // GET: RiskStrategy_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskStrategy_M riskStrategy_M = db.RiskStrategy_M.Find(id);
            if (riskStrategy_M == null)
            {
                return HttpNotFound();
            }
            return View(riskStrategy_M);
        }

        // POST: RiskStrategy_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskStrategy_M riskStrategy_M = db.RiskStrategy_M.Find(id);
            db.RiskStrategy_M.Remove(riskStrategy_M);
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
