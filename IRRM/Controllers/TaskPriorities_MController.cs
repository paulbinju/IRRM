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
    public class TaskPriorities_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: TaskPriorities_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }

            return View(db.TaskPriorities_M.ToList());
        }

        // GET: TaskPriorities_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskPriorities_M taskPriorities_M = db.TaskPriorities_M.Find(id);
            if (taskPriorities_M == null)
            {
                return HttpNotFound();
            }
            return View(taskPriorities_M);
        }

        // GET: TaskPriorities_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskPriorities_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskPriorityID,Priority,InvestigationDays,Active,Comments")] TaskPriorities_M taskPriorities_M)
        {
            if (ModelState.IsValid)
            {
                db.TaskPriorities_M.Add(taskPriorities_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskPriorities_M);
        }

        // GET: TaskPriorities_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskPriorities_M taskPriorities_M = db.TaskPriorities_M.Find(id);
            if (taskPriorities_M == null)
            {
                return HttpNotFound();
            }
            return View(taskPriorities_M);
        }

        // POST: TaskPriorities_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskPriorityID,Priority,InvestigationDays,Active,Comments")] TaskPriorities_M taskPriorities_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskPriorities_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskPriorities_M);
        }

        // GET: TaskPriorities_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskPriorities_M taskPriorities_M = db.TaskPriorities_M.Find(id);
            if (taskPriorities_M == null)
            {
                return HttpNotFound();
            }
            return View(taskPriorities_M);
        }

        // POST: TaskPriorities_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskPriorities_M taskPriorities_M = db.TaskPriorities_M.Find(id);
            db.TaskPriorities_M.Remove(taskPriorities_M);
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
