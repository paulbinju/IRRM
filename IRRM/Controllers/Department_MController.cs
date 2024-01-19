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
    public class Department_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Department_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var department_M = db.Department_M.Include(d => d.Branch_M);
            return View(department_M.ToList());
        }

        // GET: Department_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department_M department_M = db.Department_M.Find(id);
            if (department_M == null)
            {
                return HttpNotFound();
            }
            return View(department_M);
        }

        // GET: Department_M/Create
        public ActionResult Create()
        {


            List<Branch_M> itm = db.Branch_M.Where(x => x.Active == true).ToList();

            List<_Branch> itx = new List<_Branch>();
            _Branch _itx;

            foreach (var _itm in itm)
            {
                _itx = new _Branch();

                _itx.BranchID = Convert.ToString(_itm.BranchID);
                _itx.Branch = _itm.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _Branch { Branch = "Select", BranchID = "" });


            ViewBag.BranchID = new SelectList(itx, "BranchID", "Branch");
            return View();
        }

        // POST: Department_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,BranchID,Department,Active")] Department_M department_M)
        {
            if (ModelState.IsValid)
            {
                db.Department_M.Add(department_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", department_M.BranchID);
            return View(department_M);
        }

        // GET: Department_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department_M department_M = db.Department_M.Find(id);
            if (department_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", department_M.BranchID);
            return View(department_M);
        }

        // POST: Department_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,BranchID,Department,Active")] Department_M department_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", department_M.BranchID);
            return View(department_M);
        }

        // GET: Department_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department_M department_M = db.Department_M.Find(id);
            if (department_M == null)
            {
                return HttpNotFound();
            }
            return View(department_M);
        }

        // POST: Department_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department_M department_M = db.Department_M.Find(id);
            db.Department_M.Remove(department_M);
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
