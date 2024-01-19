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
    public class DynamicForm_ControlController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DynamicForm_Control
        public ActionResult Index()
        {
            return View(db.DynamicForm_Control.ToList());
        }

        // GET: DynamicForm_Control/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicForm_Control dynamicForm_Control = db.DynamicForm_Control.Find(id);
            if (dynamicForm_Control == null)
            {
                return HttpNotFound();
            }
            return View(dynamicForm_Control);
        }

        // GET: DynamicForm_Control/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DynamicForm_Control/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DFormControlID,DFormID,DControlID")] DynamicForm_Control dynamicForm_Control)
        {
            if (ModelState.IsValid)
            {
                db.DynamicForm_Control.Add(dynamicForm_Control);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dynamicForm_Control);
        }

        // GET: DynamicForm_Control/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicForm_Control dynamicForm_Control = db.DynamicForm_Control.Find(id);
            if (dynamicForm_Control == null)
            {
                return HttpNotFound();
            }
            return View(dynamicForm_Control);
        }

        // POST: DynamicForm_Control/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DFormControlID,DFormID,DControlID")] DynamicForm_Control dynamicForm_Control)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicForm_Control).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dynamicForm_Control);
        }

        // GET: DynamicForm_Control/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicForm_Control dynamicForm_Control = db.DynamicForm_Control.Find(id);
            if (dynamicForm_Control == null)
            {
                return HttpNotFound();
            }
            return View(dynamicForm_Control);
        }

        // POST: DynamicForm_Control/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicForm_Control dynamicForm_Control = db.DynamicForm_Control.Find(id);
            db.DynamicForm_Control.Remove(dynamicForm_Control);
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
