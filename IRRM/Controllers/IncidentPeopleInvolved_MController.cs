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
    public class IncidentPeopleInvolved_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentPeopleInvolved_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.IncidentPeopleInvolved_M.ToList());
        }

        // GET: IncidentPeopleInvolved_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentPeopleInvolved_M incidentPeopleInvolved_M = db.IncidentPeopleInvolved_M.Find(id);
            if (incidentPeopleInvolved_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentPeopleInvolved_M);
        }

        // GET: IncidentPeopleInvolved_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentPeopleInvolved_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentPeopleInvolvedID,IncidentPeopleInvolved,Active,Comments")] IncidentPeopleInvolved_M incidentPeopleInvolved_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentPeopleInvolved_M.Add(incidentPeopleInvolved_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentPeopleInvolved_M);
        }

        // GET: IncidentPeopleInvolved_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentPeopleInvolved_M incidentPeopleInvolved_M = db.IncidentPeopleInvolved_M.Find(id);
            if (incidentPeopleInvolved_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentPeopleInvolved_M);
        }

        // POST: IncidentPeopleInvolved_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentPeopleInvolvedID,IncidentPeopleInvolved,Active,Comments")] IncidentPeopleInvolved_M incidentPeopleInvolved_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentPeopleInvolved_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentPeopleInvolved_M);
        }

        // GET: IncidentPeopleInvolved_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentPeopleInvolved_M incidentPeopleInvolved_M = db.IncidentPeopleInvolved_M.Find(id);
            if (incidentPeopleInvolved_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentPeopleInvolved_M);
        }

        // POST: IncidentPeopleInvolved_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentPeopleInvolved_M incidentPeopleInvolved_M = db.IncidentPeopleInvolved_M.Find(id);
            db.IncidentPeopleInvolved_M.Remove(incidentPeopleInvolved_M);
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
