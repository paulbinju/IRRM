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
    public class TaskSubCategory_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: TaskSubCategory_M
        public ActionResult Index()
        {

            return View(db.TaskSubCategory_M.OrderBy(x => x.TaskCategory_M.TaskCategory).ThenBy(x => x.SubCategory).ToList());
        }

        // GET: TaskSubCategory_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskSubCategory_M taskSubCategory_M = db.TaskSubCategory_M.Find(id);
            if (taskSubCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(taskSubCategory_M);
        }

        // GET: TaskSubCategory_M/Create
        public ActionResult Create()
        {
            ViewBag.TaskCategory = db.TaskCategory_M.Where(x => x.Active == true).ToList();
            return View();
        }

        // POST: TaskSubCategory_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskSubCategoryID,TaskCategoryID,SubCategory,Active")] TaskSubCategory_M taskSubCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.TaskSubCategory_M.Add(taskSubCategory_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskSubCategory_M);
        }

        // GET: TaskSubCategory_M/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.TaskCategory = db.TaskCategory_M.Where(x => x.Active == true).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskSubCategory_M taskSubCategory_M = db.TaskSubCategory_M.Find(id);
            if (taskSubCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(taskSubCategory_M);
        }

        // POST: TaskSubCategory_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskSubCategoryID,TaskCategoryID,SubCategory,Active")] TaskSubCategory_M taskSubCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskSubCategory_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskSubCategory_M);
        }

        // GET: TaskSubCategory_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskSubCategory_M taskSubCategory_M = db.TaskSubCategory_M.Find(id);
            if (taskSubCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(taskSubCategory_M);
        }

        // POST: TaskSubCategory_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskSubCategory_M taskSubCategory_M = db.TaskSubCategory_M.Find(id);
            db.TaskSubCategory_M.Remove(taskSubCategory_M);
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
