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
    public class RiskCategory_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskCategory_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskCategory_M.ToList());
        }

        // GET: RiskCategory_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCategory_M riskCategory_M = db.RiskCategory_M.Find(id);
            if (riskCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(riskCategory_M);
        }

        // GET: RiskCategory_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskCategory_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskCategoryID,RiskCategory,Comments,Active")] RiskCategory_M riskCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskCategory_M.Add(riskCategory_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskCategory_M);
        }

        // GET: RiskCategory_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCategory_M riskCategory_M = db.RiskCategory_M.Find(id);
            if (riskCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(riskCategory_M);
        }

        // POST: RiskCategory_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskCategoryID,RiskCategory,Comments,Active")] RiskCategory_M riskCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskCategory_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskCategory_M);
        }

        // GET: RiskCategory_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCategory_M riskCategory_M = db.RiskCategory_M.Find(id);
            if (riskCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(riskCategory_M);
        }

        // POST: RiskCategory_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskCategory_M riskCategory_M = db.RiskCategory_M.Find(id);
            db.RiskCategory_M.Remove(riskCategory_M);
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
