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
    public class TaskRelation_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: TaskRelation_M
        public ActionResult Index()
        {
            return View(db.TaskRelation_M.ToList());
        }

        // GET: TaskRelation_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskRelation_M taskRelation_M = db.TaskRelation_M.Find(id);
            if (taskRelation_M == null)
            {
                return HttpNotFound();
            }
            return View(taskRelation_M);
        }

        // GET: TaskRelation_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskRelation_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskRelationID,TaskRelation,Comments,SendEmail,EmailSubject,EmailHeader,EmailFooter,Active")] TaskRelation_M taskRelation_M)
        {
            if (ModelState.IsValid)
            {
                db.TaskRelation_M.Add(taskRelation_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskRelation_M);
        }

        // GET: TaskRelation_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskRelation_M taskRelation_M = db.TaskRelation_M.Find(id);
            if (taskRelation_M == null)
            {
                return HttpNotFound();
            }
            return View(taskRelation_M);
        }

        // POST: TaskRelation_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskRelationID,TaskRelation,Comments,SendEmail,EmailSubject,EmailHeader,EmailFooter,Active")] TaskRelation_M taskRelation_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskRelation_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskRelation_M);
        }

        // GET: TaskRelation_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskRelation_M taskRelation_M = db.TaskRelation_M.Find(id);
            if (taskRelation_M == null)
            {
                return HttpNotFound();
            }
            return View(taskRelation_M);
        }

        // POST: TaskRelation_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskRelation_M taskRelation_M = db.TaskRelation_M.Find(id);
            db.TaskRelation_M.Remove(taskRelation_M);
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
