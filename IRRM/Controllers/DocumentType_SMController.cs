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
    public class DocumentType_SMController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DocumentType_SM
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.DocumentType_SM.ToList());
        }

        // GET: DocumentType_SM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType_SM documentType_SM = db.DocumentType_SM.Find(id);
            if (documentType_SM == null)
            {
                return HttpNotFound();
            }
            return View(documentType_SM);
        }

        // GET: DocumentType_SM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentType_SM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentTypeID,DocumentType,Comments,Active")] DocumentType_SM documentType_SM)
        {
            if (ModelState.IsValid)
            {
                db.DocumentType_SM.Add(documentType_SM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentType_SM);
        }

        // GET: DocumentType_SM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType_SM documentType_SM = db.DocumentType_SM.Find(id);
            if (documentType_SM == null)
            {
                return HttpNotFound();
            }
            return View(documentType_SM);
        }

        // POST: DocumentType_SM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentTypeID,DocumentType,Comments,Active")] DocumentType_SM documentType_SM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentType_SM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentType_SM);
        }

        // GET: DocumentType_SM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType_SM documentType_SM = db.DocumentType_SM.Find(id);
            if (documentType_SM == null)
            {
                return HttpNotFound();
            }
            return View(documentType_SM);
        }

        // POST: DocumentType_SM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentType_SM documentType_SM = db.DocumentType_SM.Find(id);
            db.DocumentType_SM.Remove(documentType_SM);
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
