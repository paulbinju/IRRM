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
    public class RiskLevel_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskLevel_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskLevel_M.ToList());
        }

        // GET: RiskLevel_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskLevel_M riskLevel_M = db.RiskLevel_M.Find(id);
            if (riskLevel_M == null)
            {
                return HttpNotFound();
            }
            return View(riskLevel_M);
        }

        // GET: RiskLevel_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskLevel_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskLevelID,RiskLevel,ActionTimeScale,ReviewTimeScale,Comments,Active")] RiskLevel_M riskLevel_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskLevel_M.Add(riskLevel_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskLevel_M);
        }

        // GET: RiskLevel_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskLevel_M riskLevel_M = db.RiskLevel_M.Find(id);
            if (riskLevel_M == null)
            {
                return HttpNotFound();
            }
            return View(riskLevel_M);
        }

        // POST: RiskLevel_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskLevelID,RiskLevel,ActionTimeScale,ReviewTimeScale,Comments,Active")] RiskLevel_M riskLevel_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskLevel_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskLevel_M);
        }

        // GET: RiskLevel_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskLevel_M riskLevel_M = db.RiskLevel_M.Find(id);
            if (riskLevel_M == null)
            {
                return HttpNotFound();
            }
            return View(riskLevel_M);
        }

        // POST: RiskLevel_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskLevel_M riskLevel_M = db.RiskLevel_M.Find(id);
            db.RiskLevel_M.Remove(riskLevel_M);
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
