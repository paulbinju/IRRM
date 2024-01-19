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
    public class RiskCost_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskCost_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskCost_M.ToList());
        }

        // GET: RiskCost_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCost_M riskCost_M = db.RiskCost_M.Find(id);
            if (riskCost_M == null)
            {
                return HttpNotFound();
            }
            return View(riskCost_M);
        }

        // GET: RiskCost_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskCost_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskCostID,RiskCost,Comments,Active")] RiskCost_M riskCost_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskCost_M.Add(riskCost_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskCost_M);
        }

        // GET: RiskCost_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCost_M riskCost_M = db.RiskCost_M.Find(id);
            if (riskCost_M == null)
            {
                return HttpNotFound();
            }
            return View(riskCost_M);
        }

        // POST: RiskCost_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskCostID,RiskCost,Comments,Active")] RiskCost_M riskCost_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskCost_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskCost_M);
        }

        // GET: RiskCost_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCost_M riskCost_M = db.RiskCost_M.Find(id);
            if (riskCost_M == null)
            {
                return HttpNotFound();
            }
            return View(riskCost_M);
        }

        // POST: RiskCost_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskCost_M riskCost_M = db.RiskCost_M.Find(id);
            db.RiskCost_M.Remove(riskCost_M);
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
