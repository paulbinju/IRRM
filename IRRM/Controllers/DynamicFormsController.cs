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
    public class DynamicFormsController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DynamicForms
        public ActionResult Index()
        {
            return View(db.DynamicForms.ToList());
        }

        // GET: DynamicForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicForm dynamicForm = db.DynamicForms.Find(id);
            if (dynamicForm == null)
            {
                return HttpNotFound();
            }
            return View(dynamicForm);
        }

        // GET: DynamicForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DynamicForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DFormID,DFormName")] DynamicForm dynamicForm)
        {
            if (ModelState.IsValid)
            {
                db.DynamicForms.Add(dynamicForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dynamicForm);
        }

        // GET: DynamicForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicForm dynamicForm = db.DynamicForms.Find(id);
            if (dynamicForm == null)
            {
                return HttpNotFound();
            }
            return View(dynamicForm);
        }

        // POST: DynamicForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DFormID,DFormName")] DynamicForm dynamicForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dynamicForm);
        }

        // GET: DynamicForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicForm dynamicForm = db.DynamicForms.Find(id);
            if (dynamicForm == null)
            {
                return HttpNotFound();
            }
            return View(dynamicForm);
        }

        // POST: DynamicForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicForm dynamicForm = db.DynamicForms.Find(id);
            db.DynamicForms.Remove(dynamicForm);
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
