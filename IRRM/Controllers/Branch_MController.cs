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
    public class Branch_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Branch_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            
            return View(db.Branch_M.ToList());
        }

        // GET: Branch_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch_M branch_M = db.Branch_M.Find(id);
            if (branch_M == null)
            {
                return HttpNotFound();
            }
            return View(branch_M);
        }

        // GET: Branch_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branch_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BranchID,Branch,Comments,Active")] Branch_M branch_M)
        {
            if (ModelState.IsValid)
            {
                db.Branch_M.Add(branch_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch_M);
        }

        // GET: Branch_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch_M branch_M = db.Branch_M.Find(id);
            if (branch_M == null)
            {
                return HttpNotFound();
            }
            return View(branch_M);
        }

        // POST: Branch_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchID,Branch,Comments,Active")] Branch_M branch_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch_M);
        }

        // GET: Branch_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch_M branch_M = db.Branch_M.Find(id);
            if (branch_M == null)
            {
                return HttpNotFound();
            }
            return View(branch_M);
        }

        // POST: Branch_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch_M branch_M = db.Branch_M.Find(id);
            db.Branch_M.Remove(branch_M);
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
