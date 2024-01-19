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
    public class UserRole_SMController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: UserRole_SM
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.UserRole_SM.ToList());
        }

        // GET: UserRole_SM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole_SM userRole_SM = db.UserRole_SM.Find(id);
            if (userRole_SM == null)
            {
                return HttpNotFound();
            }
            return View(userRole_SM);
        }

        // GET: UserRole_SM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRole_SM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserRoleID,Role")] UserRole_SM userRole_SM)
        {
            if (ModelState.IsValid)
            {
                db.UserRole_SM.Add(userRole_SM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRole_SM);
        }

        // GET: UserRole_SM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole_SM userRole_SM = db.UserRole_SM.Find(id);
            if (userRole_SM == null)
            {
                return HttpNotFound();
            }
            return View(userRole_SM);
        }

        // POST: UserRole_SM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRoleID,Role")] UserRole_SM userRole_SM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRole_SM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userRole_SM);
        }

        // GET: UserRole_SM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole_SM userRole_SM = db.UserRole_SM.Find(id);
            if (userRole_SM == null)
            {
                return HttpNotFound();
            }
            return View(userRole_SM);
        }

        // POST: UserRole_SM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRole_SM userRole_SM = db.UserRole_SM.Find(id);
            db.UserRole_SM.Remove(userRole_SM);
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
