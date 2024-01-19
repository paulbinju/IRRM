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
    public class RiskDescriptor_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskDescriptor_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.RiskDescriptor_M.ToList());
        }

        // GET: RiskDescriptor_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskDescriptor_M riskDescriptor_M = db.RiskDescriptor_M.Find(id);
            if (riskDescriptor_M == null)
            {
                return HttpNotFound();
            }
            return View(riskDescriptor_M);
        }

        // GET: RiskDescriptor_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskDescriptor_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiskDescriptorID,RiskDescriptor,Comments,Active")] RiskDescriptor_M riskDescriptor_M)
        {
            if (ModelState.IsValid)
            {
                db.RiskDescriptor_M.Add(riskDescriptor_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskDescriptor_M);
        }

        // GET: RiskDescriptor_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskDescriptor_M riskDescriptor_M = db.RiskDescriptor_M.Find(id);
            if (riskDescriptor_M == null)
            {
                return HttpNotFound();
            }
            return View(riskDescriptor_M);
        }

        // POST: RiskDescriptor_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskDescriptorID,RiskDescriptor,Comments,Active")] RiskDescriptor_M riskDescriptor_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskDescriptor_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskDescriptor_M);
        }

        // GET: RiskDescriptor_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskDescriptor_M riskDescriptor_M = db.RiskDescriptor_M.Find(id);
            if (riskDescriptor_M == null)
            {
                return HttpNotFound();
            }
            return View(riskDescriptor_M);
        }

        // POST: RiskDescriptor_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskDescriptor_M riskDescriptor_M = db.RiskDescriptor_M.Find(id);
            db.RiskDescriptor_M.Remove(riskDescriptor_M);
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
