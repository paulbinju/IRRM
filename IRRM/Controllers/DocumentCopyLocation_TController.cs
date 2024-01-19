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
    public class DocumentCopyLocation_TController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DocumentCopyLocation_T
        public ActionResult Index()
        {
            return View(db.DocumentCopyLocation_T.ToList());
        }

        // GET: DocumentCopyLocation_T/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentCopyLocation_T documentCopyLocation_T = db.DocumentCopyLocation_T.Find(id);
            if (documentCopyLocation_T == null)
            {
                return HttpNotFound();
            }
            return View(documentCopyLocation_T);
        }

        // GET: DocumentCopyLocation_T/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentCopyLocation_T/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocCopyLocationID,DocumentID,LocationID,Details")] DocumentCopyLocation_T documentCopyLocation_T)
        {
            if (ModelState.IsValid)
            {
                db.DocumentCopyLocation_T.Add(documentCopyLocation_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentCopyLocation_T);
        }

        // GET: DocumentCopyLocation_T/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentCopyLocation_T documentCopyLocation_T = db.DocumentCopyLocation_T.Find(id);
            if (documentCopyLocation_T == null)
            {
                return HttpNotFound();
            }
            return View(documentCopyLocation_T);
        }

        // POST: DocumentCopyLocation_T/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocCopyLocationID,DocumentID,LocationID,Details")] DocumentCopyLocation_T documentCopyLocation_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentCopyLocation_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentCopyLocation_T);
        }

        // GET: DocumentCopyLocation_T/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentCopyLocation_T documentCopyLocation_T = db.DocumentCopyLocation_T.Find(id);
            if (documentCopyLocation_T == null)
            {
                return HttpNotFound();
            }
            return View(documentCopyLocation_T);
        }

        // POST: DocumentCopyLocation_T/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentCopyLocation_T documentCopyLocation_T = db.DocumentCopyLocation_T.Find(id);
            db.DocumentCopyLocation_T.Remove(documentCopyLocation_T);
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
