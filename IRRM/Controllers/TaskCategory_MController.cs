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
    public class TaskCategory_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: TaskCategory_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);

            return View(db.TaskCategory_M.OrderBy(x=>x.TaskCategory).ToList());
        }

        // GET: TaskCategory_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskCategory_M taskCategory_M = db.TaskCategory_M.Find(id);
            if (taskCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(taskCategory_M);
        }

        // GET: TaskCategory_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskCategory_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskCategoryID,TaskCategory,Comments,Active")] TaskCategory_M taskCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.TaskCategory_M.Add(taskCategory_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskCategory_M);
        }

        // GET: TaskCategory_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskCategory_M taskCategory_M = db.TaskCategory_M.Find(id);
            if (taskCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(taskCategory_M);
        }

        // POST: TaskCategory_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskCategoryID,TaskCategory,Comments,Active")] TaskCategory_M taskCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskCategory_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskCategory_M);
        }

        // GET: TaskCategory_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskCategory_M taskCategory_M = db.TaskCategory_M.Find(id);
            if (taskCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(taskCategory_M);
        }

        // POST: TaskCategory_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskCategory_M taskCategory_M = db.TaskCategory_M.Find(id);
            db.TaskCategory_M.Remove(taskCategory_M);
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
