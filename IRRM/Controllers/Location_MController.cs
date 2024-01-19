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
    public class Location_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Location_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var location_M = db.Location_M.Include(l => l.Branch_M);
            return View(location_M.ToList());
        }

        // GET: Location_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location_M location_M = db.Location_M.Find(id);
            if (location_M == null)
            {
                return HttpNotFound();
            }
            return View(location_M);
        }

        // GET: Location_M/Create
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

        // POST: Location_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,BranchID,Location,Comments,Active")] Location_M location_M)
        {
            if (ModelState.IsValid)
            {
                db.Location_M.Add(location_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            return View(location_M);
        }

        // GET: Location_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location_M location_M = db.Location_M.Find(id);
            if (location_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", location_M.BranchID);
            return View(location_M);
        }

        // POST: Location_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,BranchID,Location,Comments,Active")] Location_M location_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", location_M.BranchID);
            return View(location_M);
        }

        // GET: Location_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location_M location_M = db.Location_M.Find(id);
            if (location_M == null)
            {
                return HttpNotFound();
            }
            return View(location_M);
        }

        // POST: Location_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location_M location_M = db.Location_M.Find(id);
            db.Location_M.Remove(location_M);
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
