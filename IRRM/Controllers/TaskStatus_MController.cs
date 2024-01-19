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
    public class TaskStatus_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: TaskStatus_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.TaskStatus_M.ToList());
        }

        // GET: TaskStatus_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskStatus_M taskStatus_M = db.TaskStatus_M.Find(id);
            if (taskStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus_M);
        }

        // GET: TaskStatus_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskStatus_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskStatusID,TaskStatus,Comments,Active")] TaskStatus_M taskStatus_M)
        {
            if (ModelState.IsValid)
            {
                db.TaskStatus_M.Add(taskStatus_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskStatus_M);
        }

        // GET: TaskStatus_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskStatus_M taskStatus_M = db.TaskStatus_M.Find(id);
            if (taskStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus_M);
        }

        // POST: TaskStatus_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskStatusID,TaskStatus,Comments,Active")] TaskStatus_M taskStatus_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskStatus_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskStatus_M);
        }

        // GET: TaskStatus_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskStatus_M taskStatus_M = db.TaskStatus_M.Find(id);
            if (taskStatus_M == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus_M);
        }

        // POST: TaskStatus_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskStatus_M taskStatus_M = db.TaskStatus_M.Find(id);
            db.TaskStatus_M.Remove(taskStatus_M);
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
