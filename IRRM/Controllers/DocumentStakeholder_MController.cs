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
    public class DocumentStakeholder_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DocumentStakeholder_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.DocumentStakeholder_M.ToList());
        }

        // GET: DocumentStakeholder_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentStakeholder_M documentStakeholder_M = db.DocumentStakeholder_M.Find(id);
            if (documentStakeholder_M == null)
            {
                return HttpNotFound();
            }
            return View(documentStakeholder_M);
        }

        // GET: DocumentStakeholder_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentStakeholder_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StakeholderID,Stakeholder,Active")] DocumentStakeholder_M documentStakeholder_M)
        {
            if (ModelState.IsValid)
            {
                db.DocumentStakeholder_M.Add(documentStakeholder_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentStakeholder_M);
        }

        // GET: DocumentStakeholder_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentStakeholder_M documentStakeholder_M = db.DocumentStakeholder_M.Find(id);
            if (documentStakeholder_M == null)
            {
                return HttpNotFound();
            }
            return View(documentStakeholder_M);
        }

        // POST: DocumentStakeholder_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StakeholderID,Stakeholder,Active")] DocumentStakeholder_M documentStakeholder_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentStakeholder_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentStakeholder_M);
        }

        // GET: DocumentStakeholder_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentStakeholder_M documentStakeholder_M = db.DocumentStakeholder_M.Find(id);
            if (documentStakeholder_M == null)
            {
                return HttpNotFound();
            }
            return View(documentStakeholder_M);
        }

        // POST: DocumentStakeholder_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentStakeholder_M documentStakeholder_M = db.DocumentStakeholder_M.Find(id);
            db.DocumentStakeholder_M.Remove(documentStakeholder_M);
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
