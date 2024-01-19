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
    public class RiskStatus_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskStatus_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskStatus_M.ToList());
        }

        // GET: RiskStatus_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskStatus_M riskStatus_M = db.RiskStatus_M.Find(id);
            if (riskStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(riskStatus_M);
        }

        // GET: RiskStatus_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskStatus_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskStatusID,RiskStatus,Comments,Active")] RiskStatus_M riskStatus_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskStatus_M.Add(riskStatus_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskStatus_M);
        }

        // GET: RiskStatus_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskStatus_M riskStatus_M = db.RiskStatus_M.Find(id);
            if (riskStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(riskStatus_M);
        }

        // POST: RiskStatus_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskStatusID,RiskStatus,Comments,Active")] RiskStatus_M riskStatus_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskStatus_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskStatus_M);
        }

        // GET: RiskStatus_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskStatus_M riskStatus_M = db.RiskStatus_M.Find(id);
            if (riskStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(riskStatus_M);
        }

        // POST: RiskStatus_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskStatus_M riskStatus_M = db.RiskStatus_M.Find(id);
            db.RiskStatus_M.Remove(riskStatus_M);
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
