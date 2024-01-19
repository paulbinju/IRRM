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
    public class DynamicControlValuesController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DynamicControlValues
        public ActionResult Index()
        {
            var dynamicControlValues = db.DynamicControlValues.Include(d => d.DynamicControl).Include(d => d.DynamicControlType);
            return View(dynamicControlValues.ToList());
        }

        // GET: DynamicControlValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControlValue dynamicControlValue = db.DynamicControlValues.Find(id);
            if (dynamicControlValue == null)
            {
                return HttpNotFound();
            }
            return View(dynamicControlValue);
        }

        // GET: DynamicControlValues/Create
        public ActionResult Create()
        {
            ViewBag.ControlID = new SelectList(db.DynamicControls, "ControlID", "ControlName");
            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType");
            return View();
        }

        // POST: DynamicControlValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ControlValueID,ControlTypeID,ControlID,ControlValue")] DynamicControlValue dynamicControlValue)
        {
            if (ModelState.IsValid)
            {
                db.DynamicControlValues.Add(dynamicControlValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ControlID = new SelectList(db.DynamicControls, "ControlID", "ControlName", dynamicControlValue.ControlID);
            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType", dynamicControlValue.ControlTypeID);
            return View(dynamicControlValue);
        }

        // GET: DynamicControlValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControlValue dynamicControlValue = db.DynamicControlValues.Find(id);
            if (dynamicControlValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.ControlID = new SelectList(db.DynamicControls, "ControlID", "ControlName", dynamicControlValue.ControlID);
            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType", dynamicControlValue.ControlTypeID);
            return View(dynamicControlValue);
        }

        // POST: DynamicControlValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ControlValueID,ControlTypeID,ControlID,ControlValue")] DynamicControlValue dynamicControlValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicControlValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ControlID = new SelectList(db.DynamicControls, "ControlID", "ControlName", dynamicControlValue.ControlID);
            ViewBag.ControlTypeID = new SelectList(db.DynamicControlTypes, "DControlID", "DControlType", dynamicControlValue.ControlTypeID);
            return View(dynamicControlValue);
        }

        // GET: DynamicControlValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControlValue dynamicControlValue = db.DynamicControlValues.Find(id);
            if (dynamicControlValue == null)
            {
                return HttpNotFound();
            }
            return View(dynamicControlValue);
        }

        // POST: DynamicControlValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicControlValue dynamicControlValue = db.DynamicControlValues.Find(id);
            db.DynamicControlValues.Remove(dynamicControlValue);
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
