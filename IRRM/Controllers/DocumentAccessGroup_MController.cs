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
    public class DocumentAccessGroup_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DocumentAccessGroup_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.DocumentAccessGroup_M.ToList());
        }

        // GET: DocumentAccessGroup_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentAccessGroup_M documentAccessGroup_M = db.DocumentAccessGroup_M.Find(id);
            if (documentAccessGroup_M == null)
            {
                return HttpNotFound();
            }
            return View(documentAccessGroup_M);
        }

        // GET: DocumentAccessGroup_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentAccessGroup_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocAccessGroupID,AccessGroup,Active")] DocumentAccessGroup_M documentAccessGroup_M)
        {
            if (ModelState.IsValid)
            {
                db.DocumentAccessGroup_M.Add(documentAccessGroup_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentAccessGroup_M);
        }

        // GET: DocumentAccessGroup_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentAccessGroup_M documentAccessGroup_M = db.DocumentAccessGroup_M.Find(id);
            if (documentAccessGroup_M == null)
            {
                return HttpNotFound();
            }
            return View(documentAccessGroup_M);
        }

        // POST: DocumentAccessGroup_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocAccessGroupID,AccessGroup,Active")] DocumentAccessGroup_M documentAccessGroup_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentAccessGroup_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentAccessGroup_M);
        }

        // GET: DocumentAccessGroup_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentAccessGroup_M documentAccessGroup_M = db.DocumentAccessGroup_M.Find(id);
            if (documentAccessGroup_M == null)
            {
                return HttpNotFound();
            }
            return View(documentAccessGroup_M);
        }

        // POST: DocumentAccessGroup_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentAccessGroup_M documentAccessGroup_M = db.DocumentAccessGroup_M.Find(id);
            db.DocumentAccessGroup_M.Remove(documentAccessGroup_M);
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
