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
    public class DynamicControlsController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DynamicControls
        public ActionResult Index()
        {
            var dynamicControls = db.DynamicControls.Include(d => d.DynamicControlType);
            return View(dynamicControls.ToList());
        }

        // GET: DynamicControls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControl dynamicControl = db.DynamicControls.Find(id);
            if (dynamicControl == null)
            {
                return HttpNotFound();
            }
            return View(dynamicControl);
        }

        // GET: DynamicControls/Create
        public ActionResult Create()
        {
            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType");
            return View();
        }

        // POST: DynamicControls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ControlID,ControlTypeID,ControlName")] DynamicControl dynamicControl)
        {
            if (ModelState.IsValid)
            {
                db.DynamicControls.Add(dynamicControl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType", dynamicControl.ControlTypeID);
            return View(dynamicControl);
        }

        // GET: DynamicControls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControl dynamicControl = db.DynamicControls.Find(id);
            if (dynamicControl == null)
            {
                return HttpNotFound();
            }
            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType", dynamicControl.ControlTypeID);
            return View(dynamicControl);
        }

        // POST: DynamicControls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ControlID,ControlTypeID,ControlName")] DynamicControl dynamicControl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicControl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType", dynamicControl.ControlTypeID);
            return View(dynamicControl);
        }

        // GET: DynamicControls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControl dynamicControl = db.DynamicControls.Find(id);
            if (dynamicControl == null)
            {
                return HttpNotFound();
            }
            return View(dynamicControl);
        }

        // POST: DynamicControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicControl dynamicControl = db.DynamicControls.Find(id);
            db.DynamicControls.Remove(dynamicControl);
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
