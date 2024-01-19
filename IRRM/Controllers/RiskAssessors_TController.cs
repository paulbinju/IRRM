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
    public class RiskAssessors_TController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: RiskAssessors_T
        public ActionResult Index()
        {
            var riskAssessors_T = db.RiskAssessors_T.Include(r => r.Users_M);
            return View(riskAssessors_T.ToList());
        }

        // GET: RiskAssessors_T/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskAssessors_T riskAssessors_T = db.RiskAssessors_T.Find(id);
            if (riskAssessors_T == null)
            {
                return HttpNotFound();
            }
            return View(riskAssessors_T);
        }

        // GET: RiskAssessors_T/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users_M, "UserID", "UserName");
            return View();
        }

        // POST: RiskAssessors_T/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "RiskAssessorID,RiskID,UserID")] RiskAssessors_T riskAssessors_T)
        {
                db.RiskAssessors_T.Add(riskAssessors_T);
                db.SaveChanges();
                return RedirectToAction("../RiskRegisters/Edit/" + riskAssessors_T.RiskID);
        }

        // GET: RiskAssessors_T/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskAssessors_T riskAssessors_T = db.RiskAssessors_T.Find(id);
            if (riskAssessors_T == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users_M, "UserID", "UserName", riskAssessors_T.UserID);
            return View(riskAssessors_T);
        }

        // POST: RiskAssessors_T/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiskAssessorID,RiskID,UserID")] RiskAssessors_T riskAssessors_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskAssessors_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users_M, "UserID", "UserName", riskAssessors_T.UserID);
            return View(riskAssessors_T);
        }

        // GET: RiskAssessors_T/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskAssessors_T riskAssessors_T = db.RiskAssessors_T.Find(id);
            if (riskAssessors_T == null)
            {
                return HttpNotFound();
            }
            return View(riskAssessors_T);
        }

        // POST: RiskAssessors_T/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiskAssessors_T riskAssessors_T = db.RiskAssessors_T.Find(id);
            db.RiskAssessors_T.Remove(riskAssessors_T);
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
