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
    public class DynamicReports_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DynamicReports_M
        public ActionResult Index()
        {
            return View(db.DynamicReports_M.ToList());
        }

        // GET: DynamicReports_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicReports_M dynamicReports_M = db.DynamicReports_M.Find(id);
            if (dynamicReports_M == null)
            {
                return HttpNotFound();
            }
            return View(dynamicReports_M);
        }

        // GET: DynamicReports_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DynamicReports_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,ReportName")] DynamicReports_M dynamicReports_M)
        {
            if (ModelState.IsValid)
            {
                db.DynamicReports_M.Add(dynamicReports_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dynamicReports_M);
        }

        // GET: DynamicReports_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicReports_M dynamicReports_M = db.DynamicReports_M.Find(id);
            if (dynamicReports_M == null)
            {
                return HttpNotFound();
            }
            return View(dynamicReports_M);
        }

        // POST: DynamicReports_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,ReportName")] DynamicReports_M dynamicReports_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicReports_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dynamicReports_M);
        }

        // GET: DynamicReports_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicReports_M dynamicReports_M = db.DynamicReports_M.Find(id);
            if (dynamicReports_M == null)
            {
                return HttpNotFound();
            }
            return View(dynamicReports_M);
        }

        // POST: DynamicReports_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicReports_M dynamicReports_M = db.DynamicReports_M.Find(id);
            db.DynamicReports_M.Remove(dynamicReports_M);
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
