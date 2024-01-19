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
    public class DocumentStatus_TController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DocumentStatus_T
        public ActionResult Index()
        {
            return View(db.DocumentStatus_T.ToList());
        }

        // GET: DocumentStatus_T/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentStatus_T documentStatus_T = db.DocumentStatus_T.Find(id);
            if (documentStatus_T == null)
            {
                return HttpNotFound();
            }
            return View(documentStatus_T);
        }

        // GET: DocumentStatus_T/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentStatus_T/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentStatusID,DocumentID,Status,CreatedOn")] DocumentStatus_T documentStatus_T)
        {
            if (ModelState.IsValid)
            {
                db.DocumentStatus_T.Add(documentStatus_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentStatus_T);
        }

        // GET: DocumentStatus_T/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentStatus_T documentStatus_T = db.DocumentStatus_T.Find(id);
            if (documentStatus_T == null)
            {
                return HttpNotFound();
            }
            return View(documentStatus_T);
        }

        // POST: DocumentStatus_T/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentStatusID,DocumentID,Status,CreatedOn")] DocumentStatus_T documentStatus_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentStatus_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentStatus_T);
        }

        // GET: DocumentStatus_T/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentStatus_T documentStatus_T = db.DocumentStatus_T.Find(id);
            if (documentStatus_T == null)
            {
                return HttpNotFound();
            }
            return View(documentStatus_T);
        }

        // POST: DocumentStatus_T/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentStatus_T documentStatus_T = db.DocumentStatus_T.Find(id);
            db.DocumentStatus_T.Remove(documentStatus_T);
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
