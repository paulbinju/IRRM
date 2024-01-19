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
    public class DynamicControlTypesController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DynamicControlTypes
        public ActionResult Index()
        {
            return View(db.DynamicControlTypes.ToList());
        }

        // GET: DynamicControlTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControlType dynamicControlType = db.DynamicControlTypes.Find(id);
            if (dynamicControlType == null)
            {
                return HttpNotFound();
            }
            return View(dynamicControlType);
        }

        // GET: DynamicControlTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DynamicControlTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DControlID,DControlType,HasValues")] DynamicControlType dynamicControlType)
        {
            if (ModelState.IsValid)
            {
                db.DynamicControlTypes.Add(dynamicControlType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dynamicControlType);
        }

        // GET: DynamicControlTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControlType dynamicControlType = db.DynamicControlTypes.Find(id);
            if (dynamicControlType == null)
            {
                return HttpNotFound();
            }
            return View(dynamicControlType);
        }

        // POST: DynamicControlTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DControlID,DControlType,HasValues")] DynamicControlType dynamicControlType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicControlType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dynamicControlType);
        }

        // GET: DynamicControlTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicControlType dynamicControlType = db.DynamicControlTypes.Find(id);
            if (dynamicControlType == null)
            {
                return HttpNotFound();
            }
            return View(dynamicControlType);
        }

        // POST: DynamicControlTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicControlType dynamicControlType = db.DynamicControlTypes.Find(id);
            db.DynamicControlTypes.Remove(dynamicControlType);
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
